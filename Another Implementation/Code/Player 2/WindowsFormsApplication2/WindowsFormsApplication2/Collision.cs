using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WindowsFormsApplication2
{
    class Collision
    {
        public static bool isWhitePixel(Color pixelColor)
        {
            //Console.WriteLine((235 < check.R && check.R < 245));
            //Console.WriteLine((240 < check.G && check.G < 250));
            //Console.WriteLine((245 < check.B && check.B < 255));
            return (230 <= pixelColor.R && pixelColor.R <= 255) && (230 <= pixelColor.G && pixelColor.G <= 255) && (230 <= pixelColor.B && pixelColor.B <= 255);
        }
        public static bool canMoveUp(DynamicImage c)
        {
            Color check = GamePanel.maze.GetPixel((int)c.pos.X, (int)c.pos.Y );
            bool check1 = isWhitePixel(check);
            check = GamePanel.maze.GetPixel((int)c.pos.X + 15, (int)c.pos.Y );
            bool check2 = isWhitePixel(check);
            return check1 && check2; 
        }
        public static bool canMoveDown(DynamicImage c)
        {
            Color check = GamePanel.maze.GetPixel((int)c.pos.X, (int)c.pos.Y + 16);
            bool check1 = isWhitePixel(check);
            check = GamePanel.maze.GetPixel((int)c.pos.X + 15, (int)c.pos.Y + 16);
            bool check2 = isWhitePixel(check);
            return check1 && check2; 
        }
        public static bool canMoveRight(DynamicImage c)
        {
            Color check = GamePanel.maze.GetPixel((int)c.pos.X + 16, (int)c.pos.Y);
            bool check1 = isWhitePixel(check);
            check = GamePanel.maze.GetPixel((int)c.pos.X + 16, (int)c.pos.Y + 15);
            bool check2 = isWhitePixel(check);
            return check1 && check2; 
        }
        public static bool canMoveLeft(DynamicImage c)
        {
            Color check = GamePanel.maze.GetPixel((int)c.pos.X, (int)c.pos.Y);
            bool check1 = isWhitePixel(check);
            check = GamePanel.maze.GetPixel((int)c.pos.X, (int)c.pos.Y + 15);
            bool check2 = isWhitePixel(check);
            return check1 && check2; 
        }

        public static bool PlayerCollideWithMonster(Player player, Monster m)
        {
            return (((player.pos.X == m.pos.X) && (Math.Abs(player.pos.Y - m.pos.Y) <= 24) || ((player.pos.Y == m.pos.Y) && (Math.Abs(player.pos.X - m.pos.X) <= 24))));
        }
        public static bool MonsterCollision(DynamicImage c)
        {

            //should be modified to handle intersection between player and monster
            if (GamePanel.monsters.Count != 0)
            {
                foreach (Monster x in GamePanel.p1.monsters)
                {
                    Rectangle rectC = new Rectangle((int)c.pos.X, (int)c.pos.Y, Constants.BULLET_RAD, Constants.BULLET_RAD);
                    Rectangle rectMonster = new Rectangle((int)x.pos.X, (int)x.pos.Y, Constants.Monster_Length, Constants.Monster_Length);


                    if (rectC.IntersectsWith(rectMonster))
                    {
                        GamePanel.p1.monsters.Remove(x);
                        return true;
                    }

                }
                return false;

                
            }
            return false;
            
        }
    }
}
