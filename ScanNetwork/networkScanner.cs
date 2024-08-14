using Lextm.SharpSnmpLib.Messaging;
using Lextm.SharpSnmpLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Management;


namespace ScanNetwork
{
    public class networkScanner
    {
        public static async Task PingSweepAsync(string subnet)
        {
            string ipAddress = "";
            for (int i = 1; i < 255; i++)
            {
                string ip = $"{subnet}.{i}";
                Ping ping = new Ping();
                PingReply reply = await ping.SendPingAsync(ip, 100);
                if (reply.Status == IPStatus.Success)
                {
                    Console.WriteLine($"{ip} is active.");
                    string macAddress = getMacAddress.GetMacAddress(ip);
                    Console.WriteLine($"MAC Address for {ip}: {macAddress}");
                    /////////////////////
                    ///


                    ipAddress = ip; 
                    string hostName = GetHostName(ipAddress);

                    if (!string.IsNullOrEmpty(hostName))
                    {
                        Console.WriteLine($"Hostname: {hostName}");
                        GetDeviceInformation(hostName);
                    }
                    else
                    {
                        Console.WriteLine("Unable to retrieve hostname.");
                    }
                }

                static string GetHostName(string ipAddress)
                {
                    try
                    {
                        IPHostEntry hostEntry = Dns.GetHostEntry(ipAddress);
                        return hostEntry.HostName;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                        return null;
                    }
                }


            }
            static void GetDeviceInformation(string hostName)
            {
                try
                {
                    string query = $"SELECT * FROM Win32_ComputerSystem WHERE Name = '{hostName}'";
                    ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

                    foreach (ManagementObject obj in searcher.Get())
                    {
                        Console.WriteLine($"Manufacturer: {obj["Manufacturer"]}");
                        Console.WriteLine($"Model: {obj["Model"]}");
                        Console.WriteLine($"Domain: {obj["Domain"]}");
                        Console.WriteLine($"Total Physical Memory: {obj["TotalPhysicalMemory"]}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
