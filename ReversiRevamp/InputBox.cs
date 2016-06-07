using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReversiRevamp
{
    class InputBox : Form
    {
        private TextBox _ibTextbox;
        private Button _ibButton;
        private ReversiForm _parent;

        private Size _getDims;

        public InputBox(Size size, ReversiForm parent)
        {
            Size = size;
            _parent = parent;
            InitializeComponent();
        }

        public Size ReadDimensions()
        {
            if (ShowDialog() == DialogResult.OK)
            {
                var temp = _ibTextbox.Text.Split(new char[] { 'X', 'x' }).Select(int.Parse).ToArray();
                _getDims = new Size(temp[0], temp[1]);
            }
            return _getDims;
        }

        private void _ibTextbox_TextChanged(object sender, EventArgs e)
       {
            TextBox tb = (TextBox)sender;
            Regex rg = new Regex(@"^[1-8][Xx][1-8]$", RegexOptions.Compiled);
            if (rg.IsMatch(tb.Text))
                _ibButton.Enabled = true;
            else
                _ibButton.Enabled = false;

        }

        private void InitializeComponent()
        {
            // Form properties
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            //Textbox
            _ibTextbox = new TextBox();
            _ibTextbox.Size = new Size(Width / 2, Height / 4);
            _ibTextbox.Location = new Point(Width / 4, Height / 4);
            _ibTextbox.TextChanged += _ibTextbox_TextChanged;


            //Button
            _ibButton = new Button();
            _ibButton.Size = new Size(100, 20);
            _ibButton.Text = "Accept";
            _ibButton.Location = new Point(Width / 4, Height / 2);
            _ibButton.DialogResult = DialogResult.OK;
            _ibButton.Enabled = false;

            Controls.Add(_ibTextbox);
            Controls.Add(_ibButton);
        }
    }
}
