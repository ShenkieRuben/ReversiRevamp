using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReversiRevamp
{
    public sealed partial class ReversiForm : Form
    {
        private Size _dimensions;
        private InputBox _ib;
        private readonly List<Rectangle> rectangles;
        private readonly List<Tile> tiles;



        public ReversiForm()
        {
            InitializeComponent();
            reversiBox.Invalidate();
            rectangles = new List<Rectangle>();
            tiles = new List<Tile>();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _ib = new InputBox(new Size(Width / 4, Height / 4), this);
            _dimensions = _ib.ReadDimensions();
            rectangles.Clear();
            tiles.Clear();
            reversiBox.Refresh();
        }

        private void ReversiPaintField(object sender, PaintEventArgs e)
        {
            rectangles.Clear();
            reversiBox.Height = ClientSize.Height;
            reversiBox.Width = ClientSize.Width;

            Graphics g = e.Graphics;

            if (!_dimensions.IsEmpty)
            {
                // (Re)drawing the field.
                int horizontalLines = _dimensions.Height;
                int verticalLines = _dimensions.Width;
                int cellSizeHeight = reversiBox.Height / horizontalLines;
                int cellSizeWidth = reversiBox.Width / verticalLines;

                for (int x = 0; x <= verticalLines; x++)
                {
                    g.DrawLine(Pens.Black, x * cellSizeWidth, 0, x * cellSizeWidth, horizontalLines * cellSizeHeight);
                }

                for (int y = 0; y <= horizontalLines; y++)
                {
                    g.DrawLine(Pens.Black, 0, y * cellSizeHeight, verticalLines * cellSizeWidth, y * cellSizeHeight);
                }

                // Inferring the rectangles of the field.
                for (int y = 0; y < horizontalLines * cellSizeHeight; y += cellSizeHeight)
                {
                    for (int x = 0; x < verticalLines * cellSizeWidth; x += cellSizeWidth)
                    {
                        rectangles.Add(new Rectangle(new Point(x, y), new Size(cellSizeWidth, cellSizeHeight)));
                    }
                }

                foreach (Tile rectangle in tiles)
                {
                    g.FillEllipse(Brushes.Red, rectangles[rectangle.FieldNumber]);
                }
            }
        }

        private void ReversiForm_Resize(object sender, EventArgs e)
        {
            reversiBox.Invalidate();
        }

        private void reversiBox_MouseClick(object sender, MouseEventArgs e)
        {
            Rectangle rect = FindCorrespondingRectangle(e.X, e.Y);
            if (rect != Rectangle.Empty)
            {
                int fieldNumber = FindCorrespondingFieldNumber(rect);
                tiles.Add(new Tile(fieldNumber, rect));
            }

            reversiBox.Invalidate();
        }

        private Rectangle FindCorrespondingRectangle(int mouseX, int mouseY)
        {
            Rectangle rect = Rectangle.Empty;
            if (rectangles.Count != 0)
            {
                rect = rectangles.Find(o => mouseX > o.X && mouseX < o.X + o.Width &&
                                            mouseY > o.Y && mouseY < o.Y + o.Height);
            }
            return rect;
        }

        private int FindCorrespondingFieldNumber(Rectangle rect)
        {
            return rectangles.FindIndex(o => o.Equals(rect));
        }
    }
}
