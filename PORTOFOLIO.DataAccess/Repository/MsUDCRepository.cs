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
    public class MsUDCRepository : Repository<MsUDC>, IMsUDCRepository
    {
        private readonly ApplicationDbContext _db;
        public MsUDCRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(MsUDC vm)
        {        
            _db.MsUDC.Update(vm);
        }
    }
}
