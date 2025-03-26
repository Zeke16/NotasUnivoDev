﻿using Microsoft.AspNetCore.Mvc;
using NotasUnivoDev.Db;
using NotasUnivoDev.Models;

namespace NotasUnivoDev.Controllers
{
    public class FacultiesController : Controller
    {
        private readonly AppDbContext DbContext;

        public FacultiesController(AppDbContext Db)
        {
            DbContext = Db;
        }
        public IActionResult Index()
        {
            List<FacultiesModel> list = DbContext.Faculties.ToList();
            return View(list);
        }

        [HttpGet]
        public IActionResult UpSert(int id)
        {
            FacultiesModel model = new();
            if (id > 0)
            {
               model = DbContext.Faculties.FirstOrDefault(x => x.FacultyId == id) ?? new();
            }

            return View(model);
        }

        [HttpPost]
        public IActionResult UpSert(FacultiesModel model)
        {
            if (model.FacultyId == 0)
            {
                if (ModelState.IsValid)
                {
                    DbContext.Faculties.Add(model);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    DbContext.Faculties.Update(model);
                    DbContext.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            FacultiesModel faculty = DbContext.Faculties.FirstOrDefault(row => row.FacultyId == id) ?? new();
            faculty.IsActive = !faculty.IsActive;
            DbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
