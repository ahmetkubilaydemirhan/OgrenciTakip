using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using FBUOgrenciTakip.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FBUOgrenciTakip.Controllers
{
    public class OgretmenController : Controller
    {
        IRepository<Ogretmen> _repository;
        IRepository<Ogrenci> _ogrRepository;
        IMemoryCache _memoryCache;
        public OgretmenController(IRepository<Ogretmen> repository, 
            IRepository<Ogrenci> ogrRepository,
            IMemoryCache memoryCache)
        {
            _repository = repository;
            _ogrRepository = ogrRepository;
            _memoryCache = memoryCache;
        }
        // GET: OgretmenController
        public ActionResult Index()
        {
            List<OgretmenViewModel> model;
            if (_memoryCache.TryGetValue<List<OgretmenViewModel>>("ogretmeListesi", out model))
            {
                return View(model);
            }
            model = new List<OgretmenViewModel>();
            List<Ogretmen> liste = _repository.List();
            foreach (var ogr in liste)
            {
                OgretmenViewModel ogrVm = new OgretmenViewModel();
                ogrVm.Id = ogr.Id;
                ogrVm.Ad = ogr.Ad;
                ogrVm.Soyad = ogr.Soyad;
                model.Add(ogrVm);
            }
            _memoryCache.Set("ogretmeListesi", model);
            return View(model);
        }

        // GET: OgretmenController/Details/5
        public ActionResult Details(int id)
        {
            Ogretmen ogr = _repository.GetById(id);
            OgretmenViewModel model = new OgretmenViewModel();

            string jsonStr=JsonSerializer.Serialize(ogr);
            model = JsonSerializer.Deserialize<OgretmenViewModel>(jsonStr);


            List<Ogrenci> ogrencilerTum = _ogrRepository.List();

            List<Ogrenci> atanmisOgrenciler = ogrencilerTum.Where(c => c.OgretmenId == ogr.Id).ToList();
            List<Ogrenci> bosOgrenciler = ogrencilerTum.Where(c => !c.OgretmenId.HasValue ||c.OgretmenId==0).ToList();

            string atanmisStr = JsonSerializer.Serialize(atanmisOgrenciler);
            model.AtanmisOgrenciler = JsonSerializer.Deserialize<List<OgrenciViewModel>>(atanmisStr);

            string bosStr = JsonSerializer.Serialize(bosOgrenciler);
            model.BosOgrenciler = JsonSerializer.Deserialize<List<OgrenciViewModel>>(bosStr);

            //model.BosOgrenciler = new List<OgrenciViewModel>();
            //foreach(Ogrenci ogrenci in bosOgrenciler)
            //{
            //    OgrenciViewModel ovm = new OgrenciViewModel();
            //    ovm.Ad = ogrenci.Ad;
            //    ovm.Id = ogrenci.Id;
            //    ovm.OgretmenId = ogrenci.OgretmenId;
            //    ovm.Soyad = ogrenci.Soyad;
            //    ovm.Tel = ogrenci.Tel;
            //    model.BosOgrenciler.Add(ovm);
            //}


            return View(model);
        }

        // GET: OgretmenController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: OgretmenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: OgretmenController/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue || id.Value <= 0)
            {
                return View(new OgretmenViewModel());
            }

            Ogretmen ogretmen = _repository.GetById(id.Value);

            OgretmenViewModel model = new OgretmenViewModel()
            {
                Id = ogretmen.Id,
                Ad = ogretmen.Ad,
                Soyad = ogretmen.Soyad
            };

            return View(model);
        }

        // POST: OgretmenController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(OgretmenViewModel model)
        {
            Ogretmen ogretmen = new Ogretmen() { Id = model.Id, Ad = model.Ad, Soyad = model.Soyad };
            _repository.AddOrUpdate(ogretmen);
            return RedirectToAction("Index");
        }

        // GET: OgretmenController/Delete/5
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }



    }
}
