// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.Threading;
// using Gostar.Common;
// using Gostar.Setting.DTO;

// namespace Gostar.Setting.BL
// {
//    public class Fixed
//     {
//         private static UserInfoDTO user = new UserInfoDTO { UserID = 999999, UserRoleType = RoleType.AdminUser };

//         private static List<Gostar.Common.StatementDTO> _MultiLanguageData = null;
//         public static string TranslateStatement(string v, List<Gostar.Common.StatementDTO> statementList = null)
//         {
//             string lang = Thread.CurrentThread.CurrentUICulture?.ToString();
//             //  string lang = Gostar.Controls.Fixed.lang;
//             List<Gostar.Common.StatementDTO> statements = (statementList?.Count > 0) ? statementList : Fixed.MultiLanguageData;

//             var statement = statements.FirstOrDefault(s => s.TagName == v);
//             string result = v;
//             switch (lang)
//             {
//                 case "en-US":
//                     if (!string.IsNullOrWhiteSpace(statement?.EnglishText))
//                         result = statement?.EnglishText;
//                     break;
//                 case "ar-IQ":
//                     if (!string.IsNullOrWhiteSpace(statement?.ArabicText))
//                         result = statement?.ArabicText;
//                     break;
//                 case "fa-IR":
//                     if (!string.IsNullOrWhiteSpace(statement?.PersianText))
//                         result = statement?.PersianText;
//                     break;
//             }
//             return result;
//         }
//         public static List<Gostar.Common.StatementDTO> MultiLanguageData
//         {
//             get
//             {
//                 if (_MultiLanguageData == null)
//                 {
//                     StatementBL statementBl = new StatementBL();
//                     statementBl.User = user;
//                     var settingStatements = statementBl.StatementGet(new DTO.StatementDTO
//                     {
//                         FilterSubsystemID = SubsystemList?.FirstOrDefault(t => t.Name.Trim() == "Setting")?.ID,
//                     });
//                     //var settingStatements = .Statement(new Gostar.Setting.SC.Messages.StatementRequest
//                     //{
//                     //    User = user,
//                     //    ActionType = Gostar.Common.ActionType.Select,
//                     //    RequestDto = new DTO.StatementDTO
//                     //    {
//                     //        FilterSubsystemID = SubsystemList?.FirstOrDefault(t => t.Name.Trim() == "Setting")?.ID,
//                     //    }
//                     //}))?.ResponseDtoList;

//                     var memberStatements = statementBl.StatementGet(new DTO.StatementDTO
//                     {
//                         FilterSubsystemID = SubsystemList?.FirstOrDefault(t => t.Name.Trim() == "Member")?.ID,
//                     });
//                     //var memberStatements = ServiceUtility.Call(s => s.Statement(new Gostar.Setting.SC.Messages.StatementRequest
//                     //{
//                     //    User = user,
//                     //    ActionType = Gostar.Common.ActionType.Select,
//                     //    RequestDto = new DTO.StatementDTO
//                     //    {
//                     //        FilterSubsystemID = SubsystemList?.FirstOrDefault(t => t.Name.Trim() == "Member")?.ID,
//                     //    }
//                     //}))?.ResponseDtoList;

//                     var temp = settingStatements?.Union(memberStatements);
//                     _MultiLanguageData = (from t in temp
//                                           select new Gostar.Common.StatementDTO
//                                           {
//                                               ArabicText = t.ArabicText,
//                                               EnglishText = t.EnglishText,
//                                               CreateDate = t.CreateDate,
//                                               PersianText = t.PersianText,
//                                               SubsystemIDList = t.SubsystemList?.Select(s => s.ID)?.ToList(),
//                                               SubsystemNameList = t.SubsystemList?.Select(s => s.Name)?.ToList(),
//                                               TagName = t.TagName,
//                                           })?.ToList();
//                 }
//                 return _MultiLanguageData;
//             }
//         }

//         private static List<SubsystemDTO> _SubsystemList = null;
//         public static List<SubsystemDTO> SubsystemList
//         {
//             get
//             {
//                 if (_SubsystemList == null)
//                 {
//                     SubsystemBL subsystemBl = new SubsystemBL();
//                     subsystemBl.User = user;
//                     _SubsystemList = subsystemBl.SubsystemGet(new SubsystemDTO());
//                     //_SubsystemList = ServiceUtility.Call(s => s.Subsystem(new Gostar.Setting.SC.Messages.SubsystemRequest
//                     //{
//                     //    User = user,
//                     //    ActionType = Gostar.Common.ActionType.Select,
//                     //    RequestDto = new DTO.SubsystemDTO { }
//                     //}))?.ResponseDtoList;
//                 }
//                 return _SubsystemList;
//             }
//         }
//     }
// }
