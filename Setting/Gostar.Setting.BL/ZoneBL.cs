using System;
using System.Collections.Generic;
using System.Linq;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;

namespace Gostar.Setting.BL
{
    public class ZoneBL : BaseBL
    {
        bool Response;
        ZoneDA ZoneDA = new ZoneDA();
        private List<ZoneDTO> TempAllZone = new List<ZoneDTO>();
        private long? _index = 1, _depth = 2;
        private bool Validate(ZoneDTO data)
        {

            return Validate<Validation.ZoneValidator, ZoneDTO>(data ?? new ZoneDTO());
            //ValidatorOptions.LanguageManager = new Gostar.Common.Validation.ErrorLanguageManager();
            //ValidatorOptions.LanguageManager.Culture = Culture;

            //var validator = new Validation.ZoneValidator();
            //ValidationResult result = validator.Validate(data ?? new ZoneDTO());
            //ValidationErrors = result.Errors;
            //return result.IsValid;

            //if (string.IsNullOrWhiteSpace(data.Title))
            //{
            //    ErrorMessage = "Zone Title Not Entered\n";
            //    return false;
            //}

            //if (string.IsNullOrWhiteSpace(data.Type.ToString()))
            //{
            //    ErrorMessage = "Zone Type Not Entered\n";
            //    return false;
            //}

            //if (data.IsDeleted == true)
            //{
            //    ErrorMessage = "Zone Not yet Save in Database\n";
            //    return false;
            //}
            //var ZoneCount = ZoneGet(new ZoneDTO { Title = data?.Title, Type = data?.Type }, null)?.Count;
            //if (ZoneCount > 0)
            //{
            //    ErrorMessage = "This Zone Is Exist\n";
            //    return false;
            //}

            //return true;
        }
        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DeletePermission(ZoneDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Zone is Mistake";
                return false;
            }
            var deletingItem = ZoneDA.ZoneGet(new ZoneDTO { ID = data.ID }, null)?.SingleOrDefault();
            var myLeft = deletingItem.LeftIndex; var myRight = deletingItem.RightIndex;
            var AllZons = AllZone;
            var deleteCount = AllZons.Where(i => i.LeftIndex >= myLeft && i.LeftIndex <= myRight && i.IsDeleted == false).Count();

            if (deleteCount > 1)
            {
                ErrorMessage += "You can't delete this zone. this zone has child";
                return false;
            }

