using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA;
using Gostar.Setting.DA.Entities;
using Gostar.Common;

namespace Gostar.Setting.BL
{
    public class SubpartBL : BaseBL
    {
        SubpartDA SubpartDA = new SubpartDA();
        private bool Validate(SubpartDTO data)
        {

            return Validate<Validation.SubpartValidator,SubpartDTO>(data ?? new SubpartDTO());
            //if (!(data?.SubsystemID >0 ))
            //{
            //    ErrorMessage = "Select SubSystem First\n";
            //    return false;
            //}
            //if (string.IsNullOrWhiteSpace(data.Name))
            //{
            //    ErrorMessage = "Subpart Name Not Entered\n";
            //    return false;
            //}
            //if (data.IsDeleted == true)
            //{
            //    ErrorMessage = "Subpart is not saved in database before.\n";
            //    return false;
            //}
            //var SubpartList = SubpartGet(new SubpartDTO())?.ToList();
            //var CheckSubpart = SubpartList.Where(s => s.Name == data?.Name)?.Count();
            //if (CheckSubpart > 0)
            //{
            //    ErrorMessage = "This Subpart Is Exist\n";
            //    return false;
            //}
            //return true;
        }
        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DeletePermission(SubpartDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Subpart is Mistake";
                return false;
            }
            //SubsystemDA SubsystemDA = new SubsystemDA();
            //var SubsystemIDCheck = SubsystemDA.SubsystemGet(new DTO.SubsystemDTO { ID = data.SubsystemID }).Count();
            //if ((SubsystemIDCheck > 0))
            //{
            //    ErrorMessage = "This Subpart use in another Tables,Please Delete  them First.\n";
            //    return false;
            //}
            return true;
        }
        public List<SubpartDTO> SubpartGet(SubpartDTO data)
        {
            var Response = SubpartDA.SubpartGet(data);

            ResponseStatus = SubpartDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubpartDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        /// <summary>
        /// Insert Subpart In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SubpartDTO SubpartInsert(SubpartDTO data)
        {
            //validate data
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.CreateDate = DateTime.Now;
            var Response = SubpartDA.SubpartInsert(data);

            if (Response?.ID > 0)
            {
                var resp = SubpartGet(new SubpartDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.SubpartAdd state = new Observers.ObserverStates.SubpartAdd
                {
                    Subpart = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = SubpartDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubpartDA.ErrorMessage;
                return null;
            }
            return Response;
        }
        /// <summary>
        /// Insert List of Subpart In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<SubpartDTO> SubpartInsert(List<SubpartDTO> data)
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
            var Response = SubpartDA.SubpartInsert(data);

            List<SubpartDTO> respList = new List<SubpartDTO>();
            foreach (var val in Response)
            {
                var resp = SubpartGet(new SubpartDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.SubpartAdd state = new Observers.ObserverStates.SubpartAdd
                {
                    Subpart = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = SubpartDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubpartDA.ErrorMessage;
                return null;
            }
            
            return respList ?? Response;

        }
        /// <summary>
        /// SubpartUpdate
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SubpartDTO SubpartUpdate(SubpartDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Subpart is Mistake";
                return null;
            }
            var Response = SubpartDA.SubpartUpdate(data);

            var resp = SubpartGet(new SubpartDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.SubpartEdit state = new Observers.ObserverStates.SubpartEdit
            {
                Subpart = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = SubpartDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubpartDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SubpartDTO SubpartDelete(SubpartDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = SubpartDA.SubpartUpdate(data);

            var resp = SubpartGet(new SubpartDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.SubpartDelete state = new Observers.ObserverStates.SubpartDelete
            {
                Subpart = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = SubpartDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubpartDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SubpartDTO SubpartDeleteComplete(SubpartDTO data)
        {
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            var Response = SubpartDA.SubpartDelete(data);

            var resp = SubpartGet(new SubpartDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.SubpartDelete state = new Observers.ObserverStates.SubpartDelete
            {
                Subpart = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = SubpartDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubpartDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
    }
}
