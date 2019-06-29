using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.DA;
using Alsahab.Setting.Entities.Models;

namespace Alsahab.Setting.BL
{
    public class OrganizationalChartBL : BaseBL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO>
    {
        private List<DTO.OrganizationalChartDTO> TempAllOrganizationalChart = new List<DTO.OrganizationalChartDTO>();
        private long? _index = 1, _depth = 2;
        OrganizationalChartDA OrganizationalChartDA = new OrganizationalChartDA();
        /// <summary>
        /// Check Data For Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool Validate(DTO.OrganizationalChartDTO data)
        {
            if (string.IsNullOrWhiteSpace(data.Title))
            {
                ErrorMessage += "OrganizationalChart Title Not Entered\n";
                return false;
            }
           
            if (data.IsDeleted == true)
            {
                ErrorMessage += "OrganizationalChart Not yet Save in Database\n";
                return false;
            }
            var OrganizationalChartList = OrganizationalChartGet(new DTO.OrganizationalChartDTO { Title = data.Title }, null)?.ToList();
            var CheckOrganizationalChart = OrganizationalChartList.Where(s => s.Title == data?.Title).Count();
            if (CheckOrganizationalChart > 0)
            {
                ErrorMessage += "This OrganizationalChart Is Exist\n";
                return false;
            }

            return true;
        }
        /// <summary>
        /// Get List of OrganizationalChart 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<DTO.OrganizationalChartDTO> OrganizationalChartGet(DTO.OrganizationalChartDTO data, OrganizationalChartFilterDTO filter = null)
        {
            var Response = OrganizationalChartDA.OrganizationalChartGet(data, filter);

            ResponseStatus = OrganizationalChartDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += OrganizationalChartDA.ErrorMessage;
                return null;
            }
            return Response;
        }
        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool DeletePermission(DTO.OrganizationalChartDTO data)
        {
           return true;
        }
        /// <summary>
        /// Insert OrganizationalChart in Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DTO.OrganizationalChartDTO Insert(DTO.OrganizationalChartDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                return null;
            }

            data.CreateDate = DateTime.Now;
            var Response = OrganizationalChartDA.OrganizationalChartInsert(data);

            if (Response?.ID > 0)
            {
                var resp = OrganizationalChartGet(new DTO.OrganizationalChartDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
                //Observers.ObserverStates.AddOrganizationalChart state = new Observers.ObserverStates.AddOrganizationalChart
                //{
                //    OrganizationalChart = resp ?? Response,
                //    User = User,
                //};
                //Notify(state);
                if (resp != null)
                    Response = resp;
            }

            ResponseStatus = OrganizationalChartDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += OrganizationalChartDA.ErrorMessage;
                return null;
            }

            return Response;
        }
        /// <summary>
        /// Insert List of OrganizationalChart In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public List<DTO.OrganizationalChartDTO> OrganizationalChartInsert(List<DTO.OrganizationalChartDTO> data)
        {
            foreach (var d in data)
            {
                if (!Validate(d))
                {
                    ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                    return null;
                }
                d.CreateDate = DateTime.Now;

            }
            var Response = OrganizationalChartDA.OrganizationalChartInsert(data);

            List<DTO.OrganizationalChartDTO> respList = new List<DTO.OrganizationalChartDTO>();
            foreach (var val in Response)
            {
                var resp = OrganizationalChartGet(new DTO.OrganizationalChartDTO { ID = val?.ID ?? 0 })?.FirstOrDefault();
                //Observers.ObserverStates.AddOrganizationalChart state = new Observers.ObserverStates.AddOrganizationalChart
                //{
                //    OrganizationalChart = resp ?? val,
                //    User = User,
                //};
                //Notify(state);
                respList.Add(resp);
            }

            ResponseStatus = OrganizationalChartDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += OrganizationalChartDA.ErrorMessage;
                return null;
            }

            return respList ?? Response;
        }
        /// <summary>
        /// Update OrganizationalChart
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        #region Upodate
        //public DTO.OrganizationalChartDTO OrganizationalChartUpdate(DTO.OrganizationalChartDTO data)
        //{
        //    if (!(data.ID > 0))
        //    {
        //        ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
        //        ErrorMessage = "Entered OrganizationalChart is Mistake";
        //        return null;
        //    }
        //    var Response = OrganizationalChartDA.OrganizationalChartUpdate(data);

        //    var resp = OrganizationalChartGet(new DTO.OrganizationalChartDTO { ID = Response?.ID ?? 0 })?.FirstOrDefault();
        //    Observers.ObserverStates.EditOrganizationalChart state = new Observers.ObserverStates.EditOrganizationalChart
        //    {
        //        OrganizationalChart = resp ?? Response,
        //        User = User,
        //    };
        //    Notify(state);

        //    ResponseStatus = OrganizationalChartDA.ResponseStatus;
        //    if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //    {
        //        ErrorMessage += OrganizationalChartDA.ErrorMessage;
        //        return null;
        //    }

        //    return resp ?? Response;
        //}
        #endregion
        /// <summary>
        /// Delete Logicly
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DTO.OrganizationalChartDTO OrganizationalChartDelete(DTO.OrganizationalChartDTO data)
        {
            // Search For Use This Item Before Delete
            if (!DeletePermission(data))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                return null;
            }
            data.IsDeleted = true;
            var Response = OrganizationalChartDA.OrganizationalChartUpdate(data);
            ResponseStatus = OrganizationalChartDA.ResponseStatus;
            var resp = OrganizationalChartGet(new DTO.OrganizationalChartDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            //Observers.ObserverStates.DeleteOrganizationalChart state = new Observers.ObserverStates.DeleteOrganizationalChart
            //{
            //    OrganizationalChart = resp ?? Response,
            //    User = User,
            //};
            //Notify(state);

            return resp ?? Response;
        }
        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DTO.OrganizationalChartDTO OrganizationalChartDeleteComplete(DTO.OrganizationalChartDTO data)
        {

            if (!DeletePermission(data))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                return null;
            }
            var Response = OrganizationalChartDA.OrganizationalChartDelete(data);

            var resp = OrganizationalChartGet(new DTO.OrganizationalChartDTO { ID = Response?.ID ?? 0, IsDeleted = true })?.FirstOrDefault();
            //Observers.ObserverStates.DeleteOrganizationalChart state = new Observers.ObserverStates.DeleteOrganizationalChart
            //{
            //    OrganizationalChart = resp ?? Response,
            //    User = User,
            //};
            //Notify(state);

            ResponseStatus = OrganizationalChartDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += OrganizationalChartDA.ErrorMessage;
                return null;
            }

            return resp ?? Response;
        }


        /////////////////////////////////////////////////////////////////////Allmi


        public DTO.OrganizationalChartDTO OrganizationalChartInsert(DTO.OrganizationalChartDTO data)
        {
            if (!Validate(data))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                return null;
            }

            UpdateAllOrganizationalChart();
            var response = data;
            var AllOrganizationalCharts = AllDeth;

            DTO.OrganizationalChartDTO tempOrganizationalChart = new DTO.OrganizationalChartDTO
            {
                ID = 0,
                Depth = 1,
                ParentID = -1,
                LeftIndex = 1,
                RightIndex = (AllOrganizationalCharts.Count + 1) * 2
            };

            if (response.ParentID == null)
                response.ParentID = 0;
            AllOrganizationalCharts.Add(tempOrganizationalChart);
            foreach (var item in AllOrganizationalCharts)
            {
                if (item.ParentID == null)
                    item.ParentID = 0;
            }

            var childs = AllOrganizationalCharts.Where(c => c.ParentID == response.ParentID).Count();
            if (childs > 0)
            {
                long? right = 0;
                if (AllOrganizationalCharts.Count > 0)
                    right = AllOrganizationalCharts.SingleOrDefault(z => z.ID == response.ParentID)?.RightIndex;
                foreach (var zItem in AllDeth)
                {
                    if (zItem.RightIndex >= right) zItem.RightIndex += 2;
                    if (zItem.LeftIndex > right) zItem.LeftIndex += 2;
                }

                foreach (var death in AllOrganizationalCharts)
                {
                    if (death.ParentID == 0)
                        death.ParentID = null;
                }

                AllOrganizationalCharts.Remove(tempOrganizationalChart);
                OrganizationalChartDA.OrganizationalChartUpdate(AllOrganizationalCharts);
                response.LeftIndex = right;
                response.RightIndex = right + 1;
            }
            else
            {
                long? left = 0;
                if (AllOrganizationalCharts.Count > 0)
                    left = AllOrganizationalCharts?.SingleOrDefault(z => z.ID == response.ParentID)?.LeftIndex;
                foreach (var zItem in AllDeth)
                {
                    if (zItem.RightIndex > left) zItem.RightIndex += 2;
                    if (zItem.LeftIndex > left) zItem.LeftIndex += 2;
                }

                foreach (var OrganizationalChart in AllOrganizationalCharts)
                {
                    if (OrganizationalChart.ParentID == 0)
                        OrganizationalChart.ParentID = null;
                }
                AllOrganizationalCharts.Remove(tempOrganizationalChart);
                OrganizationalChartDA.OrganizationalChartUpdate(AllOrganizationalCharts);
                response.LeftIndex = left + 1;
                response.RightIndex = left + 2;
            }

            long? parentDepth = 1;
            if (response.ParentID == 0)
                response.ParentID = null;
            else
                parentDepth = AllOrganizationalCharts.SingleOrDefault(d => d.ID == response.ParentID).Depth;
            response.Depth = parentDepth + 1;

            return Insert(response);
        }
        private List<DTO.OrganizationalChartDTO> _deth = new List<DTO.OrganizationalChartDTO>();
        private List<DTO.OrganizationalChartDTO> AllDeth
        {
            get
            {
                if (!(_deth.Count > 0))
                    _deth = new OrganizationalChartDA().AllOrganizationalChartGet();
                return _deth;
            }
        }
        //private DTO.OrganizationalChartDTO DeleteOrganizationalCharts(DTO.OrganizationalChartDTO deleteDTO)
        //{
        //    var deletingItem = OrganizationalChartDA.OrganizationalChartGet(new DTO.OrganizationalChartDTO { ID = deleteDTO.ID }, null)?.SingleOrDefault();
        //    var myLeft = deletingItem.LeftIndex; var myRight = deletingItem.RightIndex; //var myWidth = myRight - myLeft + 1;
        //    var AllOrganizationalCharts = AllDeth;
        //    foreach (var item in AllOrganizationalCharts)
        //    {
        //        if (item.LeftIndex >= myLeft && item.LeftIndex <= myRight)
        //            item.IsDeleted = true;
        //    }
        //    OrganizationalChartDA.OrganizationalChartUpdate(AllOrganizationalCharts);
        //    ResponseStatus = OrganizationalChartDA.ResponseStatus;
        //    if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //    {
        //        ErrorMessage += OrganizationalChartDA.ErrorMessage;
        //    }
        //    return deletingItem;
        //}
        private DTO.OrganizationalChartDTO GetChild(DTO.OrganizationalChartDTO OrganizationalChart)
        {
            return TempAllOrganizationalChart.FirstOrDefault(i => i.ParentID == OrganizationalChart.ID && !(i.LeftIndex > 0));
        }
        private DTO.OrganizationalChartDTO GetBrother(DTO.OrganizationalChartDTO OrganizationalChart)
        {
            if (!(OrganizationalChart.ParentID > 0))
                return null;
            var parent = TempAllOrganizationalChart.FirstOrDefault(i => i.ID == OrganizationalChart.ParentID);
            var brother = GetChild(parent);
            return brother?.ID > 0 ? brother : null;
        }
        public DTO.OrganizationalChartDTO OrganizationalChartUpdate(DTO.OrganizationalChartDTO data)
        {
            var Response = data;
            if (data.ParentID == 0)
                data.ParentID = null;
            if (!(data.ID > 0))
            {
                ResponseStatus = Alsahab.Common.ResponseStatus.BusinessError;
                ErrorMessage = "Entered OrganizationalChart is Mistake";
                return null;
            }
            //DTO.OrganizationalChartDTO oldOrganizationalChart = new DTO.OrganizationalChartDTO();
            //var oldOrganizationalChart = OrganizationalChartGet(new DTO.OrganizationalChartDTO { ID = data.ID }, null)?.FirstOrDefault();
            var oldOrganizationalChart = OrganizationalChartGet(new DTO.OrganizationalChartDTO { ID = Response?.ID ?? 0 }, null)?.FirstOrDefault();
            Response = OrganizationalChartDA.OrganizationalChartUpdate(data);

            //Observers.ObserverStates.EditOrganizationalChart state = new Observers.ObserverStates.EditOrganizationalChart
            //{
            //    OrganizationalChart = oldOrganizationalChart ?? Response,
            //    User = User,
            //};
            //Notify(state);

            if (data.ParentID != oldOrganizationalChart.ParentID)
            {
                UpdateAllOrganizationalChart();

               // UpdateCode(oldOrganizationalChart);
            }

            ResponseStatus = OrganizationalChartDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += OrganizationalChartDA.ErrorMessage;
                return null;
            }
            return oldOrganizationalChart ?? Response;
        }
        public List<DTO.OrganizationalChartDTO> UpdateAllOrganizationalChart()//DTO.OrganizationalChartDTO OrganizationalChartData)
        {
            var allZons = OrganizationalChartGet();
            foreach (var OrganizationalChart in allZons)
            {
                OrganizationalChart.LeftIndex = null;
                OrganizationalChart.RightIndex = null;
                OrganizationalChart.Depth = null;
                TempAllOrganizationalChart.Add(OrganizationalChart);
            }

            List<DTO.OrganizationalChartDTO> rootList = TempAllOrganizationalChart.Where(i => !(i.ParentID > 0))?.ToList();
            foreach (var root in rootList)
            {
                if (root?.ID > 0)
                {
                    _depth = 2;
                    RecursiveUpdateAllOrganizationalChart(root);
                }
            }

            TempAllOrganizationalChart = GenerateNewCodes(TempAllOrganizationalChart?.Where(s => s.ParentID == null && s.IsDeleted == false)?.ToList(), TempAllOrganizationalChart?.Where(s => s.IsDeleted == false)?.ToList());


            OrganizationalChartDA.OrganizationalChartUpdate(TempAllOrganizationalChart);
            ResponseStatus = OrganizationalChartDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += OrganizationalChartDA.ErrorMessage;
                return null;
            }
            return OrganizationalChartDA.AllOrganizationalChartGet();
            //            return result;
        }
        private void RecursiveUpdateAllOrganizationalChart(DTO.OrganizationalChartDTO OrganizationalChartData)
        {
            if (!(OrganizationalChartData?.ID > 0) || !(TempAllOrganizationalChart?.Count > 0))
                return;

            TempAllOrganizationalChart.FirstOrDefault(i => i.ID == OrganizationalChartData.ID).LeftIndex = ++_index;
            TempAllOrganizationalChart.FirstOrDefault(i => i.ID == OrganizationalChartData.ID).Depth = _depth;
            //   var aa = TempAllOrganizationalChart.FirstOrDefault(i => i.ID == OrganizationalChartData.ID);


            var tempChild = GetChild(OrganizationalChartData);
            if (tempChild?.ID > 0)
            {
                _depth++;
                RecursiveUpdateAllOrganizationalChart(tempChild);
            }

            TempAllOrganizationalChart.FirstOrDefault(i => i.ID == OrganizationalChartData.ID).RightIndex = ++_index;
            var tempBrother = GetBrother(OrganizationalChartData);

            if (tempBrother?.ID > 0)
                RecursiveUpdateAllOrganizationalChart(tempBrother);
            else
                _depth--;
        }
        public List<DTO.OrganizationalChartDTO> OrganizationalChartGet()
        {
            var response = OrganizationalChartDA.AllOrganizationalChartGet();
            ResponseStatus = OrganizationalChartDA.ResponseStatus;
            if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
            {
                ErrorMessage += OrganizationalChartDA.ErrorMessage;
                return null;
            }
            return response;
        }


        private String GenerateCode(OrganizationalChartDTO data)
        {
            var list = OrganizationalChartGet(new OrganizationalChartDTO(), null);
            if (data?.ParentID == null)
            {
                return (list?.Where(s => s.ParentID == null)?.ToList()?.Count + 1).ToString();
            }
            else
            {
                var r = list?.Where(s => s.ParentID == data?.ParentID)?.ToList()?.Count;
                return String.Format("{0}-{1}", list?.Where(s => s.ID == data?.ParentID)?.FirstOrDefault()?.Code, (r + 1).ToString());
            }
        }
        private List<OrganizationalChartDTO> GenerateNewCodes(List<OrganizationalChartDTO> data, List<OrganizationalChartDTO> All)
        {
            List<OrganizationalChartDTO> res = new List<OrganizationalChartDTO>();
            for (int thisOrganizationalChart = 0; thisOrganizationalChart < data?.Count; thisOrganizationalChart++)
            {
                var parent = GetParent(data[thisOrganizationalChart], All);
                if (parent == null) // root
                {
                    data[thisOrganizationalChart].Code = string.Format("{0}", (thisOrganizationalChart + 1));
                    res.Add(data[thisOrganizationalChart]);
                }
                else
                {
                    data[thisOrganizationalChart].Code = string.Format("{0}-{1}", parent?.Code, (thisOrganizationalChart + 1));
                    res.Add(data[thisOrganizationalChart]);
                }
                var childs = All?.Where(s => s.ParentID == data[thisOrganizationalChart]?.ID)?.ToList();

                for (int child = 0; child < childs?.Count; child++)
                {
                    childs[child].Code = string.Format("{0}-{1}", data[thisOrganizationalChart].Code, (child + 1));
                    res.Add(childs[child]);
                    res.AddRange(GenerateNewCodes(All?.Where(s => s.ParentID == childs[child]?.ID)?.ToList(), All));
                }
            }
            return res;
        }
        private OrganizationalChartDTO GetParent(OrganizationalChartDTO data, List<OrganizationalChartDTO> all)
        {
            return all?.Where(s => s.ID == data?.ParentID)?.ToList()?.FirstOrDefault();
        }

    }
}
