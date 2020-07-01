﻿using Microsoft.Xna.Framework;
using SuperMario.AbstractClasses;
using SuperMario.Enums;
using SuperMario.Interfaces;
using SuperMario.Sound;
using SuperMario.SpriteFactories;

namespace SuperMario.States.MarioStates
{
    public class RunningRightBigMarioState : MarioState
    {
        public override bool IsStar { get; } = false;
        public RunningRightBigMarioState(IMario mario) : base(mario)
        {
            StateSprite = MarioSpriteFactory.Instance.CreateRunningRightBigMarioSprite();
            this.MarioPosture = Posture.Running;
            this.MarioDirection = Direction.Right;
            this.MarioShape = Shape.Big;
            Mario.Acceleration = new Vector2(0.25f, Mario.Acceleration.Y);
        }


        public override void RunLeft()
        {
            Mario.State = new IdleRightBigMarioState(Mario);
        }

        public override void RunRight()
        {
        }

        public override void JumpOrStand()
        {
            Mario.State = new JumpRightBigMarioState(Mario);
            Mario.Velocity = new Vector2(Mario.Velocity.X, -7);
            Mario.Acceleration = new Vector2(0, Mario.Acceleration.Y);
        }

        public override void Swim()
        {
            Mario.State = new SwimmingRightBigMarioState(Mario);
            SoundManager.Instance.PlaySmallJumpSound();

        }

        public override void Crouch()
        {
            Mario.State = new CrouchRightBigMarioState(Mario);
        }

        public override void Terminated()
        {
            Mario.State = new DeadMarioState(Mario);
        }

        public override void ChangeStarMode()
        {
            Mario.State = new RunningRightStarBigMarioState(Mario);
        }

        public override void ChangeFireMode()
        {
            Mario.State = new RunningRightFireMarioState(Mario);
        }

        public override void ChangeSizeToSmall()
        {
            Mario.State = new RunningRightSmallMarioState(Mario);
        }

        public override void Update()
        {
            if (Mario.IsInAir && !Mario.IsInWater && Mario.State.MarioPosture != Posture.Jump)
            {
                Mario.State = new IdleRightBigMarioState(Mario);
            }
            if (Mario.IsInAir && Mario.IsInWater && Mario.State.MarioPosture != Posture.Swimming)
            {
                Mario.State = new SwimmingRightBigMarioState(Mario);
            }
            base.Update();
        }
    }
}
