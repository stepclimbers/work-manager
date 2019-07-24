﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WorkManager.Data.Models;

namespace WorkManager.Data
{
    public class WorkManagerDbContext : IdentityDbContext<User, UserRole, int>
    {
        public WorkManagerDbContext(DbContextOptions<WorkManagerDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().ToTable("Users");
            builder.Entity<IdentityUserRole<int>>().ToTable("UserRoles");
            builder.Entity<IdentityUserLogin<int>>().ToTable("UserLogins");
            builder.Entity<IdentityUserClaim<int>>().ToTable("UserClaims");
            builder.Entity<UserRole>().ToTable("Roles");
        }
    }
}
