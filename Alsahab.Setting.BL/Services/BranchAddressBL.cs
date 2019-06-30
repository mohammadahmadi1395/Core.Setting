using System;
using System.Collections.Generic;
using System.Linq;
using Alsahab.Common.Exceptions;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using Alsahab.Common;
using System.Threading.Tasks;
using System.Threading;

namespace Alsahab.Setting.BL
{
    public class BranchAddressBL : BaseBL<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO>
    {
        private readonly IBaseDL<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO> _BranchAddressDL;
        private readonly IBaseDL<Branch, BranchDTO, BranchFilterDTO> _BranchDL;
        private readonly IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> _ZoneDL;
        public BranchAddressBL(IBaseDL<BranchAddress, BranchAddressDTO, BranchAddressFilterDTO> branchAddressDL,
                                IBaseDL<Branch, BranchDTO, BranchFilterDTO> branchDL,
                                IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> zoneDL)
            : base(branchAddressDL)
        {
            _BranchAddressDL = branchAddressDL;
            _BranchDL = branchDL;
            _ZoneDL = zoneDL;
        }

        /// <summary>
        /// Cehck Address Date
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <summary>
        /// Check Data For Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool Validate(BranchAddressDTO data)
        {
            return Validate<Validation.BLBranchAddressValidator, BranchAddressDTO>(data ?? new BranchAddressDTO());
        }

        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> CheckDeletePermission(BranchAddressDTO data, CancellationToken cancellationToken)
        {
            if (!(data.ID > 0))
                throw new AppException(ResponseStatus.BadRequest, "Entered BranchAddress is Mistake");

            if ((await _BranchDL.GetAsync(new BranchFilterDTO { BranchAddressID = data.ID }, cancellationToken)).Count() > 0)
                throw new AppException(ResponseStatus.LoginError, "This BranchAddress use in another Tables,Please Delete  them First");

            return true;
        }

        /// <summary>
        /// Get List of BranchAddress From Database Whith DTO
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override async Task<IList<BranchAddressDTO>> GetAsync(BranchAddressFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            // zbl.User = User;
            var responseAddress = await _BranchAddressDL.GetAsync(filter, cancellationToken, paging);
            ResultCount = _BranchAddressDL.ResultCount;

            var allZone = await _ZoneDL.GetAllAsync(cancellationToken);

            foreach (var val in responseAddress)
                val.ZoneDTO.ZoneAddress = allZone.FirstOrDefault(s => s.ID == val.ZoneDTO.ID)?.ZoneAddress;

            return responseAddress;
        }

        /// <summary>
        /// Insert BranchAddress In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override async Task<BranchAddressDTO> InsertAsync(BranchAddressDTO data, CancellationToken cancellationToken)
        {
            Validate(data);

            data.CreateDate = DateTime.Now;

            var response = await _BranchAddressDL.InsertAsync(data, cancellationToken);
            if (_BranchAddressDL?.ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.DatabaseError, _BranchDL.ErrorMessage);

            response = await _BranchAddressDL.GetByIdAsync(cancellationToken, response.ID);
            Observers.ObserverStates.BranchAddressAdd state = new Observers.ObserverStates.BranchAddressAdd
            {
                BranchAddress = response,
                User = User,
            };
            Notify(state);

            return response;
        }

        public override async Task<IList<BranchAddressDTO>> InsertListAsync(IList<BranchAddressDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }

            var response = await _BranchAddressDL.InsertListAsync(data, cancellationToken);
            if (_BranchAddressDL.ResponseStatus != ResponseStatus.Successful)
                throw new AppException(ResponseStatus.DatabaseError, _BranchAddressDL.ErrorMessage);

            List<BranchAddressDTO> respList = new List<BranchAddressDTO>();
            foreach (var val in response)
            {
                var resp = await _BranchAddressDL.GetByIdAsync(cancellationToken, val?.ID);
                Observers.ObserverStates.BranchAddressAdd state = new Observers.ObserverStates.BranchAddressAdd
                {
                    BranchAddress = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            return respList;
        }

        /// <summary>
        /// BranchAddressUpdate
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override async Task<BranchAddressDTO> UpdateAsync(BranchAddressDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);

            Validate(data);

            var response = await _BranchAddressDL.UpdateAsync(data, cancellationToken);

            response = await _BranchAddressDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);

            Observers.ObserverStates.BranchAddressEdit state = new Observers.ObserverStates.BranchAddressEdit
            {
                BranchAddress = response,
                User = User,
            };
            Notify(state);

            return response;
        }

        /// <summary>
        /// Delete Logically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public override async Task<BranchAddressDTO> SoftDeleteAsync(BranchAddressDTO data, CancellationToken cancellationToken)
        {
            await CheckDeletePermission(data, cancellationToken);

            data = await _BranchAddressDL.GetByIdAsync(cancellationToken, data.ID ?? 0);
            data.IsDeleted = true;

            var response = await _BranchAddressDL.UpdateAsync(data, cancellationToken);

            Observers.ObserverStates.BranchAddressDelete state = new Observers.ObserverStates.BranchAddressDelete
            {
                BranchAddress = response,
                User = User,
            };
            Notify(state);
            
            return response;
        }

        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
    }
}
