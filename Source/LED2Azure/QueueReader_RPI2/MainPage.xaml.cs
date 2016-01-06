using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using Windows.Devices.Gpio;
using System.Threading.Tasks;
using System.Diagnostics;

namespace QueueReader_RPI2
{
    public sealed partial class MainPage : Page
    {
        private CloudQueue queue;
        private DispatcherTimer timer;                      // Timer for toggling

        private  const string CONNECTION_STRING = "INSERT CONNECTION STRING HERE"; // CONNECTION STRING
        private const string QUEUE_STRING = "INSERT QUEUE NAME HERE"; // NAME OF QUEUE
        private const int LED_PIN = 5;                      //GPIO NUMBER, NOT PIN NUMBER.

        private GpioPin ledPin;
        private GpioPinValue ledPinValue;

        public MainPage()
        {
            this.InitializeComponent();
            InitGPIO();                                     // Initialize the RPI2
            InitializeQueue();                              // Initialize Storage Queue
            timer = new DispatcherTimer();
            timer.Tick += timer_Tick;                       // Add event
            timer.Stop();


            ReadAndDeleteFromQueue();                       // Begin infinite "load-loop"
        }

        // Initialize the RPI2
        private void InitGPIO()
        {
            GpioController gpio = GpioController.GetDefault();

            ledPin = gpio.OpenPin(LED_PIN);
            ledPin.Write(GpioPinValue.Low);                 // Sets pin to low initially
            ledPin.SetDriveMode(GpioPinDriveMode.Output);
        }

        // Toggle-tick event
        private void timer_Tick(object sender, object e)
        {
            ledPinValue = (ledPinValue == 0) ? GpioPinValue.High : GpioPinValue.Low;
            ledPin.Write(ledPinValue);                      // Toggle pin value
        }

        // Infinite loop recieving messages from the queue, removing them after use.
        private async void ReadAndDeleteFromQueue()
        {
            while (true)
            {
                CloudQueueMessage message = null;                               // Create empty message object

                try
                {
                    message = await queue.GetMessageAsync();                    // Attempt message download (this reserves the message to this connection for some time)
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Could not get message! Error: " + ex.Message);
                }

                if (message != null)                                            // Only if message was recieved
                {
                    TimeSpan length = TimeSpan.FromMilliseconds(int.Parse(message.AsString));
                    timer.Interval = length;                                    // Read and parse message data
                    timer.Start();                                              // Begin toggling
                    await queue.DeleteMessageAsync(message);                    // Remove message from queue
                }
            }
        }

        // Initialize the queue
        private async void InitializeQueue()
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(CONNECTION_STRING); // Setup storage
            CloudQueueClient queueClient = cloudStorageAccount.CreateCloudQueueClient();            // Setup client
            queue = queueClient.GetQueueReference(QUEUE_STRING);                                    // Set queue name

            try
            {
                await queue.CreateIfNotExistsAsync();                                               // Create queue if one by the given name doesn't exist
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Could not initialize! Error: " + ex.Message);
            }
        }
    }
}
