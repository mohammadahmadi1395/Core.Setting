using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class BranchDA : DataAccess
    {
        public List<BranchDTO> BranchGet(BranchDTO data, BranchFilterDTO filter = null)
        {
            var result = new List<BranchDTO>();
            UseContext(database =>
            {
                var query = database.Branch.Where(t => true);
                #region Filter
                if (filter != null)
                {
                    if (filter?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate >= filter.FromCreateDate);
                    if (filter?.ToCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate <= (filter.ToCreateDate == filter.ToCreateDate.Value.Date ? filter.ToCreateDate.Value.AddDays(1).AddTicks(-1) : filter.ToCreateDate));
                    if (filter?.IDList?.Count > 0)
                        query = query.Where(s => filter.IDList.Contains(s.ID));
                    if (filter.IsLeafNode)
                        query = query.Where(s => s.RightIndex == s.LeftIndex + 1);
                }
                #endregion
                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ID == data.ID);
                    if (data?.ParentID > 0)
                        query = query.Where(s => s.ParentID == data.ParentID);
                    if (!string.IsNullOrWhiteSpace(data.Code))
                        query = query.Where(s => s.Code == data.Code);
                    if (!string.IsNullOrWhiteSpace(data.OldCode))
                        query = query.Where(s => s.OldCode == data.OldCode);
                    if (!string.IsNullOrWhiteSpace(data.Title))
                        query = query.Where(s => s.Title.Contains(data.Title));
                    if (data.HeadPersonID > 0)
                        query = query.Where(s => s.HeadPersonID == data.HeadPersonID);
                    if (!string.IsNullOrWhiteSpace(data.BranchPhoneNo))
                        query = query.Where(s => s.BranchPhoneNo.Contains(data.BranchPhoneNo));
                    if (!string.IsNullOrWhiteSpace(data.BranchEmail))
                        query = query.Where(s => s.BranchEmail.Contains(data.BranchEmail));
                    if (!string.IsNullOrWhiteSpace(data.BranchAddressID.ToString()))
                        query = query.Where(s => s.BranchAddressID == data.BranchAddressID);
                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));
                    if (data.IsCentral == true)
                        query = query.Where(s => s.IsCentral == true);
                    if (data.IsDeleted.HasValue == true)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);
                }
                else
                    query = query.Where(s => s.IsDeleted == false);
                #endregion

                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return result;
        }
        public BranchDTO BranchInsert(BranchDTO data)
        {
            Branch Branch = null;
            Branch = Mapper.Map(data);
            UseContext(c =>
            {
                c.Branch.Add(Branch);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = Branch.ID;
            return data;
        }
        public List<BranchDTO> BranchInsert(List<BranchDTO> data)
        {

            List<Branch> branchlist = null;
            branchlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.Branch.AddRange(branchlist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;

        }
        public BranchDTO BranchUpdate(BranchDTO data)
        {
            BranchDTO BranchDto = new BranchDTO();
            if (data.ID > 0)
            {
                BranchDto = BranchGet(new BranchDTO { ID = data.ID }, null)?.FirstOrDefault();
                BranchDto = new BranchDTO
                {
                    ID = data.ID,
                    Code = !string.IsNullOrWhiteSpace(data?.Code) ? data?.Code : BranchDto.Code,
                    ParentID = data?.ParentID,
                    Title = !string.IsNullOrWhiteSpace(data?.Title) ? data?.Title : BranchDto.Title,
                    HeadPersonID = !String.IsNullOrWhiteSpace(data?.HeadPersonID?.ToString()) ? data?.HeadPersonID : BranchDto.HeadPersonID,
                    BranchPhoneNo = !String.IsNullOrWhiteSpace(data?.BranchPhoneNo) ? data?.BranchPhoneNo : BranchDto.BranchPhoneNo,
                    BranchEmail = !String.IsNullOrWhiteSpace(data?.BranchEmail) ? data?.BranchEmail : BranchDto.BranchEmail,
                    BranchAddressID = !String.IsNullOrWhiteSpace(data?.BranchAddressID.ToString()) ? data?.BranchAddressID : BranchDto.BranchAddressID,
                    BranchComment = !String.IsNullOrWhiteSpace(data?.BranchComment) ? data?.BranchComment : BranchDto.BranchComment,
                    CreateDate = !String.IsNullOrWhiteSpace(data?.CreateDate.ToString()) ? data?.CreateDate : BranchDto.CreateDate,
                    IsCentral = data.IsCentral,
                    IsDeleted = data?.IsDeleted,
                    RightIndex = BranchDto?.RightIndex,
                    LeftIndex = BranchDto.LeftIndex,
                    Depth = BranchDto?.Depth,
                    OldCode = data?.OldCode
                };
            }
            Branch branch = Mapper.Map(BranchDto);
            UseContext(databsse =>
            {
                databsse.Entry(branch).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return BranchDto;
        }
        public List<BranchDTO> BranchUpdate(List<BranchDTO> data)
        {
            List<Branch> Branchlist = null;
            Branchlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                foreach (var item in Branchlist)
                    c.Entry(item).State = System.Data.Entity.EntityState.Modified;
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;
        }
        public BranchDTO BranchDelete(BranchDTO data)
        {
            BranchDTO branchDto = new BranchDTO();
            if (data.ID > 0)
            {
                branchDto = BranchGet(new BranchDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            branchDto.IsDeleted = true;

            return BranchUpdate(branchDto);
        }
        public List<BranchDTO> AllBranchGet()
        {
            var result = new List<BranchDTO>();
            UseContext(database =>
            {
                var query = database.Branch;
                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return result;
        }
    }
}
