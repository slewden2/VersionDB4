using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatabaseAndLogLibrary.DataBase;
using VersionDB4Lib.ForUI;

namespace VersionDB4.Control
{
    public partial class VersionScriptControl : UserControl
    {
        public VersionScriptControl() => InitializeComponent();

        public VersionScriptCounter Version
        {
            set
            {
                if (value == null)
                {
                    lblInfoScripts.Text = string.Empty;
                    lblInfoObjects.Text = string.Empty;
                }
                else
                {
                    lblInfoScripts.Text = Count(value.Count, "Aucun script dans cette version", "Un seul script dans cette version", $"{value.Count} scripts dans cette version");
                    lblInfoObjects.Text = Count(value.CountObject, string.Empty, "Un objet dans le référentiel pour cette version", $"{value.CountObject} objets sont dans le référentiel pour cette version");

                    if (value.CountObject == 1)
                    {
                        lblInfoObjects.LinkArea = new LinkArea(0, 8);
                    }
                    else if (value.CountObject > 1)
                    {
                        lblInfoObjects.LinkArea = new LinkArea(0, value.CountObject.ToString().Length + 7);
                    }


                    if (value.Count > 0)
                    {
                        lblTitleResume.Visible = true;
                        LoadAllResumeVersion(value.VersionId);
                    }
                    else
                    {
                        lblTitleResume.Visible = false;
                        lstResume.Visible = false;
                        lblNoAction.Visible = false;
                    }
                }
            }
        }

        public event EventHandler OnLinkReferential;

        private void LoadAllResumeVersion(int versionId)
        {
            lstResume.Items.Clear();
            using var cnn = new DatabaseConnection();
            foreach (var res in cnn.Query<ResumeVersionActions>(ResumeVersionActions.SQLSelect, new { VersionId = versionId }).OrderByDescending(x => x.Count))
            {
                lstResume.Items.Add(res);
            }

            bool isResume = lstResume.Items.Count > 0;
            lstResume.Visible = isResume;
            lblNoAction.Visible = !isResume;

            lstResume.Items.Add("");
            if (isResume)
            {
                foreach (var res in cnn.Query<ResumeVersionValidation>(ResumeVersionValidation.SQLSelect, new { VersionId = versionId }).OrderByDescending(x => x.Count))
                {
                    lstResume.Items.Add(res);
                }
            }
        }

        public string Count(int n, string v0, string v1, string vN) => n == 0 ? v0 : n == 1 ? v1 : vN;

        private void LstResume_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), e.Bounds);
            if (e.Index >= 0)
            {
                if (lstResume.Items[e.Index] is ResumeVersionActions resume)
                {
                    var txt = resume.Count.ToString();
                    using var ft = new Font(lstResume.Font.FontFamily, lstResume.Font.Size + 2, FontStyle.Bold);
                    var sz = e.Graphics.MeasureString(txt, ft);
                    e.Graphics.DrawString(txt, ft, new SolidBrush(this.ForeColor), new PointF(0, e.Bounds.Top + ((e.Bounds.Height - sz.Height) / 2)));


                    txt = $"x {resume}";
                    var sz2 = e.Graphics.MeasureString(txt, lstResume.Font);
                    e.Graphics.DrawString(txt, lstResume.Font, new SolidBrush(this.ForeColor), new PointF(sz.Width + 5, e.Bounds.Top + ((e.Bounds.Height - sz2.Height) / 2)));
                }
                else if (lstResume.Items[e.Index] is ResumeVersionValidation valid)
                {
                    var txt = valid.Count.ToString();
                    using var ft = new Font(lstResume.Font.FontFamily, lstResume.Font.Size, FontStyle.Bold);
                    var sz = e.Graphics.MeasureString(txt, ft);
                    e.Graphics.DrawString(txt, ft, new SolidBrush(valid.Color), new PointF(0, e.Bounds.Top + ((e.Bounds.Height - sz.Height) / 2)));


                    txt = $"x {valid}";
                    var sz2 = e.Graphics.MeasureString(txt, lstResume.Font);
                    e.Graphics.DrawString(txt, lstResume.Font, new SolidBrush(valid.Color), new PointF(sz.Width + 5, e.Bounds.Top + ((e.Bounds.Height - sz2.Height) / 2)));
                }
            }
        }

        private void LblInfoObjects_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) 
            => OnLinkReferential?.Invoke(this, new EventArgs());
     }
}
