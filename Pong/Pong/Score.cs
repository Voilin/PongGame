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
    class Score
    {
        #region Fields

        // game support
        int score;

        // drawing support
        Rectangle scoreRectangle;

        // string conversions for all numbers
        string zero = "0";
        string one = "1";
        string two = "2";
        string three = "3";
        string four = "4";
        string five = "5";
        string six = "6";
        string seven = "7";
        string eight = "8";
        string nine = "9";

        // texture2d's for all numbers
        Texture2D zeroTexture;
        Texture2D oneTexture;
        Texture2D twoTexture;
        Texture2D threeTexture;
        Texture2D fourTexture;
        Texture2D fiveTexture;
        Texture2D sixTexture;
        Texture2D sevenTexture;
        Texture2D eightTexture;
        Texture2D nineTexture;

        Texture2D activeTexture;

        #endregion

        #region Properties

        // gets the value that should be displayed
        public int Value
        {
            get { return score; }
        }

        #endregion

        #region Contstructor

        public Score(ContentManager contentManager, Vector2 location, int width, int height)
        {
            LoadContent(contentManager);

            score = 0;

            scoreRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);
        }

        #endregion

        #region Public methods

        public void Update()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();

            switch (score)
            {
                case 0:
                    {
                        spriteBatch.Draw(zeroTexture, scoreRectangle, Color.White);
                        break;
                    }
                case 1:
                    {
                        spriteBatch.Draw(oneTexture, scoreRectangle, Color.White);
                        break;
                    }

                case 2:
                    {
                        spriteBatch.Draw(twoTexture, scoreRectangle, Color.White);
                        break;
                    }
                case 3:
                    {
                        spriteBatch.Draw(threeTexture, scoreRectangle, Color.White);
                        break;
                    }

                case 4:
                    {
                        spriteBatch.Draw(fourTexture, scoreRectangle, Color.White);
                        break;
                    }
                case 5:
                    {
                        spriteBatch.Draw(fiveTexture, scoreRectangle, Color.White);
                        break;
                    }
                case 6:
                    {
                        spriteBatch.Draw(sixTexture, scoreRectangle, Color.White);
                        break;
                    }
                case 7:
                    {
                        spriteBatch.Draw(sevenTexture, scoreRectangle, Color.White);
                        break;
                    }
                case 8:
                    {
                        spriteBatch.Draw(eightTexture, scoreRectangle, Color.White);
                        break;
                    }
                case 9:
                    {
                        spriteBatch.Draw(nineTexture, scoreRectangle, Color.White);
                        break;
                    }
                   
            }
            

            spriteBatch.End();
        }

        public void IncrementScore()
        {
            score += 1;
        }

        #endregion

        #region Private methods

        private void LoadContent(ContentManager contentManger)
        {
            zeroTexture = contentManger.Load<Texture2D>(zero);
            oneTexture = contentManger.Load<Texture2D>(one);
            twoTexture = contentManger.Load<Texture2D>(two);
            threeTexture = contentManger.Load<Texture2D>(three);
            fourTexture = contentManger.Load<Texture2D>(four);
            fiveTexture = contentManger.Load<Texture2D>(five);
            sixTexture = contentManger.Load<Texture2D>(six);
            sevenTexture = contentManger.Load<Texture2D>(seven);
            eightTexture = contentManger.Load<Texture2D>(eight);
            nineTexture = contentManger.Load<Texture2D>(nine);
        }
        #endregion
    }
}
