using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientSide.Models
{
    public class MessageDto
    {
        public string? UserName { get; set; }
        public string? Message { get; set; }
        public override string ToString()
        {
            return $"Name {UserName}  Maessage : {Message}";
        }
    }
}
