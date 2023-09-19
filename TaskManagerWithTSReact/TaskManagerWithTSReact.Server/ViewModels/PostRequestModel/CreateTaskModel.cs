using System.ComponentModel.DataAnnotations;

namespace TaskManager.ViewModels.PostRequestModel;

public class CreateTaskModel
{
    [Required]
    public string? TaskName { get; set; }
    public Guid ProjectId { get; set; }
}
