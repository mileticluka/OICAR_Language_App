using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext ctx;
        private readonly Random random;
        private static Dictionary<int, Game> activeGames = new Dictionary<int, Game>();

        public GameRepository(DataContext context)
        {
            this.ctx = context;
            this.random = new Random();
        }
        
        public IList<T> GetGames<T>(Language language) where T : Game
        {
            List<T> games = new List<T>();

            if (typeof(T) == typeof(GameFillBlank)) {
                var list = (IList<T>) ctx.GameFillBlank.Include(gfb => gfb.ContextImage).Include(lan => lan.Language).ToList();
                games.AddRange(list);
            } else if (typeof(T) == typeof(GameFlashCard)) { 
                var list = (IList<T>)ctx.GameFlashCard.Include(gfb => gfb.ContextImage).Include(lan => lan.Language).ToList();
                games.AddRange(list);
            } else if (typeof(T) == typeof(GamePickSentence)) {
                var list = (IList<T>)ctx.GamePickSentence.Include(gfb => gfb.ContextImage).Include(lan => lan.Language).ToList();
                games.AddRange(list);
            }

            games = games.Where(g => g.LanguageId == language.Id).ToList();

            return (IList<T>) games;
        }

        public T GetRandomGame<T>(Language language) where T : Game
        {
            IList<T> games = GetGames<T>(language);

            T game = games[random.Next(games.Count)];
            return game;
        }

        public T GetGame<T>(int userId) where T : Game
        {
            return (T)activeGames[userId];
        }

        public bool IsPlaying(int userId)
        {
            return activeGames.ContainsKey(userId);
        }

        public void StartGame(int userId, Game game)
        {
            activeGames[userId] = game;
        }

        public void EndGame(int userId)
        {
            activeGames.Remove(userId);
        }
    }
}
