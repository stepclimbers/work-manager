using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkManager.Data.Models
{
    public interface IBaseEntity<TKey> : IAudit
    {
        TKey Id { get; set; }
    }
}
