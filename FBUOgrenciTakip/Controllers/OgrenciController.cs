using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FBUOgrenciTakip.Controllers
{
    public class OgrenciController : Controller
    {
        IRepository<Ogrenci> _ogrRepository;
        NotRepository _notRepository;
        IMemoryCache _memCache;
        public OgrenciController(IRepository<Ogrenci> ogrRepository, NotRepository notRepository,
            IMemoryCache memCache
            )
        {
            _ogrRepository = ogrRepository;
            _notRepository = notRepository;
            _memCache = memCache;
        }

        public IActionResult Index()
        {
            var basTar=DateTime.Now;
            
            List<Ogrenci> model =(List<Ogrenci>)_memCache.Get("ogrList");
                if(model==null)
            {
                model=_ogrRepository.List();
                _memCache.Set("ogrList", model,new TimeSpan(0,0,20));
            }
            var bitTar = DateTime.Now;
            ViewData["duration"] = (bitTar - basTar).TotalMilliseconds;
            return View(model);
        }

        [HttpPost]
        public IActionResult Index(string aranacak, string aranacakSoyad)
        {
            //var result = _ogrRepository.GetByIdGeneric(1);
            //var resultNot = _notRepository.GetByIdGeneric(4);

            List<Ogrenci> model = _ogrRepository.List();
            if (!String.IsNullOrEmpty(aranacak))
            {
                model = model.Where(c => c.Ad.ToUpper() == aranacak.ToUpper()).ToList();
            }

            if (!String.IsNullOrEmpty(aranacakSoyad))
            {
                model = model.Where(c => c.Soyad.ToUpper().StartsWith(aranacakSoyad.ToUpper())).ToList();
            }
            ViewBag.aranacak = aranacak;
            ViewBag.aranacakSoyad = aranacakSoyad;

            return View(model);
        }

        public IActionResult OgrListPartial(string aranacak, string aranacakSoyad)
        {
            if (_ogrRepository is OgrRepository)
            {
                //  List<Ogrenci> model = ((OgrRepository)_ogrRepository).Search(aranacak, aranacakSoyad);
                List<Ogrenci> model = (_ogrRepository as OgrRepository).Search(aranacak, aranacakSoyad);
                return View(model);
            }

            return View(new List<Ogrenci>());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Ogrenci ogr)
        {
            _ogrRepository.AddOrUpdate(ogr);
            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Ogrenci ogr = _ogrRepository.GetById(id);
            return View(ogr);
        }
        [HttpPost]
        public IActionResult Edit(Ogrenci ogr)
        {
            if (Request.Form.Files.Count() > 0)
            {
                IFormFile file1 = Request.Form.Files[0];

                string fileName = Guid.NewGuid() + ".jpg";
                string filePath = $"wwwroot/images/{fileName}";
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file1.CopyTo(fileStream);
                }
                ogr.FileName = fileName;
            }
            _ogrRepository.AddOrUpdate(ogr);
            _memCache.Set("ogrList",_ogrRepository.List());
            //_memCache.Remove("ogrList");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditOgrByAjax(Ogrenci ogr)
        {
            _ogrRepository.AddOrUpdate(ogr);
            return Json(new { success = true, ogr });
        }

        public IActionResult Details(int id)
        {
            Ogrenci ogr = _ogrRepository.GetById(id);
            return View(ogr);
        }

        public IActionResult Details1(int id)
        {
            
            return View(id);
        }

        public IActionResult AddNote(int id)
        {
            Ogrenci ogr = _ogrRepository.GetById(id);
            Not nt = new Not();
            nt.Ogr = ogr;
            nt.OgrId = ogr.Id;
            return View(nt);
        }
        
        [HttpPost]
        public IActionResult AddNote(Not nt)
        {
            List<myConfig> cfgs = (_ogrRepository as OgrRepository).listCfg();

            myConfig cfg = cfgs.First(c => c.Key == "maxNoteCount");
            int notCount = Convert.ToInt32(cfg.Value);

            myConfig cfgMessage = cfgs.First(c => c.Key == "maxNoteMessage");

            Ogrenci ogr = _ogrRepository.GetById(nt.OgrId);
           
            if(ogr.Nots.Count>= notCount)
            {
                ModelState.AddModelError("Text", cfgMessage.Value);
            }

            if(!ModelState.IsValid)
            {
                nt.Ogr = ogr;
                return View(nt);
            }

            nt=_notRepository.AddOrUpdate(nt);
            return RedirectToAction("Details",new { id = nt.OgrId });
        }



        public IActionResult Delete(int id)
        {
            _ogrRepository.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult NotDelete(int id, int ogrId)
        {
           // var silinecekNot = _notRepository.GetById(id);
            _notRepository.Delete(id);
            return RedirectToAction("Details",new { id= ogrId });
        }

        public IActionResult NotEdit(int id, int ogrId)
        {
            Not editlenecekNot = _notRepository.GetById(id);
            editlenecekNot.Ogr = _ogrRepository.GetById(editlenecekNot.OgrId);
            //editlenecekNot.og
            return View(editlenecekNot);
        }

        [HttpPost]
        public IActionResult NotEdit(Not nt)
        {
            _notRepository.AddOrUpdate(nt);

            return RedirectToAction("Details", new { id = nt.OgrId });
        }

        public JsonResult SaveNote(int ogrId, string text)
        {
            Not nt = new Not();
            nt.OgrId = ogrId;
            nt.Text = text;
            
            if(nt.Text.Length<5)
            {
                return Json(new { success = false, hata = "not çok kısa" });
            }
            
            nt=_notRepository.AddOrUpdate(nt);
            return Json(new { success=true,notId=nt.Id});
        }

        public IActionResult NotListByOgrId(int ogrId)
        {
            List<Not> model = _notRepository.ListByOgrId(ogrId);
            return PartialView(model);
        }

        public JsonResult deleteNoteAjax(int id)
        {
            _notRepository.Delete(id);
            return Json(true);
        }
    }
}
