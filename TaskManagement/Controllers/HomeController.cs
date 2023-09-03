using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Web.Mvc;
using TaskManagement.Models;

namespace TaskManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository _repository;

        public HomeController() : this(new Repository())
        {
        }
        public HomeController(IRepository repository)
        {
            _repository = repository;
        }
        public ActionResult Index()
        {
            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            return View("Index");
        }

        public ActionResult TaskAssigning()
        {
            try
            {
                var projectListItems = _repository.GetProjects().Select(p => new SelectListItem { Text = p.Name, Value = p.ProjectId.ToString() }).ToList();
                ViewBag.ProjectList = new SelectList(projectListItems, "Value", "Text");
                return View("TaskAssigning");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpPost]
        public ActionResult TaskAssigning(Task newTask)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _repository.AssignTask(newTask);
                    TempData["SuccessMessage"] = "Task Assigned successfully";
                    return RedirectToAction("Index");
                }
                return View("TaskAssigning");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult GetEmployeesList(int projectId)
        {
            try
            {
                var employees = _repository.GetEmployeesByProject(projectId);
                return Content(JsonConvert.SerializeObject(employees), "application/json");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        public ActionResult TaskView()
        {
            try
            {
                var projectListItems = _repository.GetProjects().Select(p => new SelectListItem { Text = p.Name, Value = p.ProjectId.ToString() }).ToList();
                projectListItems.Insert(0, new SelectListItem { Text = "All Projects", Value = "0" });
                ViewBag.ProjectList = new SelectList(projectListItems, "Value", "Text");
                return View("TaskView");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult GetTasks(int projectId)
        {
            try
            {
                var tasks = _repository.GetTasksByProject(projectId);
                return Content(JsonConvert.SerializeObject(tasks), "application/json");
            }
            catch (Exception)
            {
                return View("Error");
            }
        }
    }
}