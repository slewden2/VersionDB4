using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VersionDB4Lib.UI
{
    public class TreeView : System.Windows.Forms.TreeView
    {
        public TreeView()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            BackColor = Color.FromArgb(247, 247, 247);
            BorderStyle = System.Windows.Forms.BorderStyle.None;
            DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            FullRowSelect = true;
            HideSelection = false;
            HotTracking = true;
            ItemHeight = 24;
            ShowLines = false;
            ShowRootLines = false;
        }

        protected override void OnDrawNode(DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = false;
            if (e.Node != null && e.Bounds.Top >= 0 && e.Bounds.Height > 0)
            {
                int children = (e.Node.Tag != null && e.Node.Tag is ICounter counter) ? counter.Count : 0;
                bool hasChildren = children > 0;
                
                int indent = Math.Max(0, (e.Node.Level - (ShowRootLines ? 0 : 1))) * Indent;

                var color = (e.State & TreeNodeStates.Selected) != 0 ? Color.FromArgb(225, 225, 225) : (e.State & TreeNodeStates.Hot) != 0 ? Color.FromArgb(230, 242, 250) : BackColor;
                e.Graphics.FillRectangle(new SolidBrush(color), e.Bounds);

                var picto = e.Node.IsExpanded ? "" : (e.State & TreeNodeStates.Hot) != 0 ? "" : "";
                using var ft = new Font("Segoe MDL2 Assets", Font.Size - (e.Node.Level == 0 ? 0 : 2));
                var sz = e.Graphics.MeasureString(picto, ft);

                // +/-
                if (e.Node.Nodes.Count > 0)
                {
                    e.Graphics.DrawString(picto, ft, new SolidBrush(Color.FromArgb(120, 120, 120)), new PointF(e.Bounds.Left + indent, e.Bounds.Top + ((e.Bounds.Height - sz.Height) / 2)));
                }

                // texte
                var txt = e.Node.Text + " ";
                using var ft2 = new Font(Font, (e.State & TreeNodeStates.Selected) != 0 ? FontStyle.Bold : FontStyle.Regular);
                var sz2 = e.Graphics.MeasureString(txt, ft2);
                e.Graphics.DrawString(txt, ft2, new SolidBrush(ForeColor), new PointF(e.Bounds.Left + indent + sz.Width, e.Bounds.Top + ((e.Bounds.Height - sz2.Height) / 2)));


                if (hasChildren)
                {  // compteur
                    var cpt = children.ToString();
                    using var ft3 = new Font(Font, FontStyle.Bold);
                    e.Graphics.DrawString(cpt, ft3, new SolidBrush(Color.FromArgb(97, 146, 198)), new PointF(e.Bounds.Left + indent + sz.Width + sz2.Width, e.Bounds.Top + ((e.Bounds.Height - sz2.Height) / 2)));
                }
            }
        }

    }
}
