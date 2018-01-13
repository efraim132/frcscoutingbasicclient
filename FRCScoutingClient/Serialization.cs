using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FRCScoutingClient {
    public static class Serialization {
        public static Data decodeData(byte[] byterec) {

            using( Stream memoryStream = new MemoryStream() ) {
                var binForm = new BinaryFormatter();
                memoryStream.Write(byterec, 0, byterec.Length);
                memoryStream.Seek(0, SeekOrigin.Begin);
                var dataObject = binForm.Deserialize(memoryStream);
                return (Data) dataObject;
            }
        }

        public static byte[] encodeData(Data obj) {
            BinaryFormatter bf = new BinaryFormatter();
            using( var ms = new MemoryStream() ) {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }

        public static Data decodeDataReader(byte[] byterec) {
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(byterec, 0, byterec.Length);
            memoryStream.Seek(0, SeekOrigin.Begin);
            BinaryReader binaryReader = new BinaryReader(memoryStream);

            Data outputData = new Data();

            outputData.Name = binaryReader.ReadString();
            outputData.Competition = binaryReader.ReadInt32();
            outputData.Team = binaryReader.ReadInt32();


            int DetailsCount = binaryReader.ReadInt32();
            outputData.Details = new Dictionary<string, double>();
            string msg;
            double detail;
            for(int i = 0; i < DetailsCount; i++ ) {
                msg = binaryReader.ReadString();
                detail = binaryReader.ReadDouble();
                outputData.Details.Add(msg, detail);
            }

            return outputData;
                       
        }

        public static byte[] encodeDataWriter(Data obj) {
            MemoryStream memoryStream = new MemoryStream();
            BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
            //Serializes in order of Name, Competition, Team, Details(string then float)
            binaryWriter.Write(obj.Name);
            binaryWriter.Write(obj.Competition);
            binaryWriter.Write(obj.Team);
            binaryWriter.Write(obj.Details.Count());
            foreach(KeyValuePair<string, double> KVPair in obj.Details ) {
                binaryWriter.Write(KVPair.Key);
                binaryWriter.Write(KVPair.Value);
            }
            return memoryStream.ToArray();
        }

    }
}
