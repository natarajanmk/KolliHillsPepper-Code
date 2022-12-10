using System;
using System.ComponentModel.DataAnnotations;

namespace KH.Pepper.Core.Domain
{

    public abstract class BaseEntity
    {
        public DateTimeOffset CreatedOn { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; } = default!;

        public DateTimeOffset LastChangedOn { get; set; }

        [StringLength(100)]
        public string LastChangedBy { get; set; } = default!;

        int _Id;

        public virtual int Id
        {
            get { return _Id; }
            protected set { _Id = value; }
        }
         
    }
}
