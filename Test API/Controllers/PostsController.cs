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

        // GET api/values
        [HttpGet]
        public ActionResult<List<Post>> GetPosts([FromQuery] bool? isPublic)
        {
            List<Post> posts = null;

            if (isPublic == null)
            {
                posts = _dbContext.Posts.
                    Include(post => post.Author).
                    Include(post => post.Comments).
                    ToList();
            }
            else
            {
                posts = _dbContext.Posts.
                    Where(post => post.IsPublic == isPublic.Value).
                    Include(post => post.Author).
                    Include(post => post.Comments).
                    ToList();
            }
       
            return new OkObjectResult(posts);
        }
    }
}
