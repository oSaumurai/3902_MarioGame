﻿using SuperMario.Interfaces;
using SuperMario.SpriteFactories;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SuperMario.Sprites.StairBlocks
{
    class StairBlockSprite : ISprite
    {
        public Texture2D Texture { get; set; }
        private int width;
        private int height;
        private int row;
        private int column;
        public StairBlockSprite(Texture2D texture)
        {
            Texture = texture;
            width = BlockSpriteFactory.Instance.StairBlockWidth;
            height = BlockSpriteFactory.Instance.StairBlockHeight;
            row = BlockSpriteFactory.Instance.StairSpriteSheetRows;
            column = BlockSpriteFactory.Instance.StairSpriteSheetColum;
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 location)
        {
            Rectangle sourceRectangle = new Rectangle(0, 0, width, height);
            Rectangle destinationRectangle = MakeDestinationRectangle(location);

            spriteBatch.Draw(this.Texture, destinationRectangle, sourceRectangle, Color.White);
        }
        public void Update()
        {
        }

        public Rectangle MakeDestinationRectangle(Vector2 location)
        {
            return new Rectangle((int)location.X, (int)location.Y, width,height);
        }
    }
}
