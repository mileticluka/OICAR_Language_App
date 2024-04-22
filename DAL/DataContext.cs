using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<GameObject> GameObject { get; set; }
        public DbSet<ContextImage> ContextImage { get; set; }
        public DbSet<LanguageStat> LanguageStat { get; set; }
        public DbSet<GameFillBlank> GameFillBlank { get; set; }
        public DbSet<GameFlashCard> GameFlashCard { get; set; }
        public DbSet<GamePickSentence> GamePickSentence { get; set; }
        public DbSet<GameObjectCategory> GameObjectCategory { get; set; }
        public DbSet<GameObjectLocalized> GameObjectLocalized { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
    }
}
