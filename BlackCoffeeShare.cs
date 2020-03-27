using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace WebShareC
{
    public partial class BlackCoffeeShare : Form
    {
        // TestModuleLib.Module Testing = new TestModuleLib.Module(); //모듈 사용하지말것
        static List<string> filePath = new List<string>();
        //HostControl con;
        BoolEvent b; // BoolEvent 클래스 정리 혹은 따로 만들어서 다시 할것. 현재 스크립트에있는 클래스는 참고만하고 나중에 삭제 할것.

        private string ConnectionIPForClient;

        public static List<string> FILEPATH
        {
            get { return filePath; }
        }
        public static Int32 FILENUM
        {
            get { return filePath.Count; }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public BlackCoffeeShare()
        {
            InitializeComponent();
        }
        private void ExitButtonTesting_Click(object sender, EventArgs e) //exit bㅈtton (top of right)
        {
            Application.Exit();
        }
        private void Button1_Click(object sender, EventArgs e) // File list add button
        {
            //FolderBrowserDiaglog 는 파일을 선택할수없음. 경로지정만 가능
            OpenFileDialog t = new OpenFileDialog();
            if (t.ShowDialog() == DialogResult.OK)
            {
                FileListText.Items.Add(t.FileName.Substring(0, (t.FileName.Length)));
                filePath.Add(t.FileName.Substring(0, (t.FileName.Length)));
            }
        }
        private void Button2_Click(object sender, EventArgs e) // list remove at selected file on the list box
        {
            if (FileListText.SelectedIndex == -1)
            {
                return;
            }
            FileListText.Items.RemoveAt(FileListText.SelectedIndex);
            filePath.RemoveAt(FileListText.SelectedIndex);
        }
        private void FileListText_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Link;
        }
        private void FileListText_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            foreach (string file in files)
            {
                FileListText.Items.Add(file);
                filePath.Add(file);
            }
        }
        private void Button3_Click(object sender, EventArgs e) // Hosting button
        {
          //  con = new HostControl(); // Starting Server
            MessageBox.Show("Server Started");
            HostButton.Enabled = false;

            b = new BoolEvent();
            b.Click += new BoolEvent.MyEventHandler(p_click);
            b.DoClick();
        }
        private void Button4_Click(object sender, EventArgs e) // Connect, Stop Server Button
        {
            if (!HostButton.Enabled) // 서버를 시작한경우 서버 종료
            {
                //con.Disconnect();
            }
            else if (HostButton.Enabled) // 서버를 시작하지 않고 Connect 버튼 이벤트 발생시
            {
                IpConnect dlg = new IpConnect();
                dlg.ShowDialog();
                HostButton.Enabled = false;
                //  Client클래스 생성 후 구현
            }
        } 
        void p_click()
        {
            ConnectStopButton.Text = "Stop Server";
        }
        private void ClientDownload_Click(object sender, EventArgs e) 
        {

        }
    }
    class BoolEvent
    {
        public delegate void MyEventHandler();
        public event MyEventHandler Click;

        public void DoClick()
        {
            if (Click != null)
            {
                Click();
            }
        }
    }

}