﻿using SuperMario;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.SpriteFactories;

namespace SuperMario
{
    public class FlagSprite : ISprite
    {
        public Texture2D Texture { get; set; }
        private int width;
        private int height;
        public FlagSprite(Texture2D texture)
        {
            this.Texture = texture;
            width = this.Texture.Width / ItemSpriteFactory.FlagSpriteSheetColumns;
            height = this.Texture.Height / ItemSpriteFactory.FlagSpriteSheetRows;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            int row = ItemSpriteFactory.FlagRow;
            int column = ItemSpriteFactory.FlagSpriteSheetColumn;
            location = new Vector2(location.X + width / 2, location.Y);

            Rectangle sourceRectangle = new Rectangle(width * column, height * row, width, height);
            Rectangle destinationRectangle = MakeDestinationRectangle(location);

            spriteBatch.Draw(this.Texture, destinationRectangle, sourceRectangle, Color.White);
        }

        public void Update()
        {
        }

        public Rectangle MakeDestinationRectangle(Vector2 location)
        {
            return new Rectangle((int)location.X, (int)location.Y, width, height);
        }
    }
}
