using TaskManager.ViewModels.Utils;

namespace TaskManager.ViewModels;

public class ProjectViewModel
{
    public Guid ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public string CreateDateFormat => CreateDate.GetFormatString();
    public string UpdateDateFormat => UpdateDate.GetFormatString();
    public List<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
}
