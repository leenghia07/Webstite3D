using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using WebApp.Data;
using WebApp.Models;
using WebApp.Models.ViewModel;

namespace WebApp.Controllers
{
    public class MuseumController : Controller
    {
        private readonly MuseumDataContext _MuseumDataContext;
        public MuseumController(MuseumDataContext museumDataContext)
        {
            _MuseumDataContext = museumDataContext;
        }
        /*public async Task<ActionResult> Index()
        {
            var MuseumVM = new VMMuseum();
            MuseumVM.Museums = await _MuseumDataContext.Museum.ToListAsync();
            return View(MuseumVM);
        }*/
        public IActionResult Index()
        {
            /* var MuseumVM = new VMMuseum();
             MuseumVM.Museums = await _MuseumDataContext.Museum.ToListAsync();
             return View(Museum);*/
            return View();
        }


    }
}
