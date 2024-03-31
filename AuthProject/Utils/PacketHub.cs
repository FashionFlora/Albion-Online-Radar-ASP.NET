using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Albion.Network;
using AuthProject.Models;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Logging.Abstractions;
using PacketDotNet;
using SharpPcap;

namespace PacketCaptureServer
{


    public class PacketHub : Hub
    {
        CaptureDeviceList selectedDevices;

        static bool isCapturing = false;
        private ReceiverBuilder builder = ReceiverBuilder.Create();

        private static IPhotonReceiver receiver;


        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task UpdateSetting(string settingName, string settingValue)
        {
            await Clients.All.SendAsync("ReceiveSettingUpdate", settingName, settingValue);
        }

		public async Task GetItemsIds()
        {
        
            UserRepository userRepository = new UserRepository();
			await Clients.All.SendAsync("ReceiveItemsIds", userRepository.GetItemsIds());
			return;
        }
		public async Task GetMobsIds()
		{

            
            UserRepository userRepository = new UserRepository();
           
			await Clients.All.SendAsync("ReceiveMobsIds", userRepository.GetMobsIds());
			return;
		}



		public async Task StartPacketCapture()
        {
         

			if (isCapturing)
            {


            //    Console.WriteLine("Returning");
                return;
            }
			isCapturing = true;


			ReceiverBuilder builder = ReceiverBuilder.Create();


            receiver = builder.Build(this);




            // Initialize SharpPcap
            selectedDevices = CaptureDeviceList.Instance;
            if (selectedDevices.Count == 0)
            {

                Console.WriteLine("selectedDevices = 0 ");
                await Clients.Caller.SendAsync("NoDevices", "No capture devices found.");
                return;
            }

            

 

            CaptureDeviceList devices = CaptureDeviceList.Instance;

            foreach (var device in devices) { 

           


               //     Console.WriteLine($"Open... {device.Description}");

                    device.OnPacketArrival += new PacketArrivalEventHandler(PacketHandler);
                    device.Open(DeviceModes.Promiscuous, 5);
                    device.StartCapture();
                
       

       
        
            }
            Console.Read();

     



           
        }

        private  void PacketHandler(object sender, PacketCapture e)
        {

        
            UdpPacket packet = Packet.ParsePacket(e.GetPacket().LinkLayerType, e.GetPacket().Data).Extract<UdpPacket>();

            if (packet != null && (packet.SourcePort == 5056 || packet.DestinationPort == 5056))
            {
                try
                {
                    receiver.ReceivePacket(packet.PayloadData);
                }
                catch { }
             
            }


        }



        public async Task StopPacketCapture()
        {
            // Check if capturing is in progress
            if (!isCapturing)
            {
                await Clients.Caller.SendAsync("CaptureNotInProgress", "Packet capture is not in progress.");
                return;
            }

            // Stop capturing packets
            foreach(var device in selectedDevices.ToArray())
            {
                device.StopCapture();
                device.Close();
            }
     
            isCapturing = false;
        }
    }
}