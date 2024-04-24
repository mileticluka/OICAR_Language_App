using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataContext : DbContext
    {
        private readonly IConfiguration configuration;

        public DataContext(IConfiguration configuration, DbContextOptions<DataContext> options) : base(options) {
            this.configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("API"));

            //optionsBuilder.UseSqlServer(connectionString, b => b.MigrationsAssembly("API"));
        }
       
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
