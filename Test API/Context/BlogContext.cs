using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TestAPI.Models;

namespace TestAPI.Context
{
    public partial class BlogContext : DbContext
    {
        public BlogContext()
        {
        }

        public BlogContext(DbContextOptions<BlogContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasIndex(e => e.AuthorId)
                    .HasName("comments_author_id");

                entity.HasIndex(e => e.PostId)
                    .HasName("post_id");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("comments_author_id_fkey");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("comments_post_id_fkey");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasIndex(e => e.AuthorId)
                    .HasName("posts_author_id");

                entity.HasIndex(e => e.IsPublic)
                    .HasName("is_public");

                entity.HasOne(d => d.Author)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.AuthorId)
                    .HasConstraintName("posts_author_id_fkey");
            });
        }
    }
}
