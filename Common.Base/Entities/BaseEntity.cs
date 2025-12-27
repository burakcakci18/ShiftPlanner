using Common.Base.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base.Entities
{
    public class BaseEntity : IEntityBase
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [Column("IS_ACTIVE")]
        public bool IsActive { get; set; }
    }
}
