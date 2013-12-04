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
        int x_velocity;
        int y_velocity;

        const int MAX_INITIAL_VELOCITY = 5;

        // Drawing support
        Texture2D ballTexture;
        Rectangle ballRectangle;


        Random rand = new Random();

        const int BALL_DIAMETER = 10;

        #endregion

        #region Properties

        #endregion

        #region Constructors

        public Ball(ContentManager contentManager, Texture2D ballTexture, Vector2 ballLocation, int ballDiameter)
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

            // give the ball new random x and y velocities
            x_velocity = rand.Next(1, MAX_INITIAL_VELOCITY);
            y_velocity = rand.Next(1, MAX_INITIAL_VELOCITY);

        }

        public void Update(GraphicsDevice graphicsDevice)
        {
            ballRectangle.X += x_velocity;
            ballRectangle.Y += y_velocity;

            // checks if the ball has bounced on the top or bottom of the screen
            if (ballRectangle.Y + ballRectangle.Height <= 0)
            {
                ballRectangle.Y = 0 + ballRectangle.Height;
                y_velocity = -y_velocity;
            }
            else if (ballRectangle.Y >= graphicsDevice.Viewport.Height)
            {
                ballRectangle.Y = graphicsDevice.Viewport.Height;
                y_velocity = -y_velocity;
            }

            // checks if the ball has bounced on the right or left of the screen
            if (ballRectangle.X <= 0)
            {

            }
            else if (ballRectangle.X >= graphicsDevice.Viewport.Width + ballRectangle.Width)
            {

            }
        }

        /// <summary>
        /// Draws the ball
        /// </summary>
        /// <param name="spriteBatch">the spriteBaClass1.cstch</param>
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
        }
        #endregion

    }
}
