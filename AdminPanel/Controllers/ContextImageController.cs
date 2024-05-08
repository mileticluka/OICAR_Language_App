using AutoMapper;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class ContextImageController : Controller
    {

        private readonly IContextImageRepository contextImageRepository;

        public ContextImageController(IContextImageRepository contextImageRepository)
        {

            this.contextImageRepository = contextImageRepository;

        }

        // GET: ContextImageController
        public ActionResult Index()
        {
            return View(contextImageRepository.GetContextImages());
        }

        // GET: ContextImageController/Details/5
        public ActionResult Details(int id)
        {
            return View(contextImageRepository.GetContextImage(id));
        }

        // GET: ContextImageController/Delete/5
        public ActionResult Delete(int id)
        {
            contextImageRepository.DeleteContextImage(contextImageRepository.GetContextImage(id));
            return RedirectToAction(nameof(Index));
        }

    }
}
