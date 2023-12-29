using Microsoft.AspNetCore.Mvc;
using TaskManagement.File;

namespace TaskManagement.Controllers;

public class ProjectController : Controller
{
    private readonly FileManager _fileManager = new();
    public IActionResult Index()
    {
        return View(_fileManager.Projects);
    }
}