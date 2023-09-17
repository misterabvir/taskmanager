namespace TaskManager.ViewModels.PostRequestModel
{
    public class CreateCommentModel
    {
        public string? Content { get; set; }
        public Guid TaskId { get; set; }
    }
}
