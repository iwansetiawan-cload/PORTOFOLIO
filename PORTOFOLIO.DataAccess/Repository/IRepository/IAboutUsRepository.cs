﻿using PORTOFOLIO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PORTOFOLIO.DataAccess.Repository.IRepository
{
    public interface IAboutUsRepository : IRepository<AboutUs>
    {
        void Update(AboutUs obj);   
    }
}
