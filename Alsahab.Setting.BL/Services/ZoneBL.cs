using System;
using System.Collections.Generic;
using System.Linq;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.BL.Validation;
using Alsahab.Common.Exceptions;
using Alsahab.Common;
using System.Threading;
using System.Threading.Tasks;

namespace Alsahab.Setting.BL
{
    public class ZoneBL : BaseBL<Zone, ZoneDTO, ZoneFilterDTO>
    {
        private IList<ZoneDTO> _AllZones;
        private IList<ZoneDTO> AllZones
        {
            get
            {
                if (!(_AllZones?.Count > 0))
                    _AllZones = _ZoneDL.GetAll();
                return _AllZones;
            }
        }

        private List<ZoneDTO> TreeNodes = new List<ZoneDTO>();
        private long? _index = 0, _depth = 2;

        #region dependency injection
        private readonly IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> _ZoneDL;
        public ZoneBL(IBaseDL<Zone, ZoneDTO, ZoneFilterDTO> zoneDL)
            : base(zoneDL)
        {
            _ZoneDL = zoneDL;
        }
        #endregion dependency injection

        private bool Validate(ZoneDTO data)
        {
            return Validate<BLZoneValidator, ZoneDTO>(data);
        }

        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private async Task<bool> CheckDeletePermission(ZoneDTO data, CancellationToken cancellationToken)
        {
            if (!(data.ID > 0))
                throw new AppException(ResponseStatus.BadRequest, "Entered Zone is Mistake");

            var deletingItem = await _ZoneDL.GetByIdAsync(cancellationToken, data.ID);
            var myLeft = deletingItem.LeftIndex;
            var myRight = deletingItem.RightIndex;
            var deleteCount = AllZones.Where(i => i.LeftIndex >= myLeft && i.LeftIndex <= myRight && i.IsDeleted == false).Count();
            if (deleteCount > 1)
                throw new AppException(ResponseStatus.LoginError, "You can't delete this node. this node has child");

            return true;
        }
        public async override Task<IList<ZoneDTO>> GetAsync(ZoneFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var response = await _ZoneDL.GetAsync(filter, cancellationToken, paging);
            foreach (var val in response)
            {
                var parentList = new List<string>();
                var thisItem = val;
                parentList.Add(val.Title);
                while (thisItem.ParentID != null)
                {
                    var parent = AllZones.FirstOrDefault(s => s.ID == thisItem.ParentID);
                    parentList.Add(parent.Title);
                    thisItem = parent;
                }
                val.ZoneAddress = String.Join("-", parentList);
                // val.ZoneAndChilds = GetZoneChilds(val.ID ?? 0);
                // val.ZoneAndParents = GetZoneParents(val.ID ?? 0);
            }

            ResultCount = _ZoneDL.ResultCount;
            return response;
        }

        public async override Task<ZoneDTO> InsertAsync(ZoneDTO data, CancellationToken cancellationToken)
        {
            Validate(data);
            data.CreateDate = DateTime.Now;

            var response = await _ZoneDL.InsertAsync(data, cancellationToken);

            UpdateTreeIndicesAndCodes();

            response = await _ZoneDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);

            //TODO:
            // Observers.ObserverStates.ZoneAdd state = new Observers.ObserverStates.ZoneAdd
            // {
            //     Zone = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        public override async Task<IList<ZoneDTO>> InsertListAsync(IList<ZoneDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }

            var response = await _ZoneDL.InsertListAsync(data, cancellationToken);

            UpdateTreeIndicesAndCodes();

            List<ZoneDTO> respList = new List<ZoneDTO>();
            foreach (var val in response)
            {
                var resp = await _ZoneDL.GetByIdAsync(cancellationToken, val?.ID);
                
                //TODO:
                // Observers.ObserverStates.ZoneAdd state = new Observers.ObserverStates.ZoneAdd
                // {
                //     Zone = resp,
                //     User = User,
                // };
                // Notify(state);

                respList.Add(resp);
            }

            return respList ?? response;
        }

        /// <summary>
        /// Update Zone
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<ZoneDTO> UpdateAsync(ZoneDTO data, CancellationToken cancellationToken)
        {
            data = await MergeNewAndOldDataForUpdate(data, cancellationToken);

            Validate(data);

            var response = await _ZoneDL.UpdateAsync(data, cancellationToken);

            UpdateTreeIndicesAndCodes();

            response = await _ZoneDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);

            //TODO:
            // Observers.ObserverStates.ZoneEdit state = new Observers.ObserverStates.ZoneEdit
            // {
            //     Zone = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }

