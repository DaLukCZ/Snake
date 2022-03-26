using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snake
{
    internal class Pixel : Label
    {

        public Pixel(int x, int y)
        {
            Location = new Point(x, y);
            Size = new Size(20,20);
            BackColor = Color.Green;
            Enabled = false;
        }
    }
}
