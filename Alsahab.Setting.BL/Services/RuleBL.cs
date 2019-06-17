// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Gostar.Setting.DTO;
// using Gostar.Setting.DA;

// namespace Gostar.Setting.BL
// {
//     public class RuleBL : BaseBusiness
//     {
//         RuleDA RuleDL = new RuleDA();
//         RuleTagBL RuleTag = new RuleTagBL();
//         RuleTagBL Rtb = new RuleTagBL();  
//         private bool Validate(RuleDTO data)
//         {

//             return Validate<Validation.RuleValidator,RuleDTO>(data ?? new RuleDTO());
//             //if (String.IsNullOrWhiteSpace(data?.Description))
//             //{
//             //    ErrorMessage = "Rule Description is Empty \n";
//             //    return false;
//             //}
//             //if (String.IsNullOrWhiteSpace(data?.Type.ToString()))
//             //{
//             //    ErrorMessage = "Rule Type is Empty \n";
//             //    return false;
//             //}
//             //if (String.IsNullOrWhiteSpace(data?.Title))
//             //{
//             //    ErrorMessage = "Rule Title is Empty \n";
//             //    return false;
//             //}
       

//             //var res = RuleGet(new RuleDTO { Type = data.Type , Description = data.Description }, null).Count;
//             //if (res > 0)
//             //{
//             //    ErrorMessage = "This Rule Is Exist \n";
//             //    return false;
//             //}

//             //return true;

//         }
//         private bool DeletePermision(RuleDTO data)
//         {
            

//             return true;
//         }
//         public List<RuleDTO> RuleGet(RuleDTO data, RuleFilterDTO filter = null)
//         {
//             var Response = RuleDL.RuleGet(data, filter,PagingInfo);
//             ResultCount = RuleDL.ResultCount;
//             ResponseStatus = RuleDL.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += RuleDL.ErrorMessage;
//                 return null;
//             }
//             return Response;
//         }
//         public RuleDTO RuleInsert(RuleDTO data)
//         {
//             if (!Validate(data))
//             {
//                 ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                 return null;
//             }
//             data.CreateDate = DateTime.Now;
//             var Response = RuleDL.RuleInsert(data);

//             if (Response?.ID > 0)
//             {
//                 var resp = RuleGet(new RuleDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
//                 Observers.ObserverStates.RuleAdd state = new Observers.ObserverStates.RuleAdd
//                 {
//                     Rule = resp ?? Response,
//                     User = User,
//                 };
//                 Notify(state);
//                 if (resp != null)
//                     Response = resp;
//             }

//             ResponseStatus = RuleDL.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += RuleDL.ErrorMessage;
//                 return null;
//             }
//             return Response;
//         }
//         public List<RuleDTO> RuleInsert(List<RuleDTO> data)
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
//             var Response = RuleDL.RuleInsert(data);

//             List<RuleDTO> respList = new List<RuleDTO>();
//             foreach (var val in Response)
//             {
//                 var resp = RuleGet(new RuleDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
//                 Observers.ObserverStates.RuleAdd state = new Observers.ObserverStates.RuleAdd
//                 {
//                     Rule = resp ?? val,
//                     User = User,
//                 };
//                 Notify(state);
//                 respList.Add(resp);
//             }
            
//             ResponseStatus = RuleDL.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += RuleDL.ErrorMessage;
//                 return null;
//             }
//             return respList ?? Response;

//         }
//         public RuleDTO RuleUpdate(RuleDTO data)
//         {
//             if (!(data.ID > 0))
//             {
//                 ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                 ErrorMessage = "Entered Rule is Mistake";
//                 return null;
//             }
//             var Response = RuleDL.RuleUpdate(data);

//             var resp = RuleGet(new RuleDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
//             Observers.ObserverStates.RuleEdit state = new Observers.ObserverStates.RuleEdit
//             {
//                 Rule = resp ?? Response,
//                 User = User,
//             };
//             Notify(state);
            
//             ResponseStatus = RuleDL.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += RuleDL.ErrorMessage;
//                 return null;
//             }
//             return resp ?? Response;
//         }
//         public RuleDTO RuleDelete(RuleDTO data)
//         {
//             //Search For Use This Item Before Delete
//             if (!DeletePermision(data))
//             {
//                 ResponseStatus = Gostar.Common.ResponseStatus.BusinessError;
//                 return null;
//             }
//             data.IsDeleted = true;
//             var Response = RuleDL.RuleUpdate(data);

//             var resp = RuleGet(new RuleDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
//             Observers.ObserverStates.RuleDelete state = new Observers.ObserverStates.RuleDelete
//             {
//                 Rule = resp ?? Response,
//                 User = User,
//             };
//             Notify(state);

//             ResponseStatus = RuleDL.ResponseStatus;
//             if (ResponseStatus != Gostar.Common.ResponseStatus.Successful)
//             {
//                 ErrorMessage += RuleDL.ErrorMessage;
//                 return null;
//             }
//             var Res = Rtb.RuleTagAllDelete(Response.ID??0);
//             return resp ?? Response;
//         }
//     }
// }
