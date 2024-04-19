using PORTOFOLIO.DataAccess.Data;
using PORTOFOLIO.DataAccess.Repository.IRepository;
using PORTOFOLIO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTOFOLIO.DataAccess.Repository
{
    public class PortofolioRepository : Repository<Portofolio>, IPortofolioRepository
    {
        private readonly ApplicationDbContext _db;
        public PortofolioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }    
        public void Update(Portofolio vm)
        {        
            _db.Portofolio.Update(vm);
        }
    }
}
