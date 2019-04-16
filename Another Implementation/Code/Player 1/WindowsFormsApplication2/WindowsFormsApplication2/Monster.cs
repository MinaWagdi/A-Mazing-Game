using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; 

namespace WindowsFormsApplication2
{
    [Serializable()]
    class Monster:DynamicImage
    {
        String direction;
        int touchedTheWallHorizontal;
        int touchedTheWallVertical;
        //Random ran; 
        public Monster(float x , float y , String imagePath)
            : base(x , y , imagePath) 
        {
            
        }
        


        //public void move()
        //{
        //    String possibleDirections = this.getPossibleDirections();
        //    Random random = new Random();
            
        //    switch (possibleDirections)
        //        {
        //            case "up":
        //                this.dyU = 1;
        //                break;
        //            case "down":
        //                this.dyD = 1;
        //                break;
        //            case "right":
        //                this.dxR = 1;
        //                break;
        //            case "left":
        //                this.dxL = 1;
        //                break;
        //        }
            
            
        //    pos.X += dxR;
        //    pos.Y += dyU;
        //    pos.Y += dyD;

        //}
        public void CalcHorizontalWallTouch()
        {
            if (!Collision.isWhitePixel(GamePanel.maze.GetPixel((int)this.pos.X+7, (int)this.pos.Y)))
                touchedTheWallHorizontal++;
        }
        public void HorizontalMove()
        {
            CalcHorizontalWallTouch();
            if (touchedTheWallHorizontal % 2 == 0)
                dxR = 5;
            else
                dxR = -5;
            pos.X += dxR;
        }
        
        public void CalcVerticalWallTouch()
        {
            if (!Collision.isWhitePixel(GamePanel.maze.GetPixel((int)this.pos.X, (int)this.pos.Y)))
                touchedTheWallVertical++;
        }
        public void VerticalMove()
        {
            CalcVerticalWallTouch();
            if (touchedTheWallVertical % 2 == 0)
                dyU = 5;
            else
                dyU = -5;
            pos.Y += dyU;

        }
        
        public override void draw(Graphics g)
        {
            g.DrawImage(img, pos);
        }
        protected String getPossibleDirections()
        {
            Random ran = new Random();
            String possibleDirections=this.direction;
            if (Collision.canMoveDown(this) && Collision.canMoveUp(this)) 
                 possibleDirections="up";
            if (Collision.canMoveLeft(this) && Collision.canMoveRight(this)) 
                 possibleDirections="right";
            //else if (Collision.canMoveRight(this))
            //    return possibleDirections = "right";
            //else if (Collision.canMoveLeft(this))
            //    return possibleDirections = "left";
            //else if (Collision.canMoveUp(this))
            //    return possibleDirections = "up";
            //else 
            //    return possibleDirections = "down";
            //if (Collision.canMoveRight(this) && dxL==0) possibleDirections="right";
            //if (Collision.canMoveUp(this) && dyD==0) possibleDirections="up";
            return possibleDirections;
        }
        

    }
}
    