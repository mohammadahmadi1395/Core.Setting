// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Alsahab.Setting.Data.Repositories;

// namespace Alsahab.Setting.BL
// {
//     public class BranchAddressBL : BaseBusiness
//     {
//         BranchAddressDL BranchAddressDA = new BranchAddressDL();
//         /// <summary>
//         /// Cehck Address Date
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         /// <summary>
//         /// Check Data For Insert
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         private bool Validate(BranchAddressDTO data)
//         {

//             return Validate<Validation.BranchAddressValidator,BranchAddressDTO>(data ?? new BranchAddressDTO());
//             ////if (string.IsNullOrWhiteSpace(data?.StreeName))
//             ////{
//             ////    ErrorMessage = "Branch Street Name Title Not Entered\n";
//             ////    return false;
//             ////}
//             ////if (string.IsNullOrWhiteSpace(data?.BranchName))
//             ////{
//             ////    ErrorMessage = "Branch Branch Name Title Not Entered\n";
//             ////    return false;
//             ////}

//             ////if (string.IsNullOrWhiteSpace(data?.PlateNo))
//             ////{
//             ////    ErrorMessage = "Branch Plate No Title Not Entered\n";
//             ////    return false;
//             ////}
//             ////if (string.IsNullOrWhiteSpace(data?.NearPoint))
//             ////{
//             ////    ErrorMessage = "Branch Address NearPoint Not Entered\n";
//             ////    return false;
//             ////}
//             //if (data?.IsDeleted == true)
//             //{
//             //    ErrorMessage = "BranchAddress Not yet Save in Database\n";
//             //    return false;
//             //}
//             //return true;
//         }
//         /// <summary>
//         /// Check Data For Delete
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         private bool DeletePermission(BranchAddressDTO data)
//         {
//             if (!(data.ID > 0))
//             {
//                 ErrorMessage = "Entered BranchAddress is Mistake";
//                 return false;
//             }
//             BranchDA BranchDA = new BranchDA();
//             var BranchAddressIDCheck = BranchDA.BranchGet(new BranchDTO { BranchAddressID = data.ID }, null).Count();
//             if ((BranchAddressIDCheck > 0))
//             {
//                 ErrorMessage = "This BranchAddress use in another Tables,Please Delete  them First";
//                 return false;
//             }
//             return true;
//         }
//         /// <summary>
//         /// Get List of BranchAddress From Database Whith DTO
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         public List<BranchAddressDTO> BranchAddressGet(BranchAddressDTO data, BranchAddressFilterDTO filter)
//         {
//             ZoneBL zbl = new ZoneBL();
//             zbl.User = User;
//             var ResponseAddress = BranchAddressDA.BranchAddressGet(data, filter);
//             var AllZone = zbl.ZoneGet(new ZoneDTO(), null);
//             foreach (var val in ResponseAddress)
//                 val.ZoneDTO.ZoneAddress = AllZone.Where(s => s.ID == val.ZoneDTO.ID)?.FirstOrDefault()?.ZoneAddress;
//             ResponseStatus = BranchAddressDA.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += BranchAddressDA.ErrorMessage;
//                 return null;
//             }
//             return ResponseAddress;
//         }
//         /// <summary>
//         /// Insert BranchAddress In Database
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         public BranchAddressDTO BranchAddressInsert(BranchAddressDTO data)
//         {
//             //validate data
//             if (!Validate(data))
//             {
//                 ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                 return null;
//             }

//             data.CreateDate = DateTime.Now;
//             var Response = BranchAddressDA.BranchAddressInsert(data);

//             if (Response?.ID > 0)
//             {
//                 var resp = BranchAddressGet(new BranchAddressDTO { ID = Response?.ID ?? 0 }, null)?.FirstOrDefault();
//                 Observers.ObserverStates.BranchAddressAdd state = new Observers.ObserverStates.BranchAddressAdd
//                 {
//                     BranchAddress = resp ?? Response,
//                     User = User,
//                 };
//                 Notify(state);
//                 if (resp != null)
//                     Response = resp;
//             }
            
//             ResponseStatus = BranchAddressDA.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += BranchAddressDA.ErrorMessage;
//                 return null;
//             }

//             return Response;
//         }
//         public List<BranchAddressDTO> BranchAddressInsert(List<BranchAddressDTO> data)
//         {
//             foreach (var d in data)
//             {
//                 if (!Validate(d))
//                 {
//                     ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                     return null;

//                 }

//                 d.CreateDate = DateTime.Now;
//             }
//             var Response = BranchAddressDA.BranchAddressInsert(data);

//             List<BranchAddressDTO> respList = new List<BranchAddressDTO>();
//             foreach (var val in Response)
//             {
//                 var resp = BranchAddressGet(new BranchAddressDTO { ID = val?.ID ?? 0 }, null)?.FirstOrDefault();
//                 Observers.ObserverStates.BranchAddressAdd state = new Observers.ObserverStates.BranchAddressAdd
//                 {
//                     BranchAddress = resp ?? val,
//                     User = User,
//                 };
//                 Notify(state);
//                 respList.Add(resp);
//             }

//             ResponseStatus = BranchAddressDA.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += BranchAddressDA.ErrorMessage;
//                 return null;
//             }
            
//             return respList ?? Response;

//         }
//         /// <summary>
//         /// BranchAddressUpdate
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         public BranchAddressDTO BranchAddressUpdate(BranchAddressDTO data)
//         {
//             if (!(data.ID > 0))
//             {
//                 ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                 ErrorMessage = "Entered BranchAddress is Mistake";
//                 return null;
//             }
//             var Response = BranchAddressDA.BranchAddressUpdate(data);

//             var resp = BranchAddressGet(new BranchAddressDTO { ID = Response?.ID ?? 0 }, null)?.FirstOrDefault();
//             Observers.ObserverStates.BranchAddressEdit state = new Observers.ObserverStates.BranchAddressEdit
//             {
//                 BranchAddress = resp ?? Response,
//                 User = User,
//             };
//             Notify(state);

//             ResponseStatus = BranchAddressDA.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += BranchAddressDA.ErrorMessage;
//                 return null;
//             }

//             return resp ?? Response;
//         }
//         /// <summary>
//         /// Delete Logicly
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         public BranchAddressDTO BranchAddressDelete(BranchAddressDTO data)
//         {
//             //Search For Use This Item Before Delete
//             if (!DeletePermission(data))
//             {
//                 ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                 return null;
//             }
//             data.IsDeleted = true;
//             var Response = BranchAddressUpdate(data);

//             var resp = BranchAddressGet(new BranchAddressDTO { ID = Response?.ID ?? 0, IsDeleted = true }, null)?.FirstOrDefault();
//             Observers.ObserverStates.BranchAddressDelete state = new Observers.ObserverStates.BranchAddressDelete
//             {
//                 BranchAddress = resp ?? Response,
//                 User = User,
//             };
//             Notify(state);
//             return resp ?? Response;
//         }
//         /// <summary>
//         /// Delete physically
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//     }
// }
