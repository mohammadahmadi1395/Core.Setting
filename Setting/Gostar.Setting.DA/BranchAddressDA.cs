using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gostar.Setting.DA.Entities;
using Gostar.Setting.DTO;
namespace Gostar.Setting.DA
{
    public class BranchAddressDA : DataAccess
    {
        public List<BranchAddressDTO> BranchAddressGet(BranchAddressDTO data, BranchAddressFilterDTO filter)
        {
            var result = new List<BranchAddressDTO>();
            UseContext(database =>
            {
                var query = database.BranchAddress.Where(t => true);
                #region Filter
                if (filter != null)
                {
                    if (filter?.FromStartDate > DateTime.MinValue)
                        query = query.Where(s => s.StartDate >= filter.FromStartDate);
                    if (filter?.ToStartDate > DateTime.MinValue)
                        query = query.Where(s => s.StartDate <= filter.ToStartDate);
                    if (filter?.FromEndDate > DateTime.MinValue)
                        query = query.Where(s => s.EndDate >= filter.FromEndDate);
                    if (filter?.ToEndDate > DateTime.MinValue)
                        query = query.Where(s => s.EndDate <= filter.ToEndDate);
                    if (filter?.FromCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate >= filter.FromCreateDate);
                    if (filter?.ToCreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate <= filter.ToCreateDate);
                }
                #endregion
                #region Data
                if (data != null)
                {
                    if (data?.ID > 0)
                        query = query.Where(s => s.ID == data.ID);
                    if (data?.StartDate > DateTime.MinValue)
                        query = query.Where(s => s.StartDate.ToString().Contains(data.StartDate.ToString()));
                    if (data?.EndDate > DateTime.MinValue)
                        query = query.Where(s => s.EndDate.ToString().Contains(data.EndDate.ToString()));
                    if (data?.CreateDate > DateTime.MinValue)
                        query = query.Where(s => s.CreateDate.ToString().Contains(data.CreateDate.ToString()));
                    if (data?.ZoneID > 0)
                        query = query.Where(s => s.ZoneID == data.ZoneID);
                    if (!String.IsNullOrWhiteSpace(data?.Address))
                        query = query.Where(s => s.Address.Contains(data.Address));


                    if (data.IsDeleted.HasValue == true)
                        query = query.Where(s => s.IsDeleted == data.IsDeleted);
                    else
                        query = query.Where(s => s.IsDeleted == false);
                }
                else
                {
                    query = query.Where(s => s.IsDeleted == false);
                }
                #endregion

                result = query?.ToList().Select(s => Mapper.Map(s))?.OrderByDescending(s => s.ID).ToList();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            return result;
        }
        public BranchAddressDTO BranchAddressInsert(BranchAddressDTO data)
        {
            BranchAddress Branchaddress = null;
            Branchaddress = Mapper.Map(data);
            UseContext(c =>
            {
                c.BranchAddress.Add(Branchaddress);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;
            });
            data.ID = Branchaddress.ID;
            return data;
        }
        public List<BranchAddressDTO> BranchAddressInsert(List<BranchAddressDTO> data)
        {

            List<BranchAddress> Branchaddresslist = null;
            Branchaddresslist = data.Select(s => Mapper.Map(s))?.ToList();
            UseContext(c =>
            {
                c.BranchAddress.AddRange(Branchaddresslist);
                c.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return data;

        }
        public BranchAddressDTO BranchAddressUpdate(BranchAddressDTO data)
        {
            BranchAddressDTO Branchaddressdto = new BranchAddressDTO();
            if (data.ID > 0)
            {
                Branchaddressdto = BranchAddressGet(new BranchAddressDTO { ID = data.ID }, null)?.FirstOrDefault();
                Branchaddressdto = new BranchAddressDTO
                {
                    ID = data.ID,
                    ZoneID = data?.ZoneID > 0 ? data?.ZoneID : Branchaddressdto.ZoneID,
                    Address = !String.IsNullOrWhiteSpace(data?.Address)?data?.Address:Branchaddressdto.Address,    
                    CreateDate = data?.CreateDate > DateTime.MinValue ? data.CreateDate : Branchaddressdto.CreateDate,
                    StartDate = data?.StartDate > DateTime.MinValue ? data.StartDate : Branchaddressdto.StartDate,
                    EndDate = data?.EndDate > DateTime.MinValue ? data.EndDate : Branchaddressdto.EndDate,
                    Longitude = data?.Longitude > 0 ? data?.Longitude : Branchaddressdto.Longitude,
                    Latitude = data?.Latitude > 0 ? data?.Latitude : Branchaddressdto.Latitude,
                    IsDeleted = data.IsDeleted,
                };
            }
            BranchAddress Branchaddress = Mapper.Map(Branchaddressdto);
            UseContext(databsse =>
            {
                databsse.Entry(Branchaddress).State = System.Data.Entity.EntityState.Modified;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return Branchaddressdto;
        }
        public BranchAddressDTO BranchAddressDelete(BranchAddressDTO data)
        {
            BranchAddressDTO BranchaddressDto = new BranchAddressDTO();
            if (data.ID > 0)
            {
                BranchaddressDto = BranchAddressGet(new BranchAddressDTO { ID = data.ID }, null)?.SingleOrDefault();
            }
            BranchAddress Branchaddress = Mapper.Map(BranchaddressDto);
            UseContext(databsse =>
            {
                databsse.Entry(Branchaddress).State = System.Data.Entity.EntityState.Deleted;
                databsse.SaveChanges();
                ResponseStatus = Gostar.Common.ResponseStatus.Successful;

            });
            return BranchaddressDto;
        }
    }
}
