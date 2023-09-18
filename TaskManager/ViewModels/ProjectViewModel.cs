using TaskManager.ViewModels.Utils;

namespace TaskManager.ViewModels;

public class ProjectViewModel
{
    public Guid Id { get; set; }
    public string? ProjectName { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public IEnumerable<TaskViewModel>? Tasks { get; set; }
    public string CreateDateFormat => CreateDate.GetFormatString();
    public string UpdateDateFormat => UpdateDate.GetFormatString();
}
