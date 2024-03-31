﻿using Microsoft.AspNetCore.SignalR;

namespace Albion.Network
{
    public class ReceiverBuilder
    {
        private readonly AlbionParser parser;

        public ReceiverBuilder()
        {
            parser = new AlbionParser();
        }

        public static ReceiverBuilder Create()
        {
            return new ReceiverBuilder();
        }

        public ReceiverBuilder AddHandler<TPacket>(PacketHandler<TPacket> handler)
        {
            parser.AddHandler(handler);

            return this;
        }

        public ReceiverBuilder AddEventHandler<TEvent>(EventPacketHandler<TEvent> handler) where TEvent : BaseEvent
        {
            AddHandler(handler);

            return this;
        }

        public ReceiverBuilder AddRequestHandler<TOperation>(RequestPacketHandler<TOperation> handler) where TOperation : BaseOperation
        {
            AddHandler(handler);

            return this;
        }

        public ReceiverBuilder AddResponseHandler<TOperation>(ResponsePacketHandler<TOperation> handler) where TOperation : BaseOperation
        {
            AddHandler(handler);

            return this;
        }

        public IPhotonReceiver Build(Hub packetHub)
        {
            parser.setPacketHub(packetHub);
            return parser;
        }
    }
}
