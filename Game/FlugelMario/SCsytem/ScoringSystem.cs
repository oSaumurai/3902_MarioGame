﻿using Microsoft.Xna.Framework;
using SuperMario.Animation;
using SuperMario.GameObjects;
using SuperMario.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMario.DisplayPanel;
using SuperMario.Sound;
using SuperMario;

namespace SuperMario.SCsystem
{
    public class ScoringSystem
    {
        public static int score = 0;
        public static int Score { get { return score; } }
        public static int flagScore = 0;

        private static List<IGameObject> flagParts=new List<IGameObject>();

        private static int FlagHeight1 = 384;
        private static int FlagHeight2 = 367;
        private static int FlagHeight3 = 327;
        private static int FlagHeight4 = 303;
        private static int FlagHeight5 = 257;
        private static int CutOffScore5 = 100;
        private static int CutOffScore4 = 400;
        private static int CutOffScore3 = 800;
        private static int CutOffScore2 = 2000;
        private static int CutOffScore1 = 4000;

        public static void RegisgerFlagPole(IGameObject pole)
        {
            flagParts.Add(pole);
        }
        public static void ResetScore()
        {
            score = 0;
        }

        public static void AddPointsForCollectingPwrUp(IGameObject PUitem)
        {
            score += 1000;
            ScoreAnimation(PUitem, "1000");
        }

        public static void AddPointsForCoin(IGameObject coin)
        {
            score += 200;
            ScoreAnimation(coin, "200");
        }

        public static void AddPointsForGMush(IGameObject GMush)
        {
            ScoreAnimation(GMush, "1UP");
        }

        public static void AddPointsForStompingEnemy(IGameObject enemy)
        {
            score += 100;
            ScoreAnimation(enemy, "100");
        }

        public static void AddPointsForPole(Rectangle marioDestination)
        {
            if (marioDestination.Y <= FlagHeight5)
            {
                flagScore = CutOffScore1;
                MarioInfo.MarioLife[0]++;
                CreateNewScoreAnimation(marioDestination, flagParts[4].Destination, "" + flagScore);
                CreateNewScoreAnimation(marioDestination, flagParts[4].Destination, "1UP");
            }
            else if(marioDestination.Y < FlagHeight4)
            {
                flagScore = CutOffScore2;
                CreateNewScoreAnimation(marioDestination, flagParts[3].Destination, "" + flagScore);
            }
            else if (marioDestination.Y < FlagHeight3)
            {
                flagScore = CutOffScore3;
                CreateNewScoreAnimation(marioDestination, flagParts[2].Destination, "" + flagScore);
            }
            else if (marioDestination.Y < FlagHeight2)
            {
                flagScore = CutOffScore4;
                CreateNewScoreAnimation(marioDestination, flagParts[1].Destination, "" + flagScore);
            }
            else if (marioDestination.Y < FlagHeight1)
            {
                flagScore = CutOffScore5;
                CreateNewScoreAnimation(marioDestination, flagParts[0].Destination, "" + flagScore);
            }
            score += flagScore;
            //CreateNewScoreAnimation(marioDestination, flagParts[flagParts.Count - 1].Destination, "" + flagScore);
        }
        public static void AddPointsForRestTime()
        {
            score += 10;
            SoundManager.Instance.PlayCoinSound();
        }

        private static void ScoreAnimation(IGameObject obj, String scoreToDisplay)
        {
            Rectangle objDestination = obj.Destination;
            Vector2 location = new Vector2(objDestination.X, objDestination.Y);
            IAnimationInGame scoreAnimation = new ScoreAnimation(location, scoreToDisplay);
            scoreAnimation.StartAnimation();
        }

        private static void CreateNewScoreAnimation(Rectangle marioDestination, Rectangle poleDestination, String scoreToDisplay)
        {
            IAnimationInGame scoreAnimation = new PoleScoreAnimation(marioDestination, poleDestination, scoreToDisplay);
            scoreAnimation.StartAnimation();
        }
    }
}