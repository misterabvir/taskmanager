﻿using TaskManager.ViewModels.Utils;

namespace TaskManager.ViewModels;

public class TaskViewModel
{
    public Guid TaskId { get; set; }
    public string? TaskName { get; set; }
    public Guid ProjectId { get; set; }
    public string ProjectName { get; set; } = string.Empty;
    public DateTime CreateDate { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? CancelDate { get; set; }
    public DateTime? UpdateDate { get; set; }

    public string State
    {
        get {
            if (CancelDate != null) return Utils.State.canceled.ToString();
            if (StartDate != null) return Utils.State.started.ToString();
            return Utils.State.created.ToString();
        }
    }

    public string InWork => StartDate.HowLong(CancelDate);
    public string CreateDateFormat => CreateDate.GetFormatString();
    public string StartDateFormat => StartDate.GetFormatString();
    public string CancelDateFormat => CancelDate.GetFormatString();
    public string UpdateDateFormat => UpdateDate.GetFormatString();
}
