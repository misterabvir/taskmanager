using System;
using System.Collections.Generic;

namespace DAL.DbModels;

public partial class Project
{
    public string ProjectId { get; set; }

    public string ProjectName { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
