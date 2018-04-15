using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Packets.Client
{
    public class ClientAllPlayersPacket : Packets.Client.ClientPacketBase
    {
        public static readonly ClientAllPlayersPacket None = new ClientAllPlayersPacket("None");

        public ClientAllPlayersPacket(string key) : base(key)
        {

        }
    }
}
