using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VersionDB4Lib.UI
{
    public class ListBox : System.Windows.Forms.ListBox
    {
        public ListBox()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            DrawMode = DrawMode.OwnerDrawFixed;
        }

        [DefaultValue(typeof(Color), "0xFF92CE0")]
        public Color SelectedColor { get; set; } = Color.FromArgb(146, 192, 224);


        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count)
            {
                return;
            }

            var color = (e.State & DrawItemState.Selected) != 0 ? SelectedColor : BackColor;
            e.Graphics.FillRectangle(new SolidBrush(color), e.Bounds);

            var txt = this.Items[e.Index].ToString();
            var sz = e.Graphics.MeasureString(txt, Font);
            e.Graphics.DrawString(txt, Font, new SolidBrush(ForeColor), e.Bounds.Left, e.Bounds.Top + ((e.Bounds.Height - sz.Height) / 2));
        }
    }
}
