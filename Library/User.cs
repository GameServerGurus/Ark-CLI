using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class User
    {
        public int? id { get; set; }
        public string? gamertag { get; set; }
        public string? password_digest { get; set; }
        public string? xuid { get; set; }
    }
}
