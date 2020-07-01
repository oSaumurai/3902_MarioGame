﻿using Microsoft.Xna.Framework;
using SuperMario.AbstractClasses;
using SuperMario.Enums;
using SuperMario.Interfaces;
using SuperMario.Sound;
using SuperMario.SpriteFactories;

namespace SuperMario.States.MarioStates
{
    public class JumpRightBigMarioState : MarioState
    {
        public override bool IsStar { get; } = false;
        public JumpRightBigMarioState(IMario mario) : base(mario)
        {
            StateSprite = MarioSpriteFactory.Instance.CreateJumpRightBigMarioSprite();
            this.MarioPosture = Posture.Jump;
            this.MarioDirection = Direction.Right;
            this.MarioShape = Shape.Big;
            mario.Acceleration = new Vector2(0, mario.Acceleration.Y);
            if (mario.IsInAir == false && !mario.IsProtected)
            {
                SoundManager.Instance.PlaySuperJumpSound();
            }
            mario.IsInAir = true;
        }

        public override void RunLeft()
        {
            Mario.State = new JumpLeftBigMarioState(Mario);
        }

        public override void RunRight()
        {
            Mario.Location = new Vector2(Mario.Destination.X + 1, Mario.Destination.Y);
        }

        public override void JumpOrStand()
        {
        }

        public override void Crouch()
        {
        }

        public override void ChangeFireMode()
        {
            Mario.State = new JumpRightFireMarioState(Mario);
        }

        public override void Terminated()
        {
            Mario.State = new DeadMarioState(Mario);
        }

        public override void ChangeSizeToSmall()
        {
            Mario.State = new JumpRightSmallMarioState(Mario);
        }

        public override void ChangeStarMode()
        {
            Mario.State = new JumpRightStarBigMarioState(Mario);
        }

        public override void Update()
        {
            if (!Mario.IsInAir )
            {
                Mario.State = new IdleRightBigMarioState(Mario);
            }
            base.Update();
        }
    }
}