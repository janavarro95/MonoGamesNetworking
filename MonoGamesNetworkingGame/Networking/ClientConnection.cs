using Lidgren.Network;
using MonoGamesNetworkingLibrary.InfoLibraries;
using MonoGamesNetworkingLibrary.NetworkLibraries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoGamesNetworking.Networking
{
    public class ClientConnection
    {
        private NetClient client;
        public Player player;
        public List<Player> otherPlayers { get; set; }
        public bool active { get; set; }
        public bool initializedPlayers;

        public ClientConnection()
        {
           
        }

        /// <summary>
        /// Initialize a player connecting to the client.
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            otherPlayers = new List<Player>();
            var random = new Random();

            this.player = new Player("name_" + random.Next(0,400), random.Next(0, 400), random.Next(0, 400));
            client = new NetClient(new NetPeerConfiguration("networkGame")); //Has to be the same on all clients.
            client.Start();
            var outMessage = client.CreateMessage();
            PacketTypes.writeToMessage(outMessage, PacketType.Login);
            this.player.writeLoginInfo(outMessage); //
            outMessage.Write(player.Name); //Writes the player's name to a packet to be read by the NetIncomingMessage. If I don't write it here it can't be read later.
            var connect = client.Connect("localhost", 4207,outMessage); //Options? (localhost/IP);
            this.active = true;
            client.SendMessage(outMessage ,NetDeliveryMethod.ReliableOrdered,0);

            
            return est();
        }
        /// <summary>
        /// Reads in initial
        /// </summary>
        /// <returns></returns>
        public bool est()
        {
            NetIncomingMessage inc;
            DateTime now = DateTime.Now;
            while (true)
            {

                if (DateTime.Now.Subtract(now).Seconds > 5)
                {
                    if (this.client.ConnectionStatus == NetConnectionStatus.Connected)
                    {

                        return true; //If I time out but I am connected, give it the green light.
                    }
                        return false;
                }

                inc = client.ReadMessage();
                
                if (inc == null) continue;
                
                    switch (inc.MessageType)
                    {
                        case NetIncomingMessageType.Data:
                        Console.WriteLine("DATA CONNECT");
                        var data = inc.ReadByte();
                        if (data == (byte)PacketType.Login)
                        {
                            active = inc.ReadBoolean();
                            if (active)
                            {
                                player.xPosition = inc.ReadInt32();
                                player.yPosition = inc.ReadInt32();
                                ReceiveAllPlayers(inc);
                            }
                            var accept = inc.ReadBoolean();
                            if (accept) return true;
                            else return false;
                        }
                            break;
                    case NetIncomingMessageType.WarningMessage:
                        Console.WriteLine("OK STRING:" + inc.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        var data3 = inc.ReadByte();
                        if (data3 == (byte)PacketType.Login)
                        {
                            var accept = inc.ReadBoolean();
                            if (accept) return true;
                            else return false;
                        }
                        Console.WriteLine("Status changed:" + inc.ReadString());
                        break;
                }
                
            }
        }

        /// <summary>
        /// Updates the connection every tick.
        /// </summary>
        public void Update()
        {

            //If I don't have a list of all of the players, send it to the client when they connect.
            if (initializedPlayers == false)
            {
                var msg=client.CreateMessage();
                PacketTypes.writeToMessage(msg, PacketType.RequestAllPlayers);
                client.SendMessage(msg, NetDeliveryMethod.ReliableOrdered, 0);
            }
            NetIncomingMessage inc;

            while ((inc = client.ReadMessage()) != null)
            {
                var packetType = PacketTypes.readPacketType(inc);
                switch (packetType)
                {
                    case PacketType.Login:
                        break;
                    case PacketType.NewPlayer:
                        Player oPlayer=Player.createFromIncomingNetMessageVerbose(inc);
                        otherPlayers.Add(oPlayer);
                        break;
                    case PacketType.AllPlayers:
                        ReceiveAllPlayers(inc);
                        break;
                    case PacketType.RecieveAllPlayers: //Code that executes when the client requests all of the players from the server.
                        initializedPlayers = inc.ReadBoolean();
                        ReceiveAllPlayers(inc);
                        break;
                    default:
                        throw new Exception("Argument out of range: Not enough variety in packet types: Failed packet type is:" + packetType);
                }
            }
        }

        /// <summary>
        /// Get all of the players from the server.
        /// </summary>
        /// <param name="msg"></param>
        private void ReceiveAllPlayers(NetIncomingMessage msg)
        {
            var count = msg.ReadInt32();
            for(int i=0; i<count; i++)
            {
                Player p = Player.createFromIncomingNetMessageVerbose(msg);
                if (p.Name == player.Name) continue;

                otherPlayers.Add(p);

                if (otherPlayers.Any(pla => pla.Name == player.Name))
                {
                   var oldPlayer= otherPlayers.FirstOrDefault(pla => pla.Name == player.Name);
                    oldPlayer.xPosition = player.xPosition;
                    oldPlayer.yPosition = player.yPosition;
                }
                else
                {
                    otherPlayers.Add(p);
                }
            }

        }
    }
}
