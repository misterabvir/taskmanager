using TaskManagerWithTSReact.Server.ViewModels.Utils;
namespace TaskManagerWithTSReact.Server.ViewModels;

public class CommentViewModel
{
    public Guid CommentId { get; set; }
    public Guid TaskId { get; set; }
    public DateTime? Created { get; set; }
    public string? Content { get; set; }
    public string CreatedFormat => Created.HowLongAgo(DateTime.Now);
}
