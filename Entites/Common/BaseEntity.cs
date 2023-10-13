using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Common
{

    public interface IEntity
    {
        public bool IsActive { get; set; }
        public long CreatorId { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
    public abstract class BaseEntity<TKey> : IEntity
    {
        public BaseEntity()
        {
            IsActive = true;
            DateInsert = DateTime.Now;
        }
        public TKey Id { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool IsActive { get; set; }
        public long CreatorId { get; set; }

    }
    public abstract class BaseEntity : BaseEntity<int>
    {

    }

}

