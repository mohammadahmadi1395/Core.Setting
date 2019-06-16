using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// using Alsahab.Common;

namespace Alsahab.Setting.MyAPI.Models
{
    public class Fixed
    {
        //public static List<SubsystemDTO> Subsystems
        //{
        //    get
        //    {
        //        return new List<SubsystemDTO>
        //        {
        //            new SubsystemDTO
        //            {
        //                ID = (int)Enums.Subsystem.Setting,
        //                IsDeleted = false,
        //                Description = Enums.Subsystem.Setting.GetDescription(),
        //                IsActive = true,
        //                IsSystem = false,
        //                Name = Enums.Subsystem.Setting.ToString(),
        //            },
        //            new SubsystemDTO
        //            {
        //                ID = (int)Enums.Subsystem.Member,
        //                IsDeleted = false,
        //                Description = Enums.Subsystem.Member.GetDescription(),
        //                IsActive = true,
        //                IsSystem = false,
        //                Name = Enums.Subsystem.Member.ToString(),
        //            },
        //            new SubsystemDTO
        //            {
        //                ID = (int)Enums.Subsystem.UserManagement,
        //                IsDeleted = false,
        //                Description = Enums.Subsystem.UserManagement.GetDescription(),
        //                IsActive = true,
        //                IsSystem = false,
        //                Name = Enums.Subsystem.UserManagement.ToString(),
        //            },
        //            new SubsystemDTO
        //            {
        //                ID = (int)Enums.Subsystem.FileManagement,
        //                IsDeleted = false,
        //                Description = Enums.Subsystem.FileManagement.GetDescription(),
        //                IsActive = true,
        //                IsSystem = false,
        //                Name = Enums.Subsystem.FileManagement.ToString(),
        //            },
        //            new SubsystemDTO
        //            {
        //                ID = (int)Enums.Subsystem.OrphanPoor,
        //                IsDeleted = false,
        //                Description = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsActive = true,
        //                IsSystem = false,
        //                Name = Enums.Subsystem.OrphanPoor.ToString(),
        //            },
        //            new SubsystemDTO
        //            {
        //                ID=(int)Enums.Subsystem.InQuery,
        //                IsDeleted=false,
        //                Description = Enums.Subsystem.InQuery.GetDescription(),
        //                IsActive=true,
        //                IsSystem=false,
        //                Name = Enums.Subsystem.InQuery.ToString(),
        //            },

