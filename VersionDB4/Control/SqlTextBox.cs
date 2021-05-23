using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using VersionDB4Lib.UI;

namespace VersionDB4.Control
{
    public partial class SqlTextBox : UserControl
    {
        private bool rowAtBegin = true;
        private bool rowAtEnd = true;
        private bool columnAtBegin = true;
        private bool columnAtEnd = true;

        public SqlTextBox()
        {
            InitializeComponent();
            lblPanleInfo.Text = string.Empty;
            ChkWrap.Checked = richTextBox1.WordWrap;
        }

        public event EventHandler OnChange;

        #region Properties
        [Category("Look")]
        public override string Text
        {
            get => richTextBox1.Text;
            set
            {
                richTextBox1.Text = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    SqlColorizer.Colorise(richTextBox1, function: () => richTextBox1.BackColor = base.BackColor); 
                }
            }
        }

        [Category("Look")]
        public bool WordWrap
        {
            get => ChkWrap.Checked;
            set => ChkWrap.Checked = value;
        }

        [Category("Look"), DefaultValue(true)]
        public bool ReadOnly
        {
            get => richTextBox1.ReadOnly;
            set
            {
                richTextBox1.ReadOnly = value;
                richTextBox1.BackColor = SystemColors.Window;
                ////if (!string.IsNullOrWhiteSpace(richTextBox1.Text))
                ////{
                ////    SqlColorizer.Colorise(richTextBox1); //, function: () => MessageBox.Show("fini2"));
                ////}
            }
        }
        [Category("Look")]
        public override ContextMenuStrip ContextMenuStrip
        {
            get => richTextBox1.ContextMenuStrip;
            set => richTextBox1.ContextMenuStrip = value;
        }

        public override Color BackColor
        {
            get => base.BackColor;
            set
            {
                base.BackColor = value;
                richTextBox1.BackColor = value;
            }
        }

        public void Select(int start, int length)
            => richTextBox1.Select(start, length);
        #endregion

        #region  Events interne
        private void RichTextBox1_Enter(object sender, EventArgs e) => UpdatePosition();

        private void RichTextBox1_Leave(object sender, EventArgs e) => ClearPosition();

        private void RichTextBox1_KeyUp(object sender, KeyEventArgs e) => UpdatePosition();

        private void RichTextBox1_MouseUp(object sender, MouseEventArgs e) => UpdatePosition();

        private void CheckBox1_CheckedChanged(object sender, EventArgs e) => richTextBox1.WordWrap = ChkWrap.Checked;

        private void RichTextBox1_TextChanged(object sender, EventArgs e) => OnChange?.Invoke(this, new EventArgs());
        #endregion

        private void UpdatePosition()
        {
            var n = richTextBox1.SelectionStart;
            var row = richTextBox1.GetLineFromCharIndex(n);
            var col = n - richTextBox1.GetFirstCharIndexFromLine(row);

            if (string.IsNullOrWhiteSpace(richTextBox1.Text))
            {
                columnAtBegin = true;
                columnAtEnd = true;
                rowAtBegin = true;
                rowAtEnd = true;
            }
            else
            {
                columnAtBegin = col == 0;
                columnAtEnd = col >= (richTextBox1?.Lines[row]?.Length ?? 0);
                rowAtBegin = row == 0;
                rowAtEnd = row >= (richTextBox1?.Lines?.Length ?? 0) - 1;
            }

            lblPanleInfo.Text = $" Ln {row + 1}  Col {col + 1}";
        }

        private void ClearPosition() => lblPanleInfo.Text = string.Empty;

        private void RichTextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (columnAtBegin && e.KeyCode == Keys.Home)
            { // remove bing !
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (columnAtEnd && e.KeyCode == Keys.End)
            { // remove bing !
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (rowAtBegin && (e.KeyCode == Keys.Up || e.KeyCode == Keys.PageUp))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (rowAtEnd && (e.KeyCode == Keys.Down || e.KeyCode == Keys.PageDown))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (richTextBox1.SelectionStart == 0 && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Left || e.KeyCode == Keys.PageUp || e.KeyCode == Keys.Back))
            {  // remove bing !
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (richTextBox1.SelectionStart >= richTextBox1.Text.Length && (e.KeyCode == Keys.Down || e.KeyCode == Keys.Right || e.KeyCode == Keys.PageDown))
            {  // remove bing !
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
            else if (richTextBox1.ReadOnly && (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Back))
            {
                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }

        private void RichTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (richTextBox1.ReadOnly && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
