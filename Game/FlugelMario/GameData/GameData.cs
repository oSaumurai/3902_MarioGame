﻿using SuperMario.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario
{
    public class GameData 
    {
        // just in case we forget these data
        public static GameObjectManager GameObjectManager { get {return objM; } set { } }
        private static GameObjectManager objM;
        public GameData(GameObjectManager ObjM)
        {
            objM = ObjM;
        }
        public static int MarioJumpingSpeed = -8;
        public static float Gravity { get; set; } = 0.4f;
        public static float GoombaSpeed { get; } = .75f;
        public static float MarioCriticalSpeed { get; } = .75f;
        public static float MarioAccel { get; } = .25f;
        public static int marioBouncingSpeed { get; } = -2;
        public static int flagPoleBottomY { get; } = 368;
        public static float Float { get; } = -0.1f;
        public static float marioInWaterAcc { get; } = 0.2f;
        public static float marioInWaterJump { get; } = -5;
    }
}
