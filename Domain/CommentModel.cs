using System.Threading.Tasks;

namespace Domain;

public class CommentModel
{
    public Guid Id { get; set; }
    public Guid TaskId { get; set; }
    public DateTime? Created { get; set; }
    public string? Content { get; set; }
}
