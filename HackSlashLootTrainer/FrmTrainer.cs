using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HackSlashLootTrainer
{
    public partial class FrmTrainer : Form
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(uint dwDesiredAccess, bool bInheritHandle, uint dwProcessId);
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool CloseHandle(IntPtr hObject);
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesRead);
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int dwSize, ref int lpNumberOfBytesWritten);

        const uint PROCESS_ALL_ACCESS = 0x000F0000 | 0x00100000 | 0xFFFF;

        IntPtr hProc;
        int modBaseAddr;
        int playerStructAddr;
        BackgroundWorker bw;
        
        public FrmTrainer()
        {
            InitializeComponent();
        }

        private void FrmTrainer_Load(object sender, EventArgs e)
        {
            // Get process ID
            Process[] processes = Process.GetProcessesByName("HackSlashLoot");
            if (processes.Length == 0)
            {
                lblHealth.Text = "Could not find HackSlashLoot process...";
                lblHealth.ForeColor = Color.Red;
                numNewHealth.Enabled = false;
                btnSetHealth.Enabled = false;
                return;
            }
            Process process = processes[0];

            // Get module base address
            modBaseAddr = process.MainModule.BaseAddress.ToInt32();

            // OpenProcess with PROCESS_ALL_ACCESS flags
            hProc = OpenProcess(PROCESS_ALL_ACCESS, false, ((uint)process.Id));

            // Calculate pointer to player struct.
            // You can find these offsets using Cheat Engine.
            // For reference, in my case the health offset was 0x50.
            // With this offset I found the player struct static address 0x7a6014.
            playerStructAddr = modBaseAddr + 0x7A6014;

            // Read health to show on label
            int health = ReadHealth();
            this.lblHealth.Text = "Health: " + health;

            // Set-up background worker to update health label each second.
            bw = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            bw.DoWork += Bw_DoWork;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.RunWorkerAsync();
        }

        private void FrmTrainer_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseHandle(hProc);
        }

        private void Bw_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                int health = ReadHealth();
                bw.ReportProgress(0, health);
                Thread.Sleep(1000);
            }
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var health = (int)e.UserState;
            this.lblHealth.Text = "Health: " + health;
        }

        private int ReadHealth()
        {
            // Recalculate pointer. When the player dies, a new player struct is constructed on the heap.
            byte[] bytes = new byte[4]; int bytesRead = 0;
            ReadProcessMemory(hProc, new IntPtr(playerStructAddr), bytes, sizeof(int), ref bytesRead);
            // TODO Check error
            int healthAddr = BitConverter.ToInt32(bytes, 0) + 0x50; // Health offset inside player struct

            // Try to read
            bytes = new byte[4]; bytesRead = 0;
            ReadProcessMemory(hProc, new IntPtr(healthAddr), bytes, sizeof(int), ref bytesRead);
            // TODO Check error

            return BitConverter.ToInt32(bytes, 0);
        }

        private void SetHealth(int newHealth)
        {
            // Recalculate pointer. When the player dies, a new player struct is constructed on the heap.
            byte[] bytes = new byte[4]; int bytesRead = 0;
            ReadProcessMemory(hProc, new IntPtr(playerStructAddr), bytes, sizeof(int), ref bytesRead);
            int healthAddr = BitConverter.ToInt32(bytes, 0) + 0x50; // Health offset inside player struct

            // Try to write
            bytesRead = 0;
            WriteProcessMemory(hProc, new IntPtr(healthAddr), BitConverter.GetBytes(newHealth), sizeof(int), ref bytesRead);
            // TODO Check error
        }

        private void btnSetHealth_Click(object sender, EventArgs e)
        {
            var newHealth = (int)numNewHealth.Value;
            SetHealth(newHealth);
        }
    }
}
