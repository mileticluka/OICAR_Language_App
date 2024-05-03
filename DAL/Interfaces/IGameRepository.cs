﻿using DAL.DTO;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IGameRepository
    {
        public IList<T> GetGames<T>(Language language) where T : Game;
        public T GetRandomGame<T>(Language language) where T : Game;

        // Game tracking
        public T GetGame<T>(int userId) where T : Game;
        public void StartGame(int userId, Game game);
        public bool IsPlaying(int userId);
        public void EndGame(int userId);
    }
}