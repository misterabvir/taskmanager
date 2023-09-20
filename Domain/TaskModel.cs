namespace Domain;

public class TaskModel
{
    public Guid TaskId { get; set; }
    public string? TaskName { get; set; }
    public string? Description { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? CancelDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public List<CommentModel> Comments { get; set; } = new List<CommentModel>();
}