        public override async Task<ZoneDTO> SoftDeleteAsync(ZoneDTO data, CancellationToken cancellationToken)
        {
            await CheckDeletePermission(data, cancellationToken);

            data = await _ZoneDL.GetByIdAsync(cancellationToken, data.ID ?? 0);
            data.IsDeleted = true;
            var response = await _ZoneDL.UpdateAsync(data, cancellationToken);

            UpdateTreeIndicesAndCodes();

            //TODO:
            // Observers.ObserverStates.ZoneDelete state = new Observers.ObserverStates.ZoneDelete
            // {
            //     Zone = response,
            //     User = User,
            // };
            // Notify(state);

            return response;
        }
        #region related to tree
        private IList<ZoneDTO> UpdateTreeIndicesAndCodes()
        {
            _AllZones = null;
            var zones = AllZones;
            foreach (var node in zones)
            {
                node.LeftIndex = null;
                node.RightIndex = null;
                node.Depth = null;
                node.Code = null;
                TreeNodes.Add(node);
            }

            List<ZoneDTO> rootList = TreeNodes.Where(i => !(i.ParentID > 0))?.ToList();
            foreach (var root in rootList)
                if (root?.ID > 0)
                {
                    _depth = 2;
                    RecursiveUpdateAllZoneIndices(root);
                }

            var codedZones = GenerateNewCodeList(rootList);

            var result = _ZoneDL.UpdateList(codedZones);

            return result;
        }

        // مراحل:
        // ۱- ابتدا اندیس چپ را تنظیم می‌کند
        // ۲- سپس عمق را تنظیم می‌کند
        // ۳- اندیس چپ و عمق را برای فرزندش در صورت وجود تنظیم می‌کند
        // ۴- در صورت عدم وجود فرزند، اندیس راست را تنظیم می‌کند
        // ۵- به سراغ برادر (در صورت وجود) می‌رود و مراحل اول تا چهارم را برای آن انجام می‌دهد
        private void RecursiveUpdateAllZoneIndices(ZoneDTO data)
        {
            if (!(data?.ID > 0) || !(TreeNodes?.Count > 0))
                return;

            TreeNodes.FirstOrDefault(i => i.ID == data.ID).LeftIndex = ++_index;
            TreeNodes.FirstOrDefault(i => i.ID == data.ID).Depth = _depth;

            var tempChild = GetNotIndexedChild(data);
            if (tempChild?.ID > 0)
            {
                _depth++;
                RecursiveUpdateAllZoneIndices(tempChild);
            }

            TreeNodes.FirstOrDefault(i => i.ID == data.ID).RightIndex = ++_index;
            var tempBrother = GetNotIndexedBrother(data);

            if (tempBrother?.ID > 0)
                RecursiveUpdateAllZoneIndices(tempBrother);
            else
                _depth--;
        }

        private List<ZoneDTO> GenerateNewCodeList(List<ZoneDTO> data)
        {
            List<ZoneDTO> res = new List<ZoneDTO>();
            for (int thisZone = 0; thisZone < data?.Count; thisZone++)
            {
                var parent = GetParent(data[thisZone]);
                data[thisZone].Code = (parent == null) ?
                    string.Format("{0}", (thisZone + 1)) :
                    string.Format("{0}-{1}", parent?.Code, (thisZone + 1));

                res.Add(data[thisZone]);

                var childs = AllZones?.Where(s => s.ParentID == data[thisZone]?.ID)?.ToList();

                for (int child = 0; child < childs?.Count; child++)
                {
                    childs[child].Code = string.Format("{0}-{1}", data[thisZone].Code, (child + 1));
                    res.Add(childs[child]);
                    res.AddRange(GenerateNewCodeList(AllZones?.Where(s => s.ParentID == childs[child]?.ID)?.ToList()));
                }
            }
            return res;
        }
        private ZoneDTO GetNotIndexedBrother(ZoneDTO node)
        {
            if (!(node.ParentID > 0))
                return null;
            var parent = TreeNodes.FirstOrDefault(i => i.ID == node.ParentID);
            var brother = GetNotIndexedChild(parent);
            return brother?.ID > 0 ? brother : null;
        }
        private ZoneDTO GetNotIndexedChild(ZoneDTO node)
        {
            return TreeNodes.FirstOrDefault(i => i.ParentID == node.ID && !(i.LeftIndex > 0));
        }
        private ZoneDTO GetParent(ZoneDTO data)
        {
            return AllZones?.FirstOrDefault(s => s.ID == data?.ParentID);
        }
        #endregion related to tree

        // /// <summary>
        // /// Get List of Zone 
        // /// </summary>
        // /// <param name="data"></param>
        // /// <returns></returns>
        // public List<ZoneDTO> ZoneGet(ZoneDTO data, ZoneFilterDTO filter = null)
        // {
        //     var Response = ZoneDA.ZoneGet(data, filter);

