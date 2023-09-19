namespace Domain;

public class ProjectModel
{
    public Guid ProjectId { get; set; }
    public string? ProjectName { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public List<TaskModel> Tasks { get; set; } = new List<TaskModel>();
}