        //        };
        //    }
        //}
        //public static List<SubpartDTO> Subparts
        //{
        //    get
        //    {
        //        return new List<SubpartDTO>
        //        {
        //            //Setting
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Setting_Countries,
        //                Name = Enums.Subpart.Setting_Countries.ToString(),
        //                Description = Enums.Subpart.Setting_Countries.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Setting,
        //                SubsystemName = Enums.Subsystem.Setting.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Setting_Cities,
        //                Name = Enums.Subpart.Setting_Cities.ToString(),
        //                Description = Enums.Subpart.Setting_Cities.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Setting,
        //                SubsystemName = Enums.Subsystem.Setting.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Setting_Areas,
        //                Name = Enums.Subpart.Setting_Areas.ToString(),
        //                Description = Enums.Subpart.Setting_Areas.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Setting,
        //                SubsystemName = Enums.Subsystem.Setting.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Setting_Regions,
        //                Name = Enums.Subpart.Setting_Regions.ToString(),
        //                Description = Enums.Subpart.Setting_Regions.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Setting,
        //                SubsystemName = Enums.Subsystem.Setting.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Setting_RegionAgents,
        //                Name = Enums.Subpart.Setting_RegionAgents.ToString(),
        //                Description = Enums.Subpart.Setting_RegionAgents.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Setting,
        //                SubsystemName = Enums.Subsystem.Setting.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Setting_Statements,
        //                Name = Enums.Subpart.Setting_Statements.ToString(),
        //                Description = Enums.Subpart.Setting_Statements.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Setting,
        //                SubsystemName = Enums.Subsystem.Setting.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            //Member
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_DeathReason,
        //                Name = Enums.Subpart.Member_DeathReason.ToString(),
        //                Description = Enums.Subpart.Member_DeathReason.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_IllnessSubject,
        //                Name = Enums.Subpart.Member_IllnessSubject.ToString(),
        //                Description = Enums.Subpart.Member_IllnessSubject.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_InterestSubject,
        //                Name = Enums.Subpart.Member_InterestSubject.ToString(),
        //                Description = Enums.Subpart.Member_InterestSubject.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_Job,
        //                Name = Enums.Subpart.Member_Job.ToString(),
        //                Description = Enums.Subpart.Member_Job.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_Member,
        //                Name = Enums.Subpart.Member_Member.ToString(),
        //                Description = Enums.Subpart.Member_Member.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonAddress,
        //                Name = Enums.Subpart.Member_PersonAddress.ToString(),
        //                Description = Enums.Subpart.Member_PersonAddress.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonalInfo,
        //                Name = Enums.Subpart.Member_PersonalInfo.ToString(),
        //                Description = Enums.Subpart.Member_PersonalInfo.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonDeathDetail,
        //                Name = Enums.Subpart.Member_PersonDeathDetail.ToString(),
        //                Description = Enums.Subpart.Member_PersonDeathDetail.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonHealthStatus,
        //                Name = Enums.Subpart.Member_PersonHealthStatus.ToString(),
        //                Description = Enums.Subpart.Member_PersonHealthStatus.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonIllness,
        //                Name = Enums.Subpart.Member_PersonIllness.ToString(),
        //                Description = Enums.Subpart.Member_PersonIllness.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonInterest,
        //                Name = Enums.Subpart.Member_PersonInterest.ToString(),
        //                Description = Enums.Subpart.Member_PersonInterest.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonJob,
        //                Name = Enums.Subpart.Member_PersonJob.ToString(),
        //                Description = Enums.Subpart.Member_PersonJob.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.Member,
        //                SubsystemName = Enums.Subsystem.Member.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            //OrphanPoor
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_Asset,
        //                Name = Enums.Subpart.OrphanPoor_Asset.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_Asset.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_EducationLeaveReason,
        //                Name = Enums.Subpart.Member_EducationLeaveReason.ToString(),
        //                Description = Enums.Subpart.Member_EducationLeaveReason.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_Family,
        //                Name = Enums.Subpart.OrphanPoor_Family.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_Family.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_FamilyAddress,
        //                Name = Enums.Subpart.OrphanPoor_FamilyAddress.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_FamilyAddress.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_FamilyAsset,
        //                Name = Enums.Subpart.OrphanPoor_FamilyAsset.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_FamilyAsset.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_FamilyEconomicStatus,
        //                Name = Enums.Subpart.OrphanPoor_FamilyEconomicStatus.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_FamilyEconomicStatus.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_FamilyRelative,
        //                Name = Enums.Subpart.OrphanPoor_FamilyRelative.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_FamilyRelative.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_Foundation,
        //                Name = Enums.Subpart.OrphanPoor_Foundation.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_Foundation.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_FamilyFoundationAssistance,
        //                Name = Enums.Subpart.OrphanPoor_FamilyFoundationAssistance.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_FamilyFoundationAssistance.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_FoundationAssistantType,
        //                Name = Enums.Subpart.OrphanPoor_FoundationAssistantType.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_FoundationAssistantType.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_HouseBuildingType,
        //                Name = Enums.Subpart.OrphanPoor_HouseBuildingType.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_HouseBuildingType.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_HouseOwnershipType,
        //                Name = Enums.Subpart.OrphanPoor_HouseOwnershipType.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_HouseOwnershipType.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_HousingStatus,
        //                Name = Enums.Subpart.OrphanPoor_HousingStatus.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_HousingStatus.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_Lesson,
        //                Name = Enums.Subpart.Member_Lesson.ToString(),
        //                Description = Enums.Subpart.Member_Lesson.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.OrphanPoor_Orphan,
        //                Name = Enums.Subpart.OrphanPoor_Orphan.ToString(),
        //                Description = Enums.Subpart.OrphanPoor_Orphan.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonEducationalGrade,
        //                Name = Enums.Subpart.Member_PersonEducationalGrade.ToString(),
        //                Description = Enums.Subpart.Member_PersonEducationalGrade.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonEducationalStatus,
        //                Name = Enums.Subpart.Member_PersonEducationalStatus.ToString(),
        //                Description = Enums.Subpart.Member_PersonEducationalStatus.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonEducationLeave,
        //                Name = Enums.Subpart.Member_PersonEducationLeave.ToString(),
        //                Description = Enums.Subpart.Member_PersonEducationLeave.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonLessonStatus,
        //                Name = Enums.Subpart.Member_PersonLessonStatus.ToString(),
        //                Description = Enums.Subpart.Member_PersonLessonStatus.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonProblem,
        //                Name = Enums.Subpart.Member_PersonProblem.ToString(),
        //                Description = Enums.Subpart.Member_PersonProblem.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID = (int)Enums.Subpart.Member_PersonProblemType,
        //                Name = Enums.Subpart.Member_PersonProblemType.ToString(),
        //                Description = Enums.Subpart.Member_PersonProblemType.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.OrphanPoor,
        //                SubsystemName = Enums.Subsystem.OrphanPoor.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID=(int)Enums.Subpart.InQuery_ManageRequests,
        //                Name=Enums.Subpart.InQuery_ManageRequests.ToString(),
        //                Description = Enums.Subpart.InQuery_ManageRequests.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.InQuery,
        //                SubsystemName = Enums.Subsystem.InQuery.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //              new SubpartDTO
        //            {
        //                ID=(int)Enums.Subpart.InQuery_HandleRequests,
        //                Name=Enums.Subpart.InQuery_HandleRequests.ToString(),
        //                Description = Enums.Subpart.InQuery_HandleRequests.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.InQuery,
        //                SubsystemName = Enums.Subsystem.InQuery.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //                new SubpartDTO
        //            {
        //                ID=(int)Enums.Subpart.InQuery_Log,
        //                Name=Enums.Subpart.InQuery_Log.ToString(),
        //                Description = Enums.Subpart.InQuery_Log.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.InQuery,
        //                SubsystemName = Enums.Subsystem.InQuery.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID=(int)Enums.Subpart.UserManagement_Access,
        //                Name=Enums.Subpart.UserManagement_Access.ToString(),
        //                Description = Enums.Subpart.UserManagement_Access.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.UserManagement,
        //                SubsystemName = Enums.Subsystem.UserManagement.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //              new SubpartDTO
        //            {
        //                ID=(int)Enums.Subpart.UserManagement_SingleUser,
        //                Name=Enums.Subpart.UserManagement_SingleUser.ToString(),
        //                Description = Enums.Subpart.UserManagement_SingleUser.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.UserManagement,
        //                SubsystemName = Enums.Subsystem.UserManagement.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //                new SubpartDTO
        //            {
        //                ID=(int)Enums.Subpart.UserManagement_TeamUser,
        //                Name=Enums.Subpart.UserManagement_TeamUser.ToString(),
        //                Description = Enums.Subpart.UserManagement_TeamUser.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.UserManagement,
        //                SubsystemName = Enums.Subsystem.UserManagement.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID=(int)Enums.Subpart.UserManagement_Group,
        //                Name=Enums.Subpart.UserManagement_Group.ToString(),
        //                Description = Enums.Subpart.UserManagement_Group.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.UserManagement,
        //                SubsystemName = Enums.Subsystem.UserManagement.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //           new SubpartDTO
        //            {
        //                ID=(int)Enums.Subpart.UserManagement_PermissionType,
        //                Name=Enums.Subpart.UserManagement_PermissionType.ToString(),
        //                Description = Enums.Subpart.UserManagement_PermissionType.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.UserManagement,
        //                SubsystemName = Enums.Subsystem.UserManagement.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //            new SubpartDTO
        //            {
        //                ID=(int)Enums.Subpart.UserManagement_ChangePassword,
        //                Name=Enums.Subpart.UserManagement_ChangePassword.ToString(),
        //                Description = Enums.Subpart.UserManagement_ChangePassword.GetDescription(),
        //                SubsystemID = (int)Enums.Subsystem.UserManagement,
        //                SubsystemName = Enums.Subsystem.UserManagement.GetDescription(),
        //                IsDeleted = false,
        //                IsActive = true,
        //                IsSystem = false,
        //            },
        //        };
        //    }
        //}
    }
}