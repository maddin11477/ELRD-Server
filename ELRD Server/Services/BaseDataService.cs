using ELRDDataAccessLibrary.DataAccess;
using ELRDDataAccessLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELRDServerAPI.Services
{
    public class BaseDataService : IBaseDataService
    {
        private readonly ELRDContext _db;
        

        public BaseDataService(ELRDContext db)
        {
            _db = db;
        }

        public async Task<List<BaseUnit>> GetBaseUnitAsync()
        {
            return await _db.BaseUnits.ToListAsync();
        }

        public async Task SeedBaseUnits()
        {
            if (_db.BaseUnits.Count() == 0)
            {
                _db.BaseUnits.Add(new BaseUnit
                {
                    Callsign = "RK BISHM 71/1",
                    CrewCount = 2,
                    UnitTye = 0
                });

                _db.BaseUnits.Add(new BaseUnit
                {
                    Callsign = "RK BNEST 71/1",
                    CrewCount = 2,
                    UnitTye = 0
                });

                _db.BaseUnits.Add(new BaseUnit
                {
                    Callsign = "RK BNEST 71/2",
                    CrewCount = 2,
                    UnitTye = 0
                });

                await _db.SaveChangesAsync();
            }

        }
    }
}
