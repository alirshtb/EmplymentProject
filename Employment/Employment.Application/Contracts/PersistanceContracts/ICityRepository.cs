﻿using Employment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employment.Application.Contracts.PersistanceContracts
{
    public interface ICityRepository : IGenericRepository<City>
    {
        bool IsExists(string name);
        bool IsInCountry(int cityId, int countryId);
    }
}
