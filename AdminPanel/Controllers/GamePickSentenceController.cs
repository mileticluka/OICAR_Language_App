using AutoMapper;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class GamePickSentenceController : Controller
    {

        private readonly IGameRepository gameRepository;
        private readonly ILanguageRepository languageRepository;
        private readonly IContextImageRepository contextImageRepository;
        private readonly IMapper mapper;

        public GamePickSentenceController(IGameRepository gameRepository, IMapper mapper, ILanguageRepository languageRepository, IContextImageRepository contextImageRepository)
        {
            this.gameRepository = gameRepository;
            this.languageRepository = languageRepository;
            this.contextImageRepository = contextImageRepository;
            this.mapper = mapper;
        }

        // GET: GameFillBlankController
        public ActionResult Index()
        {
            IList<GamePickSentence> games = gameRepository.GetGames<GamePickSentence>();

            return View(games);
        }

        // GET: GameFillBlankController/Details/5
        public ActionResult Details(int id)
        {
            GamePickSentence? game = gameRepository.FindGameById<GamePickSentence>(id);

            if (game != null)
            {
                return View(game);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: GameFillBlankController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: GameFillBlankController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GamePickSentence game)
        {
            try
            {
                var img = contextImageRepository.GetContextImage(game.ContextImage.ImagePath);

                if (img != null)
                {
                    game.ContextImageId = img.Id;
                    game.ContextImage = img;
                }

                gameRepository.CreateGame(game);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GameFillBlankController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(gameRepository.FindGameById<GamePickSentence>(id));
        }

        // POST: GameFillBlankController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, GamePickSentence game)
        {


            try
            {
                var img = contextImageRepository.GetContextImage(game.ContextImage.ImagePath);

                if (img != null)
                {
                    game.ContextImageId = img.Id;
                    game.ContextImage = img;
                }

                gameRepository.UpdateGame(game);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: GameFillBlankController/Delete/5
        public ActionResult Delete(int id)
        {
            var game = gameRepository.FindGameById<GamePickSentence>(id);
            gameRepository.DeleteGame(game);
            return RedirectToAction(nameof(Index));
        }
    }
}
