using System;
using System.Drawing;
using System.Windows.Forms;
using Asteroids.Properties;

namespace Asteroids
{
    public partial class Asteroids : Form
    {
        MainSplashScreen.LightSpeed lightSpeed;
        PictureBox pictureBox;
        public Asteroids()
        {
            InitializeComponent();
            this.BackColor = Color.Black;
            this.Icon = Resources.Ship;
            this.Load += new EventHandler(this.Asteroids_Load);

            pictureBox = new PictureBox();
            pictureBox.Dock = DockStyle.Fill;
            Controls.Add(pictureBox);

            lightSpeed = new MainSplashScreen.LightSpeed(pictureBox);
        }

        private void Asteroids_Load(object sender, EventArgs eventArgs)
        {
            lightSpeed.EnableLightSpeed();
        }

    }
}
