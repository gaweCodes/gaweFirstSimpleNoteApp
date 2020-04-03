using System;
using GaweNotesApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GaweNotesApi.Database
{
    public class NoteDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public NoteDbContext(DbContextOptions options) : base(options) { }
        public new DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Note> Notes { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Note>(n => n.HasOne<ApplicationUser>("User") .WithMany("Notes").OnDelete(DeleteBehavior.Restrict));
        }
    }
}
