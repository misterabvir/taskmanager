using System;
using System.Collections.Generic;

namespace DAL.DbModels;

public partial class Task
{
    public Guid TaskId { get; set; }

    public string TaskName { get; set; }

    public string Description { get; set; }

    public Guid ProjectId { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? CancelDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual Project Project { get; set; }
}
