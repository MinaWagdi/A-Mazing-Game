using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net;
using System.Threading;


namespace WindowsFormsApplication2
{
    class GamePanel : Panel
    {
        public static Player p1;
        public static Player p2;
        public static List<Monster> monsters; // Monster of all the Game !!! 
        Random ran;
        private System.Windows.Forms.Timer timer;
        public static Bitmap maze;
        //Graphics g;
        public static UdpClient PlayerPosClient;
        public static UdpClient BulletsPosClient;
        public static IPEndPoint PlayerPosIppoint;
        public static IPEndPoint BulletsPosIppoint;
        public static IPEndPoint MonsterPosIppoint;
        public static UdpClient MonsterPosClient;
        GameState StateToSendPlayerPos;
        GameState StateRecievePlayerPos;
        GameState StateToSendBulletsPos;
        GameState StateRecieveBulletsPos;
        Treasure treasure; 
        static GamePanel()
        {
            maze = new Bitmap(Image.FromFile(@"C:\Resources\maze.jpg"));


        }
        public GamePanel()
        {
            //Console.WriteLine("hi from onPaint
            this.DoubleBuffered = true;
            this.Dock = DockStyle.Fill;
            this.BackgroundImage = maze;
        }
        public void StartGame()
        {
            PlayerPosClient = new UdpClient(7087);
            BulletsPosClient = new UdpClient(7555);
            PlayerPosIppoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7000);
            BulletsPosIppoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 7777);
            MonsterPosIppoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8000);
            MonsterPosClient = new UdpClient(8088);
            monsters = new List<Monster>();
            ran = new Random();
            initPlayers();
            //initMonsters();
            treasure = new Treasure(Constants.PNL_WIDTH / 2, Constants.PNL_HEIGHT / 2); 
            StateToSendPlayerPos = new GameState();
            StateToSendBulletsPos = new GameState();
            StateRecieveBulletsPos = new GameState();
            StateRecievePlayerPos = new GameState();
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 20;
            timer.Tick += timer_Tick;
            timer.Start();

        }
        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                checkTreasure();
                //sendUDPMonsterList(monsters, MonsterPosClient, MonsterPosIppoint);
                checkCollision();
                p1.move();
                sendUDPPointF(p1.pos, PlayerPosClient, PlayerPosIppoint);
                if (p1.bullets.Count != 0)
                {
                    foreach (Bullet b in p1.bullets)
                    {
                        b.move();
                    }
                }
                sendUDPBulletsList(p1.bullets, BulletsPosClient, BulletsPosIppoint);
                try
                {
                    if (p1.monsters.Count != 0)
                    {
                        for (int i = 0; i < 11; i++)
                        {
                            p1.monsters.ElementAt<Monster>(i).HorizontalMove();
                        }
                        for (int i = 11; i < 14; i++)
                        {
                            p1.monsters.ElementAt<Monster>(i).VerticalMove();
                        }


                    }
                    sendUDPMonsterList(p1.monsters, MonsterPosClient, MonsterPosIppoint);
                }
                catch (Exception eee)
                {
                    String msg = eee.ToString();
                    Console.WriteLine(msg);
                }
            }
            catch(Exception eee) 
            {
                Console.WriteLine("EXCEPTION");
                //String msg = eee.ToString();
                //Console.WriteLine(msg);
 
                timer.Stop();
                timer.Start();
            }

            this.Invalidate();


        }
        public void initPlayers()
        {
            Console.WriteLine("hi from initPlayers");
            p1 = new Player(100, 28, @"C:\Resources\playerr.png");
            p2 = new Player(150, 28, @"C:\Resources\playerr.png");
            new Thread(stateListener).Start();

        }

        public void stateListener()
        {
            while (true)
            {
                try
                {
                    //StateRecievePlayerPos = getUDPGameState(PlayerPosClient, ref PlayerPosIppoint);
                    //p2.pos = StateRecievePlayerPos.playerPos;
                    p2.pos = getUDPointF(PlayerPosClient, ref PlayerPosIppoint);
                    //StateRecieveBulletsPos = getUDPGameState(BulletsPosClient, ref BulletsPosIppoint);
                    p2.bullets = getUDBulletsList(BulletsPosClient, ref BulletsPosIppoint);
                    Console.WriteLine("after the getUDP");
                    try
                    {
                        p1.monsters = getUDPMonsterList(MonsterPosClient, ref MonsterPosIppoint);
                    }
                    catch
                    {
                        Console.WriteLine("exception in stateListener");
                    }
                }
                catch {
                    Console.WriteLine("Exception in PLAyer 2 stateListener");
                    stateListener();
                
                }
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Console.WriteLine("begin drawing");
            base.OnPaint(e);
            Console.WriteLine("after base.onpaint()");
            try
            {
                p1.draw(e.Graphics);
                p2.draw(e.Graphics);
            }

            catch (InvalidOperationException ex) { }
            if (p1.monsters.Count != 0)
            {
                foreach (Monster m in p1.monsters)
                {
                    m.draw(e.Graphics);
                }
                
            }
            Console.WriteLine("after drawing player 1 and player 2");
            if (p1.bullets.Count != 0)
            {
                foreach (Bullet b in p1.bullets)
                {
                    try
                    {
                        b.draw(e.Graphics);
                    }
                    catch (Exception sasd)
                    {
                        continue;
                    }

                }
            }
            Console.WriteLine("after drawing p1.bullets");
            if (p2.bullets.Count != 0)
            {
                foreach (Bullet b in p2.bullets)
                {

                    try
                    {
                        b.draw(e.Graphics);
                    }
                    catch (Exception sasd)
                    {
                        continue;
                    }
                }
            }
            treasure.draw(e.Graphics); 
            Console.WriteLine("after drawing p2.bullets");
            //if (p2.bullets.Count != 0)
            //{
            //    foreach (Bullet b in p2.bullets)
            //        b.draw(e.Graphics);
            //}
        }

        public static GameState getUDPGameState(UdpClient client, ref IPEndPoint sender)
        {
            try
            {
                return (GameState)new BinaryFormatter().Deserialize(new MemoryStream(client.Receive(ref sender)));
            }
            catch
            {
                Console.WriteLine("Exception");
                return new GameState();
            }
        }
        public static void sendUDPGameState(GameState p, UdpClient uclient, IPEndPoint reciever)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, p);
            byte[] data = ms.GetBuffer();
            uclient.Send(data, data.Length, reciever);
        }
        public static PointF getUDPointF(UdpClient client, ref IPEndPoint sender)
        {
            try
            {
                return (PointF)new BinaryFormatter().Deserialize(new MemoryStream(client.Receive(ref sender)));
            }
            catch
            {
                Console.WriteLine("Exception");
                return new PointF(0, 0);
            }
        }
        public static void sendUDPPointF(PointF p, UdpClient uclient, IPEndPoint reciever)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, p);
            byte[] data = ms.GetBuffer();
            uclient.Send(data, data.Length, reciever);
        }
        public static List<Bullet> getUDBulletsList(UdpClient client, ref IPEndPoint sender)
        {
            try
            {
                return (List<Bullet>)new BinaryFormatter().Deserialize(new MemoryStream(client.Receive(ref sender)));
            }
            catch
            {
                Console.WriteLine("Exception");
                return new List<Bullet>();
            }
        }
        public static void sendUDPBulletsList(List<Bullet> p, UdpClient uclient, IPEndPoint reciever)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, p);
            byte[] data = ms.GetBuffer();
            uclient.Send(data, data.Length, reciever);
        }
        public static List<Monster> getUDPMonsterList(UdpClient client, ref IPEndPoint sender)
        {
            try
            {
                return (List<Monster>)new BinaryFormatter().Deserialize(new MemoryStream(client.Receive(ref sender)));
            }
            catch
            {
                Console.WriteLine("Exception");
                return new List<Monster>();
            }
        }
        public static void sendUDPMonsterList(List<Monster> p, UdpClient uclient, IPEndPoint reciever)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, p);
            byte[] data = ms.GetBuffer();
            uclient.Send(data, data.Length, reciever);
        }
        public void checkCollision()
        {

            foreach (Bullet b in p1.bullets)
            {
                if (!Collision.isWhitePixel(maze.GetPixel((int)b.pos.X, (int)b.pos.Y)))
                    //b.setVisible(false);
                    p1.bullets.Remove(b);
                //if (Collision.MonsterCollision(b))
                if (p1.monsters.Count != 0)
                {
                    foreach (Monster p in p1.monsters)
                    {
                        Rectangle rectC = new Rectangle((int)b.pos.X, (int)b.pos.Y, Constants.BULLET_RAD, Constants.BULLET_RAD);
                        Rectangle rectMonster = new Rectangle((int)p.pos.X, (int)p.pos.Y, Constants.Monster_Length, Constants.Monster_Length);


                        if (rectC.IntersectsWith(rectMonster))
                        {
                            GamePanel.p1.monsters.Remove(p);
                            Console.WriteLine("p1.monsters.remove-----------------------------------------------");
                            p1.bullets.Remove(b);
                        }
                    }
                }
                //if (Collision.MonsterCollision(b))
                //{
                //    p1.bullets.Remove(b);
                //}


            }

            Console.WriteLine("after checking p1.bullets collision");
            foreach (Bullet b in p2.bullets)
            {
                if (!Collision.isWhitePixel(maze.GetPixel((int)b.pos.X, (int)b.pos.Y)))
                    //b.setVisible(false);
                    p2.bullets.Remove(b);
                //if (Collision.MonsterCollision(b))
                if (p1.monsters.Count != 0)
                {
                    foreach (Monster p in p1.monsters)
                    {
                        Rectangle rectC = new Rectangle((int)b.pos.X, (int)b.pos.Y, Constants.BULLET_RAD, Constants.BULLET_RAD);
                        Rectangle rectMonster = new Rectangle((int)p.pos.X, (int)p.pos.Y, Constants.Monster_Length, Constants.Monster_Length);


                        if (rectC.IntersectsWith(rectMonster))
                        {
                            GamePanel.p1.monsters.Remove(p);
                            Console.WriteLine("p1.monsters.remove-----------------------------------------------");

                            p2.bullets.Remove(b);
                        }
                    }
                }


            }
            Console.WriteLine("after b2.bullets collision");
            foreach (Monster p in p1.monsters)
            {
                Rectangle rectMonster = new Rectangle((int)p.pos.X, (int)p.pos.Y, Constants.Monster_Length, Constants.Monster_Length);
                Rectangle rectPlayer1 = new Rectangle((int)p1.pos.X , (int)p1.pos.Y , Constants.PLAYER_WIDTH , Constants.PLAYER_HEIGHT);
                Rectangle rectPlayer2 = new Rectangle((int)p2.pos.X, (int)p2.pos.Y, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);

                if (rectMonster.IntersectsWith(rectPlayer1))
                {
                    timer.Stop();
                    MessageBox.Show("YOU LOST");
                    Application.Exit();
                }
                if (rectMonster.IntersectsWith(rectPlayer2))
                {
                    timer.Stop();
                    MessageBox.Show("YOU WON ");
                    Application.Exit();
                }
            }
        }
        public void checkTreasure()
        {
            Rectangle rectTreasure = new Rectangle(treasure.x, treasure.y, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
            Rectangle rectPlayer1 = new Rectangle((int)p1.pos.X, (int)p1.pos.Y, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);
            Rectangle rectPlayer2 = new Rectangle((int)p2.pos.X, (int)p2.pos.Y, Constants.PLAYER_WIDTH, Constants.PLAYER_HEIGHT);

            if (rectPlayer1.IntersectsWith(rectTreasure))
            {
                timer.Stop();
                MessageBox.Show("YOU WON");
                Application.Exit();
            }
            if (rectPlayer2.IntersectsWith(rectTreasure))
            {
                timer.Stop();
                MessageBox.Show("YOU Lost");
                Application.Exit();

            }

        }

    }
}
