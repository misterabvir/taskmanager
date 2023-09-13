namespace Domain;

public class ProjectModel
{
    public Guid Id { get; set; }
    public string? ProjectName { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}
