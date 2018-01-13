using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FRCScoutingClient {
    class Launchpoint {
        public static void Main(string[] args) {
            Client client = new Client();
            client.run(2000, "Efraim", 3);
            //Data SampleData = new Data();
            //SampleData.Name = "jeff";
            //SampleData.Details = new Dictionary<string, double>();
            //SampleData.Details.Add("msg1", 12);
            //SampleData.Details.Add("msg2", 13);
            //SampleData.Competition = 1234;
            //SampleData.Team = 5678;
            //SampleData.iPAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[3];



            //byte[] sampleEncoded = Serialization.encodeDataWriter(SampleData);
            //Data decodedSample = Serialization.decodeDataReader(sampleEncoded);
            //printData(SampleData);
            //printData(decodedSample);
            //Console.Write(decodedSample.Equals(SampleData));


            Console.ReadKey();

           


        }
        static void printData(Data data) {
            
            ConsoleUtility.writeColor("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~", ConsoleColor.DarkRed);
            ConsoleUtility.writeStatus("\nName", data.Name, ConsoleColor.Gray, ConsoleColor.Yellow);
            ConsoleUtility.writeStatus("\nCompetitionID", data.Competition, ConsoleColor.Gray, ConsoleColor.Yellow);
            ConsoleUtility.writeStatus("\nTeam Surveyed", data.Team, ConsoleColor.Gray, ConsoleColor.Yellow);
            Console.WriteLine();
            foreach( KeyValuePair<string, double> KVPair in data.Details ) {
                ConsoleUtility.writeStatus(KVPair.Key, KVPair.Value, ConsoleColor.White, ConsoleColor.Yellow);
                Console.WriteLine();
            }
            ConsoleUtility.writeColor("\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n", ConsoleColor.DarkRed);

        }
    }
}
