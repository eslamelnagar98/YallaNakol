using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using YallaNakol.Data.Models;

[assembly: HostingStartup(typeof(YallaNakol.UI.Areas.Identity.IdentityHostingStartup))]
namespace YallaNakol.UI.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {

                //services.AddDbContext<ApplicationDbContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("MyConn")));

            //    services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //        .AddEntityFrameworkStores<ApplicationDbContext>();

            });
        }
    }
}