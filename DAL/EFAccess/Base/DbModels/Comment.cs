using System;

namespace DAL.DbModels;

public partial class Comment
{
    public Guid CommentId { get; set; }

    public Guid TaskId { get; set; }

    public DateTime? CreateDate { get; set; }

    public string Content { get; set; }

    public virtual Task Task { get; set; }
}
