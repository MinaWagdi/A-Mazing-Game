using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing; 

namespace WindowsFormsApplication2
{
    [Serializable()]
    abstract class DynamicImage
    {
        //protected int x, y;
        public PointF pos; 
        public int dxR, dyU , dxL , dyD;
        //protected int width, height;
        Boolean visible;
        public Image img;
        String ImagePath;

        public DynamicImage(String ImagePath) 
        {
            this.ImagePath = ImagePath;
            this.img = Image.FromFile(ImagePath); 
        }
        public DynamicImage(float x, float y, String ImagePath)
        {
            pos = new PointF(x, y); 
            //this.img = img;
            this.ImagePath = ImagePath;
            //loadImage(this.img, ImagePath);
            this.img = Image.FromFile(ImagePath); 
        }
        //public void loadImage(Image img , String ImagePath)
        //{
        //    img = Image.FromFile(ImagePath); 

        //}
        public Boolean isVisible()
        {
            return visible; 
        }
        public void setVisible(Boolean visible)
        {
            this.visible = visible;   
        }
        abstract public void draw(Graphics g);
        public PointF POS
        {
            get { return pos; }
            set { pos = value; }
        }
        
    }
}
