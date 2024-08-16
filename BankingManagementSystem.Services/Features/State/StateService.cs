using BankingManagementSystem.Models;
using BankingManagementSystem.Models.Setup.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingManagementSystem.Services.Features.State
{
    public class StateService
    {
        private readonly AppDbContext _appDbContext;

        public StateService(AppDbContext appDbContext)
        {

        _appDbContext = appDbContext; 
        }
        #region GetStates
        public async Task<StateListResponseModel> GetStates()
        {
           
            var responseModel = new StateListResponseModel();

            var query = _appDbContext.TblPlaceStates.AsNoTracking();

            var result = await query.OrderBy(x => x.StateName).ToListAsync();

            var lst = result.Select(x => x.Change()).ToList();

            responseModel.Data = lst;

            responseModel.Response = new MessageResponseModel(true, "Success");

            return responseModel;
        }

        #endregion


        #region GetStateByCode
        public async Task<StateResponseModel> GetStateByCode(string stateCode)
        {
            var query = _appDbContext.TblPlaceStates.AsNoTracking();
            var item=await query.FirstOrDefaultAsync(x=>x.StateCode == stateCode);

            StateResponseModel model = new StateResponseModel()
            {
                Data = item!.Change(),
                Response = new MessageResponseModel(true, "Success")
            };
            return model;
        }
        #endregion

        #region Create State
        public async Task<StateResponseModel> CreateState(StateRequestModel reqModel)
        {
            var stateCode=await _appDbContext.TblPlaceStates.AsNoTracking().MaxAsync(x=>x.StateCode);
            reqModel.StateCode = stateCode.GenerateCode(EnumCodePrefix.S.ToString());
            var item=reqModel.Change();
            await _appDbContext.TblPlaceStates.AddAsync(item);
            var result=await _appDbContext.SaveChangesAsync();

            StateResponseModel model = new StateResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "Sate has created successfully.")
            };
            return model;
        }
        #endregion

        #region Update State
        public async Task<StateResponseModel> UpdateState(string stateCode, StateRequestModel reqmodel)
        {
            var query=_appDbContext.TblPlaceStates.AsNoTracking();
            var item=await query.FirstOrDefaultAsync(x=> x.StateCode == stateCode);

            if(item is null)
            {
                throw new Exception("State is null.");
            }
            item.StateName=reqmodel.StateName;
            _appDbContext.Entry(item).State=EntityState.Modified;
            _appDbContext.TblPlaceStates.Update(item);
            var result=await _appDbContext.SaveChangesAsync();
            StateResponseModel model = new StateResponseModel()
            {
                Data = item.Change(),
                Response = new MessageResponseModel(true, "State has updated successfully.")
            };
            return model;
        }
        #endregion

        #region Delete State
        public async Task<StateResponseModel> DeleteState(string stateCode)
        {
            var query = _appDbContext.TblPlaceStates.AsNoTracking();
            var item = await query.FirstOrDefaultAsync(x => x.StateCode == stateCode);
            _appDbContext.Entry(item!).State = EntityState.Deleted;
            _appDbContext.TblPlaceStates.Remove(item!);
            var result = await _appDbContext.SaveChangesAsync();
            StateResponseModel model = new StateResponseModel()
            {
                Response = new MessageResponseModel(true, "State has deleted successfully.")
            };
            return model;
        }
        #endregion

        
    }
}
