using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; 

namespace WindowsFormsApplication2
{
    [Serializable()]
    class GameState
    {
        public PointF playerPos;
        public List<PointF> monsterPos;
        public List<PointF> bulletsPos;
        public GameState()
        {
            playerPos = GamePanel.p1.pos;

            //bullets Informations 
            bulletsPos = new List<PointF>();
            if (GamePanel.p1.bullets.Count != 0)
            {
                foreach (Bullet b in GamePanel.p1.bullets)
                {
                    bulletsPos.Add(b.pos); 
                }
            }

        }

         
    }
}
