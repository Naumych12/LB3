using Microsoft.AspNetCore.Mvc;
using TaskManagement.File;

namespace TaskManagement.Controllers;

public class EmployeeController : Controller
{
    private readonly FileManager _fileManager = new();
    public IActionResult Index()
    {
        return View(_fileManager.Employees);
    }
}