using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Web.Controllers;

public class EmployeeController : Controller
{
    // GET: Employee
    public IActionResult Index()
    {
        return View();
    }
}
