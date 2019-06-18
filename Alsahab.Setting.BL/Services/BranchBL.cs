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

namespace Alsahab.Setting.BL
{
    public class BranchBL : BaseBL<BranchDTO>
    {
        // private readonly BranchDL _repository;
        private List<BranchDTO> TempAllBranch = new List<BranchDTO>();
        private long? _index = 1, _depth = 2;
        private readonly IBaseDL<Branch, BranchDTO> _BranchDL;// = new IBaseDL<BranchDTO, Branch>();
        public BranchBL(IBaseDL<Branch, BranchDTO> branchDL)
        {
            _BranchDL = branchDL;
        }
        private bool Validate(BranchDTO data)
        {
            return true;
            //TODO
            // return Validate<BranchValidator, BranchDTO>(data ?? new BranchDTO());
            //ValidatorOptions.LanguageManager = new Alsahab.Common.Validation.ErrorLanguageManager();
            //ValidatorOptions.LanguageManager.Culture = Culture;

            //var validator = new Validation.BranchValidator();
            //ValidationResult result = validator.Validate(data ?? new BranchDTO());
            //ValidationErrors = result.Errors;
            //return result.IsValid;
            //if (data?.IsCentral == true)
            //{
            //    var Branch = BranchGet(new BranchDTO { IsCentral = true });
            //    if (Branch.Count > 0 && !(data.ID > 0 && data.ID == Branch.FirstOrDefault().Id))
            //    {
            //        ErrorMessage = "This Branch Can't Be Central,Because Central Branch Is Exist! \n";
            //        return false;
            //    }

            //}



            ////var Branchs = BranchGet(new BranchDTO { HeadPersonID = data?.HeadPersonID });
            ////if (Branchs.Count > 0)
            ////{
            ////    ErrorMessage = "This person is the head of another branch \n";
            ////    return false;
            ////}

            //var Branchs1 = BranchGet(new BranchDTO { });
            //if (Branchs1.Where(s=>s.Title== data.Title).ToList().Count > 0)
            //{
            //    ErrorMessage = "This Branch Title Is Exist \n";
            //    return false;
            //}

            //if (String.IsNullOrWhiteSpace(data?.Title))
            //{
            //    ErrorMessage = "Branch Title Not Entered\n";
            //    return false;
            //}

            //if (String.IsNullOrWhiteSpace(data?.Code))
            //{
            //    ErrorMessage = "Branch Code Not Entered\n";
            //    return false;
            //}
            //if (Branchs1.Where(s => s.Code == data.Code).ToList().Count > 0)
            //{
            //    ErrorMessage = "This Branch Code Is Exist \n";
            //    return false;
            //}
            ////if (String.IsNullOrWhiteSpace(data?.HeadPersonID.ToString()))
            ////{
            ////    ErrorMessage = "Branch Head Not Selected\n";
            ////    return false;
            ////}

            //if (data?.IsDeleted == true)
            //{
            //    ErrorMessage = "Branch Is Deleted \n";
            //    return false;
            //}
            //return true;
        }
        // private bool UpdateValidate(BranchDTO data)
        // {
        //     if (data?.IsCentral == true)
        //     {
        //         var Branch = BranchGet(new BranchDTO { IsCentral = true });
        //         if (Branch.Count > 0 && !(data.ID > 0 && data.ID == Branch.FirstOrDefault().ID))
        //         {
        //             ErrorMessage = "This Branch Can't Be Central,Because Central Branch Is Exist! \n";
        //             return false;
        //         }

        //     }
        //     if (data?.HeadPersonID > 0)
        //     {
        //         var Branchs = BranchGet(new BranchDTO { HeadPersonID = data?.HeadPersonID });
        //         var personhead = Branchs.Where(s => s.ID != data?.ID)?.ToList()?.Count;
        //         //var personhead = Branchs.Any(c => c.ID != data.ID && c.HeadPersonID == data?.HeadPersonID);
        //         if (personhead > 0)
        //         {
        //             ErrorMessage = "This person is the head of another branch \n";
        //             return false;
        //         }
        //     }
        //     var Branchs1 = BranchGet(new BranchDTO { Title = data?.Title });
        //     var title = Branchs1.Any(c => c.ID != data.ID);
        //     if (title)
        //     {
        //         ErrorMessage = "This Branch Title Is Exist \n";
        //         return false;
        //     }

