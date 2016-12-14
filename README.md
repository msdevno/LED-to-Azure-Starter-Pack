# LED to Azure in Storage Queue

This solution is aimed for showing the simple setup and usage of UWP and IoT Devices towards Azure by the use of Azure Storage Queue.
The solution also showcases a Windows Forms Application to easily manage messages on your Azure Storage Queue.

![LED2Azure](https://zn4qkw-db3pap001.files.1drv.com/y3mAWyRUlpPbcg27vI08NAM9Tw3oWkg7mp11KR5vCcrmFe5neVTQQ8epuCuTIiQ-l_vTT-UQ_lBzXw_GAgIiA5H3ISn0Mdb68en8g3iDbj6bm7yFdVuywG3mnrxtxmqmSBqiZrVDuJz_dsoIwLM584HtnLizchPsa8z_kDflwwfp0U)


## Step 1.1: Configure Azure

You need to create a "Storage Account" in Azure in order to create a Storage Queue.

## Step 1.2: Create Storage Account in Azure

1. Log into Azure portal at http://portal.azure.com
2. "New" => "Data + Storage" => "Storage account" => on "Select a deployment model", just choose "Resource Manager from the dropdown list
3. Type in whatever name into the Name field for your storage account
4. On "Type" you can decide on different metrics provided by the blade. We chose Standard-LRS
5. On "Diagnostics" choose if you want this enabled or disabled
6. On "Subscription", make sure you select a valid subscription like "Pay-as-you-go" or "Visual Studio Premium with MSDN" or "Bizspark" etc. Dreamspark won't work since this is not accessible for students at HED.
7. On "Resource Group" type in the name of your resource group, like for instance "ledtoazurevideo" which will be the name of the container with collection of assets. This name is obviousl ynow taken, but write whatever you wish. 
8. On "Location" select wherever you want your server to be hosted for the storage account.
9. Push "Create".

## Step 1.3: Download Azure Storage Explorer

1. Download Azure Storage Explorer from CodePlex to view and create stuff in the newly created storage account: https://azurestorageexplorer.codeplex.com/
2. Fire up the Storage Explorer
3. Push the "Add account" button
4. Select Cloud Storage Account in the first radio-button selection
5. On "Storage account name", copy your storage account name. In my case; ledtoazure.
6. On "Storage Account key", go to your storage account in Azure, and press "Access keys" under "Manage".
7. Copy and paste "KEY1"
8. On "Storage endpoint domain" just let it be standard which is core.windows.net
9. Test Access

## Step 1.4: Create Storage Queue in Azure through Azure Storage Explorer

1. Click on Queues under Blob Containers in the left blade
2. Click the "New" button
3. Give the Queue an adequate name. Mine was "ledtoazurequeue".
4. Click on the ledtoazurequeue and see the content. this is currently blank since there is nothing on the queue.
5. If you want to, you can also find your queue in Azure under the storage account.
6. We are now ready to open the project files and connect the Raspberry Pi 2 with it's components.
7. NOTICE! You might have to re-install the "WindowsAzure.Storage" NuGet package. Do this by rightclicking each Project and selecting "Nuget Packet Manager".

## Step 1.5: Assemble the Rpi2
Connect the Rpi2 properly to be able to successfully make a LED light up

You need:
* A resistor
* A LED
* A Breadboard
* A Cobbler
* 3 cables
* Some TP-cables
* A switch
* Internet.. duh
* A computer... 

Connect everything together like this:

[IMAGE][IMAGE][IMAGE][IMAGE][IMAGE][IMAGE][IMAGE]

## Step 2: Open Project

Open the main LED2Azure.sln (solution file) to see the content of the solution with all the following projects.

## Usage and content

Solution name: LED2Azure
- This solution is aimed for showing the simple setup and usage of UWP and IoT Devices towards Azure by the use of Azure Storage Queue.
The solution also showcases a Windows Forms Application to easily manage messages on your Azure Storage Queue.

Project #1: QueueAddForms
- This Windows Forms application bring together a UI for adding messages to the storage queue, as well as peaking/displaying the current messages in the queue.

Project #2: QueueReader_RPI2
- This UWP (Universal Windows Platform) application is designed to run on the Raspberry Pi 2 Windows 10 IoT Core Device, and will read the messages put on the Storage Queue.

## Contributing

1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Add some feature'`
4. Push to the branch: `git push origin my-new-feature`
5. Submit a pull request :D

## Credits

Special thanks to Tobias Kullblikk from NTNU for re-developing the solution of projects with the support of Anders Gill who is a Technical Evangelist in Microsoft Norway.
Also special thanks to J�rgen Austvik for supporting the development with much needed guidance under NTNU Hackathon late 2015.

## License

The work here is licensed under the MIT license - see the LICENSE file at the root of the repository

## Webshow

https://channel9.msdn.com/Series/MSDEVNO/LED-to-Azure-Starter-Pack

![Anders Gill](https://zn4pkw-db3pap001.files.1drv.com/y3mFUS5Oey3NwLh1dM1EwaSZFSaGitJoPZXXMouelRM1aZ9j8Qr9_jJpXhYhgP828_dOJrhJFOLwlo6dvQJkrFWTbOo3PUVMjYmuY39YN3sO-7Bzj9m9CAiHUMdVCvLI-8wWQu-bSOtwqbPTTGR4zok4E6OMU2ZWXTnV_YOCol_jIM)
