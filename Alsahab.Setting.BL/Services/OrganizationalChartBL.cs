using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alsahab.Setting.DTO;
using Alsahab.Setting.Data;
using Alsahab.Setting.Entities.Models;
using Alsahab.Setting.Data.Interfaces;
using Alsahab.Setting.BL.Validation;
using System.Threading;
using Alsahab.Common.Exceptions;
using Alsahab.Common;

namespace Alsahab.Setting.BL
{
    public class OrganizationalChartBL : BaseBL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO>
    {
        #region properties
        private IList<OrganizationalChartDTO> _AllOrgChart;
        private IList<OrganizationalChartDTO> AllOrgChart
        {
            get
            {
                if (!(_AllOrgChart?.Count > 0))
                    _AllOrgChart = _OrganizationalChartDL.GetAll();
                return _AllOrgChart;
            }
        }
        private List<OrganizationalChartDTO> TreeNodes = new List<OrganizationalChartDTO>();
        private long? _index = 0, _depth = 2;
        #endregion properties

        #region dependency injection
        private readonly IBaseDL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO> _OrganizationalChartDL;
        public OrganizationalChartBL(IBaseDL<OrganizationalChart, OrganizationalChartDTO, OrganizationalChartFilterDTO> organizationalChartDL,
                                IBaseDL<Entities.Models.Log, LogDTO, LogFilterDTO> logDL)
            : base(organizationalChartDL, logDL)
        {
            _OrganizationalChartDL = organizationalChartDL;
        }
        #endregion dependency injection

        private List<DTO.OrganizationalChartDTO> TempAllOrganizationalChart = new List<DTO.OrganizationalChartDTO>();


        /// <summary>
        /// Get List of OrganizationalChart 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<IList<DTO.OrganizationalChartDTO>> GetAsync(DTO.OrganizationalChartFilterDTO filter, CancellationToken cancellationToken, PagingInfoDTO paging = null)
        {
            var result = await _OrganizationalChartDL.GetAsync(filter, cancellationToken, paging);
            ResultCount = _OrganizationalChartDL.ResultCount;
            return result;
        }

        /// <summary>
        /// Check Data For Delete
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool CheckDeletePermission(DTO.OrganizationalChartDTO data)
        {
            //TODO: باید بررسی شود
            return true;
        }
        /// <summary>
        /// Insert OrganizationalChart in Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<DTO.OrganizationalChartDTO> InsertAsync(DTO.OrganizationalChartDTO data, CancellationToken cancellationToken)
        {
            Validate(data);

            data.CreateDate = DateTime.Now;
            var response = await _OrganizationalChartDL.InsertAsync(data, cancellationToken);

            UpdateTreeIndicesAndCodes();

            response = await _OrganizationalChartDL.GetByIdAsync(cancellationToken, response?.ID ?? 0);
            //TODO:
            //Observers.ObserverStates.AddOrganizationalChart state = new Observers.ObserverStates.AddOrganizationalChart
            //{
            //    OrganizationalChart = resp ?? Response,
            //    User = User,
            //};
            //Notify(state);

            return response;
        }

        /// <summary>
        /// Insert List of OrganizationalChart In Database
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<IList<DTO.OrganizationalChartDTO>> InsertListAsync(IList<DTO.OrganizationalChartDTO> data, CancellationToken cancellationToken)
        {
            foreach (var d in data)
            {
                Validate(d);
                d.CreateDate = DateTime.Now;
            }

            var response = await _OrganizationalChartDL.InsertListAsync(data, cancellationToken);

            UpdateTreeIndicesAndCodes();

            List<DTO.OrganizationalChartDTO> respList = new List<DTO.OrganizationalChartDTO>();
            foreach (var val in response)
            {
                var resp = await _OrganizationalChartDL.GetByIdAsync(cancellationToken, val?.ID ?? 0);
                //TODO:
                //Observers.ObserverStates.AddOrganizationalChart state = new Observers.ObserverStates.AddOrganizationalChart
                //{
                //    OrganizationalChart = resp ?? val,
                //    User = User,
                //};
                //Notify(state);
                respList.Add(resp);
            }

            return respList ?? response;
        }
        /// <summary>
        /// Update OrganizationalChart
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>

