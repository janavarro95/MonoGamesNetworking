using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Packets.Client
{
    class ClientRequestPacket : Packets.Client.ClientPacketBase
    {
        public static readonly ClientRequestPacket None = new ClientRequestPacket("None");
        public static readonly ClientRequestPacket AllPlayers = new ClientRequestPacket("AllPlayers");

        public ClientRequestPacket(string key) : base(key)
        {

        }
    }
}
