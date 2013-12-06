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
    class Paddle
    {

        #region Fields

        // gameplay aids
        bool active;
        string boardSide;
        
        //drawing aids
        Rectangle paddleRectangle;

        // movement aids
        double y_velocity;
        const int MAX_Y_VELOCITY = 6;

        // texture for the paddle
        Texture2D paddleTexture;
        
        #endregion

        #region Properties

        // gets the y coordinate of the top of the paddle
        public int TopY_Coordinate
        {
            get { return paddleRectangle.Y;}
        }

        // gets the y coordinate of the bottom of the paddle
        public int BottomY_Coordinate
        {
            get { return paddleRectangle.Y + paddleRectangle.Height; }
        }

        // gets the inward facing sides x coordinate
        public int InwardX_Coordinate
        {
            get
            {
                if (boardSide.ToLower() == "right")
                {
                    return paddleRectangle.X;
                }
                else if (boardSide.ToLower() == "left")
                {
                    return paddleRectangle.X + paddleRectangle.Width;
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Width
        {
            get { return paddleRectangle.Width; }
        }

        // gets and sets wether the paddle is active
        public bool Active
        {
            get { return active; }
            set { Active = active; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates a paddle on the specified side, with specified height and width
        /// </summary>
        /// <param name="_contentManager">the content manager</param>
        /// <param name="_height">the height of the paddle</param>
        /// <param name="_width">the width of the paddle</param>
        /// <param name="_boardSide">the side of the board that the paddle will be on</param>
        /// <param name="_graphicsDevice">the graphics device</param>
        public Paddle(ContentManager _contentManager, int _height, int _width, string _boardSide, GraphicsDevice _graphicsDevice)
        {
            LoadContent(_contentManager);

            // creates a paddle on the left hand side of the board
            if (_boardSide.ToLower() == "left")
            {
                boardSide = "left";
                paddleRectangle = new Rectangle(30,
                                               (_graphicsDevice.Viewport.Height / 2) - (_height / 2),
                                               _width,
                                               _height);
            }

            // creates a paddle on the right hand side of the board
            else if (_boardSide.ToLower() == "right")
            {
                boardSide = "right";
                paddleRectangle = new Rectangle((_graphicsDevice.Viewport.Width - _width - 30),
                                               (_graphicsDevice.Viewport.Height / 2) - (_height / 2),
                                               _width,
                                               _height);
            }
            
        }

        /// <summary>
        /// Creates a paddle with given texture, board side, height and width
        /// </summary>
        /// <param name="contentManager">the content manager</param>
        /// <param name="height">the height of the paddle</param>
        /// <param name="width">the width of the paddle</param>
        /// <param name="boardSide">the side of the board the paddle will be on</param>
        /// <param name="paddleTexture">the paddles texture</param>
        //public Paddle(ContentManager contentManager, int height, int width, string boardSide, Texture2D paddleTexture)
        //{

        //}

        //public Paddle(ContentManager contentManager, int height, int width, string boardSide, Vector2 paddleLocation)
        //{

        //}
        #endregion

        #region Public methods

        /// <summary>
        /// Updates the paddle
        /// </summary>
        /// <param name="gameTime">the game time</param>
        /// <param name="keyboardState">the current keyboard state</param>
        public void Update(GameTime gameTime, KeyboardState keyboardState, GraphicsDevice graphicsDevice)
        {
            if (boardSide.ToLower() == "right")
            {
                // checks which keyboard button is currently pressed and moves in the respective direction
                if (keyboardState.IsKeyDown(Keys.Up))
                {
                    if (y_velocity < MAX_Y_VELOCITY)
                    {
                        y_velocity -= 0.5;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.Down))
                {
                    if (y_velocity >  - MAX_Y_VELOCITY)
                    {
                        y_velocity += 0.5;
                    }
                }
                else
                {
                    y_velocity = 0;
                }
            }
            else if (boardSide.ToLower() == "left")
            {
                // checks which keyboard button is currently pressed and moves in the respective direction
                if (keyboardState.IsKeyDown(Keys.W))
                {
                    if (y_velocity < MAX_Y_VELOCITY)
                    {
                        y_velocity -= 0.5;
                    }
                }
                else if (keyboardState.IsKeyDown(Keys.S))
                {
                    if (y_velocity >  - MAX_Y_VELOCITY)
                    {
                        y_velocity += 0.5;
                    }
                }
                else
                {
                    y_velocity = 0;
                }
            }

            // updates the paddles y co-ordinate based on the velocity
            paddleRectangle.Y += (int)y_velocity;

            if (paddleRectangle.Y <= 0)
            {
                paddleRectangle.Y = 0;
            }
            else if (paddleRectangle.Y + paddleRectangle.Height >= graphicsDevice.Viewport.Height)
            {
                paddleRectangle.Y = graphicsDevice.Viewport.Height - paddleRectangle.Height;
            }
        }

        /// <summary>
        /// Draws the paddle
        /// </summary>
        /// <param name="spriteBatch">the spriteBatch</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            spriteBatch.Draw(paddleTexture, paddleRectangle, Color.White);

            spriteBatch.End();
        }
        #endregion

        #region Private methods

        /// <summary>
        /// Loads the content for the paddle
        /// </summary>
        /// <param name="contentManager">the content manager</param>
        private void LoadContent(ContentManager contentManager)
        {
            // Load the paddles texture.
            paddleTexture = contentManager.Load<Texture2D>("paddle");
        }

        #endregion
    }
}
