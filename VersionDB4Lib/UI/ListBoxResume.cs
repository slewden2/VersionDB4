using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using VersionDB4Lib.Business;
using VersionDB4Lib.CRUD;

namespace VersionDB4Lib.UI
{
    public class ListBoxResume : ListBox
    {
        public ListBoxResume()
        {
            DoubleBuffered = true;
            ResizeRedraw = true;
            DrawMode = DrawMode.OwnerDrawFixed;
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count)
            {
                return;
            }

            var color = (e.State & DrawItemState.Selected) != 0 ? SelectedColor : BackColor;
            e.Graphics.FillRectangle(new SolidBrush(color), e.Bounds);


            if (this.Items[e.Index] is Resume resume)
            {
                var st = resume.GetResumeManualValidationCode();
                string txt = st.Libelle();
                var sz = e.Graphics.MeasureString(txt + " ", Font);
                e.Graphics.DrawString(txt, Font, new SolidBrush(st.GetColor()), e.Bounds.Left, e.Bounds.Top + ((e.Bounds.Height - sz.Height) / 2));

                e.Graphics.DrawString(resume.Description(), Font, new SolidBrush(ForeColor), e.Bounds.Left + sz.Width, e.Bounds.Top + ((e.Bounds.Height - sz.Height) / 2));
            }
            else
            { // draw default (au cas ou !)

                var txt = this.Items[e.Index].ToString();
                var sz = e.Graphics.MeasureString(txt, Font);
                e.Graphics.DrawString(txt, Font, new SolidBrush(ForeColor), e.Bounds.Left, e.Bounds.Top + ((e.Bounds.Height - sz.Height) / 2));
            }
        }
    }
}
