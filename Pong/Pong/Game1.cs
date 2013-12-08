using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Pong
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        // the mouse and keyboard state
        KeyboardState keyboardState;
        MouseState mouseState;

        gameState GameState;

        // window size constants
        const int WINDOW_WIDTH = 800;
        const int WINDOW_HEIGHT = 600;

        // the ball and paddle objects
        Ball pongBall;
        Paddle leftPaddle;
        Paddle rightPaddle;

        // audio support
        AudioEngine audioEngine;
        SoundBank soundBank;
        WaveBank waveBank;

        // rectangle for the board to be drawn in and texture
        Rectangle boardRectangle;
        Texture2D boardTexture;

        // both players scores
        Score playerOneScore;
        Score playerTwoScore;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // set the window size
            graphics.PreferredBackBufferHeight = WINDOW_HEIGHT;
            graphics.PreferredBackBufferWidth = WINDOW_WIDTH;

            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            // make the mouse visible
            IsMouseVisible = true;

            // set the window title
            this.Window.Title = "Pong";

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // create ball and paddle objects
            pongBall = new Ball(Content, new Vector2(WINDOW_WIDTH / 2, WINDOW_HEIGHT / 2), 10);
            pongBall.GiveRandomVelocity();

            leftPaddle = new Paddle(Content, 100, 20, "left", GraphicsDevice);
            rightPaddle = new Paddle(Content, 100, 20, "right", GraphicsDevice);

            // load audio components
            audioEngine = new AudioEngine(@"Content/Pong.xgs");
            waveBank = new WaveBank(audioEngine, @"Content/Wave Bank.xwb");
            soundBank = new SoundBank(audioEngine, @"Content/Sound Bank.xsb");

            // set the board rectangle to the size of the window, load the board texture
            boardRectangle = new Rectangle(0, 0, WINDOW_WIDTH, WINDOW_HEIGHT);
            boardTexture = Content.Load<Texture2D>("board");

            playerOneScore = new Score(Content, new Vector2(WINDOW_WIDTH / 4, 25), 39, 75);
            playerTwoScore = new Score (Content, new Vector2 ((3 * (WINDOW_WIDTH / 4)), 25), 39, 75);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here

            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();

            // Update the ball and paddles
            pongBall.Update(GraphicsDevice, gameTime);

            leftPaddle.Update(gameTime, keyboardState, GraphicsDevice);
            rightPaddle.Update(gameTime, keyboardState, GraphicsDevice);

            if (pongBall.X_Coordinate <= leftPaddle.InwardX_Coordinate &&
                pongBall.X_Coordinate >= leftPaddle.InwardX_Coordinate - leftPaddle.Width)
            {
                if (pongBall.Y_Coordinate >= leftPaddle.TopY_Coordinate &&
                    pongBall.Y_Coordinate <= leftPaddle.BottomY_Coordinate)
                {
                    // bounces the ball, and then updates its y velocity based on a percentage of the paddles y_velocity
                    pongBall.BounceX();
                    pongBall.BouncePaddleYVelocity(leftPaddle.Y_Velocity);
                    soundBank.PlayCue("Pong");
                }
            }
            else if (pongBall.X_Coordinate + pongBall.Diameter >= rightPaddle.InwardX_Coordinate &&
                     pongBall.X_Coordinate + pongBall.Diameter <= rightPaddle.InwardX_Coordinate + rightPaddle.Width)
            {
                if (pongBall.Y_Coordinate >= rightPaddle.TopY_Coordinate &&
                    pongBall.Y_Coordinate <= rightPaddle.BottomY_Coordinate)
                {
                    // bounces the ball, and then updates its y velocity based on a percentage of the paddles y_velocity
                    pongBall.BounceX();
                    pongBall.BouncePaddleYVelocity(rightPaddle.Y_Velocity);
                    soundBank.PlayCue("Pong");
                }
            }


            // check if the scores need to be incremented
            if (pongBall.X_Coordinate <= 0)
            {
                playerTwoScore.IncrementScore();

                pongBall.ResetBall(GraphicsDevice);
                pongBall.GiveRandomVelocity();
            }
            else if (pongBall.X_Coordinate + pongBall.Diameter >= WINDOW_WIDTH)
            {
                playerOneScore.IncrementScore();

                pongBall.ResetBall(GraphicsDevice);
                pongBall.GiveRandomVelocity();
            }

            // checks if either player has one
            if (playerOneScore.Value >= 2 || playerTwoScore.Value >= 2)
            {
                leftPaddle.Active = false;
                rightPaddle.Active = false;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            spriteBatch.Begin();

            // draw the background textures
            spriteBatch.Draw(boardTexture, boardRectangle, Color.White);

            spriteBatch.End();

            // draw the ball and paddles
            pongBall.Draw(spriteBatch);
            rightPaddle.Draw(spriteBatch);
            leftPaddle.Draw(spriteBatch);

            // draw both players scores
            playerOneScore.Draw(spriteBatch);
            playerTwoScore.Draw(spriteBatch);

            

            base.Draw(gameTime);
        }
    }
}