        //     ResponseStatus = ZoneDA.ResponseStatus;
        //     if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += ZoneDA.ErrorMessage;
        //         return null;
        //     }
        //     var AllZone = ZoneDA.ZoneGet(null, null);
        //     foreach (var val in Response)
        //     {
        //         List<String> ParentList = new List<string>();
        //         var ThisItem = val;
        //         ParentList.Insert(0, val.Title);
        //         while (ThisItem.ParentID != null)
        //         {
        //             var Parent = AllZone.Where(s => s.ID == ThisItem.ParentID)?.FirstOrDefault();
        //             ParentList.Insert(0, Parent.Title);
        //             ThisItem = Parent;
        //         }
        //         val.ZoneAddress = String.Join("-", ParentList);
        //         val.ZoneAndChilds = GetZoneChilds(val.ID ?? 0);
        //         val.ZoneAndParents = GetZoneParents(val.ID ?? 0);

        //     }

        //     return Response;

        // }
        // /// <summary>
        // /// Insert Zone in Database
        // /// </summary>
        // /// <param name="data"></param>
        // /// <returns></returns>
        // private ZoneDTO Insert(ZoneDTO data)
        // {
        //     if (!Validate(data))
        //     {
        //         ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
        //         return null;
        //     }
        //     data.CreateDate = DateTime.Now;
        //     data.Code = GenerateCode(data);

        //     var Response = ZoneDA.ZoneInsert(data);

        //     if (Response?.ID > 0)
        //     {
        //         var resp = ZoneDA.ZoneGet(new ZoneDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
        //         Observers.ObserverStates.ZoneAdd state = new Observers.ObserverStates.ZoneAdd
        //         {
        //             Zone = resp ?? Response,
        //             User = User,
        //         };
        //         Notify(state);
        //         if (resp != null)
        //             Response = resp;
        //     }

        //     ResponseStatus = ZoneDA.ResponseStatus;
        //     if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += ZoneDA.ErrorMessage;
        //         return null;
        //     }

        //     return Response;
        // }
        // /// <summary>
        // /// Insert List of Zone In Database
        // /// </summary>
        // /// <param name="data"></param>
        // /// <returns></returns>
        // public List<ZoneDTO> ZoneInsert(List<ZoneDTO> data)
        // {
        //     foreach (var d in data)
        //     {
        //         if (!Validate(d))
        //         {
        //             ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
        //             return null;
        //         }
        //         d.CreateDate = DateTime.Now;
        //     }
        //     var Response = ZoneDA.ZoneInsert(data);

        //     List<ZoneDTO> respList = new List<ZoneDTO>();
        //     foreach (var val in Response)
        //     {
        //         var resp = ZoneDA.ZoneGet(new ZoneDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
        //         Observers.ObserverStates.ZoneAdd state = new Observers.ObserverStates.ZoneAdd
        //         {
        //             Zone = resp ?? val,
        //             User = User,
        //         };
        //         Notify(state);
        //         respList.Add(resp);
        //     }

        //     ResponseStatus = ZoneDA.ResponseStatus;
        //     if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += ZoneDA.ErrorMessage;
        //         return null;
        //     }
        //     return respList ?? Response;
        // }
        // /// <summary>
        // /// Delete Logicly
        // /// </summary>
        // /// <param name="data"></param>
        // /// <returns></returns>
        // public ZoneDTO ZoneDelete(ZoneDTO data)
        // {
        //     if (!DeletePermission(data))
        //     {
        //         ResponseStatus = Common.ResponseStatus.BusinessError;
        //         return null;
        //     }
        //     data.IsDeleted = true;
        //     var Result = ZoneDA.ZoneUpdate(data);

        //     var resp = ZoneGet(new ZoneDTO { ID = Result?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
        //     Observers.ObserverStates.ZoneDelete state = new Observers.ObserverStates.ZoneDelete
        //     {
        //         Zone = resp ?? Result,
        //         User = User,
        //     };
        //     Notify(state);

        //     ResponseStatus = ZoneDA.ResponseStatus;
        //     if (ResponseStatus != Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += ZoneDA.ErrorMessage;
        //         return null;
        //     }
        //     return resp ?? Result;
        // }

        // /// <summary>
        // /// Delete physically
        // /// </summary>
        // /// <param name="data"></param>
        // /// <returns></returns>
        // public ZoneDTO ZoneDeleteComplete(ZoneDTO data)
        // {
        //     if (!DeletePermission(data))
        //     {
        //         ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
        //         return null;
        //     }
        //     var Response = ZoneDA.ZoneDelete(data);

