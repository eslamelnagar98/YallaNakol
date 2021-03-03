using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static YallaNakol.Data.Models.Order;

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
        public DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrdersDetails { get; set; }
       //public DbSet<Address> Addresses { get; set; }

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
               },
               new Category
               {
                Id = 2,
                   Name = "Egyptian",
                   Description = "Local egyptian food.",
               },
               new Category
               {
                Id = 3,
                   Name = "Grill",
                   Description = "Grilled food.",
               }
               );


            builder.Entity<Menu>().HasData(
              new Menu
              {
                  Id = 1,
              }, new Menu
              {
                  Id = 2,
              },new Menu
              {
                  Id = 3,
              },new Menu
              {
                  Id = 4,
              }
              );

            builder.Entity<Restaurant>().HasData(
                new Restaurant 
                { Id = 1,
                  Name = "McDonalds", 
                  Address = "39 Abbas El Akkad StreetAbbas El Akkad Nasr City",
                  DeliveryAreas = DeliveryAreas.NasrCity | DeliveryAreas.Madinaty | DeliveryAreas.AinShams,
                  PhoneNumber = "19991",
                  Description="McDonald's Corporation is an American fast food company, founded in 1940 as a restaurant operated by Richard and Maurice McDonald, in San Bernardino, California, United States. They rechristened their business as a hamburger stand, and later turned the company into a franchise, with the Golden Arches logo being introduced in 1953 at a location in Phoenix, Arizona. In 1955, Ray Kroc, a businessman, joined the company as a franchise agent and proceeded to purchase the chain from the McDonald brothers. McDonald's had its previous headquarters in Oak Brook, Illinois, but moved its global headquarters to Chicago in June 2018",
                  Rate="4.5",
                  WorkingHours="From 11:00 AM To 02:15 AM",
                  ImageUrl= "https://www.nrn.com/sites/nrn.com/files/styles/article_featured_standard/public/mcdonalds-logo.gif?itok=U_TliriA",
                  MenuId=1
                },
                new Restaurant 
                { Id = 2,
                  Name = "Koshary Sayed Hanafy", 
                  Address = "19 Shabab El Mohandessin Blgs., Extension of Makram Ebeid Makram Ebeid, Nasr City",
                  DeliveryAreas = DeliveryAreas.NasrCity,
                  PhoneNumber = "16920",
                  Description="Sayed Hanafy restaurants chain is one of the most renowed authentic restaurant chain in Egypt. Started at 1952 from a koshary cart, until the first branch was established in 1985.",
                  Rate="4.6",
                  WorkingHours="From 09:00 AM To 01:00 AM",
                  ImageUrl= "https://scontent.fcai3-1.fna.fbcdn.net/v/t1.0-9/80390516_2962955900382554_7936731969642037248_o.jpg?_nc_cat=108&ccb=3&_nc_sid=09cbfe&_nc_eui2=AeFXUmEyrlQcE9kS0CRed1hSadTxKq-e0Mxp1PEqr57QzORy-uTd2-XxCfuIsbKZWCw&_nc_ohc=GDzNSruJTnYAX-4LMAT&_nc_ht=scontent.fcai3-1.fna&oh=b0869207a61c893aea55de10a887bcac&oe=605F788F",
                  MenuId=2
                  
                },
                new Restaurant 
                { Id = 3,
                  Name = "ElDahan", 
                  Address = "84 Hassan Ma'moon in front of Al Ahly Club Al-Ahly Club, Nasr City",
                  DeliveryAreas = DeliveryAreas.NasrCity,
                  PhoneNumber = "16194",
                  Description= "History told of El-Dahan's success since 1890. It was first was named 'El-Akr' & was known for its Eastern cuisine & barbequed meat. Now we have opened new branches in the 5th Settlement, Rehab City & in Merghany. We hope to continue our success through your support & confidence. \"Combine tradition & originality\".",
                  Rate="4.5",
                  WorkingHours="From 09:00 AM To 12:30 AM",
                  ImageUrl= "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/79262228-7ae9-48a0-8dd1-2db9f18d3ff0.jpg",
                  MenuId=3
                  
                },
                new Restaurant 
                { Id = 4,
                  Name = "Buffalo Burger", 
                  Address = "27 El Batrawy St. Off Abbas El Akkad St. Abbas El Akkad, Nasr City",
                  DeliveryAreas = DeliveryAreas.NasrCity,
                  PhoneNumber = "19914",
                  Description= "It all started with four passionate brothers who shared one big appetite, an obsession with juicy burgers and a demanding palate. They focused their energy on challenging the conventional idea that food served fast must be a junk experience and instead worked on refining it to become an elevated culinary adventure where ingredients are naturally",
                  Rate="4.6",
                  WorkingHours="From 11:00 AM To 02:45 AM",
                  ImageUrl= "https://scontent.fcai3-1.fna.fbcdn.net/v/t1.0-9/110249313_10158045681834681_5826574394868309860_o.jpg?_nc_cat=1&ccb=3&_nc_sid=09cbfe&_nc_eui2=AeGKS-9i1ns8Ux0u1h6Sh5m44DX-FVvfQFXgNf4VW99AVcpR7In_bHJ8DAoSvYZgjWg&_nc_ohc=86qMH8TqTvkAX-YWQ97&_nc_ht=scontent.fcai3-1.fna&oh=5239a5e67c94134451274c0d8bab0f7b&oe=60619A20",
                  MenuId=4
                  
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
                  
              },
              new Dish
              {
                  Id = 5,
                  Name = "Small Box",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/6bfdcfce-b4ac-4b06-b448-1414c049b5fa.jpg",
                  InStock = true,
                  Price = 10M,
                  Description = "Koshary small plate.",
                  CategoryId = 2,
                  MenuId = 2

              },
              new Dish
              {
                  Id = 6,
                  Name = "Big Star",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/3366aab0-fb50-4cbe-b662-af6febc7f25d.jpg",
                  InStock = true,
                  Price = 20M,
                  Description = "Koshary big plate.",
                  CategoryId = 2,
                  MenuId = 2

              },
              new Dish
              {
                  Id = 7,
                  Name = "1/2 Grilled Chicken with Tehina",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/d065bb8d-0d10-4d1d-abe3-ab94527d148a.jpg",
                  InStock = true,
                  Price = 48M,
                  Description = "1/2 Grilled Chicken with Tehina and bread and additional rice with great taste.",
                  CategoryId = 3,
                  MenuId = 3
              },
              new Dish
              {
                  Id = 8,
                  Name = "1/4 Kilo Kofta",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/7506c276-a4ae-401e-9d06-4d213fb301b0.jpg",
                  InStock = true,
                  Price = 67M,
                  Description = "1/4 Kilo Kofta on grill with tehina and bread.",
                  CategoryId = 3,
                  MenuId = 3
              },
              new Dish
              {
                  Id = 9,
                  Name = "Bacon Mushroom Jack",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/b1e14590-9b6c-4249-a842-d9d9729f4999.jpg",
                  InStock = true,
                  Price = 66M,
                  Description = "Beef bacon with fresh sauteed mushroom, cheddar cheese and creamy mayonnaise on top of our flamed burger patty 150 gm",
                  CategoryId = 1,
                  MenuId = 4

              },
              new Dish
              {
                  Id = 10,
                  Name = "Charbroiled BBQ",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/3c323965-7cad-49bf-a27f-51faa70313e2.jpg",
                  InStock = true,
                  Price = 53M,
                  Description = "Grilled burger topped with sweet onion, BBQ sauce, creamy Charbroiled sauce and Swiss cheese 150gm.",
                  CategoryId = 1,
                  MenuId = 4

              },
              new Dish
              {
                  Id = 11,
                  Name = "Hitchhiker",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/cf499e68-56eb-4fd7-a663-a76d9be2d6ef.jpg",
                  InStock = true,
                  Price = 66M,
                  Description = "2 mozzarella sticks on 150 gm and 3 pieces on 200 and 400 gm beef patties, loaded with original ketchup, mustard drops, beef bacon, cheddar cheese and our creamy Buffalo sauce, all on top of our clean cut beef patty",
                  CategoryId = 1,
                  MenuId = 4

              },
              new Dish
              {
                  Id = 12,
                  Name = "Chicked Grilled New Sandwich",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/298f37b4-f9f7-44ea-a297-02cda66bd50b.jpg",
                  InStock = true,
                  Price = 100M,
                  Description = "A new Sandwich introduced from macdonalds from our chefs with our great sauses",
                  CategoryId = 2,
                  MenuId = 1

              },
              new Dish
              {
                  Id = 13,
                  Name = "Chicked Grilled New Sandwich",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/298f37b4-f9f7-44ea-a297-02cda66bd50b.jpg",
                  InStock = true,
                  Price = 100M,
                  Description = "A new Sandwich introduced from macdonalds from our chefs with our great sauses",
                  CategoryId = 2,
                  MenuId = 1

              }, new Dish
              {
                  Id = 14,
                  Name = "Big Tasty",
                  ImageUrl = "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/a660c55e-d9ea-4c0d-be33-4d0cc5d0a7b4.jpg",
                  InStock = true,
                  Price = 100M,
                  Description = "Juicy beef patty smothered in three extraordinary slices of Emmental cheese and topped with sliced tomato, shredded lettuce, onions and that special Big Tasty sauce",
                  CategoryId = 3,
                  MenuId = 1

              }

              );

        }


    }
}
