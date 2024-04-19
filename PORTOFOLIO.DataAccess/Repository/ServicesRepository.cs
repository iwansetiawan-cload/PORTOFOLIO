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
    public class ServicesRepository : Repository<Services>, IServicesRepository
    {
        private readonly ApplicationDbContext _db;
        public ServicesRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }    
        public void Update(Services servis)
        {        
            _db.Services.Update(servis);
        }
    }
}
