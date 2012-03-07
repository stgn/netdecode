using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace netdecode
{
    public partial class MainForm : Form
    {
        DemoFile _currentFile;

        public MainForm()
        {
            InitializeComponent();
        }

        private void ParseIntoTree(DemoFile.DemoMessage msg)
        {
            var node = new TreeNode(String.Format("{0}, tick {1}, {2} bytes", msg.Type, msg.Tick, msg.Data.Length));
            node.Expand();
            node.BackColor = DemoMessageItem.GetTypeColor(msg.Type);

            switch (msg.Type)
            {
                case DemoFile.MessageType.ConsoleCmd:
                    node.Nodes.Add(new TreeNode(Encoding.ASCII.GetString(msg.Data)));
                    break;
                case DemoFile.MessageType.UserCmd:
                    UserCmd.ParseIntoTreeNode(node, msg.Data);
                    break;
                case DemoFile.MessageType.Signon:
                case DemoFile.MessageType.Packet:
                    Packet.Parse(msg.Data, node);
                    break;
                default:
                    node.Nodes.Add(new TreeNode("Unhandled demo message type."));
                    break;
            }

            messageTree.Nodes.Add(node);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog
            {
                Filter = "Demo files|*.dem",
                RestoreDirectory = true
            };

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    messageList.Items.Clear();

                    Stream f = ofd.OpenFile();
                    _currentFile = new DemoFile(f);
                    f.Close();

                    foreach (var msg in _currentFile.Messages)
                    {
                        messageList.Items.Add(new DemoMessageItem(msg));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error opening file", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void messageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            messageTree.Nodes.Clear();

            foreach(DemoMessageItem item in messageList.SelectedItems) {
                ParseIntoTree(item.Msg);
            }
        }
    }

    class DemoMessageItem : ListViewItem
    {
        public DemoFile.DemoMessage Msg;

        public DemoMessageItem(DemoFile.DemoMessage msg)
        {
            Msg = msg;

            BackColor = GetTypeColor(msg.Type);
            Text = Msg.Tick.ToString();
            SubItems.Add(Msg.Type.ToString());
            SubItems.Add(Msg.Data.Length.ToString());
        }

        public static Color GetTypeColor(DemoFile.MessageType type)
        {
            switch(type) {
                case DemoFile.MessageType.Signon:
                case DemoFile.MessageType.Packet:
                    return Color.LightBlue;
                case DemoFile.MessageType.UserCmd:
                    return Color.LightGreen;
                case DemoFile.MessageType.ConsoleCmd:
                    return Color.LightPink;
                default:
                    return Color.White;
            }
        }
    }
}
