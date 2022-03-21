using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
   public  class MyCfgRepository:BaseRepository<myConfig>, IRepository<myConfig>
    {
        public MyCfgRepository(OgrDbContext ctx):base(ctx)
        {

        }
    }
}
