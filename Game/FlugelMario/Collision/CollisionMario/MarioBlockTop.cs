﻿using Microsoft.Xna.Framework;
using SuperMario.Interfaces;
using SuperMario.Enums;
using SuperMario.GameObjects;

namespace SuperMario.Commands
{
    class MarioBlockTop:ICommand
    {
        CollisionHandlerMario myhandler;
        public MarioBlockTop(CollisionHandlerMario handler)
        {
            myhandler = handler;
        }

        public void Execute()
        {
            if (myhandler.block.GetType()==typeof(HiddenBlock))
            {
                return;
            }
            if (myhandler.mario.State.MarioShape == Shape.Dead)
            {
                return;
            }
            myhandler.mario.IsInAir = false;
            if (myhandler.mario.Velocity.Y > 0)
            { 
            myhandler.mario.Velocity = new Vector2(myhandler.mario.Velocity.X, 0);
            }
            myhandler.mario.Location = new Vector2(myhandler.mario.Destination.X, myhandler.block.Destination.Y - myhandler.mario.Destination.Height);
            if (myhandler.mario.State.MarioShape == Shape.Dead)
            {
                return;
            }
        }
    }
}
