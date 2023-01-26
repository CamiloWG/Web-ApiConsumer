using ApiConsumer.Models;
using ApiConsumer.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ApiConsumer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServiceAPI _serviceAPI;

        public HomeController(IServiceAPI serviceAPI)
        {
            _serviceAPI = serviceAPI;
        }

        public async Task<IActionResult> Index()
        {
            List<Custommer> lista = await _serviceAPI.GetAllCustommers();
            return View(lista);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string CustomerId)
        {
            var respuesta = await _serviceAPI.DeleteCustommer(CustomerId);
            return respuesta ? RedirectToAction("Index") : NoContent();
        }

        public async Task<IActionResult> ViewCustommer(string CustomerId)
        {
            Custommer CurrentClient = new Custommer();

            ViewBag.action = "Nuevo Cliente";
            ViewBag.newClient = true;

            if (!string.IsNullOrEmpty(CustomerId))
            {
                CurrentClient = await _serviceAPI.GetCustommerById(CustomerId);
                ViewBag.action = "Editar Cliente";
                ViewBag.newClient = false;
            }

            return View(CurrentClient);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCustommer(Custommer custommer, bool newClient)
        {
            bool respuesta = newClient ? await _serviceAPI.InsertCustommer(custommer) : await _serviceAPI.UpdateCustommer(custommer); 
            

            return respuesta ? RedirectToAction("Index") : NoContent();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}