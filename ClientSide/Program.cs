using ClientSide.Models;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;

IPAddress ip = IPAddress.Parse("192.168.43.38");
int port = 27001;
IPEndPoint endPoint=new IPEndPoint(ip, port);
TcpClient client = new TcpClient();
client.Connect(endPoint);
NetworkStream networkStream = client.GetStream();
BinaryReader binaryReader = new BinaryReader(networkStream);
BinaryWriter binaryWriter = new BinaryWriter(networkStream);

_ = Task.Run(() => 
{
	while (true)
	{
		string message = binaryReader.ReadString();
		MessageDto messageDto=JsonSerializer.Deserialize<MessageDto>(message);
		Console.WriteLine("\n"+messageDto);

    }
});

bool isCheck = true;
while (true) 
{
	if (isCheck)
	{
        Console.Write("Enter your name :");
		string name=Console.ReadLine();
		binaryWriter.Write(name);
        isCheck = false;
	}
	else 
	{
		Console.Write("Sender name : ");
		string name=Console.ReadLine();
		Console.Write("Sender message : ");
		string message=Console.ReadLine();
		MessageDto messageDto = new MessageDto()
		{
			UserName = name,
			Message=message
		};
		string messageJson = JsonSerializer.Serialize(messageDto);
		binaryWriter.Write(messageJson);

	}

}
