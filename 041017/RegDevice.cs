using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Common.Exceptions;

namespace RegDevice
{
    class Program
    {
        static RegistryManager registryManager;
        static string connectionString = "строкаСоединения";

        private static async Task AddDeviceAsync()
        {
            string deviceId = "имяУстройства";
            Device device;
            /*try
            {*/
                device = await registryManager.AddDeviceAsync(new Device(deviceId));
                Console.WriteLine("Generated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
            /*}
            catch (DeviceAlreadyExistsException)
            {
                device = await registryManager.GetDeviceAsync(deviceId);
                Console.WriteLine("Device already exists.\nGenerated device key: {0}", device.Authentication.SymmetricKey.PrimaryKey);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                Console.ReadLine();
                Environment.Exit(1);
            }*/
        }

        static void Main(string[] args)
        {
            registryManager = RegistryManager.CreateFromConnectionString(connectionString);
            AddDeviceAsync().Wait();
            Console.ReadLine();
        }
    }
}
