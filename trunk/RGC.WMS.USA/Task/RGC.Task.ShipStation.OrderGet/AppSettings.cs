using System;
using System.Collections.Generic;
using System.Text;

namespace RGC.Task.ShipStation.OrderGet
{
    public class Appsettings
    {
        public string api_key { get; set; }
        public string api_secret { get; set; }
        public int? circle_seconds { get; set; }
        public int? circle_minutes { get; set; }
        public string getOrder_startModifyTime { get; set; }
    }
}
