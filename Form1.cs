using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameMod
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        static int step = 0;
        static int stepcount = 0;
        string kill = "taskkill /F /im ";
        string kill2 = "taskkill /T /im ";
        string kill3 = "taskkill /IM ";
        string killService = "sc stop ";
        string startService = "sc start ";

        string[] gameBlackList = { "TrGUI.exe", "EXCEL.exe", "uTorrent.exe", "1cv8c.exe", "EPWD.exe", "EPWD_Tool.exe", "TracSrvWrapper.exe",
            "Anydesk.exe", "Lightshot.exe", "\"All-in-One Messenger.exe\""};
        string[] serviceBlackList = { "TracSrvWrapper", "RzActionSvc", "Razer Game Manager Service", "Razer Game Manager Service 3", "EPWD" };
        string[] workStartList = {
            "C:\\Work\\CHECK_POINT_VPN_CLIENT\\TrGUI",
            "C:\\Program Files\\1cv8\\8.3.20.1789\\bin\\1cv8c",
            "C:\\Work\\CHECK_POINT_VPN_CLIENT\\Watchdog\\EPWD",
            "C:\\Work\\CHECK_POINT_VPN_CLIENT\\TracSrvWrapper",
            "C:\\Program Files (x86)\\AnyDesk\\Anydesk",
            "C:\\Program Files (x86)\\Skillbrains\\lightshot\\Lightshot",
            "C:\\Users\\Asuka\\AppData\\Local\\Programs\\all-in-one-messenger\\All-in-One Messenger"};
        string[] workService = { "TracSrvWrapper", "EPWD" };


        async void button1_Click(object sender, EventArgs e)
        {
            CmdUsing(false, gameBlackList, serviceBlackList);

        }


        private void button2_Click(object sender, EventArgs e)
        {

            CmdUsing(true, workStartList, workService);

        }


        //Методы работы с CMD
        void CmdKill(string line)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c \"{line}\"",
                WindowStyle = ProcessWindowStyle.Hidden
            }).WaitForExit();
        }
        void CmdStart(string line)
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = "cmd",
                Arguments = $"/c \"{line}\"",
                WindowStyle = ProcessWindowStyle.Hidden
            }).WaitForExit(200);
        }

        void CmdUsing(bool starting, string[] programs, string[] services)
        {
            progressBar1.Value = 0;
            step = 100 / (programs.Length + services.Length);
            void PBPlus()
            {
                if (progressBar1.Value + step > 100)
                {
                    progressBar1.Value = 100;
                }
                else
                {
                    progressBar1.Value += step;
                }
            }
            if (!starting)
            {
                foreach (string program in programs)
                {
                    CmdKill(kill + program);
                    CmdKill(kill2 + program);
                    CmdKill(kill3 + program);
                    PBPlus();
                }
                foreach (string service in services)
                {
                    CmdKill($"sc stop \"{service}\"");
                    PBPlus();
                }
            }
            else
            {
                foreach (string service in services)
                {
                    CmdStart(service);
                    PBPlus();
                }
                foreach (string program in programs)
                {
                    CmdStart(program);
                    PBPlus();
                }
            }
            progressBar1.Value = 0;
        }

    }
}
