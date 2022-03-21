using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using FBUOgrenciTakip.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FBUOgrenciTakip.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        IRepository<Ogrenci> _ogrenciRepository;
        public HomeController(ILogger<HomeController> logger, IRepository<Ogrenci> ogrenciRepository)
        {
            _ogrenciRepository = ogrenciRepository;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var ogrenciler = _ogrenciRepository.List();
            ViewData["ogrenciler"] = ogrenciler;
            return View();
        }

        
        public double GetNumber(int sayi, int us)
        {
            double result = 1;
            for(int i=0;i<us;i++)
            {
                //result = result * sayi;
                result *= sayi;
            }
            return result;
          //  return Math.Pow(sayi, us);
          
        }

        public JsonResult GetUserByTel(string sacma)
        {
           
            List<Ogrenci> liste = _ogrenciRepository.List();
            Ogrenci result = liste.FirstOrDefault(c => c.Tel == sacma);

            if (result == null)
                return new JsonResult(new { success = false,yazi="KAyıt bulunamıyorduuuu..." });
            else
                return new JsonResult(new { success = true, data = result });
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
