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
    public class AboutUsRepository : Repository<AboutUs>, IAboutUsRepository
    {
        private readonly ApplicationDbContext _db;
        public AboutUsRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }    
        public void Update(AboutUs aboutus)
        {
            //var objFromDb = _db.Branda.FirstOrDefault(s => s.Id == branda.Id);
            //if (objFromDb != null)
            //{
            //    objFromDb.Title = branda.Title;
            //    objFromDb.Content = branda.Content;
            //    objFromDb.Status = branda.Status;
            //    _db.SaveChanges();
            //}
            _db.AboutUs.Update(aboutus);
        }
    }
}
