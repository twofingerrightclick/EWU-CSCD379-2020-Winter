using BlogEngine.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BlogEngine.Api.Tests
{
    public class BlogWebApplicationFactory : WebApplicationFactory<Startup>
    {
        private SqliteConnection Connection { get; }

        public BlogWebApplicationFactory()
        {
            Connection = new SqliteConnection("DataSource=:memory:");
            Connection.Open();
        }

        public ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseSqlite(Connection)
               .EnableSensitiveDataLogging()
               .Options;
            return new ApplicationDbContext(options);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder?.ConfigureServices(services =>
            {
                services.RemoveDbContext<ApplicationDbContext>();

                services.AddDbContext<ApplicationDbContext>(options =>
                    options.EnableSensitiveDataLogging()
                      .UseSqlite(Connection)
                      );
            });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Connection.Dispose();
            }
            base.Dispose(disposing);
        }
    }

}
