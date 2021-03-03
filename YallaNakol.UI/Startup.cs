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
            //services.AddScoped<IAddress, AddressRepo>();

            services.AddTransient<IEmailSender, EmailSender>();

            services.Configure<AuthMessageSenderOptions>(options=>
            {
                options.SendGridUser = Configuration["Sendgrid:User"];
                options.SendGridKey = Configuration["Sendgrid:APIKey"];
                options.SenderEmail = Configuration["Sendgrid:SenderEmail"];
            });

            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<ApplicationUser>(
                options =>
                {
                    options.SignIn.RequireConfirmedAccount = true;
                    options.SignIn.RequireConfirmedEmail = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireUppercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHttpContextAccessor();
            services.AddSession();
            services.AddControllersWithViews();

            services.AddAuthentication()
            .AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = "91bad2d5-2df1-43d9-bd34-974b608eb1ae";
                microsoftOptions.ClientSecret = "_T.3iD_ihE2Xof7Peat_iRqcq0rVY.2-UR";
            })
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.ClientId = "735045007188520";
                facebookOptions.ClientSecret = "46c921fbdbb3f9785257e7eb812696ce";
            });
            //.AddGoogle(googleOptions => 
            //{
            //    googleOptions.ClientId = "834611763364-dmvsrajis9ru1po23nknom0vkp5ntpos.apps.googleusercontent.com";
            //    googleOptions.ClientSecret = "WjG0ZLPA9rmWg4_OGPEbujdb";
            //});//error??!

            //.AddGoogle(googleOptions => {  })
            //.AddTwitter(twitterOptions => {  })
            

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serv)
        {
            CreateRoles(serv).Wait();
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
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //initializing custom roles 
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            List<string> roleNames = new List<string> () { UserRoles.Admin, UserRoles.Customer };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var adminUser = new ApplicationUser
            {
                FirstName = "Bruce",
                LastName = "Wayne",
                UserName = Configuration["Admin:Username"],
                Email = "Batman@batcave.com"
            };

            var CreateAdmin = await UserManager.CreateAsync(adminUser, Configuration["Admin:Password"]);
            if (CreateAdmin.Succeeded)
            {
                //here we tie the new user to the role
                await UserManager.AddToRoleAsync(adminUser, UserRoles.Admin);
            }

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
