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
using Alsahab.Common.Utilities;
using Alsahab.Setting.BL.Validation;

namespace Alsahab.Setting.BL
{
    public class BranchBL : BaseBL<Branch, BranchDTO, BranchFilterDTO>
    {
        #region properties
        private IList<BranchDTO> _AllBranch;
        private IList<BranchDTO> AllBranch
        {
            get
            {
                if (!(_AllBranch?.Count > 0))
                    _AllBranch = _BranchDL.GetAll();
                return _AllBranch;
            }
        }
        private List<BranchDTO> TreeNodes = new List<BranchDTO>();
        private long? _index = 0, _depth = 2;
        #endregion properties

        #region dependency injection
        private readonly IBaseDL<Branch, BranchDTO, BranchFilterDTO> _BranchDL;
        private readonly IBaseDL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO> _BranchRegionWorkDL;
        public BranchBL(IBaseDL<Branch, BranchDTO, BranchFilterDTO> branchDL,
                        IBaseDL<BranchRegionWork, BranchRegionWorkDTO, BranchRegionWorkFilterDTO> branchRegionWorkDL)
            : base(branchDL)
        {
            _BranchDL = branchDL;
            _BranchRegionWorkDL = branchRegionWorkDL;
        }
        #endregion dependency injection

        #region Async methods
        public async override Task<IList<BranchDTO>> GetAsync(BranchFilterDTO filter, CancellationToken cancellationToken)
        {
            var response = await _BranchDL.GetAsync(filter, cancellationToken);

            if (_BranchDL.ResponseStatus != ResponseStatus.Successful)
                throw new AppException(ResponseStatus.DatabaseError, _BranchDL.ErrorMessage);

            if (!(response.Count > 0))
                return response;

            var memberResponse = ServiceUtility.CallMember(s => s.Person(new PersonRequest
            {
                ActionType = Gostar.Common.ActionType.Select,
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
                                ParentId = r.ParentId,
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
        public async override Task<BranchDTO> InsertAsync(BranchDTO data, CancellationToken cancellationToken)
        {
            Validate(data);

            data.CreateDate = DateTime.Now;

            var response = await _BranchDL.InsertAsync(data, cancellationToken);//.BranchInsert(data);            
            if (_BranchDL?.ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.DatabaseError, _BranchDL.ErrorMessage);

            UpdateTreeIndicesAndCodes();

            response = await _BranchDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);

            Observers.ObserverStates.BranchAdd state = new Observers.ObserverStates.BranchAdd
            {
                Branch = response,
                User = User,
            };
            Notify(state);

            return response;
        }
        public override async Task<IList<BranchDTO>> InsertListAsync(IList<BranchDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }

            var response = await _BranchDL.InsertListAsync(data, cancellationToken);
            if (_BranchDL.ResponseStatus != ResponseStatus.Successful)
                throw new AppException(ResponseStatus.DatabaseError, _BranchDL.ErrorMessage);

            UpdateTreeIndicesAndCodes();

            List<BranchDTO> respList = new List<BranchDTO>();
            foreach (var val in response)
            {
                var resp = (await GetAsync(new BranchFilterDTO { ID = val?.ID ?? 0 }, cancellationToken))?.FirstOrDefault();
                Observers.ObserverStates.BranchAdd state = new Observers.ObserverStates.BranchAdd
                {
                    Branch = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            return respList ?? response;
        }
        public override async Task<BranchDTO> UpdateAsync(BranchDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);

            Validate(data);

            var response = await _BranchDL.UpdateAsync(data, cancellationToken);
            if (_BranchDL?.ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.DatabaseError, _BranchDL.ErrorMessage);

            UpdateTreeIndicesAndCodes();

            response = await _BranchDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);

            Observers.ObserverStates.BranchEdit state = new Observers.ObserverStates.BranchEdit
            {
                Branch = response,
                User = User,
            };
            Notify(state);

            return response;
        }
        public override async Task<BranchDTO> SoftDeleteAsync(BranchDTO data, CancellationToken cancellationToken)
        {
            //Search For Use This Item Before Delete
            await CheckDeletePermision(data, cancellationToken);

            data = await _BranchDL.GetByIdAsync(cancellationToken, data.ID ?? 0);
            data.IsDeleted = true;
            var response = await _BranchDL.UpdateAsync(data, cancellationToken);

            UpdateTreeIndicesAndCodes();

            Observers.ObserverStates.BranchDelete state = new Observers.ObserverStates.BranchDelete
            {
                Branch = response,
                User = User,
            };
            Notify(state);

            return response;
        }
        #endregion Async methods

        #region Validation
        private bool Validate(BranchDTO data)
        {
            return Validate<BLBranchValidator, BranchDTO>(data ?? new BranchDTO());
        }
        private async Task<bool> CheckDeletePermision(BranchDTO data, CancellationToken cancellationToken)
        {
            if (!(data?.ID > 0))
                throw new AppException(ResponseStatus.BadRequest, "node Entered Is Mistake.");

            if ((await _BranchRegionWorkDL.GetAsync(new BranchRegionWorkFilterDTO { BranchID = data?.ID }, cancellationToken)).Count > 0)
                throw new AppException(ResponseStatus.BadRequest, "This node is used in another tables [ node Regions ], Please delete them first");

            var deletingItem = await _BranchDL.GetByIdAsync(cancellationToken, data.ID);
            var myLeft = deletingItem.LeftIndex; 
            var myRight = deletingItem.RightIndex;
            var AllZons = AllBranch;
            var deleteCount = AllZons.Where(i => i.LeftIndex >= myLeft && i.LeftIndex <= myRight && i.IsDeleted == false).Count();
            if (deleteCount > 1)
                throw new AppException(ResponseStatus.LoginError, "You can't delete this node. this node has child");

            return true;
        }
        #endregion Validation

        #region related to tree
        private IList<BranchDTO> UpdateTreeIndicesAndCodes()
        {
            _AllBranch = null;
            var branches = AllBranch;
            foreach (var node in branches)
            {
                node.LeftIndex = null;
                node.RightIndex = null;
                node.Depth = null;
                node.Code = null;
                TreeNodes.Add(node);
            }

            List<BranchDTO> rootList = TreeNodes.Where(i => !(i.ParentId > 0))?.ToList();
            foreach (var root in rootList)
                if (root?.ID > 0)
                {
                    _depth = 2;
                    RecursiveUpdateAllBranchIndices(root);
                }

            var codedBranches = GenerateNewCodeList(rootList);

            var result = _BranchDL.UpdateList(codedBranches);

            ResponseStatus = _BranchDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(ResponseStatus.DatabaseError, _BranchDL.ErrorMessage);

            return result;
        }
        // private async Task<IList<BranchDTO>> UpdateTreeIndicesAndCodesAsync(CancellationToken cancellationToken)
        // {
        //     var branches  = AllBranch;
        //     foreach (var node in branches)
        //     {
        //         node.LeftIndex = null;
        //         node.RightIndex = null;
        //         node.Depth = null;
        //         node.Code = null;
        //         TreeNodes.Add(node);
        //     }

        //     List<BranchDTO> rootList = TreeNodes.Where(i => !(i.ParentID > 0))?.ToList();
        //     foreach (var root in rootList)
        //         if (root?.ID > 0)
        //         {
        //             _depth = 2;
        //             RecursiveUpdateAllBranchIndices(root);
        //         }

        //     GenerateNewCodeList(rootList);

        //     var result = await _BranchDL.UpdateListAsync(TreeNodes, cancellationToken);

        //     ResponseStatus = _BranchDL.ResponseStatus;
        //     if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //         throw new AppException(ResponseStatus.DatabaseError, _BranchDL.ErrorMessage);

        //     return result;
        // }

        // مراحل:
        // ۱- ابتدا اندیس چپ را تنظیم می‌کند
        // ۲- سپس عمق را تنظیم می‌کند
        // ۳- اندیس چپ و عمق را برای فرزندش در صورت وجود تنظیم می‌کند
        // ۴- در صورت عدم وجود فرزند، اندیس راست را تنظیم می‌کند
        // ۵- به سراغ برادر (در صورت وجود) می‌رود و مراحل اول تا چهارم را برای آن انجام می‌دهد
        private void RecursiveUpdateAllBranchIndices(BranchDTO BranchDLta)
        {
            if (!(BranchDLta?.ID > 0) || !(TreeNodes?.Count > 0))
                return;

            TreeNodes.FirstOrDefault(i => i.ID == BranchDLta.ID).LeftIndex = ++_index;
            TreeNodes.FirstOrDefault(i => i.ID == BranchDLta.ID).Depth = _depth;

            var tempChild = GetNotIndexedChild(BranchDLta);
            if (tempChild?.ID > 0)
            {
                _depth++;
                RecursiveUpdateAllBranchIndices(tempChild);
            }

            TreeNodes.FirstOrDefault(i => i.ID == BranchDLta.ID).RightIndex = ++_index;
            var tempBrother = GetNotIndexedBrother(BranchDLta);

            if (tempBrother?.ID > 0)
                RecursiveUpdateAllBranchIndices(tempBrother);
            else
                _depth--;
        }
        // private String GenerateCodeInInsert(BranchDTO data)
        // {
        //     if (!(data?.ParentId > 0))
        //         return (AllBranch?.Where(s => s.ParentId == null)?.ToList()?.Count + 1).ToString();
        //     else
        //     {
        //         var r = AllBranch?.Where(s => s.ParentId == data?.ParentId)?.ToList()?.Count;
        //         var parentCode = AllBranch?.FirstOrDefault(s => s.ID == data?.ParentId)?.Code;
        //         return String.Format("{0}-{1}", parentCode, (r + 1)?.ToString());
        //     }
        // }
        private List<BranchDTO> GenerateNewCodeList(List<BranchDTO> data)
        {
            List<BranchDTO> res = new List<BranchDTO>();
            for (int thisBranch = 0; thisBranch < data?.Count; thisBranch++)
            {
                var parent = GetParent(data[thisBranch]);
                data[thisBranch].Code = (parent == null) ?
                    string.Format("{0}", (thisBranch + 1)) :
                    string.Format("{0}-{1}", parent?.Code, (thisBranch + 1));

                res.Add(data[thisBranch]);

                var childs = AllBranch?.Where(s => s.ParentId == data[thisBranch]?.ID)?.ToList();

                for (int child = 0; child < childs?.Count; child++)
                {
                    childs[child].Code = string.Format("{0}-{1}", data[thisBranch].Code, (child + 1));
                    res.Add(childs[child]);
                    res.AddRange(GenerateNewCodeList(AllBranch?.Where(s => s.ParentId == childs[child]?.ID)?.ToList()));
                }
            }
            return res;
        }
        private BranchDTO GetNotIndexedBrother(BranchDTO node)
        {
            if (!(node.ParentId > 0))
                return null;
            var parent = TreeNodes.FirstOrDefault(i => i.ID == node.ParentId);
            var brother = GetNotIndexedChild(parent);
            return brother?.ID > 0 ? brother : null;
        }
        private BranchDTO GetNotIndexedChild(BranchDTO node)
        {
            return TreeNodes.FirstOrDefault(i => i.ParentId == node.ID && !(i.LeftIndex > 0));
        }
        private BranchDTO GetParent(BranchDTO data)
        {
            return AllBranch?.FirstOrDefault(s => s.ID == data?.ParentId);
        }
        #endregion related to tree
    }
}
