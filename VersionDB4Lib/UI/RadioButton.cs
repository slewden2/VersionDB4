using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace VersionDB4Lib.UI
{
    public class RadioButton : Label
    {
        private bool isDown = false;
        private bool isHover = false;
        private bool isChecked = false;
        private bool isEnabled = true;

        public RadioButton()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            AutoSize = false;
            Width = 40;
            Height = 40;
        }

        [DefaultValue(typeof(Color), "0xFF0074C6")]
        public Color CheckedColor { get; set; } = Color.FromArgb(0, 114, 198);

        [DefaultValue(typeof(Color), "0xFF92CE0")]
        public Color DownColor { get; set; } = Color.FromArgb(146, 192, 224);

        [DefaultValue(typeof(Color), "0xFFCDE6F7")]
        public Color HoverColor { get; set; } = Color.FromArgb(205, 230, 247);

        public bool Checked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                UnselAssociedRadioButtons();
                Invalidate();
            }
        }

        public new bool Enabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                Invalidate();
            }
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            e.Graphics.CompositingQuality = CompositingQuality.HighQuality;
            e.Graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Fond 
            Color color = isEnabled && isDown ? DownColor : isEnabled && isHover ? HoverColor : BackColor;
            using var br = new SolidBrush(color);
            e.Graphics.FillRectangle(br, ClientRectangle);

            // text 
            var sz = e.Graphics.MeasureString(Text, Font);
            var pt2 = new PointF((ClientSize.Width - sz.Width) / 2, (ClientSize.Height - sz.Height) / 2);
            using var brTxt = new SolidBrush(isChecked ? CheckedColor : ForeColor);
            e.Graphics.DrawString(Text, Font, brTxt, pt2);

            if (isChecked)
            {
                using var p = new Pen(CheckedColor, 6);
                e.Graphics.DrawLine(p, 5, ClientSize.Height - 4, ClientSize.Width - 5, ClientSize.Height - 4);
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            isDown = false;
            isHover = false;
            Invalidate();
            base.OnMouseLeave(e);
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            isHover = true;
            Invalidate();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            isDown = true;
            Invalidate();
            base.OnMouseDown(e);
        }

        protected override void OnClick(EventArgs e)
        {
            if (!isEnabled)
            {
                return;
            }

            isChecked = true;
            UnselAssociedRadioButtons();
            Invalidate();

            base.OnClick(e);
        }

        private void UnselAssociedRadioButtons()
        {
            if (!isChecked)
            {
                return;
            }

            foreach (var control in this.Parent.Controls)
            {
                if (control != this)
                {
                    var type = control.GetType();
                    var pr = type.GetProperty("Checked");
                    if (pr != null && pr.PropertyType == typeof(bool) && pr.CanWrite)
                    {
                        pr.SetValue(control, false);
                    }
                }
            }
        }
    }
}
