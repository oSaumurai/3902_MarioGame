﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SuperMario.Enums;
using SuperMario.Interfaces;
using SuperMario.SpriteFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static SuperMario.GameObjects.GameObjectType;
using SuperMario.States.MarioStates;
using SuperMario.Sound;
using SuperMario.SCsystem;

namespace SuperMario.GameObjects
{
    public class MarioObject : IMario
    {
        public ObjectType Type { get; } = ObjectType.Mario;
        public IMarioState State { get; set; }
        private bool isAir = false;
        private bool isWater = false;
        private bool swimable = true;
        private bool Level2 = false;
        private bool canFlash = true;
        public bool IsInAir
        {
            get { return isAir; }
            set
            {
                isAir = value;
            }
        }

        public bool Flashable
        {
            get { return canFlash; }
            set
            {
                canFlash = value;
            }
        }

        public bool IsLevel2
        {
            get { return Level2 ; }
            set
            {
                Level2 = value;
            }
        }

        public bool Swimable
        {
            get { return swimable; }
            set
            {
                swimable = value;
            }
        }

        public bool IsInWater
        {
            get { return isWater; }
            set
            {
                isWater = value;
            }
        }
        public Shape PreStarShape { set; get; }
        private Vector2 location;
        private bool isProtect = false;
        private int starTime;
        private const int starDuration = 400;
        private const int starTimeMin = -1;
        private const int starTimeNormal = 0;
        public bool IsProtected
        {
            get { return isProtect; }
            set
            {
                isProtect = value;
            }
        }

        public Vector2 Location
        {
            get { return location; }
            set
            {
                int horizontalDisplacement = (MarioSpriteFactory.Instance.NormalMarioWidth - this.Destination.Width) / 2;
                int verticalDisplacement = (MarioSpriteFactory.Instance.NormalMarioHeight - this.Destination.Height);
                location.X = value.X - horizontalDisplacement;
                location.Y = value.Y - verticalDisplacement;
            }
        }

        public Vector2 Velocity { set; get; }
        public float maxSpeed { set; get; }
        public float maxYSpeed { set; get; }
        public Vector2 Acceleration { set; get; }
        private const int smallMarioVertDis = 3;
        private const int smallMarioHeriDis = 3;
        private const int bigMarioVertDis = 5;
        private const int bigMarioHeriDis = 2;
        private int protectTime;
        private const int protectDuration = 50;
        public MarioObject(Vector2 location)
        {
            State = new IdleRightSmallMarioState(this);
            Location = location;
            State.MarioPosture = Posture.Stand;
            State.MarioShape = Shape.Small;
            State.MarioDirection = Direction.Right;
            Velocity = new Vector2(0, 0);
            Acceleration = new Vector2(0, GameData.Gravity);
            IsProtected = false;
            maxSpeed = 4f;
            maxYSpeed = 8f;
        }

        public Rectangle Destination
        {
            set { Destination = value; }
            get
            {
                int verticalDisplacement;
                int horizontalDisplacement;
                if (State.MarioPosture == Posture.Crouch ||
                    State.MarioShape == Shape.Small ||
                    State.MarioShape == Shape.StarSmall)
                {
                    verticalDisplacement = MarioSpriteFactory.Instance.HalfNormalMarioHeight+ smallMarioVertDis;
                    horizontalDisplacement = smallMarioHeriDis;
                }else 
                {
                    verticalDisplacement = bigMarioVertDis;
                    horizontalDisplacement = bigMarioHeriDis;
                }
                if (State.MarioPosture == Posture.Swimming)
                {
                    verticalDisplacement = smallMarioVertDis;
                    horizontalDisplacement = smallMarioHeriDis;
                }
                return new Rectangle((int)this.location.X + horizontalDisplacement, (int)this.location.Y + verticalDisplacement, MarioSpriteFactory.Instance.NormalMarioWidth - 2 * horizontalDisplacement, MarioSpriteFactory.Instance.NormalMarioHeight - verticalDisplacement);
            }
        }
        
        public void Update()
        {
            if (IsInWater)
            {
                maxSpeed = 2f;
                maxYSpeed = 4f;
            }
            if (location.X < Camera.CameraX)
            {
                location = new Vector2(location.X + 5, location.Y);
                return;
            }
            else if (location.Y < Camera.CameraY - 20)
            {
                location = new Vector2(location.X, location.Y + 10);
                return;
            }
            else if (location.X >= 3580)
            {
                location = new Vector2(location.X - 5 , location.Y);
                return;
            }
            //TODO: add level condition for Game (ground/underground)
            if (location.Y <= 450 && location.Y >= 445 )
            {
                State.Terminated();
            }

            if (IsProtected)
            {
                protectTime++;
                if (protectTime >= protectDuration)
                {
                    IsProtected = false;
                }
            }
            //horizontal velocity control
            float newVelocityX = this.Velocity.X;
            if ((this.Velocity.X < maxSpeed && this.Velocity.X > -maxSpeed) ||
                (this.Velocity.X <= -maxSpeed && this.Acceleration.X >0) ||
                (this.Velocity.X >= maxSpeed && this.Acceleration.X < 0))
            {
                newVelocityX += this.Acceleration.X;
            }

            //vertical velocity control
            float newVelocityY = this.Velocity.Y;
            if (this.Velocity.Y < maxYSpeed)
            {
                newVelocityY += this.Acceleration.Y;
            }
            this.Velocity = new Vector2(newVelocityX, newVelocityY);
            float newLocationX = this.Destination.X + this.Velocity.X;
            float newLocationY = this.Destination.Y + this.Velocity.Y;
            this.Location = new Vector2(newLocationX, newLocationY);
            State.Update();

            if(State.IsStar)
            {
                starTime--;
                if (starTime == starTimeMin)
                {
                    starTime = starDuration;
                }
                if (starTime == starTimeNormal)
                {
                    SoundManager.Instance.PlayOverWorldSong();
                    switch (PreStarShape)
                    {
                        case Shape.Big:
                            State.ChangeSizeToBig();
                            break;
                        case Shape.Fire:
                            State.ChangeFireMode();
                            break;
                        case Shape.Small:
                            State.ChangeSizeToSmall();
                            break;
                        default:
                            State.ChangeSizeToSmall();
                            break;
                    }
                }
            }
            else
            {
                PreStarShape = State.MarioShape;
                if (starTime != starTimeNormal)
                {
                    starTime = starTimeNormal;
                }
            }           
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            State.Draw(spriteBatch, this.Location);
        }
    }
}
