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
            var result = await _bL_Blog.CreateBlogV1(blog);
            return Content(result);
        }

        [HttpPut("{blogId}")]
        public async Task<IActionResult> UpdateBlog([FromBody] BlogModel blog, string blogId)
        {
            var result = await _bL_Blog.UpdateBlogV1(blog, blogId);
            return Content(result);
        }

        [HttpPatch("{blogId}")]
        public async Task<IActionResult> PatchBlog([FromBody] BlogModel blog, string blogId)
        {
            var result = await _bL_Blog.PatchBlogV1(blog, blogId);
            return Content(result);
        }

        [HttpDelete("{blogId}")]
        public async Task<IActionResult> DeleteBlog(string blogId)
        {
            var result = await _bL_Blog.DeleteBlogV1(blogId);
            return Content(result);
        }
    }
}