            return true;
        }
        public List<ZoneDTO> ZoneGet()
        {
            var response = ZoneDA.AllZoneGet();
            ResponseStatus = ZoneDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ZoneDA.ErrorMessage;
                return null;
            }
            return response;
        }
        public List<ZoneDTO> ZoneSearch(ZoneDTO data)
        {
            var response = ZoneDA.ZoneSearch(data);
            ResponseStatus = ZoneDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ZoneDA.ErrorMessage;
                return null;
            }
            return response;
        }
        public ZoneDTO ZoneInsert(ZoneDTO data)
        {
            UpdateAllZone();
            var response = data;
            var AllZons = AllZone;

            ZoneDTO tempZone = new ZoneDTO
            {
                ID = 0,
                Depth = 1,
                ParentID = -1,
                LeftIndex = 1,
                RightIndex = (AllZons.Count + 1) * 2
            };

            if (response.ParentID == null)
                response.ParentID = 0;
            AllZons.Add(tempZone);
            foreach (var item in AllZons)
            {
                if (item.ParentID == null)
                    item.ParentID = 0;
            }

            var childs = AllZons.Where(c => c.ParentID == response.ParentID).Count();
            if (childs > 0)
            {
                long? right = 0;
                if (AllZons.Count > 0)
                    right = AllZons.SingleOrDefault(z => z.ID == response.ParentID)?.RightIndex;
                foreach (var zItem in AllZone)
                {
                    if (zItem.RightIndex >= right) zItem.RightIndex += 2;
                    if (zItem.LeftIndex > right) zItem.LeftIndex += 2;
                }

                foreach (var zone in AllZons)
                {
                    if (zone.ParentID == 0)
                        zone.ParentID = null;
                }

                AllZons.Remove(tempZone);
                ZoneDA.ZoneUpdate(AllZons);
                response.LeftIndex = right;
                response.RightIndex = right + 1;
            }
            else
            {
                long? left = 0;
                if (AllZons.Count > 0)
                    left = AllZons?.SingleOrDefault(z => z.ID == response.ParentID)?.LeftIndex;
                foreach (var zItem in AllZone)
                {
                    if (zItem.RightIndex > left) zItem.RightIndex += 2;
                    if (zItem.LeftIndex > left) zItem.LeftIndex += 2;
                }

                foreach (var zone in AllZons)
                {
                    if (zone.ParentID == 0)
                        zone.ParentID = null;
                }
                AllZons.Remove(tempZone);
                ZoneDA.ZoneUpdate(AllZons);
                response.LeftIndex = left + 1;
                response.RightIndex = left + 2;
            }

            long? parentDepth = 1;
            if (response.ParentID == 0)
                response.ParentID = null;
            else
                parentDepth = AllZons.SingleOrDefault(d => d.ID == response.ParentID).Depth;
            response.Depth = parentDepth + 1;

            return Insert(response);
        }
        /// <summary>
        /// Update Zone
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ZoneDTO ZoneUpdate(ZoneDTO data)
        {
            var Response = data;
            if (data.ParentID == 0)
                data.ParentID = null;
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Zone is Mistake";
                return null;
            }
            var oldZone = ZoneGet(new ZoneDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Response = ZoneDA.ZoneUpdate(data);

            Observers.ObserverStates.ZoneEdit state = new Observers.ObserverStates.ZoneEdit
            {
                Zone = oldZone ?? Response,
                User = User,
            };
            Notify(state);

            if (data.ParentID != oldZone.ParentID)
            {
                UpdateAllZone();
                //UpdateCode(oldZone);
                //GenerateCode();
            }


            ResponseStatus = ZoneDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ZoneDA.ErrorMessage;
                return null;
            }
            return oldZone ?? Response;
        }
        private ZoneDTO GetChild(ZoneDTO zone)
        {
            return TempAllZone.FirstOrDefault(i => i.ParentID == zone.ID && !(i.LeftIndex > 0));
        }
        private ZoneDTO GetBrother(ZoneDTO zone)
        {
            if (!(zone.ParentID > 0))
                return null;
            var parent = TempAllZone.FirstOrDefault(i => i.ID == zone.ParentID);
            var brother = GetChild(parent);
            return brother?.ID > 0 ? brother : null;
        }
        public List<ZoneDTO> UpdateAllZone()
        {
            var allZons = ZoneGet();
            foreach (var zone in allZons)
            {
                zone.LeftIndex = null;
                zone.RightIndex = null;
                zone.Depth = null;
                TempAllZone.Add(zone);
            }

            List<ZoneDTO> rootList = TempAllZone.Where(i => !(i.ParentID > 0))?.ToList();
            foreach (var root in rootList)
            {
                if (root?.ID > 0)
                {
                    _depth = 2;
                    RecursiveUpdateAllZone(root);
                }
            }

            TempAllZone = GenerateNewCodes(TempAllZone?.Where(s => s.ParentID == null && s.IsDeleted == false)?.ToList(), TempAllZone?.Where(s => s.IsDeleted == false)?.ToList());


            ZoneDA.ZoneUpdate(TempAllZone);
            ResponseStatus = ZoneDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ZoneDA.ErrorMessage;
                return null;
            }
            return ZoneDA.AllZoneGet();
        }
        private void RecursiveUpdateAllZone(ZoneDTO zoneData)
        {
            if (!(zoneData?.ID > 0) || !(TempAllZone?.Count > 0))
                return;

            TempAllZone.FirstOrDefault(i => i.ID == zoneData.ID).LeftIndex = ++_index;
            TempAllZone.FirstOrDefault(i => i.ID == zoneData.ID).Depth = _depth;

            var tempChild = GetChild(zoneData);
            if (tempChild?.ID > 0)
            {
                _depth++;
                RecursiveUpdateAllZone(tempChild);
            }

            TempAllZone.FirstOrDefault(i => i.ID == zoneData.ID).RightIndex = ++_index;
            var tempBrother = GetBrother(zoneData);

            if (tempBrother?.ID > 0)
                RecursiveUpdateAllZone(tempBrother);
            else
                _depth--;
        }
        /// <summary>
        /// Get List of Zone 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ZoneDTO> ZoneGet(ZoneDTO data, ZoneFilterDTO filter = null)
        {
            var Response = ZoneDA.ZoneGet(data, filter);

            ResponseStatus = ZoneDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ZoneDA.ErrorMessage;
                return null;
            }
            var AllZone = ZoneDA.ZoneGet(null, null);
            foreach (var val in Response)
            {
                List<String> ParentList = new List<string>();
                var ThisItem = val;
                ParentList.Insert(0, val.Title);
                while (ThisItem.ParentID != null)
                {
                    var Parent = AllZone.Where(s => s.ID == ThisItem.ParentID)?.FirstOrDefault();
                    ParentList.Insert(0, Parent.Title);
                    ThisItem = Parent;
                }
                val.ZoneAddress = String.Join("-", ParentList);
                val.ZoneAndChilds = GetZoneChilds(val.ID ?? 0);
                val.ZoneAndParents = GetZoneParents(val.ID ?? 0);

            }

            return Response;

        }
        /// <summary>
        /// Insert Zone in Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private ZoneDTO Insert(ZoneDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.CreateDate = DateTime.Now;
            data.Code = GenerateCode(data);

            var Response = ZoneDA.ZoneInsert(data);

            if (Response?.ID > 0)
            {
                var resp = ZoneDA.ZoneGet(new ZoneDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.ZoneAdd state = new Observers.ObserverStates.ZoneAdd
                {
                    Zone = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = ZoneDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ZoneDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        /// <summary>
        /// Insert List of Zone In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<ZoneDTO> ZoneInsert(List<ZoneDTO> data)
        {
            foreach (var d in data)
            {
                if (!Validate(d))
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                    return null;
                }
                d.CreateDate = DateTime.Now;
            }
            var Response = ZoneDA.ZoneInsert(data);

            List<ZoneDTO> respList = new List<ZoneDTO>();
            foreach (var val in Response)
            {
                var resp = ZoneDA.ZoneGet(new ZoneDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.ZoneAdd state = new Observers.ObserverStates.ZoneAdd
                {
                    Zone = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = ZoneDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ZoneDA.ErrorMessage;
                return null;
            }
            return respList ?? Response;
        }
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ZoneDTO ZoneDelete(ZoneDTO data)
        {
            if (!DeletePermission(data))
            {
                ResponseStatus = Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Result = ZoneDA.ZoneUpdate(data);

            var resp = ZoneGet(new ZoneDTO { ID = Result?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.ZoneDelete state = new Observers.ObserverStates.ZoneDelete
            {
                Zone = resp ?? Result,
                User = User,
            };
            Notify(state);

            ResponseStatus = ZoneDA.ResponseStatus;
            if (ResponseStatus != Common.ResponseStatus.Successful)
            {
                ErrorMessage += ZoneDA.ErrorMessage;
                return null;
            }
            return resp ?? Result;
            //Search For Use This Item Before Delete
            //if (!DeletePermission(data))
            //{
            //    ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
            //    return null;
            //}

            //UpdateAllZone();
            //var Result = new ZoneDTO();

            //Result = DeleteZons(data);


            //var DeleteItems = ItemsToDelete(data, ZoneGet(new ZoneDTO { }));
            //DeleteItems.Add(data);
            //List<ZoneDTO> ItemsForDelete = new List<ZoneDTO>();
            //foreach (var val in DeleteItems)
            //    if (DeletePermission(val))
            //        ItemsForDelete.Add(val);
            //var Result = new ZoneDTO();
            //foreach (var zone in ItemsForDelete)
            //{
            //    zone.IsDeleted = true;
            //    Result = ZoneDA.ZoneUpdate(zone);
            //    ResponseStatus = ZoneDA.ResponseStatus;
            //    if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            //    {
            //        ErrorMessage += ZoneDA.ErrorMessage;
            //    }
            //}
            //if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            //{
            //    ErrorMessage += ZoneDA.ErrorMessage;
            //    return null;
            //}
            // return Result;

        }

        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public ZoneDTO ZoneDeleteComplete(ZoneDTO data)
        {
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            var Response = ZoneDA.ZoneDelete(data);

            var resp = ZoneDA.ZoneGet(new ZoneDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.ZoneDelete state = new Observers.ObserverStates.ZoneDelete
            {
                Zone = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = ZoneDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += ZoneDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
        private List<long> GetZoneParents(long ZoneID)
        {
            var _allzone = AllZone;
            var ThisZone = _allzone.Where(s => s.ID == ZoneID)?.FirstOrDefault();
            List<long> res = new List<long>();
            res.Insert(0, ThisZone.ID ?? 0);
            while (ThisZone.ParentID != null)
            {
                var Parent = _allzone.Where(s => s.ID == ThisZone.ParentID)?.FirstOrDefault();
                res.Insert(0, Parent.ID ?? 0);
                ThisZone = Parent;
            }
            return res;
        }
        private List<long> GetZoneChilds(long ZoneID)
        {
            var ThisZone = AllZone.Where(s => s.ID == ZoneID)?.FirstOrDefault();
            List<long> res = new List<long>();
            res.Add(ZoneID);
            var ChildList = new List<ZoneDTO>();
            GetAllChild(ThisZone, ChildList);
            res.AddRange(ChildList?.Select(s => (long)s.ID)?.ToList());
            return res;
        }
        private void GetAllChild(ZoneDTO Zone, List<ZoneDTO> Result)
        {
            var Childs = AllZone.Where(s => s.ParentID == Zone.ID)?.ToList();
            foreach (var Child in Childs)
            {
                Result.Add(Child);
                GetAllChild(Child, Result);
            }
        }
        private List<ZoneDTO> _zone = new List<ZoneDTO>();
        private List<ZoneDTO> AllZone
        {
            get
            {
                if (!(_zone.Count > 0))
                    _zone = new ZoneDA().AllZoneGet();
                return _zone;
            }
        }


        private String GenerateCode(ZoneDTO data)
        {
            var list = ZoneGet(new ZoneDTO(), null);
            if (data?.ParentID == null)
            {
                return (list?.Where(s => s.ParentID == null)?.ToList()?.Count + 1).ToString();
            }
            else
            {
                var r = list?.Where(s => s.ParentID == data?.ParentID)?.ToList()?.Count;
                return String.Format("{0}-{1}", list?.Where(s => s.ID == data?.ParentID)?.FirstOrDefault()?.Code, (r + 1).ToString());
            }
        }
        private List<ZoneDTO> GenerateNewCodes(List<ZoneDTO> data, List<ZoneDTO> All)
        {
            List<ZoneDTO> res = new List<ZoneDTO>();
            for (int thisZone = 0; thisZone < data?.Count; thisZone++)
            {
                var parent = GetParent(data[thisZone], All);
                if (parent == null) // root
                {
                    data[thisZone].Code = string.Format("{0}", (thisZone + 1));
                    res.Add(data[thisZone]);
                }
                else
                {
                    data[thisZone].Code = string.Format("{0}-{1}", parent?.Code, (thisZone + 1));
                    res.Add(data[thisZone]);
                }
                var childs = All?.Where(s => s.ParentID == data[thisZone]?.ID)?.ToList();

                for (int child = 0; child < childs?.Count; child++)
                {
                    childs[child].Code = string.Format("{0}-{1}", data[thisZone].Code, (child + 1));
                    res.Add(childs[child]);
                    res.AddRange(GenerateNewCodes(All?.Where(s => s.ParentID == childs[child]?.ID)?.ToList(), All));
                }
            }
            return res;
        }
        private ZoneDTO GetParent(ZoneDTO data, List<ZoneDTO> all)
        {
            return all?.Where(s => s.ID == data?.ParentID)?.ToList()?.FirstOrDefault();
        }

    }
}