        //     var resp = ZoneDA.ZoneGet(new ZoneDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
        //     Observers.ObserverStates.ZoneDelete state = new Observers.ObserverStates.ZoneDelete
        //     {
        //         Zone = resp ?? Response,
        //         User = User,
        //     };
        //     Notify(state);

        //     ResponseStatus = ZoneDA.ResponseStatus;
        //     if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
        //     {
        //         ErrorMessage += ZoneDA.ErrorMessage;
        //         return null;
        //     }

        //     return resp ?? Response;
        // }
        // private List<long> GetZoneParents(long ZoneID)
        // {
        //     var _allzone = AllZone;
        //     var ThisZone = _allzone.Where(s => s.ID == ZoneID)?.FirstOrDefault();
        //     List<long> res = new List<long>();
        //     res.Insert(0, ThisZone.ID ?? 0);
        //     while (ThisZone.ParentID != null)
        //     {
        //         var Parent = _allzone.Where(s => s.ID == ThisZone.ParentID)?.FirstOrDefault();
        //         res.Insert(0, Parent.ID ?? 0);
        //         ThisZone = Parent;
        //     }
        //     return res;
        // }
        // private List<long> GetZoneChilds(long ZoneID)
        // {
        //     var ThisZone = AllZone.Where(s => s.ID == ZoneID)?.FirstOrDefault();
        //     List<long> res = new List<long>();
        //     res.Add(ZoneID);
        //     var ChildList = new List<ZoneDTO>();
        //     GetAllChild(ThisZone, ChildList);
        //     res.AddRange(ChildList?.Select(s => (long)s.ID)?.ToList());
        //     return res;
        // }
        // private void GetAllChild(ZoneDTO Zone, List<ZoneDTO> Result)
        // {
        //     var Childs = AllZone.Where(s => s.ParentID == Zone.ID)?.ToList();
        //     foreach (var Child in Childs)
        //     {
        //         Result.Add(Child);
        //         GetAllChild(Child, Result);
        //     }
        // }
        // private List<ZoneDTO> _zone = new List<ZoneDTO>();
        // private List<ZoneDTO> AllZone
        // {
        //     get
        //     {
        //         if (!(_zone.Count > 0))
        //             _zone = new ZoneDA().AllZoneGet();
        //         return _zone;
        //     }
        // }


        // private String GenerateCode(ZoneDTO data)
        // {
        //     var list = ZoneGet(new ZoneDTO(), null);
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
        // private List<ZoneDTO> GenerateNewCodes(List<ZoneDTO> data, List<ZoneDTO> All)
        // {
        //     List<ZoneDTO> res = new List<ZoneDTO>();
        //     for (int thisZone = 0; thisZone < data?.Count; thisZone++)
        //     {
        //         var parent = GetParent(data[thisZone], All);
        //         if (parent == null) // root
        //         {
        //             data[thisZone].Code = string.Format("{0}", (thisZone + 1));
        //             res.Add(data[thisZone]);
        //         }
        //         else
        //         {
        //             data[thisZone].Code = string.Format("{0}-{1}", parent?.Code, (thisZone + 1));
        //             res.Add(data[thisZone]);
        //         }
        //         var childs = All?.Where(s => s.ParentID == data[thisZone]?.ID)?.ToList();

        //         for (int child = 0; child < childs?.Count; child++)
        //         {
        //             childs[child].Code = string.Format("{0}-{1}", data[thisZone].Code, (child + 1));
        //             res.Add(childs[child]);
        //             res.AddRange(GenerateNewCodes(All?.Where(s => s.ParentID == childs[child]?.ID)?.ToList(), All));
        //         }
        //     }
        //     return res;
        // }
        // private ZoneDTO GetParent(ZoneDTO data, List<ZoneDTO> all)
        // {
        //     return all?.Where(s => s.ID == data?.ParentID)?.ToList()?.FirstOrDefault();
        // }
        // public List<long> ZoneBranchGet(long branchid)
        // {
        //     BranchRegionWorkBL BranchRegionWorkBL = new BranchRegionWorkBL();
        //     BranchRegionWorkBL.User = User;

        //     var Branch = BranchRegionWorkBL.BranchRegionWorkGet(new DTO.BranchRegionWorkDTO { BranchID = branchid });
        //     var m = ((Branch.Select(s => s.ZoneAndChilds)).Union(Branch.Select(s => s.ZoneAndParents)))?.SelectMany(s => s).Union(Branch.Select(s => s.ZoneID ?? 0)).Distinct()?.ToList();
        //     if (m == null)
        //     {
        //         ResponseStatus = Common.ResponseStatus.BusinessError;
        //         return null;
        //     }
        //     ResponseStatus = Common.ResponseStatus.Successful;

        //     return m;
        // }


    }
}
