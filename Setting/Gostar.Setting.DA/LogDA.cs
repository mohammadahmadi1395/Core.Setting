using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gostar.Setting.DTO;
using Gostar.Setting.DA.Entities;

namespace Gostar.Setting.DA
{
    public class LogDA : DataAccess
    {
        public List<Gostar.Common.LogDTO> LogGet(Gostar.Common.LogFilterDTO data)
        {
            List<Gostar.Common.LogDTO> LP = null;
            UseContext(c =>
            {
                var query = c.Log.Where(p => true);
                if (data != null)
                {
                    if (data?.UserIDS?.Count > 0)
                        query = query.Where(p => data.UserIDS.Contains(p.UserID));

                    if (data?.EntityIDs?.Count > 0)
                        query = query.Where(p => data.EntityIDs.Contains(p.EntityID));

                    if (data?.ActionTypeIDs?.Count > 0)
                        query = query.Where(p => data.ActionTypeIDs.Contains(p.ActionTypeID));

                    if (data.FromDate > DateTime.MinValue)
                    {
                        var fsd = data.FromDate;//.Date;
                        query = query.Where(p => p.CreateDate >= fsd);
                    }

                    if (data.ToDate > DateTime.MinValue)
                    {
                        var tsd = ((DateTime)data.ToDate).Date.AddDays(1).AddTicks(-1);
                        query = query.Where(p => p.CreateDate <= tsd);
                    }

                }
                LP = query.ToList().Select(s => Mapper.Map(s))?.ToList()?.OrderByDescending(p => p.CreateDate)?.ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return LP;
        }
        public Gostar.Common.LogDTO LogSet(Gostar.Common.LogDTO data)
        {
            Log Log = null;
            Log = Mapper.Map(data);
            UseContext(c =>
            {
                c.Log.Add(Log);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            data.ID = Log.ID;
            return data;
        }
    }
}
