﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMario.Interfaces
{
    public enum GameStates
    {
        Title, Demo, Playing, Pause, LevelComplete, 
        LifeDisplay, GameOver, Victory, Transition
    }

    public interface IGameState
    {
        GameStates Type { get; }
        void Proceed();
        void PlayDemo();
        void Pause();
        void MarioDied();
        void GameOver();
        void Transit();
    }
}
