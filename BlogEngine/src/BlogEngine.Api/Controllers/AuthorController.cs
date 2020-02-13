using BlogEngine.Business;
using BlogEngine.Business.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogEngine.Api.Controllers
{
    public class AuthorController : BaseApiController<Author, AuthorInput>
    {
        public AuthorController(IAuthorService authorService) : base(authorService) { }
    }
}
