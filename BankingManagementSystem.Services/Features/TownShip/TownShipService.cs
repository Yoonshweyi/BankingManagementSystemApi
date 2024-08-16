using BankingManagementSystem.Models.Setup.State;
using BankingManagementSystem.Models.Setup.Township;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystem.Services.Features.TownShip
{
    public class TownShipService
    {
        private readonly AppDbContext _appDbContext;

        public TownShipService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

       
        #region Get TownShips
        public async Task<TownshipListResponseModel> GetTownshipList()
        {
            var query = _appDbContext.TblPlaceTownships.AsNoTracking();
            var result= await query.OrderBy(x=>x.TownshipName).ToListAsync();

            var lst=result.Select(x=>x.Change()).ToList();
            TownshipListResponseModel model = new TownshipListResponseModel()
            {
                Data = lst,
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }
        #endregion


        #region Get TownShip By TownshipCode
        public async Task<TownshipResponseModel> GetTownShipByCode(string townshipCode)
        {
            var query = _appDbContext.TblPlaceTownships.AsNoTracking();
            var item=await query.FirstOrDefaultAsync(x=>x.TownshipCode == townshipCode);

            TownshipResponseModel model = new TownshipResponseModel()
            {
                Data = item!.Change(),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }
        #endregion

        #region Create Township
        public async Task<TownshipResponseModel> CreateTownShip(TownshipRequestModel reqmodel)
        {
            var item=reqmodel.Change();
            await _appDbContext.TblPlaceTownships.AddAsync(item);
            var result=await _appDbContext.SaveChangesAsync();

            TownshipResponseModel model = new TownshipResponseModel()
            {
                Data = item!.Change(),
                Response = new MessageResponseModel(true, "Township has created successfully")
            };
            return model;
        }

        #endregion

        #region Update Township
        public async Task<TownshipResponseModel> UpdateTownship(string townshipCode, TownshipRequestModel reqModel)
        {
            var query = _appDbContext.TblPlaceTownships.AsNoTracking();
            var item=await query.FirstOrDefaultAsync(x=>x.TownshipCode==townshipCode);  
           // item!.TownshipCode=reqModel.TownshipCode;
            item!.TownshipName=reqModel.TownshipName;
            item.StateCode=reqModel.StateCode;

            _appDbContext.Entry(item).State = EntityState.Modified;
            _appDbContext.TblPlaceTownships.Update(item);
            var result= await _appDbContext.SaveChangesAsync();

            TownshipResponseModel model = new TownshipResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "Township has updated successfully.")
            };
            return model;
            
        }
        #endregion


        #region Delete Township
        public async Task<TownshipResponseModel> DeleteTownShip(string townShipCode)
        {
            var query = _appDbContext.TblPlaceTownships.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(x => x.TownshipCode == townShipCode);
            _appDbContext.Entry(item!).State = EntityState.Deleted;
            _appDbContext.TblPlaceTownships.Remove(item);
            var result=await _appDbContext.SaveChangesAsync();
            TownshipResponseModel model = new TownshipResponseModel()
            {
                Response= new MessageResponseModel(true,"Township has deleted successfully.")
            };

            return model;
        }
        #endregion

        #region Get Township by StateCode

        public async Task<TownshipListResponseModel> GtTownshipByStateCode(string stateCode)
        {
            var query = _appDbContext
                       .TblPlaceTownships
                       .AsNoTracking()
                       .Where(x => x.StateCode == stateCode);
            var lst=await query.ToListAsync();
            TownshipListResponseModel model = new TownshipListResponseModel()
            {
                Data = lst.Select(x => x.Change()).ToList(),
                Response = new MessageResponseModel(true, "success")

            };
            return model;
        }
        #endregion

    }
}
