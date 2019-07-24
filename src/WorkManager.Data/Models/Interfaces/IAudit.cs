using System;

namespace WorkManager.Data.Models.Interfaces
{
    public interface IAudit
    {
        DateTime CreatedOn { get; set; }
        DateTime? ModifiedOn { get; set; }
    }
}
