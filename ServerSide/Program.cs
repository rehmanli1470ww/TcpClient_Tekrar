using ServerSide.Models;
using System.Net.Sockets;
using System.Net;
using ClientSide.Models;
using System.Text.Json;


var ip = IPAddress.Parse("192.168.43.38");
var port = 27001;
TcpListener listener = new TcpListener(ip, port);
listener.Start();
Console.WriteLine($"{listener.Server.LocalEndPoint} Listener connect");

while (true)
{
    try
    {
        var client = listener.AcceptTcpClient();
        Console.WriteLine($"{client.Client.RemoteEndPoint} is connect ");
        _ = Task.Run(() =>
        {
            var clientdata = client.GetStream();
            var writer = new BinaryWriter(clientdata);
            var reader = new BinaryReader(clientdata);
            bool isCheck = true;
            string username = "";
            while (true)
            {
                if (isCheck)
                {
                    username = reader.ReadString();
                    DataBase.Users.Add(new User()
                    {
                        UserName = username,
                        TcpClient=client
                    });
                    
                    isCheck= false;
                }
                if (!isCheck) 
                {
                    var message=reader.ReadString();
                    MessageDto? msg=JsonSerializer.Deserialize<MessageDto>(message);

                    if (msg is not null)
                    {
                        User user = DataBase.Users.FirstOrDefault(u => u.UserName.ToLower() == msg.UserName.ToLower());
                        if (user is not null)
                        {
                            BinaryWriter userWriter = new BinaryWriter(user.TcpClient.GetStream());
                            MessageDto sendMessage = new MessageDto()
                            {
                                UserName=username,
                                Message=msg.Message
                            };
                            
                            string temp=JsonSerializer.Serialize(sendMessage);
                            userWriter.Write(temp);

                        }
                    }
                }
            }
        });

    }
    catch (Exception ex)
    {
        Console.WriteLine(ex);
    }
}


static class DataBase
{
    public static List<User> Users = new List<User>();
}