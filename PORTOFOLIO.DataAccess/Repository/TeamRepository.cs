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
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        private readonly ApplicationDbContext _db;
        public TeamRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }    
        public void Update(Team vm)
        {        
            _db.Team.Update(vm);
        }
    }
}
