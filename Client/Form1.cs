using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Client : Form
    {
        private TcpClient client;
        private NetworkStream stream;
        public Client()
        {
            InitializeComponent();

            //add user, use like login
            cboUser.Items.Add("Timmy");
            cboUser.Items.Add("Annie"); 
            cboUser.Items.Add("FoodSeller"); 
            cboUser.Items.Add("DrinkSeller");

            //add items, use like click and buy item
            cboItem.Items.Add("Burger");
            cboItem.Items.Add("Pizza");
            cboItem.Items.Add("Sandwitch");
            cboItem.Items.Add("Cocacola");
            cboItem.Items.Add("Pepsi");
            cboItem.Items.Add("Aquafina");

            //add city
            cboCity.Items.Add("Hải Châu");
            cboCity.Items.Add("Ngũ Hành Sơn");
            cboCity.Items.Add("Cẩm Lệ");
            cboCity.Items.Add("Hòa Vang");
            cboCity.Items.Add("Sơn Trà");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            //Check role
            if (cboUser.SelectedIndex == -1)
            {
                MessageBox.Show("Please select your user role!!!");
                return;
            }
            //Declare role and server addr
            string role = cboUser.SelectedIndex.ToString();
            string serverAddress = "127.0.0.1"; //ip
            int port = 9000; //port

            try
            {
                client = new TcpClient(serverAddress, port);
                stream = client.GetStream();
                txtInfo.AppendText($"Connected to server at {serverAddress}:{port} as {role}{Environment.NewLine}");
                
                //Qualify role 
                byte[] data = Encoding.UTF8.GetBytes(role);
                stream.Write(data, 0, data.Length);

                //Receive data from server
                Thread receiverThread = new Thread(ReceiveData);
                receiverThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Couldn't connect to server: {ex.Message}", "Connection error!!!");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (cboItem.SelectedItem == null || cboCity.SelectedItem == null || string.IsNullOrEmpty(txtAddress.Text))
            {
                MessageBox.Show("Please fill all required fields.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string orderDetails = $"User: {cboUser.SelectedItem}{Environment.NewLine}Item: {cboItem.SelectedItem}{Environment.NewLine}City: {cboCity.SelectedItem}{Environment.NewLine}Address: {txtAddress.Text}{Environment.NewLine}Note: {txtNote.Text}";
            byte[] data = Encoding.UTF8.GetBytes(orderDetails);
            stream.Write(data, 0, data.Length);
            txtInfo.AppendText($"Sent: {orderDetails}{Environment.NewLine}");
        }

        private void ReceiveData()
        {
            byte[] buffer = new byte[1024];
            int bytesRead;

            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                txtInfo.Invoke(new Action(() => txtInfo.AppendText($"Received: {message}{Environment.NewLine}")));
            }
        }
    }
}