        #region Update
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
        /// Delete Logically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<DTO.OrganizationalChartDTO> SoftDeleteAsync(DTO.OrganizationalChartDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermission(data);

            data.IsDeleted = true;
            var response = await _OrganizationalChartDL.UpdateAsync(data, cancellationToken);

            UpdateTreeIndicesAndCodes();

            //TODO:
            //Observers.ObserverStates.DeleteOrganizationalChart state = new Observers.ObserverStates.DeleteOrganizationalChart
            //{
            //    OrganizationalChart = resp ?? Response,
            //    User = User,
            //};
            //Notify(state);

            return response;
        }

        /// <summary>
        /// Delete physically
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async override Task<DTO.OrganizationalChartDTO> DeleteAsync(DTO.OrganizationalChartDTO data, CancellationToken cancellationToken)
        {
            CheckDeletePermission(data);

            var response = await _OrganizationalChartDL.DeleteAsync(data, cancellationToken);

            //TODO:
            //Observers.ObserverStates.DeleteOrganizationalChart state = new Observers.ObserverStates.DeleteOrganizationalChart
            //{
            //    OrganizationalChart = resp ?? Response,
            //    User = User,
            //};
            //Notify(state);

            return response;
        }

        #region Validation
        /// <summary>
        /// Check Data For Insert
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private bool Validate(DTO.OrganizationalChartDTO data)
        {
            return Validate<BLOrganizationalChartValidator, OrganizationalChartDTO>(data);
        }

        private async Task<bool> CheckDeletePermision(BranchDTO data, CancellationToken cancellationToken)
        {
            if (!(data?.ID > 0))
                throw new AppException(ResponseStatus.BadRequest, "node Entered Is Mistake.");
            //TODO:
            //وابستگی‌های بیشتر در جداول دیگر مشخص شوند
            var deletingItem = await _OrganizationalChartDL.GetByIdAsync(cancellationToken, data.ID);
            var myLeft = deletingItem.LeftIndex; 
            var myRight = deletingItem.RightIndex;
            var deleteCount = AllOrgChart.Where(i => i.LeftIndex >= myLeft && i.LeftIndex <= myRight && i.IsDeleted == false).Count();
            if (deleteCount > 1)
                throw new AppException(ResponseStatus.LoginError, "You can't delete this node. this node has child");

            return true;
        }
        #endregion Validation

        #region related to tree
        private IList<OrganizationalChartDTO> UpdateTreeIndicesAndCodes()
        {
            _AllOrgChart = null;
            var organCharts = AllOrgChart;
            foreach (var node in organCharts)
            {
                node.LeftIndex = null;
                node.RightIndex = null;
                node.Depth = null;
                node.Code = null;
                TreeNodes.Add(node);
            }

            List<OrganizationalChartDTO> rootList = TreeNodes.Where(i => !(i.ParentID > 0))?.ToList();
            foreach (var root in rootList)
                if (root?.ID > 0)
                {
                    _depth = 2;
                    RecursiveUpdateAllOrgChartIndices(root);
                }

            var codedOrganizationalChart = GenerateNewCodeList(rootList);

            var result = _OrganizationalChartDL.UpdateList(codedOrganizationalChart);

            return result;
        }
        // private async Task<IList<BranchDTO>> UpdateTreeIndicesAndCodesAsync(CancellationToken cancellationToken)
        // {
        //     var branches  = AllBranch;
        //     foreach (var node in branches)
        //     {
        //         node.LeftIndex = null;
        //         node.RightIndex = null;
        //         node.Depth = null;
        //         node.Code = null;
        //         TreeNodes.Add(node);
        //     }

