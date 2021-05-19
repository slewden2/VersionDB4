using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VersionDB4Lib.UI
{
    public class ComboBox : System.Windows.Forms.ComboBox
    {
        public ComboBox()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            DrawMode = DrawMode.OwnerDrawFixed;
        }

        [DefaultValue(typeof(Color), "0xFF92CE0")]
        public Color SelectedColor { get; set; } = Color.FromArgb(146, 192, 224);


        protected override void OnDrawItem(DrawItemEventArgs e)
        {

            if (e.Index >= 0)
            {
                var color = ((e.State & DrawItemState.Selected) == DrawItemState.Selected) ? SelectedColor : BackColor;
                using var br = new SolidBrush(color);
                e.Graphics.FillRectangle(br, e.Bounds);

                // texte
                var txt = this.Items[e.Index].ToString() + " ";
                var sz = e.Graphics.MeasureString(txt, Font);
                e.Graphics.DrawString(txt, Font, new SolidBrush(ForeColor), new PointF(e.Bounds.Left, e.Bounds.Top + ((e.Bounds.Height - sz.Height) / 2)));


                if (this.Items[e.Index] is ICounter counter && counter.Count > 0)
                {  // compteur
                    var cpt = $"({counter.Count})";
                    using var ft3 = new Font(Font.FontFamily, Font.Size - 2);
                    var sz2 = e.Graphics.MeasureString(txt, ft3);
                    e.Graphics.DrawString(cpt, ft3, new SolidBrush(Color.FromArgb(97, 146, 198)), new PointF(e.Bounds.Left + sz.Width, e.Bounds.Top + ((e.Bounds.Height - sz2.Height) / 2)));
                }
            }
        }
    }
}
