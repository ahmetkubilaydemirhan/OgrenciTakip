using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class NotRepository:BaseRepository<Not>
    {
        public NotRepository(OgrDbContext ctx):base(ctx)
        { 
        }

        public List<Not> ListByOgrId(int ogrId)
        {
            return _ctx.Notlar.Where(c => c.OgrId == ogrId).ToList();
        }


    }
}
