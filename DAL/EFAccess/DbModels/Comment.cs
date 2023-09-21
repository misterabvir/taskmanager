using System;

namespace DAL.DbModels;

public partial class Comment
{
    public string CommentId { get; set; }

    public string TaskId { get; set; }

    public DateTime? CreateDate { get; set; }

    public string Content { get; set; }

    public virtual Task Task { get; set; }
}
