using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public class SqlRepository : IRepository
    {
        public bool Create(Guid id)
        {
            return true;
        }

        public bool Read(Guid id)
        {
            return true;
        }

        public bool Update(Guid id)
        {
            return true;
        }

        public bool Delete(Guid id)
        {
            return true;
        }
    }
}
