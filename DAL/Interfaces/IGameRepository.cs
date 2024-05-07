using DAL.DTO;
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
        IList<T> GetGames<T>() where T : Game;
        public IList<T> GetGames<T>(Language language) where T : Game;
        public T GetRandomGame<T>(Language language) where T : Game;

        // Game tracking
        public T GetGame<T>(int userId) where T : Game;
        public void StartGame(int userId, Game game);
        public bool IsPlaying(int userId);
        public void EndGame(int userId);

        //CRUD FOR ADMIN PANEL
        void DeleteGame(Game game);
        void UpdateGame(Game game);
        void CreateGame(Game game);
        T? FindGameById<T>(int id) where T : Game;
    }
}
