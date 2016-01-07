using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;


namespace QueueAddForms
{
    public partial class Form1 : Form
    {
        CloudQueue queue;

        public Form1()
        {
            InitializeComponent();
        }

        // Initialize the queue
        private async void InitializeQueue(string connectionString, string queueName)
        {
            CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(connectionString);  // Setup storage
            CloudQueueClient queueClient = cloudStorageAccount.CreateCloudQueueClient();            // Setup client
            queue = queueClient.GetQueueReference(queueName);                                       // Set queue name

            try
            {
                await queue.CreateIfNotExistsAsync();                                               // Create queue if one by the given name doesn't exist
            }
            catch (Exception ex)
            {
                Console.WriteLine("Could not initialize! Error: " + ex.Message);
            }

            groupBox2.Enabled = true;
            ReadFromQueue();                                                                        // Reload messages for listBox1
        }

        // Add message to the queue
        private async void addMessage(string text)
        {
            CloudQueueMessage message = new CloudQueueMessage(text);
            await queue.AddMessageAsync(message);                               // Add message to the queue
            Console.WriteLine("Message added to queue");
            ReadFromQueue();                                                    // Reload messages for listBox1
        }

        // Read messages from queue and add to listBox1
        private async void ReadFromQueue()
        {
            await queue.FetchAttributesAsync();                                 // Downloading information about the queue (message count, etc.)
            int? count = queue.ApproximateMessageCount;

            if (count > 0)
            {
                var messages = await queue.PeekMessagesAsync(count.Value);      // Get all messages in queue without restricting access from other connections

                listBox1.Items.Clear();

                for (int i = 0; i < messages.ToArray().Length; i++)
                    listBox1.Items.Add(messages.ToArray()[i].AsString);         // Add every message to listBox1
            }
            else
                listBox1.Items.Clear();
        }

        // Async Queue Clearing
        private async void ClearQueue()
        {
            await queue.ClearAsync();
        }

        // Update Queue-List
        private void button2_Click(object sender, EventArgs e)
        {
            ReadFromQueue();
        }

        // Add Message Button
        private void button1_Click(object sender, EventArgs e)
        {
            addMessage(queue, numericUpDown1.Value.ToString());
        }

        // Connect Button
        private void button3_Click(object sender, EventArgs e)
        {
            InitializeQueue(textBox1.Text, textBox2.Text);
        }

        // Clear Queue Button
        private void button4_Click(object sender, EventArgs e)
        {
            ClearQueue();
        }
    }
}
