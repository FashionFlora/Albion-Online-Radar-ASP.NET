using Microsoft.AspNetCore.SignalR;
using PhotonPackageParser;
using System;
using System.Collections.Generic;

namespace Albion.Network
{
    internal sealed class AlbionParser : PhotonParser, IPhotonReceiver
    {
        private readonly HandlersCollection handlers;
        Hub packetHub = null;


        public AlbionParser()
        {
            handlers = new HandlersCollection();
        }

        public void AddHandler<TPacket>(PacketHandler<TPacket> handler)
        {
            handlers.Add(handler);
        }

        protected override async void OnEvent(byte Code, Dictionary<byte, object> Parameters)
        {
            if (Code == 3)
            {
                Parameters.Add(252, EventCodes.Move);
                byte[] bytes = (byte[])Parameters[1];

                Parameters.Add(4, BitConverter.ToSingle(bytes, 9));
                Parameters.Add(5, BitConverter.ToSingle(bytes, 13));
                //   Parameters.Add()
            }
            short eventCode = ParseEventCode(Parameters);

            if (eventCode == 3 || eventCode == 27 || eventCode == 1 || eventCode == 86 || eventCode == 36 || eventCode == 37 || eventCode == 44| eventCode == 58 || eventCode == 118 || eventCode ==201||  eventCode == 309 || eventCode == 378)

			{
                await packetHub.Clients.All.SendAsync("InvokeEvent", Parameters);
            }
            return;

        }

        protected override async void OnRequest(byte OperationCode, Dictionary<byte, object> Parameters)
        {

            short operationCode = ParseOperationCode(Parameters);
            if (operationCode == 21)
            {

                await packetHub.Clients.All.SendAsync("InvokeRequest", Parameters);
          
            }
    
       
            /*
            short operationCode = ParseOperationCode(Parameters);
            var requestPacket = new RequestPacket(operationCode, Parameters);

            _ = handlers.HandleAsync(requestPacket);
            */
        }

        protected override void OnResponse(byte OperationCode, short ReturnCode, string DebugMessage, Dictionary<byte, object> Parameters)
        {
            return;

            /*
            short operationCode = ParseOperationCode(Parameters);
            var responsePacket = new ResponsePacket(operationCode, Parameters);

            _ = handlers.HandleAsync(responsePacket);
            */
        }
        internal void setPacketHub(Hub packetHub)
        {
            this.packetHub = packetHub;

        }
        private short ParseOperationCode(Dictionary<byte, object> parameters)
        {
            if (!parameters.TryGetValue(253, out object value))
            {
                throw new InvalidOperationException();
            }

            return (short)value;
        }

        private short ParseEventCode(Dictionary<byte, object> parameters)
        {
            if (!parameters.TryGetValue(252, out object value))
            {
                throw new InvalidOperationException();
            }

            return (short)value;
        }
    }
}
