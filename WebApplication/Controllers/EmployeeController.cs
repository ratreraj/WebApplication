using Microsoft.AspNetCore.Mvc;

using WebApplication.Models;
using System.Text.Json;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using System;

namespace WebApplication.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            var data = TempData["Employee"];
            TempData.Keep("Employee");

            var data2 = TempData.Peek("Employee");
           


            EmployeeModel employeeModel = JsonSerializer.Deserialize<EmployeeModel>(data.ToString());
            List<EmployeeModel> employees = new List<EmployeeModel>();
            employees.Add(employeeModel);
            EmployeeModel employeeModel2 = JsonSerializer.Deserialize<EmployeeModel>(HttpContext.Session.GetString("emp"));


            return View(employees);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeModel employeeModel)
        {


            TempData["Employee"] = JsonSerializer.Serialize(employeeModel);
            TempData["Message"]=  "Hello Guys wellcome in my class";
            string msg = "Hi Welcome my class";
            HttpContext.Session.SetString("Msg", msg);
            HttpContext.Session.SetString("emp", JsonSerializer.Serialize(employeeModel));

            Response.Cookies.Append("non-persistent", "my non persistent cookies");
            Response.Cookies.Append("persistent", "my persistent cookies", new CookieOptions { Expires= DateTime.Now.AddDays(1) });

            return RedirectToAction("Index");
        }
    }
}
