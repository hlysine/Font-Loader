using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace Font_Loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(int hWnd, uint Msg, int wParam, int lParam);
        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceW", SetLastError = true)]
        public static extern int AddFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);
        [DllImport("gdi32.dll", EntryPoint = "RemoveFontResourceW", SetLastError = true)]
        public static extern int RemoveFontResource([In][MarshalAs(UnmanagedType.LPWStr)] string lpFileName);

        private void Button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            InstalledFontCollection ifc = new InstalledFontCollection();

            listBox1.Items.AddRange(ifc.Families.Select(f => f.Name).ToArray());
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ask = new OpenFileDialog();
            if (ask.ShowDialog() == DialogResult.OK)
            {
                string filename = ask.FileName;

                const int WM_FONTCHANGE = 0x001D;
                const int HWND_BROADCAST = 0xffff;

                int res = AddFontResource(filename);
                SendMessage(HWND_BROADCAST, WM_FONTCHANGE, 0, 0);
                if (res == 0) MessageBox.Show("Failed"); else MessageBox.Show("Success");
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                Application.Exit();
            }
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ask = new OpenFileDialog();
            if (ask.ShowDialog() == DialogResult.OK)
            {
                string filename = ask.FileName;

                const int WM_FONTCHANGE = 0x001D;
                const int HWND_BROADCAST = 0xffff;

                int res = RemoveFontResource(filename);
                SendMessage(HWND_BROADCAST, WM_FONTCHANGE, 0, 0);
                if (res == 0) MessageBox.Show("Failed"); else MessageBox.Show("Success");
                System.Diagnostics.Process.Start(Application.ExecutablePath);
                Application.Exit();
            }
        }
    }
}
