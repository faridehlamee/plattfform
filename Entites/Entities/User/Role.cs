using Entities.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entites.Entities.User
{
    [Table("Role", Schema = "ACC")]
    public class Role : IdentityRole<int>, IEntity<int>
    {
        [Required]
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public long CreatorId { get; set; }
        public DateTime DateInsert { get; set; }
        public DateTime? DateUpdate { get; set; }
    }
    public class RoleConfiguratin : IEntityTypeConfiguration<Role>
    {
        public void  Configure(EntityTypeBuilder<Role> builder)
        {
            builder.Property(p => p.Name).IsRequired().HasMaxLength(50);
        }
    }
}
