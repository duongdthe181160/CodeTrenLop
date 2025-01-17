using System;
using System.Net.Sockets;
using System.Text;

class ClientApp
{
    static void ConnectToServer(string server, int port)
    {
        try
        {
            // Establish connection
            TcpClient client = new TcpClient(server, port);
            Console.Title = "Client Application";
            Console.WriteLine($"Connected to server at {server}:{port}");

            NetworkStream stream = client.GetStream();

            while (true)
            {
                Console.Write("Enter message <press Enter to exit>: ");
                string message = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(message))
                {
                    Console.WriteLine("Closing connection...");
                    break;
                }

                // Send message to server
                byte[] dataToSend = Encoding.ASCII.GetBytes(message);
                stream.Write(dataToSend, 0, dataToSend.Length);
                Console.WriteLine($"Sent: {message}");

                // Receive response from server
                byte[] buffer = new byte[256];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"Received: {response}");
            }

            // Close resources
            stream.Close();
            client.Close();
        }
        catch (SocketException ex)
        {
            Console.WriteLine($"Socket error: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    public static void Main(string[] args)
    {
        string server = "127.0.0.1";
        int port = 13000;
        ConnectToServer(server, port);
    }
}
