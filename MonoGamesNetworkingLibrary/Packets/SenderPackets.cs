using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Packets
{
    public class SenderPackets
    {
        public static readonly SenderPackets Server = new SenderPackets("Server");
        public static readonly SenderPackets Client = new SenderPackets("Client");

        /// <summary>
        /// Changes the value passed in to a string;
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }


        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="Value"></param>
        protected SenderPackets(string Value)
        {
            this.Value = Value;
        }

        /// <summary>
        /// The value of my enum.
        /// </summary>
        public string Value { get; private set; }


        public void writeToMessage(NetOutgoingMessage msg){
            msg.Write(this.Value);
     }

        public static SenderPackets readFromMessage(NetIncomingMessage msg)
        {
            string s= msg.ReadString();
            SenderPackets packet = new SenderPackets(s);
            return packet;
        }

    }
}
