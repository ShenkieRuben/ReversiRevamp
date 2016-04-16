using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReversiRevamp
{
    class InputBox : ReversiForm
    {
        private ReversiForm _parent;
        public InputBox(Size size, ReversiForm parent)
        {
            Size = size;
            _parent = parent;
            StartPosition = FormStartPosition.CenterParent;
            showInputBox();
        }

        private void showInputBox()
        {

            ShowDialog();
        }
    }
}
