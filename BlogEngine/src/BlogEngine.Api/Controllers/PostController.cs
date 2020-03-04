using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogEngine.Business;
using BlogEngine.Business.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BlogEngine.Api.Controllers
{
    public class PostController : BaseApiController<Post, PostInput>
    {
        public PostController(IPostService postService) : base(postService) { }

        [HttpGet("PostsWithAuthors")]
        public async Task<IEnumerable<Post>> GetAllWithAuthors() => await ((IPostService)Service).FetchAllWithAuthorsAsync();
    }
}
