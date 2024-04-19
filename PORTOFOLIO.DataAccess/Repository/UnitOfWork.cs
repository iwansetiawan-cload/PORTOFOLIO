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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Branda = new BrandaRepository(_db);
            AboutUs = new AboutUsRepository(_db); 
            Services = new ServicesRepository(_db);
            Portofolio = new PortofolioRepository(_db);
            Team = new TeamRepository(_db);
        }
        public IBrandaRepository Branda { get; private set; }
        public IAboutUsRepository AboutUs { get; private set; }        
        public IServicesRepository Services { get; private set; }
        public IPortofolioRepository Portofolio { get; private set; }
        public ITeamRepository Team { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
