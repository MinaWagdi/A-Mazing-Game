using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
 


namespace WindowsFormsApplication2
{
    class Player : DynamicImage
    {
        public List<Bullet> bullets;
        public int score;
        public int playerHealth=Constants.PLAYER_HEALTH;
        public List<Monster> monsters;
        //String dir; 

        public Player(float x, float y, String img) 
            :base( x , y , img) 
        {
            bullets = new List<Bullet>();
            monsters = new List<Monster>();
        }
        
        // me7taga ta3dil akiid !!!!
        public void move()
        {
            // w kolo if(isVisible()) 
            //if(Collision.canMoveRight(this))
            //    pos.X += dx;
            //if (Collision.canMoveLeft(this))
            //    pos.X -= dx;
            //if (Collision.canMoveUp(this))
            //    pos.Y -= dy;
            //if (Collision.canMoveDown(this))
            //    pos.Y += dy;
            try
            {
                if (Collision.canMoveRight(this))
                    pos.X += dxR;
                if (Collision.canMoveLeft(this))
                    pos.X += dxL;
                if (Collision.canMoveUp(this))
                    pos.Y += dyU;
                if (Collision.canMoveDown(this))
                    pos.Y += dyD;
            }
            catch (ArgumentOutOfRangeException e)
            { pos.Y += 5; }
            
        }
        public override void draw(Graphics g)
        {
            g.DrawImage(img, pos); 
        }
        // me7tag a3raf el path beta3 image el bullet 
        public void fire(String direction)
        {
            Bullet b = new Bullet(pos.X, pos.Y, @"C:\Resources\bullett.png" , direction );
            bullets.Add(b); 
        }


    }
}
