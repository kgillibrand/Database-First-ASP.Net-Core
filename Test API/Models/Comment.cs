using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestAPI.Models
{
    [Table("comments")]
    public partial class Comment
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("post_id")]
        public int PostId { get; set; }
        [Column("author_id")]
        public int AuthorId { get; set; }
        [Required]
        [Column("content")]
        public string Content { get; set; }
        [Column("created")]
        public DateTime Created { get; set; }

        [ForeignKey("AuthorId")]
        [InverseProperty("Comments")]
        public virtual User Author { get; set; }
        [ForeignKey("PostId")]
        [InverseProperty("Comments")]
        public virtual Post Post { get; set; }
    }
}
