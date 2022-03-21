using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class OgrDbContext:DbContext
    {
        public OgrDbContext(DbContextOptions<OgrDbContext> options) :base(options)
        {

        }

        public DbSet<Ogrenci> Ogrenciler { get; set; }
        public DbSet<Not> Notlar { get; set; }
        public DbSet<myConfig> MyConfigs { get; set; }

        public DbSet<Ogretmen> Ogretmenler { get; set; }

    }
}
