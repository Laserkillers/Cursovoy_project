using System;
using System.Collections.Generic;

#nullable disable

namespace Cursovoy_project
{
    public partial class User
    {
        public int Id { get; set; }
        public int TypeOfAccount { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public virtual UsersDatum UsersDatum { get; set; }
    }
}
