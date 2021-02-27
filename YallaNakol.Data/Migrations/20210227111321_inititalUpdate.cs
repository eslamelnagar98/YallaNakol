using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YallaNakol.Data.Migrations
{
    public partial class inititalUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AddressLine2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<int>(type: "int", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderPlaced = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TrackingID = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryRestaurant",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    RestaurantsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryRestaurant", x => new { x.CategoriesId, x.RestaurantsId });
                    table.ForeignKey(
                        name: "FK_CategoryRestaurant_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InStock = table.Column<bool>(type: "bit", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dishes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishId = table.Column<int>(type: "int", nullable: false),
                    DishOfTheWeekId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Dishes_DishOfTheWeekId",
                        column: x => x.DishOfTheWeekId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrdersDetails",
                columns: table => new
                {
                    OrderDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DishId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrdersDetails", x => x.OrderDetailId);
                    table.ForeignKey(
                        name: "FK_OrdersDetails_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrdersDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShoppingCartItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DishId = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShoppingCartItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShoppingCartItems_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Rate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkingHours = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    DeliveryAreas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Restaurants_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "A hamburger (also burger for short) is a sandwich consisting of one or more cooked patties of ground meat, usually beef, placed inside a sliced bread roll or bun. The patty may be pan fried, grilled, smoked or flame broiled. Hamburgers are often served with cheese, lettuce, tomato, onion, pickles, bacon, or chiles; condiments such as ketchup, mustard, mayonnaise, relish, or a special sauce, often a variation of Thousand Island dressing; and are frequently placed on sesame seed buns. A hamburger topped with cheese is called a cheeseburger", "Burger" },
                    { 2, "Local egyptian food.", "Egyptian" },
                    { 3, "Grilled food.", "Grill" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "DishId", "DishOfTheWeekId" },
                values: new object[,]
                {
                    { 1, 0, null },
                    { 2, 0, null },
                    { 3, 0, null },
                    { 4, 0, null }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "CategoryId", "Description", "ImageUrl", "InStock", "MenuId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "Juicy beef patty smothered in three extraordinary slices of Emmental cheese and topped with sliced tomato, shredded lettuce, onions and that special Big Tasty sauce", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/a660c55e-d9ea-4c0d-be33-4d0cc5d0a7b4.jpg", true, 1, "Big Tasty", 100m },
                    { 2, 1, "Two beef patties, that unbeatably tasty Big Mac sauce, melting signature cheese, crisp shredded lettuce, onions, pickles and a bun in the middle all between a toasted sesame seed bun", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/298f37b4-f9f7-44ea-a297-02cda66bd50b.jpg", true, 1, "Big Mac", 60m },
                    { 3, 1, "Two Juicy beef patty smothered in three extraordinary slices of Emmental cheese and topped with sliced tomato, shredded lettuce, onions and that special Big Tasty sauce", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/b7d655fd-9cee-4a79-89f7-8a4b6a28ab00.jpg", true, 1, "Double Big Tasty", 110m },
                    { 4, 1, "A delicious combination of breaded chicken patties, crisp lettuce, melting cheese, onions, pickles, and our special sauce, all framed between a toasted sesame seed bun", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Thumbnail/0e13cb43-dacb-4788-8c7d-2c0ab72c23e9.jpg", true, 1, "Chicken Mac", 90m },
                    { 12, 1, "Fera5 Cheicken", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/298f37b4-f9f7-44ea-a297-02cda66bd50b.jpg", true, 1, "Chicked Grilled New Sandwich", 100m },
                    { 5, 2, "Koshary small plate.", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/6bfdcfce-b4ac-4b06-b448-1414c049b5fa.jpg", true, 2, "Small Box", 10m },
                    { 6, 2, "Koshary big plate.", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/3366aab0-fb50-4cbe-b662-af6febc7f25d.jpg", true, 2, "Big Star", 20m },
                    { 7, 3, "1/2 Grilled Chicken with Tehina and bread and additional rice with great taste.", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/d065bb8d-0d10-4d1d-abe3-ab94527d148a.jpg", true, 3, "1/2 Grilled Chicken with Tehina", 48m },
                    { 8, 3, "1/4 Kilo Kofta on grill with tehina and bread.", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/7506c276-a4ae-401e-9d06-4d213fb301b0.jpg", true, 3, "1/4 Kilo Kofta", 67m },
                    { 9, 1, "Beef bacon with fresh sauteed mushroom, cheddar cheese and creamy mayonnaise on top of our flamed burger patty 150 gm", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/b1e14590-9b6c-4249-a842-d9d9729f4999.jpg", true, 4, "Bacon Mushroom Jack", 66m },
                    { 10, 1, "Grilled burger topped with sweet onion, BBQ sauce, creamy Charbroiled sauce and Swiss cheese 150gm.", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/3c323965-7cad-49bf-a27f-51faa70313e2.jpg", true, 4, "Charbroiled BBQ", 53m },
                    { 11, 1, "2 mozzarella sticks on 150 gm and 3 pieces on 200 and 400 gm beef patties, loaded with original ketchup, mustard drops, beef bacon, cheddar cheese and our creamy Buffalo sauce, all on top of our clean cut beef patty", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/cf499e68-56eb-4fd7-a663-a76d9be2d6ef.jpg", true, 4, "Hitchhiker", 66m }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "DeliveryAreas", "Description", "ImageUrl", "MenuId", "Name", "PhoneNumber", "Rate", "WorkingHours" },
                values: new object[,]
                {
                    { 1, "39 Abbas El Akkad StreetAbbas El Akkad Nasr City", 22, "McDonald's Corporation is an American fast food company, founded in 1940 as a restaurant operated by Richard and Maurice McDonald, in San Bernardino, California, United States. They rechristened their business as a hamburger stand, and later turned the company into a franchise, with the Golden Arches logo being introduced in 1953 at a location in Phoenix, Arizona. In 1955, Ray Kroc, a businessman, joined the company as a franchise agent and proceeded to purchase the chain from the McDonald brothers. McDonald's had its previous headquarters in Oak Brook, Illinois, but moved its global headquarters to Chicago in June 2018", "https://www.nrn.com/sites/nrn.com/files/styles/article_featured_standard/public/mcdonalds-logo.gif?itok=U_TliriA", 1, "McDonalds", "19991", "4.5", "From 11:00 AM To 02:15 AM" },
                    { 2, "19 Shabab El Mohandessin Blgs., Extension of Makram Ebeid Makram Ebeid, Nasr City", 4, "Sayed Hanafy restaurants chain is one of the most renowed authentic restaurant chain in Egypt. Started at 1952 from a koshary cart, until the first branch was established in 1985.", "https://scontent.fcai3-1.fna.fbcdn.net/v/t1.0-9/80390516_2962955900382554_7936731969642037248_o.jpg?_nc_cat=108&ccb=3&_nc_sid=09cbfe&_nc_eui2=AeFXUmEyrlQcE9kS0CRed1hSadTxKq-e0Mxp1PEqr57QzORy-uTd2-XxCfuIsbKZWCw&_nc_ohc=GDzNSruJTnYAX-4LMAT&_nc_ht=scontent.fcai3-1.fna&oh=b0869207a61c893aea55de10a887bcac&oe=605F788F", 2, "Koshary Sayed Hanafy", "16920", "4.6", "From 09:00 AM To 01:00 AM" },
                    { 3, "84 Hassan Ma'moon in front of Al Ahly Club Al-Ahly Club, Nasr City", 4, "History told of El-Dahan's success since 1890. It was first was named 'El-Akr' & was known for its Eastern cuisine & barbequed meat. Now we have opened new branches in the 5th Settlement, Rehab City & in Merghany. We hope to continue our success through your support & confidence. \"Combine tradition & originality\".", "https://s3-eu-west-1.amazonaws.com/elmenusv5-stg/Normal/79262228-7ae9-48a0-8dd1-2db9f18d3ff0.jpg", 3, "ElDahan", "16194", "4.5", "From 09:00 AM To 12:30 AM" },
                    { 4, "27 El Batrawy St. Off Abbas El Akkad St. Abbas El Akkad, Nasr City", 4, "It all started with four passionate brothers who shared one big appetite, an obsession with juicy burgers and a demanding palate. They focused their energy on challenging the conventional idea that food served fast must be a junk experience and instead worked on refining it to become an elevated culinary adventure where ingredients are naturally", "https://scontent.fcai3-1.fna.fbcdn.net/v/t1.0-9/110249313_10158045681834681_5826574394868309860_o.jpg?_nc_cat=1&ccb=3&_nc_sid=09cbfe&_nc_eui2=AeGKS-9i1ns8Ux0u1h6Sh5m44DX-FVvfQFXgNf4VW99AVcpR7In_bHJ8DAoSvYZgjWg&_nc_ohc=86qMH8TqTvkAX-YWQ97&_nc_ht=scontent.fcai3-1.fna&oh=5239a5e67c94134451274c0d8bab0f7b&oe=60619A20", 4, "Buffalo Burger", "19914", "4.6", "From 11:00 AM To 02:45 AM" }
                });

            migrationBuilder.InsertData(
                table: "ShoppingCartItems",
                columns: new[] { "Id", "Amount", "DishId", "ShoppingCartId" },
                values: new object[] { 1, 3, 1, "30377b50-77fc-4e43-81d0-bbdc0e188ccb" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryRestaurant_RestaurantsId",
                table: "CategoryRestaurant",
                column: "RestaurantsId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_CategoryId",
                table: "Dishes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_MenuId",
                table: "Dishes",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_DishOfTheWeekId",
                table: "Menus",
                column: "DishOfTheWeekId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDetails_DishId",
                table: "OrdersDetails",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_OrdersDetails_OrderId",
                table: "OrdersDetails",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Restaurants_MenuId",
                table: "Restaurants",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCartItems_DishId",
                table: "ShoppingCartItems",
                column: "DishId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryRestaurant_Restaurants_RestaurantsId",
                table: "CategoryRestaurant",
                column: "RestaurantsId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Dishes_Menus_MenuId",
                table: "Dishes",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Categories_CategoryId",
                table: "Dishes");

            migrationBuilder.DropForeignKey(
                name: "FK_Dishes_Menus_MenuId",
                table: "Dishes");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CategoryRestaurant");

            migrationBuilder.DropTable(
                name: "OrdersDetails");

            migrationBuilder.DropTable(
                name: "ShoppingCartItems");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Dishes");
        }
    }
}
