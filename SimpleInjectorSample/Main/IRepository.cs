using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    public interface IRepository
    {
        bool Create(Guid id);

        bool Read(Guid id);

        bool Update(Guid id);

        bool Delete(Guid id);
    }
}