        //     List<BranchDTO> rootList = TreeNodes.Where(i => !(i.ParentID > 0))?.ToList();
        //     foreach (var root in rootList)
        //         if (root?.ID > 0)
        //         {
        //             _depth = 2;
        //             RecursiveUpdateAllOrgChartIndices(root);
        //         }

        //     GenerateNewCodeList(rootList);

        //     var result = await _BranchDL.UpdateListAsync(TreeNodes, cancellationToken);

        //     ResponseStatus = _BranchDL.ResponseStatus;
        //     if (ResponseStatus != Alsahab.Common.ResponseStatus.Successful)
        //         throw new AppException(ResponseStatus.DatabaseError, _BranchDL.ErrorMessage);

        //     return result;
        // }

        // مراحل:
        // ۱- ابتدا اندیس چپ را تنظیم می‌کند
        // ۲- سپس عمق را تنظیم می‌کند
        // ۳- اندیس چپ و عمق را برای فرزندش در صورت وجود تنظیم می‌کند
        // ۴- در صورت عدم وجود فرزند، اندیس راست را تنظیم می‌کند
        // ۵- به سراغ برادر (در صورت وجود) می‌رود و مراحل اول تا چهارم را برای آن انجام می‌دهد
        private void RecursiveUpdateAllOrgChartIndices(OrganizationalChartDTO dto)
        {
            if (!(dto?.ID > 0) || !(TreeNodes?.Count > 0))
                return;

            TreeNodes.FirstOrDefault(i => i.ID == dto.ID).LeftIndex = ++_index;
            TreeNodes.FirstOrDefault(i => i.ID == dto.ID).Depth = _depth;

            var tempChild = GetNotIndexedChild(dto);
            if (tempChild?.ID > 0)
            {
                _depth++;
                RecursiveUpdateAllOrgChartIndices(tempChild);
            }

            TreeNodes.FirstOrDefault(i => i.ID == dto.ID).RightIndex = ++_index;
            var tempBrother = GetNotIndexedBrother(dto);

            if (tempBrother?.ID > 0)
                RecursiveUpdateAllOrgChartIndices(tempBrother);
            else
                _depth--;
        }

        private List<OrganizationalChartDTO> GenerateNewCodeList(List<OrganizationalChartDTO> data)
        {
            List<OrganizationalChartDTO> res = new List<OrganizationalChartDTO>();
            for (int thisOrgChart = 0; thisOrgChart < data?.Count; thisOrgChart++)
            {
                var parent = GetParent(data[thisOrgChart]);
                data[thisOrgChart].Code = (parent == null) ?
                    string.Format("{0}", (thisOrgChart + 1)) :
                    string.Format("{0}-{1}", parent?.Code, (thisOrgChart + 1));

                res.Add(data[thisOrgChart]);

                var childs = AllOrgChart?.Where(s => s.ParentID == data[thisOrgChart]?.ID)?.ToList();

                for (int child = 0; child < childs?.Count; child++)
                {
                    childs[child].Code = string.Format("{0}-{1}", data[thisOrgChart].Code, (child + 1));
                    res.Add(childs[child]);
                    res.AddRange(GenerateNewCodeList(AllOrgChart?.Where(s => s.ParentID == childs[child]?.ID)?.ToList()));
                }
            }
            return res;
        }
        private OrganizationalChartDTO GetNotIndexedBrother(OrganizationalChartDTO node)
        {
            if (!(node.ParentID > 0))
                return null;
            var parent = TreeNodes.FirstOrDefault(i => i.ID == node.ParentID);
            var brother = GetNotIndexedChild(parent);
            return brother?.ID > 0 ? brother : null;
        }
        private OrganizationalChartDTO GetNotIndexedChild(OrganizationalChartDTO node)
        {
            return TreeNodes.FirstOrDefault(i => i.ParentID == node.ID && !(i.LeftIndex > 0));
        }
        private OrganizationalChartDTO GetParent(OrganizationalChartDTO data)
        {
            return AllOrgChart?.FirstOrDefault(s => s.ID == data?.ParentID);
        }
        #endregion related to tree


    }

}
