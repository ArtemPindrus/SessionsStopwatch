using System.IO.Pipes;
using System.Text;

using var client = new NamedPipeClientStream(".", "RestartPipe", PipeDirection.InOut);
client.Connect();

using var writer = new StreamWriter(client, Encoding.UTF8, -1, true);
writer.WriteLine("RestartStopwatch");