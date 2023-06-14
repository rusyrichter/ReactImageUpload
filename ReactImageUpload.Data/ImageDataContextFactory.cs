using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using ReactImageUpload.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreWebDemo.Data
{
    public class PeopleDataContextFactory : IDesignTimeDbContextFactory<ImageDataContext>
    {
        public ImageDataContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
               .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), $"..{Path.DirectorySeparatorChar}ReactImageUpload.Web"))
               .AddJsonFile("appsettings.json")
               .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true).Build();

            return new ImageDataContext(config.GetConnectionString("ConStr"));
        }
    }
}

