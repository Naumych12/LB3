using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TaskManagement.Models;

namespace TaskManagement.File;

public class FileManager
{
    private const string IssuesPath = "Tables/Issue.txt";
    private const string EmployeesPath = "Tables/Employee.txt";
    private const string ProjectsPath = "Tables/Project.txt";
        
    public List<Issue> Issues { get; }
    public List<Employee> Employees { get;}
    public List<Project> Projects { get; }

    public FileManager()
    {
        using StreamReader reader1 = new StreamReader(EmployeesPath);
        Employees = JsonConvert.DeserializeObject<List<Employee>>(reader1.ReadToEnd());

        using StreamReader reader2 = new StreamReader(ProjectsPath);
        Projects = JsonConvert.DeserializeObject<List<Project>>(reader2.ReadToEnd());

        using StreamReader reader3 = new StreamReader(IssuesPath);
        Issues = JsonConvert.DeserializeObject<List<Issue>>(reader3.ReadToEnd()).OrderByDescending(x => x.Created).ToList();
        foreach (var issue in Issues)
        {
            issue.Project = Projects.FirstOrDefault(x => x.Id == issue.ProjectId);
            issue.Employee = Employees.FirstOrDefault(x => x.Id == issue.EmployeeId);
        }
    }
    
    public List<SelectListItem> GetIssuesListItems()
    {
        List<SelectListItem> selectListItems = new List<SelectListItem>();
        foreach (var issue in Issues)
        {
            selectListItems.Add(new (issue.Summary, issue.Id.ToString()));
        }
        return selectListItems;
    }
    
    public List<SelectListItem> GetEmployeesListItems()
    {
        List<SelectListItem> selectListItems = new List<SelectListItem>();
        foreach (var employee in Employees)
        {
            selectListItems.Add(new (employee.Name, employee.Id.ToString()));
        }
        return selectListItems;
    }
    
    public List<SelectListItem> GetProjectsListItems()
    {
        List<SelectListItem> selectListItems = new List<SelectListItem>();
        foreach (var project in Projects)
        {
            selectListItems.Add(new (project.Name, project.Id.ToString()));
        }
        return selectListItems;
    }
    
    public void RemoveIssue(Guid id)
    {
        Issues.RemoveAll(x => x.Id == id);
        SaveIssues();
    }

    public void AddIssue(Issue issue)
    {
        Issues.Add(issue);
        SaveIssues();
    }
    
    public void UpdateIssue(Issue issue)
    {
        Issues.RemoveAll(x => x.Id == issue.Id);
        Issues.Add(issue);
        SaveIssues();
    }
    
    public Issue GetIssue(Guid id)
    {
        return Issues.FirstOrDefault(x => x.Id == id);
    }
    
    public Employee GetEmployee(Guid id)
    {
        return Employees.FirstOrDefault(x => x.Id == id);
    }

    public void SaveIssues()
    {
        using StreamWriter streamWriter = new StreamWriter(IssuesPath, false);
        streamWriter.Write(JsonConvert.SerializeObject(Issues));
    }
}
