using web.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public static class DbInitializer
    {
        public static async void Initialize(BlagajnaContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Users.Any())
            {
                var testUsers = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    UserName = "user1@finance.com",
                    Email = "user1@finance.com",
                    FirstName = "Alice",
                    LastName = "Smith",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    UserName = "user2@finance.com",
                    Email = "user2@finance.com",
                    FirstName = "Bob",
                    LastName = "Johnson",
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    UserName = "user3@finance.com",
                    Email = "user3@finance.com",
                    FirstName = "Charlie",
                    LastName = "Brown",
                    EmailConfirmed = true
                }
            };

                foreach (var u in testUsers)
                {
                    var password = new PasswordHasher<ApplicationUser>();
                    var result = password.HashPassword(u, "Password123!");
                    context.Users.Add(u);
                }
            }

            // Seed Categories
            if (!context.Categories.Any())
            {
                var categories = new List<Category>
            {
                new Category { Name = "Food", Description = "Expenses for food and dining" },
                new Category { Name = "Rent", Description = "Monthly rent payments" },
                new Category { Name = "Utilities", Description = "Utility bills like electricity, water, etc." },
                new Category { Name = "Entertainment", Description = "Movies, games, and other leisure activities" }
            };

                context.Categories.AddRange(categories);
                context.SaveChanges();
            }

            // Seed Transactions and Budgets for Each User
            var users = context.Users.ToList();
            var categoriesList = context.Categories.ToList();

            foreach (var u in users)
            {
                if (!context.Transactions.Any(t => t.User == u))
                {
                    var transactions = new List<Transaction>
                {
                    new Transaction { Amount = 50.75m, Date = DateTime.UtcNow, Description = "Grocery shopping", User = u,  Category = categoriesList[0] },
                    new Transaction { Amount = 1200.00m, Date = DateTime.UtcNow.AddDays(-1), Description = "Monthly rent", User = u, Category = categoriesList[1] },
                    new Transaction { Amount = 60.00m, Date = DateTime.UtcNow.AddDays(-2), Description = "Electricity bill", User = u, Category = categoriesList[2] },
                    new Transaction { Amount = 30.00m, Date = DateTime.UtcNow.AddDays(-3), Description = "Movie night", User = u, Category = categoriesList[3] }
                };

                    context.Transactions.AddRange(transactions);
                    context.SaveChanges();
                }

                if (!context.Budgets.Any(b => b.User == u))
                {
                    var budgets = new List<Budget>
                {
                    new Budget { Amount = 500.00m, StartDate = DateTime.UtcNow.AddMonths(-1), EndDate = DateTime.UtcNow.AddMonths(1), User = u, Category = categoriesList[0] },
                    new Budget { Amount = 1500.00m, StartDate = DateTime.UtcNow.AddMonths(-1), EndDate = DateTime.UtcNow.AddMonths(1), User = u, Category = categoriesList[1] },
                    new Budget { Amount = 200.00m, StartDate = DateTime.UtcNow.AddMonths(-1), EndDate = DateTime.UtcNow.AddMonths(1), User = u, Category = categoriesList[2] },
                    new Budget { Amount = 300.00m, StartDate = DateTime.UtcNow.AddMonths(-1), EndDate = DateTime.UtcNow.AddMonths(1), User = u, Category = categoriesList[3] }
                };

                    context.Budgets.AddRange(budgets);
                    context.SaveChanges();
                }
            }

            // Seed Admin

            var roles = new IdentityRole[]{
                new IdentityRole{Id="1",Name="Administrator"},
                new IdentityRole{Id="2",Name="Manager"},
                new IdentityRole{Id="3",Name="Staff"}
            };

            if (!context.Roles.Any())
            {
                foreach (IdentityRole role in roles)
                {
                    context.Roles.Add(role);
                }
            };

            var user = new ApplicationUser
            {
                FirstName = "Admin",
                LastName = "Admin",
                Email = "admin@blagajna.com",
                NormalizedEmail = "ADMIN@BLAGAJNA.COM",
                UserName = "admin@example.com",
                NormalizedUserName = "admin@example.com",
                PhoneNumber = "+111111111111",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString("D")
            };

            if (!context.Users.Any(u => u.UserName == user.UserName))
            {
                var password = new PasswordHasher<ApplicationUser>();
                var hashed = password.HashPassword(user, "Admin@123");
                user.PasswordHash = hashed;
                context.Users.Add(user);
            }

            context.SaveChanges();

            var UserRoles = new IdentityUserRole<string>[]
            {
            new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=user.Id},
            new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id},
            };

            if (!context.UserRoles.Any())
            {
                foreach (IdentityUserRole<string> r in UserRoles)
                {
                    context.UserRoles.Add(r);
                }
            }

            context.SaveChanges();
        }
    }
}