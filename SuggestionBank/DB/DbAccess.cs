using Microsoft.EntityFrameworkCore;
using SuggestionBank.Helpers;
using SuggestionBank.Model;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SuggestionBank.DB
{
    public class DbAccess : DbContext
    {
        public DbSet<Department> Department { get; set; }
        public DbSet<Suggestions> Suggestions { get; set; }

        public DbAccess()
        {
            this.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var e = DependencyService.Get<IDBPath>();
            var dbPath = e.GetDbPath();
            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }
    }
}
