using DAL.DTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repository
{
    public class GameRepository : IGameRepository
    {
        private readonly DataContext ctx;
        private readonly Random random;

        public GameRepository(DataContext context)
        {
            ctx = context;
            random = new Random();
        }

        public Game GetGame(Type type, int id)
        {
            return GetGames<GameFillBlank>().FirstOrDefault(p => p.Id == id);
        }

        public IList<T> GetGames<T>()
        {
            if (typeof(T) == typeof(GameFillBlank)) {
                IList<GameFillBlank> games = new List<GameFillBlank>();
                games.Concat(ctx.GameFillBlank.ToList());

                games.Add(new GameFillBlank()
                {
                    Id = 1,
                    ContextImageId = 1,
                    LanguageId = 1,
                    Sentence = "Hello world"
                });

                return (IList<T>) games;
            } else if (typeof(T) == typeof(GameFlashCard)) {
                IList<GameFlashCard> games = new List<GameFlashCard>();
                games.Concat(ctx.GameFlashCard.ToList());

                games.Add(new GameFlashCard());

                return (IList<T>) games;
            } else if (typeof(T) == typeof(GamePickSentence)) {
                IList<GamePickSentence> games = new List<GamePickSentence>();
                games.Concat(ctx.GamePickSentence.ToList());

                games.Add(new GamePickSentence());

                return (IList<T>) games;
            }

            return new List<T>();
        }

        public T GetRandomGame<T>()
        {
            IList<T> games = GetGames<T>();

            T game = games[random.Next(games.Count)];
            return game;
        }
    }
}
