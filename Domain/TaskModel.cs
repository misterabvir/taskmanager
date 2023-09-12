namespace Domain;

public class TaskModel
{
    public Guid Id { get; set; }
    public string? TaskName { get; set; }
    public Guid? ProjectId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime CancelDate { get; set; }
    public DateTime CreateDate { get; set; }
    public DateTime UpdateDate { get; set; }
}
