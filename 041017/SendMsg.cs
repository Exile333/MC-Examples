using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace ConsoleApplication1
{
    class Program
    {
        static DeviceClient deviceClient;
        static string iotHubUri = "URLToIoTHub";
        static string deviceKey = "ключУстройства";
        private static async void SendAsyncMsg()
        {
            double minTemperature = 20;
            double minHumidity = 60;
            int messageId = 1;
            Random rand = new Random();

            while (true)
            {
                double currentTemperature = minTemperature + rand.NextDouble() * 15;
                double currentHumidity = minHumidity + rand.NextDouble() * 20;

                var telemetryDataPoint = new
                {
                    messageId = messageId++,
                    deviceId = "имяУстройства",
                    temperature = currentTemperature,
                    humidity = currentHumidity
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Microsoft.Azure.Devices.Client.Message(Encoding.ASCII.GetBytes(messageString));
                message.Properties.Add("temperatureAlert", (currentTemperature > 30) ? "true" : "false");

                await deviceClient.SendEventAsync(message);
                Console.WriteLine("{0} > Sending message: {1}", DateTime.Now, messageString);

                await Task.Delay(1000);
            }
        }
        static void Main(string[] args)
        {
            /*try
            {*/
                Console.WriteLine("Simulating device\n");
                deviceClient = DeviceClient.Create(iotHubUri, new DeviceAuthenticationWithRegistrySymmetricKey("myFirstDevice", deviceKey), Microsoft.Azure.Devices.Client.TransportType.Mqtt);
                SendAsyncMsg();
                Console.ReadLine();
            /*}
            catch (Exception e)
            {
                Console.Write("Error: ");
                Console.WriteLine(e.Message);
                Console.ReadLine();
                return;
            }*/
        }
    }
}
