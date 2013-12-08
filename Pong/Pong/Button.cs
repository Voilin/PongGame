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
    class Button
    {

        #region Fields

        Rectangle buttonRectangle;
        buttonType type;

        #endregion

        #region Properties

        #endregion

        #region Constructor

        /// <summary>
        /// Creates a button of a specific type and at a location
        /// </summary>
        /// <param name="contentManager">the content manager</param>
        /// <param name="location">the location of the button</param>
        /// <param name="buttonType">the type of button that it will be</param>
        /// <param name="height">the height of the button</param>
        /// <param name="width">the width of the button</param>
        public Button(ContentManager contentManager, Vector2 location, buttonType buttonType, int width, int height)
        {
            LoadContent(contentManager);

            buttonRectangle = new Rectangle((int)location.X, (int)location.Y, width, height);

            type = buttonType;
        }

        #endregion

        #region Public Methods
        
        // Draw Method here

        // Update method here
        
        // CheckIfClicked method here

        #endregion

        #region Private Methods

        private void LoadContent(ContentManager contentManager)
        {

        }

        #endregion

    }
}
