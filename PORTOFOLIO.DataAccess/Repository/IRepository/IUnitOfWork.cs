using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTOFOLIO.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBrandaRepository Branda { get; }
        IAboutUsRepository AboutUs { get; }
        IServicesRepository Services { get; }
        IPortofolioRepository Portofolio { get; }
        ITeamRepository Team { get; }
        void Save();
    }
}
