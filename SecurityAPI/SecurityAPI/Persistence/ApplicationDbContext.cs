using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SecurityAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityAPI.Persistence
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(
           DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<ApplicationUser>().ToTable("ApplicationUsers");
            builder.Entity<ApplicationRole>().ToTable("ApplicationRoles");
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("ApplicationUserClaims");
            builder.Entity<IdentityUserRole<Guid>>().ToTable("ApplicationUserRoles")
                .HasKey(x => new { x.UserId, x.RoleId }); // Set relationship cho bảng ApplicationUserRoles

            //Trường hợp update database bị lỗi The entity type 'IdentityUserLogin<int>' requires a primary key to be defined. If you intended to use a keyless entity type, call 'HasNoKey' in 'OnModelCreating'
            // thì comment đoạn này lại => sau đó update xong lại mở ra chạy lại

            builder.Entity<IdentityUserLogin<Guid>>().ToTable("ApplicationUserLogins").HasKey(x => x.UserId);
            //end xảy ra lỗi
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("ApplicationRoleClaims");
            builder.Entity<IdentityUserToken<Guid>>().ToTable("ApplicationUserTokens").HasKey(x => x.UserId);

            // Set length cho column UserName
            builder.Entity<ApplicationUser>()
                .Property(x => x.UserName)
                .HasMaxLength(300);

            // Set length cho column NormalizedUserName
            builder.Entity<ApplicationUser>()
                .Property(x => x.NormalizedUserName)
                .HasMaxLength(300);

            // Set Unique Index cho column UserName và NormalizedUserName
            builder.Entity<ApplicationUser>()
                .HasIndex(x => new { x.UserName, x.NormalizedUserName, x.Email, x.NormalizedEmail })
                .IsUnique();

            // Set Index cho column UserId và RoleId
            builder.Entity<IdentityUserRole<Guid>>()
                .HasIndex(x => new { x.UserId, x.RoleId })
                .IsUnique(false);
        }
    }
}
