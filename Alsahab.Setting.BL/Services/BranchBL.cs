using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Alsahab.Setting.Data.Contracts;
using Alsahab.Setting.Data.Repositories;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Entities.Models;
using Alyatim.Member.SC.Messages;
using Alsahab.Common;
using Gostar.Common;
using Alsahab.Setting.Common.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Alsahab.Setting.BL
{
    public class BranchBL : BaseBL<Branch, BranchDTO, BranchFilterDTO>
    {
        private List<BranchDTO> TreeNodes = new List<BranchDTO>();
        private long? _index = 1, _depth = 2;
        private readonly IBaseDL<Branch, BranchDTO, BranchFilterDTO> _BranchDL;
        public BranchBL(IBaseDL<Branch, BranchDTO, BranchFilterDTO> branchDL)
            : base(branchDL)
        {
            _BranchDL = branchDL;
        }

        private IList<BranchDTO> _Branch = new List<BranchDTO>();
        private IList<BranchDTO> AllBranch
        {
            get
            {
                if (!(_Branch.Count > 0))
                    _Branch = _BranchDL.GetAll();
                return _Branch;
            }
        }

        #region Async methods
        public async override Task<IList<BranchDTO>> GetAsync(BranchFilterDTO filter, CancellationToken cancellationToken)
        {
            var response = await _BranchDL.GetAsync(filter, cancellationToken);
            ResponseStatus = _BranchDL.ResponseStatus;
            ErrorMessage = _BranchDL.ErrorMessage;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(Common.Api.ApiResultStatusCode.LogicError, ErrorMessage);

            if (!(response.Count > 0))
                return response;
            // throw new NotFoundException();

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
        public override async Task<IList<BranchDTO>> InsertListAsync(IList<BranchDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                // if (!Validate(d))
                // {
                //     ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                //     return null;
                // }
                d.CreateDate = DateTime.Now;
            }

            // TODO:
            // فرایند تولید کد لحاظ نشده است

            var response = await _BranchDL.InsertListAsync(data, cancellationToken);

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

            ResponseStatus = _BranchDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += _BranchDL.ErrorMessage;
                return null;
            }
            return respList ?? response;
        }

        public override async Task<BranchDTO> UpdateAsync(BranchDTO data, CancellationToken cancellationToken)
        {
            Validate(data);
            // if (!Validate(data))
            // {
            //     ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
            //     return null;
            // }
            if (!(data.ID > 0))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered node is Mistake";
                return null;
            }
            var response = await _BranchDL.UpdateAsync(data, cancellationToken);

            var resp = (await GetAsync(new BranchFilterDTO { ID = response?.ID ?? 0 }, cancellationToken))?.FirstOrDefault();
            Observers.ObserverStates.BranchEdit state = new Observers.ObserverStates.BranchEdit
            {
                Branch = resp ?? response,
                User = User,
            };
            Notify(state);

            ResponseStatus = _BranchDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += _BranchDL.ErrorMessage;
                return null;
            }
            return resp ?? response;
        }


        // public async Task<BranchDTO> BranchUpdateAsync(BranchDTO data, CancellationToken cancellationToken)
        // {
        //     if (!UpdateValidate(data))
        //     {
        //         ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
        //         return null;
        //     }
        //     var response = data;
        //     if (data.ParentID == 0)
        //         data.ParentID = null;
        //     if (!(data.ID > 0))
        //     {
        //         ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
        //         ErrorMessage = "Entered node is Mistake";
        //         return null;
        //     }
        //     var oldBranch = await _BranchDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);
        //     response = await _BranchDL.UpdateAsync(data, cancellationToken);
        //     Observers.ObserverStates.BranchEdit state = new Observers.ObserverStates.BranchEdit
        //     {
        //         Branch = oldBranch ?? response,
        //         User = User,
        //     };
        //     Notify(state);
        //     if (data.ParentID != oldBranch.ParentID)
        //         await UpdateTreeIndicesAndCodesAsync(cancellationToken);
        //     return oldBranch ?? response;
        // }

        public async override Task<BranchDTO> InsertAsync(BranchDTO data, CancellationToken cancellationToken)
        {
            Validate(data);
            // if (!Validate(data))
            // {
            //     string error = "";
            //     foreach (var verror in ValidationErrors)
            //         error = error + "\n" + verror.ErrorMessage;

            //     throw new BadRequestException(error);
            // }

            data.CreateDate = DateTime.Now;
            data.Code = GenerateCodeInInsert(data);

            var response = await _BranchDL.InsertAsync(data, cancellationToken);//.BranchInsert(data);
            if (_BranchDL?.ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(Common.Api.ApiResultStatusCode.ServerError, _BranchDL.ErrorMessage);

            await UpdateTreeIndicesAndCodesAsync(cancellationToken);

            Observers.ObserverStates.BranchAdd state = new Observers.ObserverStates.BranchAdd
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
            if (!DeletePermision(data))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                return null;
            }
            data = await _BranchDL.GetByIdAsync(cancellationToken, data.ID ?? 0);
            data.IsDeleted = true;
            var response = _BranchDL.Update(data);

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
            return Validate<BranchValidator, BranchDTO>(data ?? new BranchDTO());
        }
        private bool DeletePermision(BranchDTO data)
        {
            if (!(data?.ID > 0))
            {
                ErrorMessage = "node Entered Is Mistake \n";
                return false;
            }
            return true;
            //TODO:
            // BranchRegionWorkDL BranchRegionworkDL = new BranchRegionWorkDL();
            // var regionworks = BranchRegionworkDL.BranchRegionWorkGet(new BranchRegionWorkDTO { BranchID = data?.ID }).Count;
            // if (regionworks > 0)
            // {
            //     ErrorMessage = "This node is used in another tables [ node Regions ], Please delete them first";
            //     return false;
            // }
            // var deletingItem = _BranchDL.BranchGet(new BranchDTO { ID = data.ID }, null)?.SingleOrDefault();
            // var myLeft = deletingItem.LeftIndex; var myRight = deletingItem.RightIndex;
            // var AllZons = AllBranch;
            // var deleteCount = AllZons.Where(i => i.LeftIndex >= myLeft && i.LeftIndex <= myRight && i.IsDeleted == false).Count();

            // if (deleteCount > 1)
            // {
            //     ErrorMessage += "You can't delete this node. this node has child";
            //     return false;
            // }
            // return true;
        }
        #endregion Validation

        private async Task<IList<BranchDTO>> UpdateTreeIndicesAndCodesAsync(CancellationToken cancellationToken)
        {
            foreach (var node in AllBranch)
            {
                node.LeftIndex = null;
                node.RightIndex = null;
                node.Depth = null;
                node.Code = null;
                TreeNodes.Add(node);
            }

            List<BranchDTO> rootList = TreeNodes.Where(i => !(i.ParentID > 0))?.ToList();
            foreach (var root in rootList)
                if (root?.ID > 0)
                {
                    _depth = 2;
                    RecursiveUpdateAllBranchIndices(root);
                }

            GenerateNewCodeList(rootList);

            var result = await _BranchDL.UpdateListAsync(TreeNodes, cancellationToken);

            ResponseStatus = _BranchDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
                throw new AppException(Common.Api.ApiResultStatusCode.LogicError, _BranchDL.ErrorMessage);

            return result;
        }

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

        private String GenerateCodeInInsert(BranchDTO data)
        {
            if (data?.ParentID == null)
                return (AllBranch?.Where(s => s.ParentID == null)?.ToList()?.Count + 1).ToString();
            else
            {
                var r = AllBranch?.Where(s => s.ParentID == data?.ParentID)?.ToList()?.Count;
                return String.Format("{0}-{1}", AllBranch?.FirstOrDefault(s => s.ID == data?.ParentID)?.Code, (r + 1)?.ToString());
            }
        }

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

                var childs = AllBranch?.Where(s => s.ParentID == data[thisBranch]?.ID)?.ToList();

                for (int child = 0; child < childs?.Count; child++)
                {
                    childs[child].Code = string.Format("{0}-{1}", data[thisBranch].Code, (child + 1));
                    res.Add(childs[child]);
                    res.AddRange(GenerateNewCodeList(AllBranch?.Where(s => s.ParentID == childs[child]?.ID)?.ToList()));
                }
            }
            return res;
        }

        private BranchDTO GetNotIndexedBrother(BranchDTO node)
        {
            if (!(node.ParentID > 0))
                return null;
            var parent = TreeNodes.FirstOrDefault(i => i.ID == node.ParentID);
            var brother = GetNotIndexedChild(parent);
            return brother?.ID > 0 ? brother : null;
        }

        private BranchDTO GetNotIndexedChild(BranchDTO node)
        {
            return TreeNodes.FirstOrDefault(i => i.ParentID == node.ID && !(i.LeftIndex > 0));
        }


        private BranchDTO GetParent(BranchDTO data)
        {
            return AllBranch?.FirstOrDefault(s => s.ID == data?.ParentID);
        }



                // private bool UpdateValidate(BranchDTO data)
        // {
        //     if (data?.IsCentral == true)
        //     {
        //         var node = Get(new BranchFilterDTO { IsCentral = true });
        //         if (node.Count > 0 && !(data.ID > 0 && data.ID == node.FirstOrDefault().ID))
        //         {
        //             ErrorMessage = "This node Can't Be Central,Because Central node Is Exist! \n";
        //             return false;
        //         }
        //     }
        //     if (data?.HeadPersonID > 0)
        //     {
        //         var Branchs = Get(new BranchFilterDTO { HeadPersonID = data?.HeadPersonID });
        //         var personhead = Branchs.Where(s => s.ID != data?.ID)?.ToList()?.Count;
        //         //var personhead = Branchs.Any(c => c.ID != data.ID && c.HeadPersonID == data?.HeadPersonID);
        //         if (personhead > 0)
        //         {
        //             ErrorMessage = "This person is the head of another node \n";
        //             return false;
        //         }
        //     }
        //     var Branchs1 = Get(new BranchFilterDTO { Title = data?.Title });
        //     var title = Branchs1.Any(c => c.ID != data.ID);
        //     if (title)
        //     {
        //         ErrorMessage = "This node Title Is Exist \n";
        //         return false;
        //     }

        //     var Branchs2 = Get(new BranchFilterDTO { Code = data?.Code });
        //     var Code = Branchs2.Any(c => c.ID != data.ID);
        //     if (Code)
        //     {
        //         ErrorMessage = "This node Code Is Exist \n";
        //         return false;
        //     }
        //     return true;
        // }


    }
}
