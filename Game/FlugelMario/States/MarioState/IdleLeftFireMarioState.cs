﻿using Microsoft.Xna.Framework;
using SuperMario.AbstractClasses;
using SuperMario.Enums;
using SuperMario.Interfaces;
using SuperMario.Sound;
using SuperMario.SpriteFactories;

namespace SuperMario.States.MarioStates
{
    public class IdleLeftFireMarioState : MarioState
    {
        public override bool IsStar { get; } = false;
        public IdleLeftFireMarioState(IMario mario) : base(mario)
        {
            StateSprite = MarioSpriteFactory.Instance.CreateIdleLeftFireMarioSprite();
            this.MarioPosture = Posture.Stand;
            this.MarioDirection = Direction.Left;
            this.MarioShape = Shape.Fire;
        }
        public override void ChangeSizeToBig()
        {
            Mario.State = new IdleLeftBigMarioState(Mario);
        }

        public override void RunLeft()
        {
            if (!Mario.IsInAir)
            {
                Mario.State = new RunningLeftFireMarioState(Mario);
            }
        }

        public override void Swim()
        {
            Mario.State = new SwimmingLeftFireMarioState(Mario);
            SoundManager.Instance.PlaySmallJumpSound();
        }

        public override void RunRight()
        {
            Mario.State = new IdleRightFireMarioState(Mario);
        }

        public override void Crouch()
        {
            if (!Mario.IsInAir)
            {
                Mario.State = new CrouchLeftFireMarioState(Mario);
            }
        }
        public override void ChangeStarMode()
        {
            Mario.State = new IdleLeftStarBigMarioState(Mario);
        }

        public override void JumpOrStand()
        {
            if (!Mario.IsInAir)
            {
                Mario.State = new JumpLeftFireMarioState(Mario);
                Mario.Velocity = new Vector2(Mario.Velocity.X, -7);
            }
        }

        public override void Terminated()
        {
            Mario.State = new DeadMarioState(Mario);
        }

        public override void ChangeSizeToSmall()
        {
            Mario.State = new IdleLeftSmallMarioState(Mario);
        }
        public override void Update()
        {
            base.Update();
            if (Mario.IsInAir) return;
            if (!Mario.IsInAir)
            {
                if (Mario.Velocity.X >= 0.75f)
                {
                    Mario.Acceleration = new Vector2(-0.75f, Mario.Acceleration.Y);
                }
                else if (Mario.Velocity.X <= -0.75)
                {
                    Mario.Acceleration = new Vector2(0.75f, Mario.Acceleration.Y);
                }
                else
                {
                    Mario.Acceleration = new Vector2(0, Mario.Acceleration.Y);
                    Mario.Velocity = new Vector2(0, Mario.Velocity.Y);
                }
            }
        }
    }
}