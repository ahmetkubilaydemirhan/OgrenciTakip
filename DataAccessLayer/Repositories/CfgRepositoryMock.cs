using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repositories
{
    public class CfgRepositoryMock : IRepository<myConfig>
    {
        static List<myConfig> resultSet;
        public CfgRepositoryMock()
        {
            resultSet = new List<myConfig>();
        }
        public myConfig AddOrUpdate(myConfig entity)
        {
            resultSet.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            resultSet = resultSet.Where(c => c.Id != id).ToList();
        }

        public myConfig GetById(int id)
        {
            return resultSet.FirstOrDefault(c => c.Id == id);
        }

        public List<myConfig> List()
        {
            return resultSet;
        }
    }
}
