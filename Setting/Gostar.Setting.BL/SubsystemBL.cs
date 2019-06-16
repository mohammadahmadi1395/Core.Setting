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
    public class SubsystemBL : BaseBusiness
    {
        SubsystemDA SubsystemDA = new SubsystemDA();
        private bool Validate(SubsystemDTO data)
        {
            return Validate<Validation.SubsystemValidator,SubsystemDTO>(data ?? new SubsystemDTO());

            //if (string.IsNullOrWhiteSpace(data.Name))
            //{
            //    ErrorMessage = "Subsystem Name Not Entered\n";
            //    return false;
            //}
            //if (data.IsDeleted == true)
            //{
            //    ErrorMessage = "Subsystem is not saved in database before.\n";
            //    return false;
            //}
            //var SubsystemList = SubsystemGet(new SubsystemDTO())?.ToList();
            //var CheckSubsystem = SubsystemList.Where(s => s.Name == data?.Name)?.Count();
            //if (CheckSubsystem > 0)
            //{
            //    ErrorMessage = "This Subsystem Is Exist\n";
            //    return false;
            //}
            //return true;
        }
        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DeletePermission(SubsystemDTO data)
        {
            if (!(data.ID > 0))
            {
                ErrorMessage = "Entered Subsystem is Mistake";
                return false;
            }
            StatementDA StatementDA = new StatementDA();
            var StatementSubsystemIDCheck = StatementDA.StatementGet(new DTO.StatementDTO { FilterSubsystemID = data.ID }).Count();
            SubpartDA SubpartDA = new SubpartDA();
            var SubpartIDCheck = SubpartDA.SubpartGet(new DTO.SubpartDTO { SubsystemID = data.ID }).Count();

            if (StatementSubsystemIDCheck > 0 || SubpartIDCheck > 0)
            {
                ErrorMessage = "This Subsystem use in another Tables,Please Delete  them First";
                return false;
            }
            return true;
        }
        public List<SubsystemDTO> SubsystemGet(SubsystemDTO data)
        {
            var Response = SubsystemDA.SubsystemGet(data);

            ResponseStatus = SubsystemDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubsystemDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        /// <summary>
        /// Insert Subsystem In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SubsystemDTO SubsystemInsert(SubsystemDTO data)
        {
            //validate data
            if (!Validate(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.CreateDate = DateTime.Now;
            var Response = SubsystemDA.SubsystemInsert(data);

            if (Response?.ID > 0)
            {
                var resp = SubsystemGet(new SubsystemDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.SubsystemAdd state = new Observers.ObserverStates.SubsystemAdd
                {
                    Subsystem = resp ?? Response,
                    User = User,
                };
                Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = SubsystemDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubsystemDA.ErrorMessage;
                return null;
            }
            return Response;
        }
        /// <summary>
        /// Insert List of Subsystem In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<SubsystemDTO> SubsystemInsert(List<SubsystemDTO> data)
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
            var Response = SubsystemDA.SubsystemInsert(data);

            List<SubsystemDTO> respList = new List<SubsystemDTO>();
            foreach (var val in Response)
            {
                var resp = SubsystemGet(new SubsystemDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                Observers.ObserverStates.SubsystemAdd state = new Observers.ObserverStates.SubsystemAdd
                {
                    Subsystem = resp ?? val,
                    User = User,
                };
                Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = SubsystemDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubsystemDA.ErrorMessage;
                return null;
            }
            
            return respList ?? Response;

        }
        /// <summary>
        /// SubsystemUpdate
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SubsystemDTO SubsystemUpdate(SubsystemDTO data)
        {
            if (!(data.ID > 0))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered Subsystem is Mistake";
                return null;
            }
            var Response = SubsystemDA.SubsystemUpdate(data);

            var resp = SubsystemGet(new SubsystemDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
            Observers.ObserverStates.SubsystemEdit state = new Observers.ObserverStates.SubsystemEdit
            {
                Subsystem = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = SubsystemDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubsystemDA.ErrorMessage;
                return null;
            }
            return resp ?? Response;
        }
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SubsystemDTO SubsystemDelete(SubsystemDTO data)
        {
            //Search For Use This Item Before Delete
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = SubsystemDA.SubsystemUpdate(data);

            var resp = SubsystemGet(new SubsystemDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.SubsystemDelete state = new Observers.ObserverStates.SubsystemDelete
            {
                Subsystem = resp ?? Response,
                User = User,
            };
            Notify(state);
            
            ResponseStatus = SubsystemDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubsystemDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public SubsystemDTO SubsystemDeleteComplete(SubsystemDTO data)
        {
            if (!DeletePermission(data))
            {
                ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
                return null;
            }
            var Response = SubsystemDA.SubsystemDelete(data);

            var resp = SubsystemGet(new SubsystemDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            Observers.ObserverStates.SubsystemDelete state = new Observers.ObserverStates.SubsystemDelete
            {
                Subsystem = resp ?? Response,
                User = User,
            };
            Notify(state);

            ResponseStatus = SubsystemDA.ResponseStatus;
            if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
            {
                ErrorMessage += SubsystemDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }
    }
}
