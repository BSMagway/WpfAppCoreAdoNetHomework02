using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppAdoNetHomework02.Model
{
    public class MusicShopContext : DbContext
    {
        string ConnectionString { get; set; }
        int SelectorBD { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            switch (SelectorBD)
            {
                case 0:
                    optionsBuilder.UseSqlServer(ConnectionString);
                    break;
                case 1:
                    //optionsBuilder.UseMySql(ConnectionString, new MySqlServerVersion(new Version(8, 0, 25)));
                    break;
                case 2:
                    //optionsBuilder.UseNpgsql(ConnectionString);
                    break;
                default:
                    break;
            }
        }

        public DbSet<CompactDisc> CompactDiscs { get; set; }
        public DbSet<CompactDiscOnWarehouse> CompactDiscOnWarehouses { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<SingerOrGroupName> SingerOrGroupNames { get; set; }

        public MusicShopContext(string connectionString, int selectorBD)
        {
            ConnectionString = connectionString;
            SelectorBD = selectorBD;
            Database.EnsureCreated();
        }
    }
}
