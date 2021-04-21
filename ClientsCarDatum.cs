using System;
using System.Collections.Generic;

#nullable disable

namespace Cursovoy_project
{
    public partial class ClientsCarDatum
    {
        public int Id { get; set; }
        public string GosNumber { get; set; }
        public string CarBrend { get; set; }
        public string CarModel { get; set; }
        public long? Odometr { get; set; }
    }
}
