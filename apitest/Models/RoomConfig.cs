using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace apitest.Models
{
    // Describes the non-variable aspects of a room that we'll need to calculate duration/intensity and anything else
    public class RoomConfig
    {
        public int roomId { get; set; }
        public int roomWidth { get; set; }
        public int roomHeight { get; set; }

    }
}
