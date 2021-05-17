using System.Drawing;
using System.Windows.Forms;

namespace VersionDB4
{
    partial class FVersionDB
    {
        private void SplitContainer1_Paint(object sender, PaintEventArgs e)
            => e.Graphics.DrawLine(new Pen(Color.FromArgb(212, 212, 212)), splitContainer1.SplitterDistance, 0, splitContainer1.SplitterDistance, splitContainer1.ClientSize.Height);

        private void SplitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
            => e.Graphics.DrawLine(new Pen(Color.FromArgb(212, 212, 212)), 0, 80, splitContainer1.Panel2.ClientSize.Width, 80);
    }
}
