using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class OrganizationalChartDA : DataAccess
    {
        public List<OrganizationalChartDTO> OrganizationalChartGet(OrganizationalChartDTO data = null, OrganizationalChartFilterDTO filter = null)
        {
            var result = new List<OrganizationalChartDTO>();
            UseContext(database =>
            {
                var query = database.OrganizationalChart.Where(t => true);
                #region Filter
                if (filter != null)
                {
                    if (filter?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate >= filter.FromCreateDate);
                    if (filter?.ToCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate <= (filter.ToCreateDate == filter.ToCreateDate.Value.Date ? filter.ToCreateDate.Value.AddDays(1).AddTicks(-1) : filter.ToCreateDate));
                    if (filter?.IDList?.Count > 0)
                        query = query.Where(s => filter.IDList.Contains(s.ID));
                }
                #endregion
                #region Data
                if (data != null)
                {
                    if (data.ID > 0)
                        query = query.Where(s => s.ID == data.ID);
                    if (!string.IsNullOrWhiteSpace(data.Title))
                        query = query.Where(s => s.Title.Contains(data.Title));
                    if (!string.IsNullOrWhiteSpace(data.Code))
                        query = query.Where(s => s.Code.Contains(data.Code));
                    if (!string.IsNullOrWhiteSpace(data.OldCode))
                        query = query.Where(s => s.OldCode.Contains(data.OldCode));
                    if (data.ParentID > 0)
                        query = query.Where(s => s.ParentID == data.ParentID);
                    if (!string.IsNullOrWhiteSpace(data.CreateDate.ToString()))
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));
                    if (data.IsDeleted.HasValue)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);
                }
                else
                    query = query.Where(s => s.IsDeleted == false);
                #endregion

                result = query?.ToList().Select(s => Mapper.Map(s))?.OrderBy(s => s.Code)?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return result;
        }
        public OrganizationalChartDTO OrganizationalChartInsert(OrganizationalChartDTO data)
        {
            OrganizationalChart OrganizationalChart = null;
            //string lastcode = GnrateCode(data);
            //if (string.IsNullOrWhiteSpace(lastcode))
            //{
            //    ResponseStatus = Gostar.Common.ResponseStatus.DatabaseError;
            //    ErrorMessage += "Internal Error\n";
            //    return null;
            //}
            //data.Code = lastcode;
            OrganizationalChart = Mapper.Map(data);

            UseContext(c =>
            {
                c.OrganizationalChart.Add(OrganizationalChart);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            data.ID = OrganizationalChart.ID;
            return data;
        }
        public List<OrganizationalChartDTO> OrganizationalChartInsert(List<OrganizationalChartDTO> data)
        {

            List<OrganizationalChart> OrganizationalChartlist = null;
            OrganizationalChartlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.OrganizationalChart.AddRange(OrganizationalChartlist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;

        }
        public OrganizationalChartDTO OrganizationalChartUpdate(OrganizationalChartDTO data)
        {
            OrganizationalChartDTO OrganizationalChartDto = new OrganizationalChartDTO();
            if (data.ID > 0)
            {
                OrganizationalChartDto = OrganizationalChartGet(new OrganizationalChartDTO { ID = data.ID }, null)?.FirstOrDefault();
                OrganizationalChartDto = new OrganizationalChartDTO
                {
                    ID = data.ID,
                    Title = !string.IsNullOrWhiteSpace(data?.Title) ? data?.Title : OrganizationalChartDto.Title,
                    CreateDate = !string.IsNullOrWhiteSpace(data?.CreateDate.ToString()) ? data?.CreateDate : OrganizationalChartDto.CreateDate,
                    IsDeleted = data.IsDeleted,
                    ParentID = data.ParentID,
                    Code = OrganizationalChartDto.Code,
                    LeftIndex = OrganizationalChartDto.LeftIndex,
                    RightIndex = OrganizationalChartDto.RightIndex,
                    Depth = OrganizationalChartDto.Depth,
                    OldCode = data.OldCode,
                };
            }
            OrganizationalChart OrganizationalChart = Mapper.Map(OrganizationalChartDto);
            UseContext(databsse =>
            {
                databsse.Entry(OrganizationalChart).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return OrganizationalChartDto;
        }
        public OrganizationalChartDTO OrganizationalChartDelete(OrganizationalChartDTO data)
        {
            OrganizationalChartDTO OrganizationalChartDto = new OrganizationalChartDTO();
            if (data.ID > 0)
            {
                OrganizationalChartDto = OrganizationalChartGet(new OrganizationalChartDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            OrganizationalChart OrganizationalChart = Mapper.Map(OrganizationalChartDto);
            UseContext(databsse =>
            {
                databsse.Entry(OrganizationalChart).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return OrganizationalChartDto;
        }
        public List<OrganizationalChartDTO> OrganizationalChartUpdate(List<OrganizationalChartDTO> data)
        {
            List<OrganizationalChart> OrganizationalChartlist = null;
            OrganizationalChartlist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                foreach (var item in OrganizationalChartlist)
                    c.Entry(item).State = System.Data.Entity.EntityState.Modified;
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return data;
        }
        public List<OrganizationalChartDTO> AllOrganizationalChartGet()
        {
            var result = new List<OrganizationalChartDTO>();
            UseContext(database =>
            {
                var query = database.OrganizationalChart;
                result = query?.ToList().Select(s => Mapper.Map(s))?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return result;
        }
        // Gnrate Code Llike 1-2, 1-22-3, 4
  
    }
}
