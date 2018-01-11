using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace FRCScoutingClient {
    class Listener {
        public Client hostClient;
        public Listener(Client hostClient) {
            this.hostClient = hostClient;
        }
        public void run() {
            while( true ) {
                hostClient.ClientSocket.Receive(hostClient.byteRec);
                hostClient.recUpdate = true;
            }
        }
    }
}
