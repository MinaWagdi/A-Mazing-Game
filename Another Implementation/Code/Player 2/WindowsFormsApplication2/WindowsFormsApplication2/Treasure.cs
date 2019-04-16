using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication2
{
    class Treasure
    {
        public int x, y;
        Image img;
        public Treasure(int x, int y)
        {
            this.x = x;
            this.y = y;
            this.img = Image.FromFile(@"C:\Resources\treasure.jpg");
        }
        public void draw(Graphics g)
        {
            g.DrawImage(this.img, this.x, this.y);
        }
    }
}
