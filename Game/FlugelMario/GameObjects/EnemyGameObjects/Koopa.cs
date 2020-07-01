﻿using SuperMario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.GameObjects;
using SuperMario.States.EnemyStates;

namespace SuperMario
{
    public class Koopa : IEnemy
    {
        public Rectangle Destination { get; set; }
        public Vector2 Location { get; set; }
        public IEnemyState State { get; set; }
        public bool Alive { get; set; } = true;
        public bool Moving { get; set; } = true;
        public GameObjectType.ObjectType Type { get; } = GameObjectType.ObjectType.Koopa;
        private Vector2 velocity;
        private Vector2 acceleration;
        public Vector2 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        private bool hasBeenInView;
        public bool CanUpdate { get { return hasBeenInView; } }


        public Koopa(Vector2 location)
        {
            Location = location;
            State = new KoopaAliveState(this);
            Destination = State.StateSprite.MakeDestinationRectangle(Location);
            velocity = new Vector2(-1, 0);
            acceleration = new Vector2(0, GameData.Gravity);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, this.Location);
        }
        public void Update()
        {
            hasBeenInView = true;
            if (velocity.Y < 3)
            {
                velocity.Y += acceleration.Y;
            }
            if (Location.Y > 420)
            {
                Terminate("TOP");
            }
            Location = new Vector2(Location.X + velocity.X, Location.Y + velocity.Y);
            State.Update();
            Destination = State.StateSprite.MakeDestinationRectangle(Location);
        }
        public void ChangeDirection()
        {
            Velocity = new Vector2(-Velocity.X, Velocity.Y);
            State.ChangeDirection();
        }

        public void Terminate(string direction)
        {
            State.Terminate(direction);
            Moving = false;
            Alive = false;
        }
    }
}