        //     var Branchs2 = BranchGet(new BranchDTO { Code = data?.Code });
        //     var Code = Branchs2.Any(c => c.ID != data.ID);
        //     if (Code)
        //     {
        //         ErrorMessage = "This Branch Code Is Exist \n";
        //         return false;
        //     }
        //     return true;
        // }

        //TODO
        // private bool DeletePermision(BranchDTO data)
        // {
        //     if (!(data?.ID > 0))
        //     {
        //         ErrorMessage = "Branch Entered Is Mistake \n";
        //         return false;
        //     }

        //     BranchRegionWorkDL BranchRegionworkDL = new BranchRegionWorkDL();
        //     var regionworks = BranchRegionworkDL.BranchRegionWorkGet(new BranchRegionWorkDTO { BranchID = data?.ID }).Count;
        //     if (regionworks > 0)
        //     {
        //         ErrorMessage = "This Branch use in another Tables [ Branch Regions ], Please Delete them First";
        //         return false;
        //     }

        //     var deletingItem = BranchDL.BranchGet(new BranchDTO { ID = data.ID }, null)?.SingleOrDefault();
        //     var myLeft = deletingItem.LeftIndex; var myRight = deletingItem.RightIndex;
        //     var AllZons = AllBranch;
        //     var deleteCount = AllZons.Where(i => i.LeftIndex >= myLeft && i.LeftIndex <= myRight && i.IsDeleted == false).Count();

        //     if (deleteCount > 1)
        //     {
        //         ErrorMessage += "You can't delete this Branch. this Branch has child";
        //         return false;
        //     }
        //     return true;
        // }
        // public List<BranchDTO> BranchGet(BranchDTO data, BranchFilterDTO filter = null)
        // {
        //     // var Response = BranchDL.BranchGet(data, filter);
        //     // ResponseStatus = BranchDL.ResponseStatus;
        //     // if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //     // {
        //     //     ErrorMessage += BranchDL.ErrorMessage;
        //     //     return null;
        //     // }

        //     // var memberResponse = ServiceUtility.CallMember(s => s.Person(new PersonRequest
        //     // {
        //     //     ActionType = Common.ActionType.Select,
        //     //     User = new Common.UserInfoDTO { UserID = 1},
        //     //     //PersonFilter = new Alyatim.Member.DTO.PersonFilterDTO
        //     //     //{
        //     //     //    IDList = Response?.Select(t => t.HeadPersonID)?.ToList(),
        //     //     //}
        //     // }))?.ResponseDtoList;
        //     // if (memberResponse?.Count > 0)
        //     // {
        //     //     Response = (from r in Response
        //     //                 join p in memberResponse on r.HeadPersonID equals p.ID into TempResult
        //     //                 from x in TempResult.DefaultIfEmpty()
        //     //                 select new BranchDTO
        //     //                 {
        //     //                     ID = r.ID,
        //     //                     Code = r.Code,
        //     //                     ParentID = r.ParentID,
        //     //                     Title = r.Title,
        //     //                     IsCentral = r.IsCentral,
        //     //                     HeadPersonID = x?.Id,
        //     //                     HeadMemberName = x?.FullName,
        //     //                     HeadMemberPhoneNo = x?.MobileNo,
        //     //                     BranchPhoneNo = r.BranchPhoneNo,
        //     //                     BranchEmail = r.BranchEmail,
        //     //                     BranchAddressID = r.BranchAddressID,
        //     //                     BranchComment = r.BranchComment,
        //     //                     CreateDate = r.CreateDate,
        //     //                     IsDeleted = r.IsDeleted,
        //     //                     RightIndex = r.RightIndex,
        //     //                     LeftIndex = r.LeftIndex,
        //     //                     Depth = r.Depth,
        //     //                     OldCode = r.OldCode
        //     //                 })?.ToList();
        //     // }
        //     List<BranchDTO> Response = null;

        //     return Response;
        // }


        public override async Task<BranchDTO> InsertAsync(BranchDTO data, CancellationToken cancellationToken)
        {
            if (!Validate(data))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                return null;
            }

            data.CreateDate = DateTime.Now;
            //TODO
            // data.Code = GenerateCode(data);

            var Response = await _BranchDL.AddAsync(data, cancellationToken);//.BranchInsert(data);

            //TODO
            // if (Response?.ID > 0)
            // {
            //     var resp = BranchGet(new BranchDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            //     Observers.ObserverStates.BranchAdd state = new Observers.ObserverStates.BranchAdd
            //     {
            //         Branch = resp ?? Response,
            //         User = User,
            //     };
            //     Notify(state);
            //     if (resp != null)
            //         Response = resp;
            // }

