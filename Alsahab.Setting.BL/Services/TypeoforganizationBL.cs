// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Gostar.Setting.DA;
// using Gostar.Setting.DTO;

// namespace Gostar.Setting.BL
// {
//     public class TypeoforganizationBL : BaseBL
//     {
//         TypeoforganizationDA TypeoforganizationDA = new TypeoforganizationDA();
//         /// <summary>
//         /// Check Data For Insert
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         private bool Validate(TypeoforganizationDTO data)
//         {

//             return Validate<Validation.TypeoforganizationValidator,TypeoforganizationDTO>(data ?? new TypeoforganizationDTO());
//             //ValidatorOptions.LanguageManager = new Gostar.Common.Validation.ErrorLanguageManager();
//             //ValidatorOptions.LanguageManager.Culture = Culture;

//             //var validator = new Validation.TypeoforganizationValidator();
//             //ValidationResult result = validator.Validate(data ?? new TypeoforganizationDTO());
//             //ValidationErrors = result.Errors;
//             //return result.IsValid;
//             //if (data == null)
//             //{
//             //    ErrorMessage = "Data Not Entered!";
//             //    return false;
//             //}
//             //if (string.IsNullOrWhiteSpace(data.Title))
//             //{
//             //    ErrorMessage = "Typeoforganization Title Not Entered\n";
//             //    return false;
//             //}

//             //if (data.IsDeleted == true)
//             //{
//             //    ErrorMessage = "Typeoforganization Not yet Save in Database\n";
//             //    return false;
//             //}
//             //var TypeoforganizationList = TypeoforganizationGet(new TypeoforganizationDTO { Title = data?.Title })?.ToList();
//             //var CheckTypeoforganization = TypeoforganizationList.Where(s => s.Title == data?.Title)?.Count();
//             //if (CheckTypeoforganization > 0)
//             //{
//             //    ErrorMessage = "This Typeoforganization Is Exist\n";
//             //    return false;
//             //}

//             //return true;
//         }
//         /// <summary>
//         /// Check Data For Delete
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         private bool DeletePermission(TypeoforganizationDTO data)
//         {
//             if (!(data.ID > 0))
//             {
//                 ErrorMessage = "Entered Typeoforganization is Mistake";
//                 return false;
//             }
//             return true;
//         }
//         /// <summary>
//         /// Get List of Typeoforganization 
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         public List<TypeoforganizationDTO> TypeoforganizationGet(TypeoforganizationDTO data)
//         {
//             var Response = TypeoforganizationDA.TypeoforganizationGet(data,PagingInfo);
//             ResultCount = TypeoforganizationDA.ResultCount;
//             ResponseStatus = TypeoforganizationDA.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += TypeoforganizationDA.ErrorMessage;
//                 return null;
//             }
//             return Response;
//         }
//         /// <summary>
//         /// Insert Typeoforganization in Database
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         public TypeoforganizationDTO TypeoforganizationInsert(TypeoforganizationDTO data)
//         {
//             if (!Validate(data))
//             {
//                 ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                 return null;
//             }

//             data.CreateDate = DateTime.Now;
//             var Response = TypeoforganizationDA.TypeoforganizationInsert(data);

//             if (Response?.ID > 0)
//             {
//                 var resp = TypeoforganizationGet(new TypeoforganizationDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
//                 Observers.ObserverStates.TypeoforganizationAdd state = new Observers.ObserverStates.TypeoforganizationAdd
//                 {
//                     Typeoforganization = resp ?? Response,
//                     User = User,
//                 };
//                 Notify(state);
//                 if (resp != null)
//                     Response = resp;
//             }

//             ResponseStatus = TypeoforganizationDA.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += TypeoforganizationDA.ErrorMessage;
//                 return null;
//             }

//             return Response;
//         }
//         /// <summary>
//         /// Insert List of Typeoforganization In Database
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         public List<TypeoforganizationDTO> TypeoforganizationInsert(List<TypeoforganizationDTO> data)
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
//             var Response = TypeoforganizationDA.TypeoforganizationInsert(data);

//             List<TypeoforganizationDTO> respList = new List<TypeoforganizationDTO>();
//             foreach (var val in Response)
//             {
//                 var resp = TypeoforganizationGet(new TypeoforganizationDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
//                 Observers.ObserverStates.TypeoforganizationAdd state = new Observers.ObserverStates.TypeoforganizationAdd
//                 {
//                     Typeoforganization = resp ?? val,
//                     User = User,
//                 };
//                 Notify(state);
//                 respList.Add(resp);
//             }
            
//             ResponseStatus = TypeoforganizationDA.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += TypeoforganizationDA.ErrorMessage;
//                 return null;
//             }

//             return respList ?? Response;
//         }
//         /// <summary>
//         /// Update Typeoforganization
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         public TypeoforganizationDTO TypeoforganizationUpdate(TypeoforganizationDTO data)
//         {
//             if (!(data.ID > 0))
//             {
//                 ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                 ErrorMessage = "Entered Typeoforganization is Mistake";
//                 return null;
//             }
//             var Response = TypeoforganizationDA.TypeoforganizationUpdate(data);

//             var resp = TypeoforganizationGet(new TypeoforganizationDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
//             Observers.ObserverStates.TypeoforganizationEdit state = new Observers.ObserverStates.TypeoforganizationEdit
//             {
//                 Typeoforganization = resp ?? Response,
//                 User = User,
//             };
//             Notify(state);

//             ResponseStatus = TypeoforganizationDA.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += TypeoforganizationDA.ErrorMessage;
//                 return null;
//             }

//             return resp ?? Response;
//         }
//         /// <summary>
//         /// Delete Logicly
//         /// </summary>
//         /// <param name="data"></param>
//         /// <returns></returns>
//         public TypeoforganizationDTO TypeoforganizationDelete(TypeoforganizationDTO data)
//         {
//             //Search For Use This Item Before Delete
//             if (!DeletePermission(data))
//             {
//                 ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                 return null;
//             }
//             data.IsDeleted = true;
//             var Response = TypeoforganizationUpdate(data);

//             var resp = TypeoforganizationGet(new TypeoforganizationDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
//             Observers.ObserverStates.TypeoforganizationDelete state = new Observers.ObserverStates.TypeoforganizationDelete
//             {
//                 Typeoforganization = resp ?? Response,
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
//         public TypeoforganizationDTO TypeoforganizationDeleteComplete(TypeoforganizationDTO data)
//         {
//             if (!DeletePermission(data))
//             {
//                 ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                 return null;
//             }
//             var Response = TypeoforganizationDA.TypeoforganizationDelete(data);

//             var resp = TypeoforganizationGet(new TypeoforganizationDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
//             Observers.ObserverStates.TypeoforganizationDelete state = new Observers.ObserverStates.TypeoforganizationDelete
//             {
//                 Typeoforganization = resp ?? Response,
//                 User = User,
//             };
//             Notify(state);

//             ResponseStatus = TypeoforganizationDA.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += TypeoforganizationDA.ErrorMessage;
//                 return null;
//             }

//             return resp ?? Response;
//         }
//     }
// }
