using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; 

namespace WindowsFormsApplication2
{
    [Serializable()]
    class Bullet:DynamicImage
    {

        public Bullet(float x, float y, String imgPath , String dir)
            : base(x, y, imgPath) 
        {
            if (dir == "UP")
            {
                dyU = -Constants.BULLET_SPEED;
                //if (Collision.isWhitePixel(GamePanel.maze.GetPixel((int)this.pos.X + 4, (int)this.pos.Y-2))) // men nos el bullet (ka average)
                //this.setVisible(false);
            }
            if (dir == "DOWN")
            {
                dyU = Constants.BULLET_SPEED;
                //if (Collision.isWhitePixel(GamePanel.maze.GetPixel((int)this.pos.X + 4, (int)this.pos.Y + 10)))
                //    this.setVisible(false);
            }
            if (dir == "LEFT")
            {
                dxL = -Constants.BULLET_SPEED;
                //if (Collision.isWhitePixel(GamePanel.maze.GetPixel((int)this.pos.X-2, (int)this.pos.Y+4))) 
                //this.setVisible(false);
            }
            if (dir == "RIGHT")
            {
                dxR = Constants.BULLET_SPEED;
                //if (Collision.isWhitePixel(GamePanel.maze.GetPixel((int)this.pos.X+10, (int)this.pos.Y + 4))) 
                //this.setVisible(false);
            }
        }
        public Bullet(float x, float y, String imgPath)
            : base(x, y, imgPath)
        { }

        public void move()
        {
            //hatemshi 3ala 7asab etegah el la3ib 
            // w kolo if(isVisible()) 
            pos.X += dxL;
            pos.X += dxR;
            pos.Y += dyU;
            pos.Y += dyD;
            

        }
        public override void draw(Graphics g)
        {
            g.DrawImage(img, pos);
        }

    }
}