            ResponseStatus = _BranchDL.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += _BranchDL.ErrorMessage;
                return null;
            }

            return Response;
        }


        // public BranchDTO Insert(BranchDTO data, CancellationToken cancellationToken)
        // {
        //     if (!Validate(data))
        //     {
        //         ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
        //         return null;
        //     }

        //     data.CreateDate = DateTime.Now;
        //     data.Code = GenerateCode(data);

        //     var Response = BranchDL.AddAsync(data, cancellationToken);//.BranchInsert(data);

        //     if (Response?.ID > 0)
        //     {
        //         var resp = BranchGet(new BranchDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
        //         Observers.ObserverStates.BranchAdd state = new Observers.ObserverStates.BranchAdd
        //         {
        //             Branch = resp ?? Response,
        //             User = User,
        //         };
        //         Notify(state);
        //         if (resp != null)
        //             Response = resp;
        //     }

        //     ResponseStatus = BranchDL.ResponseStatus;
        //     if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += BranchDL.ErrorMessage;
        //         return null;
        //     }

        //     return Response;
        // }
        // public List<BranchDTO> BranchInsert(List<BranchDTO> data)
        // {
        //     foreach (var d in data)
        //     {
        //         if (!Validate(d))
        //         {
        //             ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
        //             return null;
        //         }
        //         d.CreateDate = DateTime.Now;
        //     }
        //     var Response = BranchDL.BranchInsert(data);

        //     List<BranchDTO> respList = new List<BranchDTO>();
        //     foreach (var val in Response)
        //     {
        //         var resp = BranchGet(new BranchDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
        //         Observers.ObserverStates.BranchAdd state = new Observers.ObserverStates.BranchAdd
        //         {
        //             Branch = resp ?? val,
        //             User = User,
        //         };
        //         Notify(state);
        //         respList.Add(resp);
        //     }

        //     ResponseStatus = BranchDL.ResponseStatus;
        //     if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += BranchDL.ErrorMessage;
        //         return null;
        //     }

        //     return respList ?? Response;
        // }

        #region Update

        // public BranchDTO BranchUpdate(BranchDTO data)
        //{
        //    if (!UpdateValidate(data))
        //    {
        //        ResponseStatus = Common.ResponseStatus.BusinessError;
        //        return null;
        //    }
        //    if (!(data.ID > 0))
        //    {
        //        ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
        //        ErrorMessage = "Entered Branch is Mistake";
        //        return null;
        //    }
        //    var Response = BranchDL.BranchUpdate(data);

        //    var resp = BranchGet(new BranchDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
        //    Observers.ObserverStates.BranchEdit state = new Observers.ObserverStates.BranchEdit
        //    {
        //        Branch = resp ?? Response,
        //        User = User,
        //    };
        //    Notify(state);

        //    ResponseStatus = BranchDL.ResponseStatus;
        //    if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //    {
        //        ErrorMessage += BranchDL.ErrorMessage;
        //        return null;
        //    }

        //    return resp ?? Response;
        //}

        #endregion

        // public BranchDTO BranchDelete(BranchDTO data)
        // {
        //     //Search For Use This Item Before Delete
        //     if (!DeletePermision(data))
        //     {
        //         ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
        //         return null;
        //     }
        //     data.IsDeleted = true;
        //     var Response = BranchDL.BranchUpdate(data);

        //     var resp = BranchGet(new BranchDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
        //     Observers.ObserverStates.BranchDelete state = new Observers.ObserverStates.BranchDelete
        //     {
        //         Branch = resp ?? Response,
        //         User = User,
        //     };
        //     Notify(state);

        //     ResponseStatus = BranchDL.ResponseStatus;
        //     if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += BranchDL.ErrorMessage;
        //         return null;
        //     }
        //     return resp ?? Response;
        // }
        /////////////////////////////////////////////////////////////////////Allmi

        // public BranchDTO BranchInsert(BranchDTO data)
        // {
        //     if (!Validate(data))
        //     {
        //         ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
        //         return null;
        //     }
        //     UpdateAllBranch();
        //     var response = data;
        //     var AllBranchs = AllBranch;

        //     BranchDTO tempBranch = new BranchDTO
        //     {
        //         ID = 0,
        //         Depth = 1,
        //         ParentID = -1,
        //         LeftIndex = 1,
        //         RightIndex = (AllBranchs.Count + 1) * 2
        //     };

        //     if (response.ParentID == null)
        //         response.ParentID = 0;
        //     AllBranchs.Add(tempBranch);
        //     foreach (var item in AllBranchs)
        //     {
        //         if (item.ParentID == null)
        //             item.ParentID = 0;
        //     }

        //     var childs = AllBranchs.Where(c => c.ParentID == response.ParentID).Count();
        //     if (childs > 0)
        //     {
        //         long? right = 0;
        //         if (AllBranchs.Count > 0)
        //             right = AllBranchs.SingleOrDefault(z => z.ID == response.ParentID)?.RightIndex;
        //         foreach (var zItem in AllBranch)
        //         {
        //             if (zItem.RightIndex >= right) zItem.RightIndex += 2;
        //             if (zItem.LeftIndex > right) zItem.LeftIndex += 2;
        //         }

        //         foreach (var death in AllBranchs)
        //         {
        //             if (death.ParentID == 0)
        //                 death.ParentID = null;
        //         }

        //         AllBranchs.Remove(tempBranch);
        //         BranchDL.BranchUpdate(AllBranchs);
        //         response.LeftIndex = right;
        //         response.RightIndex = right + 1;
        //     }
        //     else
        //     {
        //         long? left = 0;
        //         if (AllBranchs.Count > 0)
        //             left = AllBranchs?.SingleOrDefault(z => z.ID == response.ParentID)?.LeftIndex;
        //         foreach (var zItem in AllBranch)
        //         {
        //             if (zItem.RightIndex > left) zItem.RightIndex += 2;
        //             if (zItem.LeftIndex > left) zItem.LeftIndex += 2;
        //         }

        //         foreach (var Branch in AllBranchs)
        //         {
        //             if (Branch.ParentID == 0)
        //                 Branch.ParentID = null;
        //         }
        //         AllBranchs.Remove(tempBranch);
        //         BranchDL.BranchUpdate(AllBranchs);
        //         response.LeftIndex = left + 1;
        //         response.RightIndex = left + 2;
        //     }

        //     long? parentDepth = 1;
        //     if (response.ParentID == 0)
        //         response.ParentID = null;
        //     else
        //         parentDepth = AllBranchs.SingleOrDefault(d => d.ID == response.ParentID).Depth;
        //     response.Depth = parentDepth + 1;

        //     return Insert(response);
        // }
        // private List<BranchDTO> _Branch = new List<BranchDTO>();
        // private List<BranchDTO> AllBranch
        // {
        //     get
        //     {
        //         if (!(_Branch.Count > 0))
        //             _Branch = new BranchDL().AllBranchGet();
        //         return _Branch;
        //     }
        // }
        // private BranchDTO GetChild(BranchDTO Branch)
        // {
        //     return TempAllBranch.FirstOrDefault(i => i.ParentID == Branch.ID && !(i.LeftIndex > 0));
        // }
        // private BranchDTO GetBrother(BranchDTO Branch)
        // {
        //     if (!(Branch.ParentID > 0))
        //         return null;
        //     var parent = TempAllBranch.FirstOrDefault(i => i.ID == Branch.ParentID);
        //     var brother = GetChild(parent);
        //     return brother?.ID > 0 ? brother : null;
        // }
        // public BranchDTO BranchUpdate(BranchDTO data)
        // {
        //     var Response = data;
        //     if (data.ParentID == 0)
        //         data.ParentID = null;
        //     if (!(data.ID > 0))
        //     {
        //         ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
        //         ErrorMessage = "Entered Branch is Mistake";
        //         return null;
        //     }
        //     //BranchDTO oldBranch = new BranchDTO();

        //     var oldBranch = BranchGet(new BranchDTO { ID = Response?.ID ?? 0 }, null)?.FirstOrDefault();
        //     Response = BranchDL.BranchUpdate(data);

        //     Observers.ObserverStates.BranchEdit state = new Observers.ObserverStates.BranchEdit
        //     {
        //         Branch = oldBranch ?? Response,
        //         User = User,
        //     };
        //     Notify(state);

        //     if (data.ParentID != oldBranch.ParentID)
        //     {
        //         UpdateAllBranch();
        //     }

        //     ResponseStatus = BranchDL.ResponseStatus;
        //     if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += BranchDL.ErrorMessage;
        //         return null;
        //     }
        //     return oldBranch ?? Response;
        // }
        // public List<BranchDTO> UpdateAllBranch()
        // {
        //     var allZons = BranchGet();
        //     foreach (var Branch in allZons)
        //     {
        //         Branch.LeftIndex = null;
        //         Branch.RightIndex = null;
        //         Branch.Depth = null;
        //         TempAllBranch.Add(Branch);
        //     }

        //     List<BranchDTO> rootList = TempAllBranch.Where(i => !(i.ParentID > 0))?.ToList();
        //     foreach (var root in rootList)
        //     {
        //         if (root?.ID > 0)
        //         {
        //             _depth = 2;
        //             RecursiveUpdateAllBranch(root);
        //         }
        //     }

        //     TempAllBranch = GenerateNewCodes(TempAllBranch?.Where(s => s.ParentID == null && s.IsDeleted == false)?.ToList(), TempAllBranch?.Where(s => s.IsDeleted == false)?.ToList());
            
        //     BranchDL.BranchUpdate(TempAllBranch);
        //     ResponseStatus = BranchDL.ResponseStatus;
        //     if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += BranchDL.ErrorMessage;
        //         return null;
        //     }
        //     return BranchDL.AllBranchGet();
        //     //            return result;
        // }
        // private void RecursiveUpdateAllBranch(BranchDTO BranchDLta)
        // {
        //     if (!(BranchDLta?.ID > 0) || !(TempAllBranch?.Count > 0))
        //         return;

        //     TempAllBranch.FirstOrDefault(i => i.ID == BranchDLta.Id).LeftIndex = ++_index;
        //     TempAllBranch.FirstOrDefault(i => i.ID == BranchDLta.Id).Depth = _depth;
        //     //   var aa = TempAllBranch.FirstOrDefault(i => i.ID == BranchDLta.Id);


        //     var tempChild = GetChild(BranchDLta);
        //     if (tempChild?.ID > 0)
        //     {
        //         _depth++;
        //         RecursiveUpdateAllBranch(tempChild);
        //     }

        //     TempAllBranch.FirstOrDefault(i => i.ID == BranchDLta.Id).RightIndex = ++_index;
        //     var tempBrother = GetBrother(BranchDLta);

        //     if (tempBrother?.ID > 0)
        //         RecursiveUpdateAllBranch(tempBrother);
        //     else
        //         _depth--;
        // }
        // public List<BranchDTO> BranchGet()
        // {
        //     var response = BranchDL.AllBranchGet();
        //     ResponseStatus = BranchDL.ResponseStatus;
        //     if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += BranchDL.ErrorMessage;
        //         return null;
        //     }
        //     return response;
        // }


        // private String GenerateCode(BranchDTO data)
        // {
        //     var list = BranchGet(new BranchDTO(), null);
        //     if (data?.ParentID == null)
        //     {
        //         return (list?.Where(s => s.ParentID == null)?.ToList()?.Count + 1).ToString();
        //     }
        //     else
        //     {
        //         var r = list?.Where(s => s.ParentID == data?.ParentID)?.ToList()?.Count;
        //         return String.Format("{0}-{1}", list?.Where(s => s.ID == data?.ParentID)?.FirstOrDefault()?.Code, (r + 1).ToString());
        //     }
        // }
        // private List<BranchDTO> GenerateNewCodes(List<BranchDTO> data, List<BranchDTO> All)
        // {
        //     List<BranchDTO> res = new List<BranchDTO>();
        //     for (int thisBranch = 0; thisBranch < data?.Count; thisBranch++)
        //     {
        //         var parent = GetParent(data[thisBranch], All);
        //         if (parent == null) // root
        //         {
        //             data[thisBranch].Code = string.Format("{0}", (thisBranch + 1));
        //             res.Add(data[thisBranch]);
        //         }
        //         else
        //         {
        //             data[thisBranch].Code = string.Format("{0}-{1}", parent?.Code, (thisBranch + 1));
        //             res.Add(data[thisBranch]);
        //         }
        //         var childs = All?.Where(s => s.ParentID == data[thisBranch]?.Id)?.ToList();

        //         for (int child = 0; child < childs?.Count; child++)
        //         {
        //             childs[child].Code = string.Format("{0}-{1}", data[thisBranch].Code, (child + 1));
        //             res.Add(childs[child]);
        //             res.AddRange(GenerateNewCodes(All?.Where(s => s.ParentID == childs[child]?.Id)?.ToList(), All));
        //         }
        //     }
        //     return res;
        // }
        // private BranchDTO GetParent(BranchDTO data, List<BranchDTO> all)
        // {
        //     return all?.Where(s => s.ID == data?.ParentID)?.ToList()?.FirstOrDefault();
        // }

    }
}
