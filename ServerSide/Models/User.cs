using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Models
{
    public class User
    {
        public string? UserName { get; set; }
        public TcpClient? TcpClient {  get; set; }
    }
}
