using PORTOFOLIO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTOFOLIO.DataAccess.Repository.IRepository
{
    public interface IMsUDCRepository : IRepository<MsUDC>
    {
        void Update(MsUDC obj);
    }
}
