using DotNet8JsonCrud.Api.Helpers;
using DotNet8JsonCrud.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8JsonCrud.Api.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : BaseController
    {
        private readonly BL_Blog _bL_Blog;

        public BlogController(BL_Blog bL_Blog)
        {
            _bL_Blog = bL_Blog;
        }

        [HttpGet]
        public async Task<IActionResult> GetBlogs()
        {
            var result = await _bL_Blog.GetBlogs();
            return Content(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog([FromBody] BlogModel blog)
        {
            var result = await _bL_Blog.CreateBlog(blog);
            return Content(result);
        }

        [HttpPut("{blogId}")]
        public async Task<IActionResult> UpdateBlog([FromBody] BlogModel blog, string blogId)
        {
            var result = await _bL_Blog.UpdateBlog(blog, blogId);
            return Content(result);
        }
    }
}
