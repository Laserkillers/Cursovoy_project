using System;
using System.Collections.Generic;

#nullable disable

namespace Cursovoy_project
{
    public partial class AutoService
    {
        public int Id { get; set; }
        public string GosNumber { get; set; }
        public int ClientId { get; set; }
        public string Fault { get; set; }
        public decimal? Cost { get; set; }
        public DateTime ReceptionTime { get; set; }
        public DateTime? IssureTime { get; set; }
        public bool? NeedToDelete { get; set; }
    }
}
