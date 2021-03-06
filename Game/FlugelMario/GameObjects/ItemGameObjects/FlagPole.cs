﻿using SuperMario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.GameObjects;
using SuperMario.SpriteFactories;
using SuperMario;
using static SuperMario.GameObjects.GameObjectType;

namespace SuperMario.GameObjects.ItemGameObjects
{
    public class FlagPole : IItem
    {
        private ISprite sprite;
        public Vector2 Location { get; set; }
        public ObjectType Type { get; } = ObjectType.FlagPole;

        public Rectangle Destination { get; set; }

        public bool IsCollected { get; set; } = true;
        public bool IsPreparing { get; set; } = false;
        public Vector2 Velocity { get; set; }
        public FlagPole(Vector2 location)
        {
            sprite = ItemSpriteFactory.Instance.CreateFlagPoleSprite();
            Location = location;
            Destination = sprite.MakeDestinationRectangle(Location);
            Velocity = new Vector2(0, 0);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch, this.Location);
        }

        public void Collect()
        {
        }

        public void Update()
        {
            Destination = sprite.MakeDestinationRectangle(Location);
            sprite.Update();
        }

        public void ChangeDirection()
        {
            throw new NotImplementedException();
        }
    }
}
