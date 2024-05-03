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
            // TODO: actually filter by language

            if (typeof(T) == typeof(GameFillBlank)) {
                var list = ctx.GameFillBlank.Include(gfb => gfb.ContextImage);
                IList<GameFillBlank> games = list.ToList();

                return (IList<T>) games;
            } else if (typeof(T) == typeof(GameFlashCard)) {
                IList<GameFlashCard> games = ctx.GameFlashCard.ToList();

                return (IList<T>) games;
            } else if (typeof(T) == typeof(GamePickSentence)) {
                IList<GamePickSentence> games = ctx.GamePickSentence.ToList();
                
                return (IList<T>) games;
            }

            return new List<T>();
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
