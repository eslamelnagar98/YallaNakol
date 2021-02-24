using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;


namespace YallaNakol.Data.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
       // protected override void OnModelCreating(ModelBuilder builder) => builder.Entity<Restaurant>().Property(R => R.DeliveryAreas).HasConversion<int>();

        public DbSet<Category> Categories { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<ShoppingCartItem> shoppingCartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ShoppingCartItem>().HasData(
                new ShoppingCartItem
                {
                    Id=1,
                    Amount = 3,
                    ShoppingCartId= "30377b50-77fc-4e43-81d0-bbdc0e188ccb",
                    DishId=1

                });

            builder.Entity<Category>().HasData(
               new Category
               {
                   Id = 1,
                   Name = "Burger",
                   Description = "A hamburger (also burger for short) is a sandwich consisting of one or more cooked patties of ground meat, usually beef, placed inside a sliced bread roll or bun. The patty may be pan fried, grilled, smoked or flame broiled. Hamburgers are often served with cheese, lettuce, tomato, onion, pickles, bacon, or chiles; condiments such as ketchup, mustard, mayonnaise, relish, or a special sauce, often a variation of Thousand Island dressing; and are frequently placed on sesame seed buns. A hamburger topped with cheese is called a cheeseburger",
               });


            builder.Entity<Menu>().HasData(
              new Menu
              {
                  Id = 1,
              });

            builder.Entity<Restaurant>().HasData(
                new Restaurant 
                { Id = 1,
                  Name = "McDonalds", 
                  Address = "39 Abbas El Akkad StreetAbbas El Akkad Nasr City",
                  DeliveryAreas = DeliveryAreas.NasrCity,
                  PhoneNumber = "19991",
                  Description="McDonald's Corporation is an American fast food company, founded in 1940 as a restaurant operated by Richard and Maurice McDonald, in San Bernardino, California, United States. They rechristened their business as a hamburger stand, and later turned the company into a franchise, with the Golden Arches logo being introduced in 1953 at a location in Phoenix, Arizona. In 1955, Ray Kroc, a businessman, joined the company as a franchise agent and proceeded to purchase the chain from the McDonald brothers. McDonald's had its previous headquarters in Oak Brook, Illinois, but moved its global headquarters to Chicago in June 2018",
                  Rate="4.6",
                  WorkingHours="From 11:00 AM To 02:15 AM",
                  ImageUrl= "https://www.nrn.com/sites/nrn.com/files/styles/article_featured_standard/public/mcdonalds-logo.gif?itok=U_TliriA",
                  MenuId=1
                  
                });

            builder.Entity<Dish>().HasData(
              new Dish
              {
                  Id = 1,
                  Name = "Big Tasty",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/a660c55e-d9ea-4c0d-be33-4d0cc5d0a7b4.jpg",
                  InStock= true,
                  Price=100M,
                  Description= "Juicy beef patty smothered in three extraordinary slices of Emmental cheese and topped with sliced tomato, shredded lettuce, onions and that special Big Tasty sauce",
                  CategoryId=1,
                  MenuId=1
                  
              },
              new Dish
              {
                  Id = 2,
                  Name = "Big Mac",
                  ImageUrl="https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/298f37b4-f9f7-44ea-a297-02cda66bd50b.jpg",            
                  InStock= true,
                  Price =60M,
                  Description= "Two beef patties, that unbeatably tasty Big Mac sauce, melting signature cheese, crisp shredded lettuce, onions, pickles and a bun in the middle all between a toasted sesame seed bun",
                  CategoryId=1,
                  MenuId=1
                  
              }
              ,
               new Dish
              {
                  Id = 3,
                  Name = "Double Big Tasty",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/b7d655fd-9cee-4a79-89f7-8a4b6a28ab00.jpg",
                  InStock= true,
                  Price=110M,
                  Description= "Two Juicy beef patty smothered in three extraordinary slices of Emmental cheese and topped with sliced tomato, shredded lettuce, onions and that special Big Tasty sauce",
                  CategoryId=1,
                  MenuId=1
                  
              },
              new Dish
              {
                  Id = 4,
                  Name = "Chicken Mac",
                  ImageUrl= "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Thumbnail/0e13cb43-dacb-4788-8c7d-2c0ab72c23e9.jpg",              
                  InStock= true,
                  Price =90M,
                  Description= "A delicious combination of breaded chicken patties, crisp lettuce, melting cheese, onions, pickles, and our special sauce, all framed between a toasted sesame seed bun",
                  CategoryId=1,
                  MenuId=1
                  
              });






        }


    }
}
