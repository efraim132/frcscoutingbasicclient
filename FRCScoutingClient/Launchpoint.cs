using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRCScoutingClient {
    class Launchpoint {
        public static void Main(string[] args) {
            Client client = new Client();
            client.run(2000, "Efraim");
        }
    }
}
