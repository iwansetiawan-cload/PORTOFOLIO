using PORTOFOLIO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTOFOLIO.DataAccess.Repository.IRepository
{
    public interface IBrandaRepository : IRepository<Branda>
    {
        void Update(Branda obj);   
    }
}
