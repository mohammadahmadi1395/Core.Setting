using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;

namespace Gostar.Setting.BL
{
    public class RuleTagBL : BaseBusiness
    {
        RuleTagDA RuleTagDL = new RuleTagDA();
        private bool Validate(RuleTagDTO data)
        {
            return Validate<Validation.RuleTagValidator,RuleTagDTO>(data ?? new RuleTagDTO());
            //if (!(data?.RuleID>0))
            //{
            //    ErrorMessage = "This Rule Is InCorrect \n";
            //    return false;
            //}
            //if (!(data?.FormTypeID > 0))
            //{
            //    ErrorMessage = "This FormType Is InCorrect \n";
            //    return false;
            //}


            

            //return true;

        }
        private bool IsExist(RuleTagDTO data)
        {
            var res = RuleTagGet(new RuleTagDTO { RuleID = data.RuleID, FormTypeID = data.FormTypeID }, null).Count;
            if (res > 0)
            {
                return true;
            }
            return false;
        }
        private bool DeletePermision(RuleTagDTO data)
        {
           
            return true;
        }
        public List<RuleTagDTO> RuleTagGet(RuleTagDTO data, RuleTagFilterDTO filter = null)
        {
            var Response = RuleTagDL.RuleTagGet(data, filter);

            ResponseStatus = RuleTagDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RuleTagDL.ErrorMessage;
                return null;
            }
            return Response;
        }
        public RuleTagDTO RuleTagInsert(RuleTagDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.CreateDate = DateTime.Now;
            var Response = RuleTagDL.RuleTagInsert(data);

            if (Response?.ID > 0)
            {
                var resp = RuleTagGet(new RuleTagDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.RuleTagAdd state = new Observers.ObserverStates.RuleTagAdd
                {
                    RuleTag = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = RuleTagDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RuleTagDL.ErrorMessage;
                return null;
            }
            return Response;
        }
        public List<RuleTagDTO> RuleTagInsert(List<RuleTagDTO> data)
        {
            List<RuleTagDTO> NewData = new List<RuleTagDTO>();
            foreach (var d in data)
            {
                if (!Validate(d))
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                    return null;
                }
                d.CreateDate = DateTime.Now;
            }
            foreach(var val in data)
            {
                if (IsExist(val))
                    continue;
                else
                {
                    NewData.Add(val);
                }
            }
            var Res = RuleTagGet(new RuleTagDTO {RuleID = data.FirstOrDefault().RuleID });
            foreach(var val in Res)
            {
                bool Exist = false;
                foreach(var newval in data)
                {
                    if(val.RuleID == newval.RuleID && val.FormTypeID == newval.FormTypeID)
                    {
                        Exist = true;
                        break;
                    }
                }
                if (!Exist)
                    RuleTagDelete(val);
            }
            var Response = RuleTagDL.RuleTagInsert(NewData);

            ResponseStatus = RuleTagDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RuleTagDL.ErrorMessage;
                return null;
            }
            return Response;

        }
        public RuleTagDTO RuleTagUpdate(RuleTagDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered RuleTag is Mistake";
                return null;
            }
            var Response = RuleTagDL.RuleTagUpdate(data);

            var resp = RuleTagGet(new RuleTagDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.RuleTagEdit state = new Observers.ObserverStates.RuleTagEdit
            {
                RuleTag = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = RuleTagDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RuleTagDL.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        public RuleTagDTO RuleTagDelete(RuleTagDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermision(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = RuleTagDL.RuleTagUpdate(data);

            var resp = RuleTagGet(new RuleTagDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.RuleTagDelete state = new Observers.ObserverStates.RuleTagDelete
            {
                RuleTag = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = RuleTagDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RuleTagDL.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        public List<RuleTagDTO> RuleTagAllDelete(long RuleID)
        {
            var data = RuleTagGet(new RuleTagDTO { RuleID = RuleID });
            List<RuleTagDTO> Response = new List<RuleTagDTO>();
            //Search For Use This Item Before Delete
            foreach (var val in data)
            {
                if (!DeletePermision(val))
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                    return null;
                }
                val.IsDeleted = true;
             Response.Add(RuleTagDL.RuleTagUpdate(val));
            }
            ResponseStatus = RuleTagDL.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += RuleTagDL.ErrorMessage;
                return null;
            }
            return Response;
        }
    }
}
