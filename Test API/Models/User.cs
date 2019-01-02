using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Models
{
    [Table("users")]
    public partial class User
    {
        public User()
        {
            Comments = new HashSet<Comment>();
            Posts = new HashSet<Post>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(200)]
        public string Name { get; set; }
        [Column("dob", TypeName = "date")]
        public DateTime Dob { get; set; }

        [InverseProperty("Author")]
        public virtual ICollection<Comment> Comments { get; set; }
        [InverseProperty("Author")]
        public virtual ICollection<Post> Posts { get; set; }
    }
}
