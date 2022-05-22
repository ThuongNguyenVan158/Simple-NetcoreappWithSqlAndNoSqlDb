using EmployeeManagementWithMongoDb.Data;
using EmployeeManagementWithMongoDb.Models;
using EmployeeManagementWithMongoDb.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeManagementWithMongoDb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICrudEmployeeService _crudEmployeeService;

        public HomeController(ILogger<HomeController> logger, ICrudEmployeeService crudEmployeeService)
        {
            _logger = logger;
            _crudEmployeeService = crudEmployeeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listEmployee = await _crudEmployeeService.GetAsync();
            return View(listEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(string employeeName, DateTime birthdate,
           string email, string phone, DateTime timeStartWorking)
        {
            var newEmployee = new Employee()
            {
                Name = employeeName,
                BirthDate = birthdate,
                Email = email,
                Phone = phone,
                TimeStartWork = timeStartWorking
            };
            await _crudEmployeeService.CreateAsync(newEmployee);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateInfo(string id)
        {
            var employeeInfo = await _crudEmployeeService.GetAsync(id);
            return View(employeeInfo);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateInfo(Employee employeeVM)
        {
            await _crudEmployeeService.UpdateAsync(employeeVM._id, employeeVM);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteEmployee(string id)
        {
            await _crudEmployeeService.RemoveAsync(id);
            return RedirectToAction("Index");
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