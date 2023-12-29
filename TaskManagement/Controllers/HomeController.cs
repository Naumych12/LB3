using Microsoft.AspNetCore.Mvc;
using TaskManagement.File;
using TaskManagement.Models;

namespace TaskManagement.Controllers;

public class HomeController : Controller
{
    private readonly FileManager _fileManager = new();
    public IActionResult Index()
    {
        return View(_fileManager.Issues);
    }
    
    public IActionResult DeleteIssue(Guid id)
    {
        _fileManager.RemoveIssue(id);
        return RedirectToAction("Index");
    }
    
    public IActionResult EditIssue(Guid id)
    {
        ViewBag.Projects = _fileManager.GetProjectsListItems();
        ViewBag.Employees = _fileManager.GetEmployeesListItems();
        var issue = _fileManager.GetIssue(id);
        return View(issue);
    }
    
    public IActionResult EditedIssue(Issue issue)
    {
        _fileManager.UpdateIssue(issue);
        return RedirectToAction("Index");
    }
    
    public IActionResult AddIssue()
    {
        ViewBag.Projects = _fileManager.GetProjectsListItems();
        ViewBag.Employees = _fileManager.GetEmployeesListItems();
        return View("EditIssue", new Issue());
    }
    
    public IActionResult AddedIssue(Issue issue)
    {
        issue.Id = Guid.NewGuid();
        issue.Created = DateTime.Now;
        _fileManager.AddIssue(issue);
        return RedirectToAction("Index");
    }
}