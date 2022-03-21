using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class OgretmenRepository:BaseRepository<Ogretmen>,IRepository<Ogretmen>
    {
        public OgretmenRepository(OgrDbContext ctx):base(ctx)
        {

        }
    }
}
