using BlogEngine.Business;
using BlogEngine.Business.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Api.Controllers
{
    //https://localhost/api/Author
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorService AuthorService { get; }

        public AuthorController(IAuthorService authorService)
        {
            AuthorService = authorService ?? throw new System.ArgumentNullException(nameof(authorService));
        }

        // GET: https://localhost/api/Author
        [HttpGet]
        public async Task<IEnumerable<Author>> Get()
        {
            List<Author> authors = await AuthorService.FetchAllAsync();
            return authors;
        }

        // GET: api/Author/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Author>> Get(int id)
        {
            if (await AuthorService.FetchByIdAsync(id) is Author author)
            {
                return Ok(author);
            }
            return NotFound();
        }

        // POST: api/Author
        [HttpPost]
        public async Task<Author> Post(AuthorInput value)
        {
            return await AuthorService.InsertAsync(value);
        }

        // PUT: api/Author/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<Author>> Put(int id, AuthorInput value)
        {
            if (await AuthorService.UpdateAsync(id, value) is Author author)
            {
                return author;
            }
            return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Delete(int id)
        {
            if (await AuthorService.DeleteAsync(id))
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
