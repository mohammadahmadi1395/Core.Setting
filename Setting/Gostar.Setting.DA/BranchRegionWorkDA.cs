using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class BranchRegionWorkDA : DataAccess
    {
        public List<BranchRegionWorkDTO> BranchRegionWorkGet(BranchRegionWorkDTO data)
        {

            var result = new List<BranchRegionWorkDTO>();
            UseContext(database =>
            {
                var query = database.BranchRegionWork.Where(t => true);
                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ID == data.ID);
                    if (data.ZoneID > 0)
                        query = query.Where(s => s.ZoneID == data.ZoneID);
                    if (data.BranchID > 0)
                        query = query.Where(s => s.BranchID == data.BranchID);
                    if (data.IsDeleted.HasValue == true)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);
                }
                #endregion

                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return result;
        }

        public BranchRegionWorkDTO BranchRegionWorkInsert(BranchRegionWorkDTO data)
        {
            BranchRegionWork BranchRegionWork = null;
            BranchRegionWork = Mapper.Map(data);
            UseContext(c =>
            {
                c.BranchRegionWork.Add(BranchRegionWork);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = BranchRegionWork.ID;
            return data;
        }

        public List<BranchRegionWorkDTO> BranchRegionWorkInsert(List<BranchRegionWorkDTO> data)
        {
            List<BranchRegionWork> branchRegionWorklist = null;
            branchRegionWorklist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.BranchRegionWork.AddRange(branchRegionWorklist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;

        }


        public BranchRegionWorkDTO BranchRegionWorkUpdate(BranchRegionWorkDTO data)
        {
            BranchRegionWorkDTO BranchRegionWorkDto = new BranchRegionWorkDTO();
            if (data.BranchID > 0)
            {
                BranchRegionWorkDto = BranchRegionWorkGet(new BranchRegionWorkDTO { BranchID = data.BranchID })?.FirstOrDefault();
                BranchRegionWorkDto = new BranchRegionWorkDTO
                {
                    ID = BranchRegionWorkDto.ID,
                    BranchID = data?.BranchID > 0 ? data?.BranchID : BranchRegionWorkDto?.BranchID,
                    ZoneID = data?.ZoneID,
                    CreateDate = BranchRegionWorkDto.CreateDate,
                    IsDeleted = BranchRegionWorkDto?.IsDeleted
                };
            }
            BranchRegionWork branchRegionWork = Mapper.Map(BranchRegionWorkDto);
            UseContext(databsse =>
            {
                databsse.Entry(branchRegionWork).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return BranchRegionWorkDto;
        }

        public BranchRegionWorkDTO BranchRegionWorkDelete(BranchRegionWorkDTO data)
        {
            BranchRegionWorkDTO branchRegionWorkDto = new BranchRegionWorkDTO();
            if (data.ID > 0)
            {
                branchRegionWorkDto = BranchRegionWorkGet(new BranchRegionWorkDTO { ID = data.ID })?.SingleOrDefault();
            }
            branchRegionWorkDto.IsDeleted = true;

            return BranchRegionWorkUpdate(branchRegionWorkDto);
        }


        public Gostar.Common.ResponseStatus BranchRegionWorkDeleteByBranchID(long? _BranchID)
        {
            Gostar.Common.ResponseStatus r = Common.ResponseStatus.DatabaseError;
            UseContext(c =>
            {
                var BranchRegionWorkList = c.BranchRegionWork.Where(s =>s.BranchID==_BranchID)?.ToList();
                c.BranchRegionWork.RemoveRange(BranchRegionWorkList);
                c.SaveChanges();
                r= Gostar.Common.ResponseStatus.Successful;
            });
            return r;
        }

    }
}
