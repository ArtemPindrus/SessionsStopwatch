using System.IO.Pipes;

using var client = new NamedPipeClientStream(".", "RestartPipe", PipeDirection.InOut);
client.Connect();

using var writer = new StreamWriter(client);
writer.WriteLine("RestartStopwatch");