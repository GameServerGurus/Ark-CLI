using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Command
    {
        public string? address { get; set; }
        public string code { get; set; }
        public bool executed { get; set; }
        public string operation { get; set; }
        public string? operation_id { get; set; }
        public string? player_id { get; set; }
        public string? server_id { get; set; }
        public string? xuid { get; set; }

        public Command()
        {
            this.code = "";
            this.executed = false;
            this.operation = "OTHER";
        }
    }
}
