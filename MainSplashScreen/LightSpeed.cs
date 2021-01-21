using System;
using System.Drawing;
using System.Windows.Forms;

namespace Asteroids.MainSplashScreen
{
    class LightSpeed
    {
        PictureBox pictureBox;
        Random rand = new Random();
        Timer timer;
        Star[] stars;
        float numSteps = 10000;
        public LightSpeed(PictureBox pictureBox)
        {
            this.pictureBox = pictureBox;
            stars = Create_Stars(2000);
            timer = new Timer();
            timer.Interval = 16;
        }

        public void EnableLightSpeed()
        {
            pictureBox.Paint += new PaintEventHandler(this.LightSpeed_Painter);
            timer.Tick += new EventHandler(this.Clock_Event);
            timer.Enabled = true;
            timer.Start();
        }

        public void DisableLightSpeed()
        {
            pictureBox.Paint -= this.LightSpeed_Painter;
            timer.Tick -= this.Clock_Event;
            timer.Enabled = false;
            timer.Stop();
        }

        private void LightSpeed_Painter(object sender, PaintEventArgs paintEventArgs)
        {
            Graphics g = paintEventArgs.Graphics;
            SolidBrush solidBrush = new SolidBrush(Color.White);
            foreach (Star s in stars)
            {
                float x = s.GetPosition()[0] * pictureBox.Width + pictureBox.Width / 2;
                float y = s.GetPosition()[1] * pictureBox.Height + pictureBox.Height / 2;
                g.FillEllipse(solidBrush, x, y, s.Radius * pictureBox.Width / s.Delta - 1, s.Radius * pictureBox.Width / s.Delta - 1);
            }
        }

        private void Clock_Event(object sender, EventArgs clockEvent)
        {
            for (int i = 0; i < stars.Length; i++)
            {
                stars[i].UpdatePostion();
                if (stars[i].GetPosition()[0] * pictureBox.Width + pictureBox.Width / 2 < 0 ||
                    stars[i].GetPosition()[1] * pictureBox.Height + pictureBox.Height / 2 < 0 ||
                    stars[i].GetPosition()[0] * pictureBox.Width + pictureBox.Width / 2 > pictureBox.Width ||
                    stars[i].GetPosition()[1] * pictureBox.Height + pictureBox.Height / 2 > pictureBox.Height ||
                    stars[i].Delta == 1)
                {
                    stars[i] = Create_Star();
                }
            }
            pictureBox.Invalidate();
            numSteps--;
            if (numSteps <= 0)
            {
                DisableLightSpeed();
            }
        }

        private Star[] Create_Stars(int numStars)
        {
            Star[] newStars = new Star[numStars];
            for (int i = 0; i < numStars; i++)
            {
                newStars[i] = Create_Star();
            }
            return newStars;
        }

        private Star Create_Star()
        {
            return new Star(2,
                rand.Next(-pictureBox.Width, pictureBox.Width),
                rand.Next(-pictureBox.Height, pictureBox.Height),
                pictureBox.Width);
        }
    }

    class Star
    {
        float radius;
        float posX;
        float posY;
        float delta;
        public Star(float radius, float posX, float posY, float delta)
        {
            this.Radius = radius;
            this.posX = posX;
            this.posY = posY;
            this.delta = delta;
        }

        public void UpdatePostion()
        {
            this.delta -= 10;
            if (this.delta < 1)
            {
                this.delta = 1;
            }
        }

        public float[] GetPosition()
        {
            float[] retVal = { this.PosX / this.Delta, this.PosY / this.Delta };
            return retVal;
        }

        public float Radius { get => radius; set => radius = value; }
        public float PosX { get => posX; set => posX = value; }
        public float PosY { get => posY; set => posY = value; }
        public float Delta { get => delta; set => delta = value; }
    }
}
