using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestAPI.Context;
using TestAPI.Models;

namespace Test_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly BlogContext _dbContext;

        public PostsController(BlogContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET api/posts
        // GET api/posts?isPublic=true/false
        [HttpGet]
        public ActionResult<List<Post>> GetPosts([FromQuery] bool? isPublic)
        {
            List<Post> posts = null;

            if (isPublic == null)
            {
                posts = _dbContext.Posts.
                    Include(post => post.Comments).
                    ToList();
            }
            else
            {
                posts = _dbContext.Posts.
                    Where(post => post.IsPublic == isPublic.Value).
                    Include(post => post.Comments).
                    ToList();
            }
       
            return new OkObjectResult(posts);
        }

        // POST api/posts
        [HttpPost]
        public ActionResult CreatePost([FromBody] Post newPost)
        {
            _dbContext.Posts.Add(newPost);
            _dbContext.SaveChanges();

            return new OkResult();
        }

        // PUT api/posts/{postId}
        [HttpPut("{postId}")]
        public ActionResult UpdatePost([FromBody] Post newPost, int postId)
        {
            Post existing = _dbContext.Posts.Find(postId);

            ActionResult result;
            if (existing == null)
            {
                result = new NotFoundResult();
            }
            else
            {
                newPost.Id = postId; //Make sure this is set
                _dbContext.Entry(existing).CurrentValues.SetValues(newPost); //Set all values from the new entity
                _dbContext.SaveChanges();

                result = new OkResult();
            }

            return result;
        }

        // DELETE api/posts/{postId}
        [HttpDelete("{postId}")]
        public ActionResult DeletePost(int postId)
        {
            Post toDelete = _dbContext.Posts.Find(postId);

            ActionResult result;
            if (toDelete == null)
            {
                result = new NotFoundResult();
            }
            else
            {
                _dbContext.Posts.Remove(toDelete);
                _dbContext.SaveChanges();
                result = new OkResult();
            }

            return result;
        }
    }
}
