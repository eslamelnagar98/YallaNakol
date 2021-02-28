using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YallaNakol.Data.Models;
using YallaNakol.Data.Services;
using YallaNakol.Data.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Http;
using Stripe;

namespace YallaNakol.UI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("MyConn")));

            services.AddScoped<IShoppingCart, ShoppingCart>(sp => {
                var dbContext = sp.GetRequiredService<ApplicationDbContext>();
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                return ShoppingCart.GetCart(dbContext, httpContextAccessor);
            });

            services.AddScoped<ICategory,CategoryRepo>();
            services.AddScoped<IMenu,MenuRepo>();
            services.AddScoped<IDish, DishRepo>();
            services.AddScoped<IRestaurant,RestaurantRepo>();
            services.AddScoped<IOrder, OrderRepo>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddControllersWithViews();
            
            services.AddAuthentication()
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = "51217d7a-4861-45fe-8134-febfeebb8ec8";
                microsoftOptions.ClientSecret = "_4DNh8j._XAF~1e1kB67g._zilRSSx1TxK";
            });

            //.AddGoogle(googleOptions => {  })
            //.AddTwitter(twitterOptions => {  })
            //.AddFacebook(facebookOptions => {  });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseExceptionHandler("/Home/Error");
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            var sessionOptions = new SessionOptions()
            {
                Cookie = new CookieBuilder()
                {
                    Name = "MVCSID",
                    Expiration = TimeSpan.FromMinutes(30),
                }
            };
            app.UseSession(sessionOptions);

            app.UseAuthentication();
            app.UseAuthorization();
            StripeConfiguration.ApiKey = Configuration["StripeSecret"];
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
               endpoints.MapRazorPages();
            });
        }
    }
}

//https://app.pluralsight.com/library/courses/play-by-play-get-paid-dot-net-core-modern-payment-gateways/table-of-contents
//steps for paypal account:
//1-create account on https://www.paypal.com/mep/dashboard
//2-go to https://developer.paypal.com/home
//3-fo to dashboard
//4-create new porject and generate ID and secret
//5-go to visual studio and open nuget packages
//6-install PayPal package and PayPalCoreSDK


//steps for stripe paying
//https://techtolia.medium.com/accept-payments-with-stripe-and-asp-net-c-83f285ed98e0
//


//steps for logging in an external file
//https://www.youtube.com/watch?v=o5u4fE0t79k&list=PL6n9fhu94yhVkdrusLaQsfERmL_Jh4XmU&index=63
//https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-5.0
//1-setup package NLog.Web.AspNetCore
//2-add a text file named nlog.config for your project 
//3- add these configrations from this site https://github.com/NLog/NLog/wiki/Configuration-file
//4-right click on nlog.config file then properties and enable copy to output directly if newer
//5-edit the Program.cs file where you override the ConfigureLogging function
