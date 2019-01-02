using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Models
{
    [Table("posts")]
    public partial class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        [Column("id")]
        public int Id { get; set; }
        [Column("author_id")]
        public int AuthorId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [Column("content")]
        public string Content { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }
        [Column("is_public")]
        public bool IsPublic { get; set; }

        [ForeignKey("AuthorId")]
        [InverseProperty("Posts")]
        public virtual User Author { get; set; }
        [InverseProperty("Post")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}
