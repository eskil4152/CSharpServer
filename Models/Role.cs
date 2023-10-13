using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CSharpServer.Models
{
	public class Role
	{
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public long Id { get; set; }

        [Required]
        [Column("name")]
        public string Name { get; set; }

        [InverseProperty("Role")]
        public virtual ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}

