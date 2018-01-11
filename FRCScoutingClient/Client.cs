using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace FRCScoutingClient {
    class Client {
        public Socket ClientSocket;
        public byte[] byteRec = new byte[1024];
        public bool recUpdate = false;



        public void run(int port, string serverHostName) {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(serverHostName);
            IPAddress ipAddress = ipHostInfo.AddressList[3];
            IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, port);
 
            ClientSocket = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try {

                ClientSocket.Connect(remoteEndPoint);

                Listener reciever = new Listener(this);
                Thread recieverThread = new Thread(new ThreadStart(reciever.run));
                recieverThread.Start();
                while(!recieverThread.IsAlive);

                Console.Write("Name: ");
                string name = Console.ReadLine();

                Console.Write("SurveyedTeam#: ");
                int teamNumber;
                while( true ) {
                    try {
                        teamNumber = int.Parse(Console.ReadLine());
                        break;
                    } catch {
                        Console.WriteLine("Please enter a valid number");
                        Console.Write("SurveyedTeam#: ");
                    }
                }

                Console.Write("Competition#: ");
                int CompetitionID;
                while( true ) {
                    try {
                        CompetitionID = int.Parse(Console.ReadLine());
                        break;
                    } catch {
                        Console.WriteLine("Please enter a valid number");
                        Console.Write("Competition#: ");
                    }
                }

                string userInput = "";
                string message;
                float val;
                Dictionary<string, float> Details = new Dictionary<string, float>();
                while( true ) {
                    Console.WriteLine("Message:");
                    userInput = Console.ReadLine();
                    if( userInput.Equals("break") ) { break; };
                    message = userInput;
                    Console.WriteLine("Value:");
                    if( userInput.Equals("break") ) { break; };

                    while( true ) {
                        try {
                            userInput = Console.ReadLine();
                            val = float.Parse(userInput);
                            break;
                        } catch { Console.WriteLine("try again"); }
                    }
                    Details.Add(message, val);
                }

                Console.WriteLine("Encoding");
                Data OutputData = new Data();

                OutputData.Name = name;
                OutputData.Competition = CompetitionID;
                OutputData.Details = Details;
                OutputData.Team = teamNumber;
                OutputData.TimeEdited = DateTime.Now;

                byte[] SerializedData = Serialization.encodeData(OutputData);
                ClientSocket.Send(SerializedData);


            } catch(Exception e ) {
                throw new ArgumentException("Cannot connect!");
            }

        }
    }
}
