using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Session
    {
        public int? id { get; set; }
        public string? xuid { get; set; }
        public string? session_token { get; set; }
        public string? security_token { get; set; }
        public bool? authenticated { get; set; }
    }
}
