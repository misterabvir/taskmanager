using Org.BouncyCastle.Asn1.Crmf;

namespace TaskManager.ViewModels;

public class TaskViewModel
{
    public int Index { get; set; } = 0;
    public Guid Id { get; set; }
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
            if (CancelDate != null) return "canceled";
            if (StartDate != null) return "started";
            return "created";
        }
    }

    public string InWork
    {
        get
        {
            if (StartDate == null) return "00:00";
            if (CancelDate == null) return (DateTime.UtcNow - StartDate).Value.ToString(@"hh\:mm");
            else return (CancelDate - StartDate).Value.ToString(@"hh\:mm");
        }
    }
   
}
