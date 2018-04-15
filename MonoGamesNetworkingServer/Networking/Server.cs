using Lidgren.Network;
using MonoGamesNetworkingLibrary.InfoLibraries;
using MonoGamesNetworkingLibrary.NetworkLibraries;
using MonoGamesNetworkingLibrary.Packets;
using MonoGamesNetworkingLibrary.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworkingLibrary.Server
{
    /*
     * TODO: Port all packet type code to new Packet protocall I made.
     * Make players move
     * 
     */ 


    /// <summary>
    /// Create a new server for clients to connect to.
    /// </summary>
    class Server
    {
        private static List<Player> players;
        private NetPeerConfiguration config;
        private static NetServer server;

        public Server()
        {
            players = new List<Player>();
            var config = new NetPeerConfiguration("networkGame")
            {
                Port = 4207
            };
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            config.EnableMessageType(NetIncomingMessageType.Data);

            server = new NetServer(config);
            server.Start();
        }
        /// <summary>
        /// Run the server.
        /// </summary>
        public void Run()
        {
            Console.WriteLine("Starting server...");


            while (true)
            {

                NetIncomingMessage inc;
                if ((inc = server.ReadMessage()) == null) continue;

                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.Error:
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        break;
                    case NetIncomingMessageType.UnconnectedData:
                        break;
                    case NetIncomingMessageType.ConnectionApproval:
                        ConnectionApproval(inc);
                        break;
                    case NetIncomingMessageType.Data:
                        var packet = PacketTypes.readPacketType(inc);
                        if (packet == PacketType.RequestAllPlayers)
                        {
                            Console.WriteLine("SEND THE DATA!!!");
                            SendFullPlayerList(inc);
                        }

                        break;
                    case NetIncomingMessageType.Receipt:
                        break;
                    case NetIncomingMessageType.DiscoveryRequest:
                        break;
                    case NetIncomingMessageType.DiscoveryResponse:
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                        break;
                    case NetIncomingMessageType.DebugMessage:
                        break;
                    case NetIncomingMessageType.WarningMessage:
                        var str = inc.ReadString();
                        Console.Write("WARNING" + str);
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.NatIntroductionSuccess:
                        break;
                    case NetIncomingMessageType.ConnectionLatencyUpdated:
                        break;
                }

                var ok = server.Connections;
                foreach (var v in ok)
                {
                    Console.WriteLine(v);
                }
            }
        }



        /// <summary>
        /// Create a new player.
        /// </summary>
        /// <param name="inc"></param>
        /// <returns></returns>
        public static Player CreatePlayer(NetIncomingMessage inc)
        {
            Random ran = new Random();

            Player player=Player.createFromIncomingNetMessageSimple(inc); //Reads the player from variables written to a message, not the entire player.
            
            players.Add(player);
            return player;

        }

        /// <summary>
        /// Send a new player to the clients.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="inc"></param>
        public static void SendNewPlayer(Player player, NetIncomingMessage inc)
        {

            var outMessage = server.CreateMessage();
            outMessage.Write((byte)PacketType.NewPlayer);
            player.writeToMessage(outMessage);
            server.SendToAll(outMessage, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered,0);
        }



        /// <summary>
        /// Send all of the player info to the clients.
        /// </summary>
        /// <param name="inc"></param>
        public static void SendFullPlayerList(NetIncomingMessage inc)
        {
            Console.WriteLine("Sending out full server to all!.");
            var msg=server.CreateMessage();
            PacketTypes.writeToMessage(msg, PacketType.RecieveAllPlayers);
            msg.Write((bool)true);
            msg.Write(players.Count);
            foreach (var player in players)
            {
                player.writeToMessage(msg);
            }
            server.SendMessage(msg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
            //server.SendToAll(msg, NetDeliveryMethod.ReliableOrdered);
        }


        //Approves the client connecting to the server
        public void ConnectionApproval(NetIncomingMessage inc)
        {

            Console.WriteLine("New Connection...");
            var packet=PacketInfo.readFromMessage(inc);
            Console.WriteLine(packet.ToString());
            Console.WriteLine(packet.getPacketType());
            if (packet.getPacketType() == Packets.ClientPacketTypes.Type_Login)
            {
                Console.WriteLine("Connection Approved");

                inc.SenderConnection.Approve();

                Player player = CreatePlayer(inc);
                //SendNewPlayer(player, inc);
                Console.WriteLine(player.Name);
                var outMessage = server.CreateMessage();
                outMessage.Write((byte)PacketType.Login);
                outMessage.Write(true);

                outMessage.Write(player.xPosition);
                outMessage.Write(player.yPosition);
               

                server.SendMessage(outMessage, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);

                var outMessage2 = server.CreateMessage();
                outMessage2.Write((byte)PacketType.AllPlayers);
                //SendFullPlayerList(inc, outMessage2);
                SendNewPlayer(player, inc);
                
                
                Console.WriteLine("Finished Connect");

            }
            else
            {
                inc.SenderConnection.Deny("Didn't send the right data...");
            }
        }

        /// <summary>
        /// Writes a packet to the server to figure out what to do.
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="serverPacket"></param>
        private void writePackageInfo(NetOutgoingMessage msg, ClientPacketTypes clientPacket)
        {
            var packet = new PacketInfo(SenderPackets.Server, clientPacket, ServerPacketTypes.None_None);
            msg.WriteAllProperties(packet);
        }

        /// <summary>
        /// Reads the packet info from a message.
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        private PacketInfo readPacketInfo(NetIncomingMessage msg)
        {
            return PacketInfo.readFromMessage(msg);
        }

    }
}
