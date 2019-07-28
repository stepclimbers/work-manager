using System;
using WorkManager.Data.Models.Interfaces;

namespace WorkManager.Data.Models.Base
{
    public class BaseEntity<TKey> : IBaseEntity<TKey>
    {
        public TKey Id { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
