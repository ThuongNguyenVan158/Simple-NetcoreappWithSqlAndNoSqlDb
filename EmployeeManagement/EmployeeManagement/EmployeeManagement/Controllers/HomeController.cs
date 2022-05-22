using EmployeeManagement.Data;
using EmployeeManagement.Models;
using EmployeeManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeeManagement.Controllers
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
        public IActionResult Index()
        {
            var listEmployee = _crudEmployeeService.GetAsync();
            return View(listEmployee);
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(string employeeName, DateTime birthdate,
           string email, string phone, DateTime timeStartWorking)
        {
            var newEmployee = new EmployeeViewModel()
            {
                Id  = System.Guid.NewGuid(),
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
        public async Task<IActionResult> UpdateInfo(Guid id)
        {
            var employeeInfo = await _crudEmployeeService.GetAsync(id);
            return View(employeeInfo);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateInfo(EmployeeViewModel employeeVM)
        {
            await _crudEmployeeService.UpdateAsync(employeeVM.Id, employeeVM);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> DeleteEmployee(Guid id)
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