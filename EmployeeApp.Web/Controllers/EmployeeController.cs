using Microsoft.AspNetCore.Mvc;
using EmployeeApp.Web.Models;
using EmployeeApp.Web.Services;

namespace EmployeeApp.Web.Controllers;

public class EmployeeController : Controller
{
    private readonly EmployeeApiService _apiService;

    public EmployeeController(EmployeeApiService apiService)
    {
        _apiService = apiService;
    }

    // GET: Employee
    public async Task<IActionResult> Index()
    {
        try
        {
            var employees = await _apiService.GetAllEmployeesAsync();
            return View(employees);
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Failed to load employees: {ex.Message}";
            return View(new List<Employee>());
        }
    }

    // GET: Employee/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Employee/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Employee employee)
    {
        try
        {
            var (success, message, errors) = await _apiService.CreateEmployeeAsync(employee);
            
            if (success)
            {
                TempData["Success"] = message;
                return RedirectToAction(nameof(Index));
            }
            
            // Show validation errors
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, message);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
        }

        return View(employee);
    }

    // GET: Employee/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        try
        {
            var employee = await _apiService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Employee not found";
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Failed to load employee: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Employee/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Employee employee)
    {
        try
        {
            var (success, message, errors) = await _apiService.UpdateEmployeeAsync(id, employee);
            
            if (success)
            {
                TempData["Success"] = message;
                return RedirectToAction(nameof(Index));
            }
            
            // Show validation errors
            if (errors != null)
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(string.Empty, error);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, message);
            }
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"Error: {ex.Message}");
        }

        return View(employee);
    }

    // GET: Employee/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var employee = await _apiService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Employee not found";
                return RedirectToAction(nameof(Index));
            }
            return View(employee);
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Failed to load employee: {ex.Message}";
            return RedirectToAction(nameof(Index));
        }
    }

    // POST: Employee/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var (success, message) = await _apiService.DeleteEmployeeAsync(id);
            
            if (success)
            {
                TempData["Success"] = message;
            }
            else
            {
                TempData["Error"] = message;
            }
        }
        catch (Exception ex)
        {
            TempData["Error"] = $"Failed to delete employee: {ex.Message}";
        }

        return RedirectToAction(nameof(Index));
    }
}
