using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VersionDB4Lib.UI;

namespace VersionDB4
{
    partial class FVersionDB
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void SplitContainer1_Paint(object sender, PaintEventArgs e)
            => e.Graphics.DrawLine(new Pen(Color.FromArgb(212, 212, 212)), splitContainer1.SplitterDistance, 0, splitContainer1.SplitterDistance, splitContainer1.ClientSize.Height);

        private void SplitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
            => e.Graphics.DrawLine(new Pen(Color.FromArgb(212, 212, 212)), 0, 80, splitContainer1.Panel2.ClientSize.Width, 80);


        private void FVersionDB_Paint(object sender, PaintEventArgs e) 
            => e.Graphics.DrawRectangle(SystemPens.ControlDark, 0, 0, ClientSize.Width - 1, ClientSize.Height - 1);

        protected override void WndProc(ref Message m)
        {
            if (!ControlWindow.ProcessWndProcForSizingWindow(this, ref m))
            {
                base.WndProc(ref m);
            }
        }

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        private void Form1_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

    }
}
