using System;
using System.Collections.Generic;

#nullable disable

namespace Cursovoy_project
{
    public partial class UsersDatum
    {
        public int Id { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public DateTime? DateBirth { get; set; }

        public virtual User IdNavigation { get; set; }
    }
}
