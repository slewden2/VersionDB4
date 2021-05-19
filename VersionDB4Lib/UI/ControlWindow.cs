using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace VersionDB4Lib.UI
{   
    /// <summary>
     /// Desssine les boutons min/max et close en haut à gauche de la fenêtre
     /// + gère le resize de la fenêtre
     /// Le control peut être positionné n'importe ou : il se recale seul au bon endroit
     /// La fenêtre va se voir affecter les properties : 
     ///   FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
     ///   Padding = new System.Windows.Forms.Padding(1); 
     /// La fenêtre doit avoir :
     ///   frm.ResizeRedraw = true;
     ///   protected override void WndProc(ref Message m)
     ///    {
     ///        if (!ControlWindow.ProcessWndProcForSizingWindow(this, ref m))
     ///        {
     ///            base.WndProc(ref m);
     ///        }
     ///    }
     /// </summary>
    public class ControlWindow : Label
    {
        private const string PICTOS = "";  // minimized, Maximize, Maximized, Close
        private const int BTNWIDTH = 45;
        private const int BTNHEIGHT = 31;

        private const int BORDERGRIPSIZE = 5;
        private bool showMinMaxButtons = true;

        FormWindowState windowState = FormWindowState.Normal;
        private int btnIndex = -1;

        public ControlWindow()
        {
            Font = new Font("Segoe MDL2 Assets", 7.75F, FontStyle.Regular, GraphicsUnit.Point);
            ForeColor = SystemColors.ControlDark;
            AutoSize = true;
            ResizeMe();
            Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
        }

        [DefaultValue(typeof(Color), "0xFFE3ECFA")]
        public Color HotLightColor { get; set; } = Color.FromArgb(227, 236, 250);

        [DefaultValue(true)]
        public bool MinMaxButtonVisible
        {
            get => showMinMaxButtons;
            set
            {
                if (showMinMaxButtons != value)
                {
                    showMinMaxButtons = value;
                    ResizeMe();
                    this.Left += BTNWIDTH * (value ? -2 : 2);
                    btnIndex = -1;
                    Invalidate();
                }
            }
        }

        public new bool AutoSize
        {
            get => false;
            set { }
        }

        public static bool ProcessWndProcForSizingWindow(Control frm, ref Message m)
        {
            if (m.Msg != 0x84)
            { // Trap only WM_NCHITTEST
                return false;
            }

            // Trap WM_NCHITTEST
            Point pos = new Point(m.LParam.ToInt32());
            pos = frm.PointToClient(pos);

            if (pos.Y < BTNHEIGHT && pos.Y > BORDERGRIPSIZE)
            {
                m.Result = (IntPtr)2;  // HTCAPTION
                return true;
            }


            if (pos.X < BORDERGRIPSIZE && pos.Y < BORDERGRIPSIZE)
            {
                m.Result = (IntPtr)13; // HTTOPLEFT
                return true;
            }

            if (pos.X >= frm.ClientSize.Width - BORDERGRIPSIZE && pos.Y < BORDERGRIPSIZE)
            {
                m.Result = (IntPtr)14; // HTTOPRIGHT
                return true;
            }

            if (pos.X < BORDERGRIPSIZE && pos.Y >= frm.ClientSize.Height - BORDERGRIPSIZE)
            {
                m.Result = (IntPtr)16; // HTBOTTOMLEFT
                return true;
            }

            if (pos.X >= frm.ClientSize.Width - BORDERGRIPSIZE && pos.Y >= frm.ClientSize.Height - BORDERGRIPSIZE)
            {
                m.Result = (IntPtr)17; // HTBOTTOMRIGHT
                return true;
            }

            if (pos.X < BORDERGRIPSIZE)
            {
                m.Result = (IntPtr)10; // HTLEFT
                return true;
            }

            if (pos.X >= frm.ClientSize.Width - BORDERGRIPSIZE)
            {
                m.Result = (IntPtr)11; // HTRIGHT
                return true;
            }

            if (pos.Y < BORDERGRIPSIZE)
            {
                m.Result = (IntPtr)12; // HTTOP
                return true;
            }

            if (pos.Y >= frm.ClientSize.Height - BORDERGRIPSIZE)
            {
                m.Result = (IntPtr)15; // HTBOTTOM
                return true;
            }

            if (pos.Y < BTNHEIGHT)
            {
                m.Result = (IntPtr)2;  // HTCAPTION
                return true;
            }

            return false;
        }

        protected override void OnParentChanged(EventArgs e)
        {
            var frm = FindForm();
            if (frm != null)
            {
                if (frm.Padding == null || frm.Padding.Left < 1 || frm.Padding.Top < 1 || frm.Padding.Right < 1 || frm.Padding.Bottom < 1)
                { // to Make Resize arrows works !
                    frm.Padding = new Padding(1);
                }

                if (frm.FormBorderStyle != FormBorderStyle.None)
                {
                    frm.FormBorderStyle = FormBorderStyle.None;
                }

                frm.SizeChanged += Frm_SizeChanged;
                this.Location = new Point(frm.Width - this.Width - 1, 1);
                this.BringToFront();
            }
            else
            {
                windowState = FormWindowState.Normal;
            }

            base.OnParentChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int w = 0;

            using var brback = new SolidBrush(BackColor);
            e.Graphics.FillRectangle(brback, new Rectangle(ClientRectangle.Left, ClientRectangle.Top, BTNWIDTH * 3, BTNHEIGHT));


            if (showMinMaxButtons)
            {
                w = 2 * BTNWIDTH;
                if (btnIndex == 0)
                {
                    using var br = new SolidBrush(HotLightColor);
                    e.Graphics.FillRectangle(br, new Rectangle(ClientRectangle.Left, ClientRectangle.Top, BTNWIDTH, BTNHEIGHT));
                }
                else if (btnIndex == 1 || btnIndex == 2)
                {
                    using var br = new SolidBrush(HotLightColor);
                    e.Graphics.FillRectangle(br, new Rectangle(ClientRectangle.Left + BTNWIDTH, ClientRectangle.Top, BTNWIDTH, BTNHEIGHT));
                }
            }

            if (btnIndex == 3)
            {
                using var br = new SolidBrush(Color.FromArgb(232, 17, 35));
                e.Graphics.FillRectangle(br, new Rectangle(ClientRectangle.Left + w, ClientRectangle.Top, BTNWIDTH, BTNHEIGHT));
            }

            var sz = e.Graphics.MeasureString(PICTOS[0].ToString(), Font);

            if (showMinMaxButtons)
            {
                // Min 
                using var brTxtMin = new SolidBrush(btnIndex == 1 ? ForeColor : SystemColors.ControlDarkDark);
                e.Graphics.DrawString(PICTOS[0].ToString(), Font, brTxtMin, new PointF(ClientRectangle.Left + ((BTNWIDTH - sz.Width) / 2), ClientRectangle.Top + ((BTNHEIGHT - sz.Height) / 2)));
                // Max 
                using var brTxtMax = new SolidBrush((btnIndex == 2 || btnIndex == 3) ? ForeColor : SystemColors.ControlDarkDark);
                e.Graphics.DrawString(PICTOS[windowState == FormWindowState.Maximized ? 2 : 1].ToString(), Font, brTxtMax, new PointF(ClientRectangle.Left + BTNWIDTH + ((BTNWIDTH - sz.Width) / 2), ClientRectangle.Top + ((BTNHEIGHT - sz.Height) / 2)));
            }

            // close
            using var brTxtClose = new SolidBrush(btnIndex == 3 ? Color.White : ForeColor);
            e.Graphics.DrawString(PICTOS[3].ToString(), Font, brTxtClose, new PointF(ClientRectangle.Left + w + ((BTNWIDTH - sz.Width) / 2), ClientRectangle.Top + ((BTNHEIGHT - sz.Height) / 2)));
        }

        protected override void OnClick(EventArgs e)
        {
            var frm = this.FindForm();
            if (frm != null)
            {
                if (btnIndex == 0)
                {   // min bt
                    frm.WindowState = FormWindowState.Minimized;
                    windowState = FormWindowState.Minimized;
                }
                else if (btnIndex == 1)
                {   // max bt when normal
                    frm.WindowState = FormWindowState.Maximized;
                    windowState = FormWindowState.Maximized;
                }
                else if (btnIndex == 2)
                {   // max bt when maximized
                    frm.WindowState = FormWindowState.Normal;
                    windowState = FormWindowState.Normal;
                }
                else
                {   // close bt
                    frm.Close();
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            bool change;
            if (showMinMaxButtons)
            {
                if (e.X < BTNWIDTH)
                {   // min bt ou close
                    change = btnIndex != 0;
                    btnIndex = 0;
                }
                else if (e.X < 2 * BTNWIDTH)
                {   // max bt
                    var idx = windowState == FormWindowState.Maximized ? 2 : 1;
                    change = btnIndex != idx;
                    btnIndex = idx;
                }
                else
                {   // close bt
                    change = btnIndex != 3;
                    btnIndex = 3;
                }
            }
            else
            { // toujours le bouton close
                change = btnIndex != 3;
                btnIndex = 3;
            }

            if (change)
            {
                Invalidate();
            }

            base.OnMouseMove(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            if (btnIndex > -1)
            {
                btnIndex = -1;
                Invalidate();
            }

            base.OnMouseLeave(e);
        }


        private void Frm_SizeChanged(object sender, EventArgs e)
        {
            if (sender is Form frm)
            {
                if (frm.WindowState != windowState)
                {
                    windowState = frm.WindowState;
                    Invalidate();
                }
            }
        }

        private void ResizeMe()
        {
            int nbBtn = showMinMaxButtons ? 3 : 1;
            Size = new Size(BTNWIDTH * nbBtn, BTNHEIGHT);
            MinimumSize = new Size(BTNWIDTH * nbBtn, BTNHEIGHT);
            MaximumSize = new Size(BTNWIDTH * nbBtn, BTNHEIGHT);
        }
    }
}
