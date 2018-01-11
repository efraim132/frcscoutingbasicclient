using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FRCScoutingClient {
    [Serializable]public struct Data {
        /// <summary>
        /// Person who Created
        /// </summary>
        public string Name;
        /// <summary>
        /// Time Created
        /// </summary>
        public DateTime TimeEdited;
        /// <summary>
        /// Actual Data
        /// </summary>
        public Dictionary<string, float> Details;
        /// <summary>
        /// Competition ID
        /// </summary>
        public int Competition;
        /// <summary>
        /// Team surveyed ID
        /// </summary>
        public int Team;

        IPAddress iPAddress;

    }
}
