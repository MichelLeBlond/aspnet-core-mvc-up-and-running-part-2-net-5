
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using ShoppingCart_DataAccess.Data;
using ShoppingCart_Models;
using ShoppingCart_Utility;
using ShoppingCart_DataAccess.Repository.IRepository;

namespace ShoppingCart.Controllers
{
    [Authorize(Roles = WC.AdminRole)]
    public class ApplicationTypeController : Controller
    {
        private readonly IApplicationTypeRepository _appTypeRepo;

        public ApplicationTypeController(IApplicationTypeRepository appTypeRepo)
        {
            _appTypeRepo = appTypeRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<ApplicationType> objLIst = _appTypeRepo.GetAll();
            return View(objLIst);
        }
        // Get - Create
        public IActionResult Create()
        {
            return View();
        }
        // Post - Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _appTypeRepo.Add(obj);
                _appTypeRepo.Save();
                TempData[WC.Success] = "Application Type is created";
                return RedirectToAction("Index");
            }

            return View(obj);
        }
        // Get - Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _appTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // Post - Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType obj)
        {
            if (ModelState.IsValid)
            {
                _appTypeRepo.Update(obj);
                _appTypeRepo.Save();
                TempData[WC.Success] = "Application Type is updated";
                return RedirectToAction("Index");
            }

            return View(obj);
        }


        // Get - Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var obj = _appTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        // Post - Det
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _appTypeRepo.Find(id.GetValueOrDefault());
            if (obj == null)
            {
                return NotFound();
            }

            _appTypeRepo.Remove(obj);
            _appTypeRepo.Save();
            TempData[WC.Success] = "Application Type is removed";
            return RedirectToAction("Index");

        }




    }
}
