﻿using Microsoft.Xna.Framework;
using SuperMario.AbstractClasses;
using SuperMario.Enums;
using SuperMario.Interfaces;
using SuperMario.Sound;
using SuperMario.SpriteFactories;

namespace SuperMario.States.MarioStates
{
    internal class JumpLeftBigMarioState : MarioState
    {
        public override bool IsStar { get; } = false;
        public JumpLeftBigMarioState(IMario mario) : base(mario)
        {
            StateSprite = MarioSpriteFactory.Instance.CreateJumpLeftBigMarioSprite();
            this.MarioPosture = Posture.Jump;
            this.MarioDirection = Direction.Left;
            this.MarioShape = Shape.Big;
            mario.Acceleration = new Vector2(0, mario.Acceleration.Y);
            if (mario.IsInAir == false && !mario.IsProtected)
            {
                SoundManager.Instance.PlaySuperJumpSound();
            }
            mario.IsInAir = true;
        }

        public override void ChangeFireMode()
        {
            Mario.State = new JumpLeftFireMarioState(Mario);
        }
        public override void ChangeStarMode()
        {
            Mario.State = new JumpLeftStarBigMarioState(Mario);
        }
        public override void RunLeft()
        {
            Mario.Location = new Vector2(Mario.Destination.X - 1, Mario.Destination.Y);
        }

        public override void RunRight()
        {
            Mario.State = new JumpRightBigMarioState(Mario);
        }

        public override void JumpOrStand()
        {
        }

        public override void Crouch()
        {
        }

        public override void Terminated()
        {
            Mario.State = new DeadMarioState(Mario);
        }

        public override void ChangeSizeToSmall()
        {
            Mario.State = new JumpLeftSmallMarioState(Mario);
        }

        public override void Update()
        {
            if (!Mario.IsInAir)
            {
                Mario.State = new IdleLeftBigMarioState(Mario);
            }
            base.Update();
        }
    }
}