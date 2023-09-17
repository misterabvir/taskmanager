using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModels;

public class ProjectsViewModel
{
    public Guid Id { get; set; }
    public string? ProjectName { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public IEnumerable<TaskViewModel>? Tasks { get; set; }
}

public class ProjectsListViewModel
{
    public Guid? Id { get; set; }
    public string? ProjectName { get; set; }
}
