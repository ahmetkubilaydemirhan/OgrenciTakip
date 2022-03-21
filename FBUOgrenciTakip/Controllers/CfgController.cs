using DataAccessLayer.Entities;
using DataAccessLayer.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FBUOgrenciTakip.Controllers
{
    public class CfgController : Controller
    {
        IRepository<myConfig> _repository;
        public CfgController(IRepository<myConfig> repository)
        {
            _repository = repository;
        }
        // GET: CfgController
        public ActionResult Index()
        {
            var model = _repository.List();
            return View(model);
        }

        // GET: CfgController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CfgController/Create
        public ActionResult Create()
        {
            myConfig model = new myConfig();
            return View(model);
        }

        // POST: CfgController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(myConfig entity)
        {
            _repository.AddOrUpdate(entity);
            return RedirectToAction("Index");
          
            
        }

        // GET: CfgController/Edit/5
        public ActionResult Edit(int id)
        {
            myConfig model = _repository.GetById(id);

            return View(model);
        }

        // POST: CfgController/Edit/5
    

        // GET: CfgController/Delete/5
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }

        // POST: CfgController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
