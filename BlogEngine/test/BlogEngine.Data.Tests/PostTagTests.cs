using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogEngine.Data.Tests
{
    [TestClass]
    public class PostTagTests : TestBase
    {
        [TestMethod]
        public async Task CreatePostWithManyTags()
        {
            // Arrange
            IHttpContextAccessor httpContextAccessor = Mock.Of<IHttpContextAccessor>(hta =>
                hta.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier) == new Claim(ClaimTypes.NameIdentifier, "imontoya"));

            var post = new Post
            {
                Title = "My Title",
                Slug = "my-title",
                Content = "Here is some basic content"
            };
            var author = new Author
            {
                FirstName = "Inigo",
                LastName = "Montoya",
                Email = "inigo@montoya.me"
            };
            var tag1 = new Tag
            {
                Name = "C#"
            };
            var tag2 = new Tag
            {
                Name = "Lecture"
            };

            // Act
            post.Author = author;
            post.PostTags = new List<PostTag>();
            post.PostTags.Add(new PostTag { Post = post, Tag = tag1 });
            post.PostTags.Add(new PostTag { Post = post, Tag = tag2 });

            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                dbContext.Posts.Add(post);
                await dbContext.SaveChangesAsync();
            }

            // Assert
            using (ApplicationDbContext dbContext = new ApplicationDbContext(Options, httpContextAccessor))
            {
                var retrievedPost = await dbContext.Posts.Where(p => p.Id == post.Id)
                    .Include(p => p.PostTags).ThenInclude(pt => pt.Tag).SingleOrDefaultAsync();

                Assert.IsNotNull(retrievedPost);
                Assert.AreEqual(2, retrievedPost.PostTags.Count);
                Assert.IsNotNull(retrievedPost.PostTags[0].Tag);
                Assert.IsNotNull(retrievedPost.PostTags[1].Tag);
            }
        }
    }
}
