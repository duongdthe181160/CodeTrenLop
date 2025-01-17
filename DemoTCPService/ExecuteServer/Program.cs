using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

class ServerApp
{
    static void ProcessMessage(object parm)
    {
        try
        {
            TcpClient client = parm as TcpClient;
            if (client == null) return;

            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[256];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string receivedData = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {receivedData} at {DateTime.Now:t}");

                // Respond with uppercase string
                string response = receivedData.ToUpper();
                byte[] responseBytes = Encoding.ASCII.GetBytes(response);
                stream.Write(responseBytes, 0, responseBytes.Length);
                Console.WriteLine($"Sent: {response}");
            }

            client.Close();
            Console.WriteLine("Client disconnected.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void ExecuteServer(string host, int port)
    {
        TcpListener server = null;
        try
        {
            Console.Title = "Server Application";
            server = new TcpListener(IPAddress.Parse(host), port);
            server.Start();

            Console.WriteLine(new string('*', 40));
            Console.WriteLine($"Server started at {host}:{port}");
            Console.WriteLine("Waiting for connections...");

            while (true)
            {
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine($"Client connected: {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

                // Handle each client in a new thread
                Thread thread = new Thread(ProcessMessage);
                thread.Start(client);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Server error: {ex.Message}");
        }
        finally
        {
            server?.Stop();
            Console.WriteLine("Server stopped.");
        }
    }

    public static void Main()
    {
        string host = "127.0.0.1";
        int port = 13000;
        ExecuteServer(host, port);
    }
}
