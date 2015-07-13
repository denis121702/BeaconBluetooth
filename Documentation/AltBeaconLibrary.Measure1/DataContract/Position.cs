using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AltBeaconLibrary.Measure1.DataContract
{
    public class Position
    {        
        public Guid DeviceId { get; set; }
     
        public DateTime Time { get; set; }
        
        public int X { get; set; }
        
        public int Y { get; set; }

        public int XReal { get; set; }

        public int YReal { get; set; }
    }
}
