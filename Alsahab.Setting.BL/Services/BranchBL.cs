using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using Alyatim.Member.SC.Messages;
using Alsahab.Common;
using Alsahab.Common.Exceptions;
using Alsahab.Setting.BL.Validation;

namespace Alsahab.Setting.BL
{
    public class BranchBL : BaseBL<Branch, BranchDTO, BranchFilterDTO>
    {
        #region properties
        private readonly IBaseDL<Branch, BranchDTO, BranchFilterDTO> _BranchDL;
        private readonly IBaseDL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO> _BranchRegionWorkDL;
        #endregion properties

        #region constructor
        public BranchBL(IBaseDL<Branch, BranchDTO, BranchFilterDTO> branchDL,
                        IBaseDL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO> branchRegionWorkDL,
                        IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL)
            : base(branchDL, logDL)
        {
            _BranchDL = branchDL;
            _BranchRegionWorkDL = branchRegionWorkDL;
            FormHasTree = true;
            NeedToAutoCode = true;
        }
        #endregion constructor

        #region Async methods
        public async override Task<IList<BranchDTO>> GetAsync(BranchFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var response = await base.GetAsync(filter, cancellationToken, paging);// await _BranchDL.GetAsync(filter, cancellationToken, paging);

            if (!(response.Count > 0))
                return response;

            var memberResponse = ServiceUtility.CallMember(s => s.Person(new PersonRequest
            {
                ActionType = Gostar.Common.ActionType.Select,
                //TODO:
                User = new Gostar.Common.UserInfoDTO { UserID = 1 },
                PersonFilter = new Alyatim.Member.DTO.PersonFilterDTO
                {
                    IDList = response?.Select(t => t.HeadPersonID)?.ToList(),
                }
            }))?.ResponseDtoList;

            if (memberResponse?.Count > 0)
            {
                response = (from r in response
                            join p in memberResponse on r.HeadPersonID equals p.ID into TempResult
                            from x in TempResult.DefaultIfEmpty()
                            select new BranchDTO
                            {
                                ID = r.ID,
                                Code = r.Code,
                                ParentID = r.ParentID,
                                Title = r.Title,
                                IsCentral = r.IsCentral,
                                HeadPersonID = x?.ID,
                                HeadMemberName = x?.FullName,
                                HeadMemberPhoneNo = x?.MobileNo,
                                BranchPhoneNo = r.BranchPhoneNo,
                                BranchEmail = r.BranchEmail,
                                BranchAddressID = r.BranchAddressID,
                                BranchComment = r.BranchComment,
                                CreateDate = r.CreateDate,
                                IsDeleted = r.IsDeleted,
                                RightIndex = r.RightIndex,
                                LeftIndex = r.LeftIndex,
                                Depth = r.Depth,
                                OldCode = r.OldCode
                            })?.ToList();
            }
            return response;
        }
        #endregion Async methods

        #region Validation
        public async override Task CheckDeletePermisionAsync(BranchDTO data, CancellationToken cancellationToken)
        {
            await base.CheckDeletePermisionAsync(data, cancellationToken);

            if ((await _BranchRegionWorkDL.GetAsync(new BranchRegionWorkFilterDTO { BranchID = data?.ID }, cancellationToken)).Count > 0)
                throw new AppException(ResponseStatus.BadRequest, "This node is used in another tables [ node Regions ], Please delete them first");
        }
        #endregion Validation
    }
}
