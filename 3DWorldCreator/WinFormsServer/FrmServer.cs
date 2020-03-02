using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Hosting;
using Microsoft.AspNet.SignalR;
using System.ComponentModel;
using WinFormsServer;

namespace WinFormsServer
{
    public partial class FrmServer : Form
    {
        private IDisposable _signalR;
        private BindingList<ClientItem> _clients = new BindingList<ClientItem>();
        private BindingList<string> _groups = new BindingList<string>();

        public FrmServer()
        {
            InitializeComponent();

            bindListsToControls();

            //Register to static hub events
            SimpleHub.ClientConnected += SimpleHub_ClientConnected;
            SimpleHub.ClientDisconnected += SimpleHub_ClientDisconnected;
            SimpleHub.ClientNameChanged += SimpleHub_ClientNameChanged;
            SimpleHub.ClientJoinedToGroup += SimpleHub_ClientJoinedToGroup;
            SimpleHub.ClientLeftGroup += SimpleHub_ClientLeftGroup;
            SimpleHub.MessageReceived += SimpleHub_MessageReceived;
        }

        private void bindListsToControls()
        {
            //Clients list
            cmbClients.DisplayMember = "Name";
            cmbClients.ValueMember = "Id";
            cmbClients.DataSource = _clients;

            //Groups list
            cmbGroups.DataSource = _groups;
        }

        private void SimpleHub_ClientConnected(string clientId)
        {
            //Add client to our clients list
            this.BeginInvoke(new Action(() => _clients.Add(new ClientItem() { Id = clientId, Name = clientId })));

            writeToLog($"Client connected:{clientId}");
        }

        private void SimpleHub_ClientDisconnected(string clientId)
        {
            //Remove client from the list
            this.BeginInvoke(new Action(() =>
            {
                var client = _clients.FirstOrDefault(x => x.Id == clientId);
                if (client != null)
                    _clients.Remove(client);
            }));

            writeToLog($"Client disconnected:{clientId}");
        }

        private void SimpleHub_ClientNameChanged(string clientId, string newName)
        {
            //Update the client's name if it exists
            this.BeginInvoke(new Action(() =>
            {
                var client = _clients.FirstOrDefault(x => x.Id == clientId);
                if (client != null)
                    client.Name = newName;
            }));

            writeToLog($"Client name changed. Id:{clientId}, Name:{newName}");
        }

        private void SimpleHub_ClientJoinedToGroup(string clientId, string groupName)
        {
            //Only add the groups name to our groups list
            this.BeginInvoke(new Action(() =>
            {
                var group = _groups.FirstOrDefault(x => x == groupName);
                if (group == null)
                    _groups.Add(groupName);
            }));

            writeToLog($"Client joined to group. Id:{clientId}, Group:{groupName}");
        }

        private void SimpleHub_ClientLeftGroup(string clientId, string groupName)
        {
            writeToLog($"Client left group. Id:{clientId}, Group:{groupName}");
        }

        private void SimpleHub_MessageReceived(string senderClientId, string message)
        {
            //One of the clients sent a message, log it
            this.BeginInvoke(new Action(() =>
            {
                string clientName = _clients.FirstOrDefault(x => x.Id == senderClientId)?.Name;

                writeToLog($"{clientName}:{message}");
            }));
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            txtLog.Clear();

            try
            {
                //Start SignalR server with the give URL address
                //Final server address will be "URL/signalr"
                //Startup.Configuration is called automatically
                _signalR = WebApp.Start<Startup>(txtUrl.Text);

                btnStartServer.Enabled = false;
                txtUrl.Enabled = false;
                btnStop.Enabled = true;
                grpBroadcast.Enabled = true;

                writeToLog($"Server started at:{txtUrl.Text}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _clients.Clear();
            _groups.Clear();

            SimpleHub.ClearState();

            if (_signalR != null)
            {
                _signalR.Dispose();
                _signalR = null;

                btnStop.Enabled = false;
                btnStartServer.Enabled = true;
                txtUrl.Enabled = true;
                grpBroadcast.Enabled = false;

                writeToLog("Server stopped.");
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();

            if (rdToAll.Checked)
            {
                hubContext.Clients.All.addMessage("SERVER", txtMessage.Text);
            }
            else if (rdToGroup.Checked)
            {
                hubContext.Clients.Group(cmbGroups.Text).addMessage("SERVER", txtMessage.Text);
            }
            else if (rdToClient.Checked)
            {
                hubContext.Clients.Client((string)cmbClients.SelectedValue).addMessage("SERVER", txtMessage.Text);
            }
        }

        private void writeToLog(string log)
        {
            if (this.InvokeRequired)
                this.BeginInvoke(new Action(() => txtLog.AppendText(log + Environment.NewLine)));
            else
                txtLog.AppendText(log + Environment.NewLine);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //WinFormsServer.FrmServer client_ = new WinFormsServer.FrmClient();
            //client_.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "stop_");
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "start_");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "1");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "2");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "3");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "4");
        }

        private void button35_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "5");
        }

        private void button34_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "6");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "7");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "7");
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "8");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "9");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "10");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "11");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "12");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "13");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "14");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "15");
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "16");
        }

        private void button13_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "17");
        }

        private void button14_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "18");
        }

        private void button15_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "19");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "20");
        }

        private void button26_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "21");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "22");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "23");
        }

        private void button16_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "24");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "25");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "26");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "27");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "28");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "29");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            hubContext.Clients.All.addMessage("SERVER", "30");
        }

        private int ticks__=-1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<SimpleHub>();
            ticks__++;
            //System.IO.File.WriteAllText(@"C:\Temp\files\text.txt", ticks__.ToString());
            string command_anim;
            while (true)
            {
                try
                {
                    command_anim = System.IO.File.ReadAllText(@"C:\Temp\files\animation.txt");
                    break;
                }
                catch
                {

                }
            }


            //MessageBox.Show(command_anim);
            hubContext.Clients.All.addMessage("SERVER", command_anim);//, command_anim);
            if (ticks__ > 29)
            {
                ticks__ = 1;
            }
        }
    }
}
