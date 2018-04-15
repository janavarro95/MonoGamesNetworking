using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Packets
{
    public class Packet
    {
        /// <summary>
        /// The user who sent the packet.
        /// </summary>
        public enum SenderType
        {
            Server,
            Client
        }

        /// <summary>
        /// Different packet types to send to the clients so they know how to process the packet data.
        /// </summary>
        public enum PacketTypeClientDataType {
            None, //Send a null packet type to the client.
            Login, //Send a login notification to the client
            AllPlayers, //Send a packet to the players notifiying them about a packet sent to all players.
            ReceiveAllPlayers //Send a packet to the client to receive (read) info sent to it about all of the players.

        }

        /// <summary>
        /// Differnt packet types to send to the server so that it knows how to process the data.
        /// </summary>
        public enum PacketTypeServerDataType
        {
            None, //Send a null packet type to the server.
            Login, //Send login request packet to server.
            RequestAllPlayers //Request all players data from server

        }

        enum PacketTypeClientFunctionality
        {

        }

        enum PacketTypeServerFunctionality
        {


        }


    }
}
