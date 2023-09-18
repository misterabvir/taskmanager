using TaskManager.ViewModels.Utils;
namespace TaskManager.ViewModels;

public class CommentViewModel
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public DateTime? Created { get; set; }
    public string? Content { get; set; }
    public string CreatedFormat => Created.GetFormatString();
}
