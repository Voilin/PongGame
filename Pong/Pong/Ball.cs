using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Pong
{
    class Ball
    {
        #region Fields

        // Movement support
        double x_velocity;
        double y_velocity;

        const int MAX_INITIAL_VELOCITY = 5;

        // Drawing support
        Texture2D ballTexture;
        Rectangle ballRectangle;

        // Audio components
        WaveBank waveBank;
        SoundBank soundBank;
        AudioEngine audioEngine;

        Random rand = new Random();

        const int BALL_DIAMETER = 10;

        #endregion

        #region Properties

        // returns the X coordinate of the ball
        public int X_Coordinate
        {
            get { return ballRectangle.X; }
            set { X_Coordinate = ballRectangle.X;}
        }

        // returns the Y coordinate of the ball
        public int Y_Coordinate
        {
            get { return ballRectangle.Y; }
            set { X_Coordinate = ballRectangle.X; }
        }

        // returns the diameter of the ball
        public int Diameter
        {
            get { return ballRectangle.Width; }
        }

        // gets and sets the x_velocity of the ball
        public double X_Velocity
        {
            get { return x_velocity; }
            set { this.X_Velocity = x_velocity; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a ball with given location and diameter
        /// </summary>
        /// <param name="contentManager">the content manager</param>
        /// <param name="ballLocation">the spawn location for the ball</param>
        /// <param name="ballDiameter">the diameter of the ball</param>
        public Ball(ContentManager contentManager, Vector2 ballLocation, int ballDiameter)
        {
            LoadContent(contentManager);

            ballRectangle = new Rectangle((int)ballLocation.X, (int)ballLocation.Y, ballDiameter, ballDiameter);
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Resets the ball to the center of the screen
        /// </summary>
        /// <param name="graphicsDevice">the graphics device</param>
        public void ResetBall(GraphicsDevice graphicsDevice)
        {
            
            // reset the ball back to the center
            ballRectangle.X = (graphicsDevice.Viewport.Width / 2) + (ballRectangle.Width / 2);
            ballRectangle.Y = (graphicsDevice.Viewport.Height / 2) - (ballRectangle.Height / 2);

            // reset velocities and set new ones
            x_velocity = 0;
            y_velocity = 0;
            GiveRandomVelocity();
        }

        /// <summary>
        /// Gives the ball random x and y velocities
        /// </summary>
        public void GiveRandomVelocity()
        {
            // reset the random seed
            Random rand = new Random();

            // give the ball new random x and y velocities
            while (x_velocity == 0 || y_velocity == 0)
            {
                x_velocity = rand.Next(-MAX_INITIAL_VELOCITY, MAX_INITIAL_VELOCITY);
                y_velocity = rand.Next(-MAX_INITIAL_VELOCITY, MAX_INITIAL_VELOCITY);
            }
        }

        public void BounceX()
        {
            x_velocity = -x_velocity;
            x_velocity = x_velocity * 1.1;
            y_velocity = y_velocity * 1.1;
        }


        /// <summary>
        /// Updates the location of the ball, and checks if it should bounce off of anything
        /// </summary>
        /// <param name="graphicsDevice">the graphics device</param>
        public void Update(GraphicsDevice graphicsDevice, GameTime gameTime)
        {
            // updates the balls position based on its velocity
            ballRectangle.X += (int)x_velocity;
            ballRectangle.Y += (int)y_velocity;

            // checks if the ball has bounced on the top or bottom of the screen
            if (ballRectangle.Y<= 0)
            {
                ballRectangle.Y = 0 + ballRectangle.Height ;
                y_velocity = -y_velocity;
                soundBank.PlayCue("Pong");
            }
            else if (ballRectangle.Y + ballRectangle.Height >= graphicsDevice.Viewport.Height)
            {
                ballRectangle.Y = graphicsDevice.Viewport.Height - ballRectangle.Height;
                y_velocity = -y_velocity;
                soundBank.PlayCue("Pong");
                
            }

            // checks if the ball has contacted the left or right hand side, and will then increment the scroes appropriately.
            if (ballRectangle.X <= 0)
            {
     
                // reset the ball back to the center and give it a velocity
                ResetBall(graphicsDevice);
            }

            else if (ballRectangle.X + ballRectangle.Width>= graphicsDevice.Viewport.Width)
            {

                // reset the ball back to the center and give it a velocity
                ResetBall(graphicsDevice);
                
            }
        }

        /// <summary>
        /// Draws the ball
        /// </summary>
        /// <param name="spriteBatch">the sprite batch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(ballTexture, ballRectangle, Color.White);

            spriteBatch.End();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Loads the content for the ball
        /// </summary>
        /// <param name="contentManager">the contentManager</param>
        private void LoadContent(ContentManager contentManager)
        {
            ballTexture = contentManager.Load<Texture2D>("ball");
            audioEngine = new AudioEngine(@"Content/Pong.xgs");
            waveBank = new WaveBank(audioEngine, @"Content/Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content/Sound Bank.xsb");
        }
        #endregion

    }
}
