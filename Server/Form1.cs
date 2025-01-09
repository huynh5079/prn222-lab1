using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Server : Form
    {
        private TcpListener tcpListener;
        private Thread listenerThread;
        private List<TcpClient> connectedClients = new List<TcpClient>();
        public Server()
        {
            InitializeComponent();
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            int port = 9000;
            tcpListener = new TcpListener(IPAddress.Parse("127.0.0.1"), port);
            tcpListener.Start();
            txtInfo.AppendText($"Server started on port {port}{Environment.NewLine}");
            txtServer.Text = ($"IP: 127.0.0.1 Port: {port}");

            lstUser.Items.Add("All");

            listenerThread = new Thread(ListenForClients);
            listenerThread.Start();
        }

        private void ListenForClients()
        {
            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                connectedClients.Add(client);
                string clientEndpoint = client.Client.RemoteEndPoint.ToString();

                txtInfo.Invoke(new Action(() =>
                {
                    txtInfo.AppendText($"Client connected: {clientEndpoint}{Environment.NewLine}");
                    lstUser.Items.Add(clientEndpoint);
                }));

                Thread clientThread = new Thread(() => HandleClientComm(client));
                clientThread.Start();
            }
        }
        private void HandleClientComm(TcpClient client)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int byteRead;

            while ((byteRead = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, byteRead);
                txtInfo.Invoke(new Action(() => txtInfo.AppendText($"Received: {message}{Environment.NewLine}")));
                //response client
                byte[] response = Encoding.UTF8.GetBytes("Order accepted.");
                stream.Write(response, 0, response.Length);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNote.Text)) 
            { 
                MessageBox.Show("Please enter a message to send.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return; 
            }

            string message = txtNote.Text; 
            byte[] data = Encoding.UTF8.GetBytes(message); 

            if (lstUser.SelectedItem != null && lstUser.SelectedItem.ToString() != "All")
            // Send note to specify user
            {
                string selectedClient = lstUser.SelectedItem.ToString();
                var client = connectedClients.FirstOrDefault(c => c.Client.RemoteEndPoint.ToString() == selectedClient); 
                if (client != null) 
                { 
                    NetworkStream stream = client.GetStream(); 
                    stream.Write(data, 0, data.Length); 
                    txtInfo.AppendText($"Sent to {selectedClient}: {message}{Environment.NewLine}"); 
                }
            }
            else
            { // send to all
                foreach (var client in connectedClients) 
                { 
                    NetworkStream stream = client.GetStream(); 
                    stream.Write(data, 0, data.Length); 
                } 
                
                txtInfo.AppendText($"Sent to All: {message}{Environment.NewLine}");
            }
        }
    }
}
