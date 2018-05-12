using System;
using System.Collections.Generic;
using System.Linq;

using System.Web;
using Century2.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Century2.WebApp.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            var employees = from e in GetEmployeeList()
                            orderby e.ID
                            where (e.Age < 30)
                            select e;
            return View(employees);
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [NonAction]
        public List<Employee> GetEmployeeList()
        {
            return new List<Employee>{
              new Employee{
                 ID = 1,
                 Name = "Allan",
                 JoiningDate = DateTime.Parse(DateTime.Today.ToString()),
                 Age = 23
              },

              new Employee{
                 ID = 2,
                 Name = "Carson",
                 JoiningDate = DateTime.Parse(DateTime.Today.ToString()),
                 Age = 45
              },

              new Employee{
                 ID = 3,
                 Name = "Carson",
                 JoiningDate = DateTime.Parse(DateTime.Today.ToString()),
                 Age = 37
              },

              new Employee{
                 ID = 4,
                 Name = "Laura",
                 JoiningDate = DateTime.Parse(DateTime.Today.ToString()),
                 Age = 26
              },
           };
        }
    }
}
