using ELRDDataAccessLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELRDServerAPI.Services
{
    public interface IBaseDataService
    {
        Task<List<BaseUnit>> GetBaseUnitAsync();
        Task SeedBaseUnits();
    }
}
