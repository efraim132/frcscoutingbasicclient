using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace FRCScoutingClient {
    class Client {
        public Socket ClientSocket;
        public byte[] byteRec = new byte[1024];
        public bool recUpdate = false;
        public int netAdapter;
        public byte[] Identifier = Encoding.ASCII.GetBytes("Efraim");



        public void run(int port, string serverHostName, int networkAdapter) {
            netAdapter = networkAdapter;
            IPHostEntry ipHostInfo = Dns.GetHostEntry(serverHostName);
            IPAddress ipAddress = ipHostInfo.AddressList[netAdapter];
            IPEndPoint remoteEndPoint = new IPEndPoint(ipAddress, port);
 
            ClientSocket = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            try {

                ClientSocket.Connect(remoteEndPoint);

                Listener reciever = new Listener(this);
                Thread recieverThread = new Thread(new ThreadStart(reciever.run));
                recieverThread.Start();
                while(!recieverThread.IsAlive);


                //Data OutputData = GetData();

                Data SampleData = new Data();
                SampleData.Name = "jeff";
                SampleData.Details = new Dictionary<string, double>();
                SampleData.Details.Add("msg1", 12);
                SampleData.Details.Add("msg2", 13);
                SampleData.Competition = 1234;
                SampleData.Team = 2345;
                //SampleData.iPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[netAdapter];


                List<byte> SerializedData = new List<byte>();
                foreach(byte b in Serialization.encodeDataWriter(SampleData) ) {
                    SerializedData.Add(b);
                }
                Console.WriteLine("Serialized Data, Adding identifier, Size is {0} bytes", SerializedData.Count);
                foreach(byte b in Identifier ) {
                    SerializedData.Add(b);
                }
                Console.WriteLine("Added Identifier, Sending!");



                ClientSocket.Send(SerializedData.ToArray());
                Console.WriteLine("Sent!");


            } catch(Exception e ) {
                throw new ArgumentException("Cannot connect!",e);
            }

        }
        public Data GetData() {

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
            Dictionary<string, double> Details = new Dictionary<string, double>();
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
            // OutputData.TimeEdited = DateTime.Now;
            //OutputData.iPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[netAdapter];

            return OutputData;
        }
    }
}
