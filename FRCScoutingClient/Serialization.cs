using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FRCScoutingClient {
    class Serialization {
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
    }
}
