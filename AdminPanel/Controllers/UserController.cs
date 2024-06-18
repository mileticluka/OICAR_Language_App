using AdminPanel.Models;
using AutoMapper;
using DAL.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdminPanel.Controllers
{
    [Authorize]
    public class UserController : Controller
    {

        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }


        // GET: UserController
        public ActionResult Index()
        {
            IList<UserViewModel> userViewModels = mapper.Map<IList<UserViewModel>>(userRepository.GetAll());

            return View(userViewModels);
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            UserViewModel userViewModel = mapper.Map<UserViewModel>(userRepository.GetUser(id));

            return View(userViewModel);
        }

      
        // GET: UserController/Delete/5
        public ActionResult Delete(int id)
        {
            userRepository.DeleteUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
