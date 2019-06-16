using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Gostar.Setting.SC;
using Gostar.Setting.SC.Messages;
using Gostar.Setting.BL;
using Gostar.Setting.DTO;
using Gostar.Common;

namespace Gostar.Setting.SL
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class SettingService : ISettingService, IRestSettingService
    {
        OrganizationalChartBL OrganizationalChartBL = new OrganizationalChartBL();
        public OrganizationalChartResponse OrganizationalChart(OrganizationalChartRequest request)
        {
            OrganizationalChartResponse response = new OrganizationalChartResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new OrganizationalChartResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = OrganizationalChartBL.CallBL(b => b.OrganizationalChartGet(new OrganizationalChartDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = OrganizationalChartBL.CallBL(b => b.OrganizationalChartGet(request?.RequestDto, request?.OrganizationalChartFilter),
                                request?.User, request?.Language);// OrganizationalChartBL.OrganizationalChartGet(request.RequestDto, request.OrganizationalChartFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = OrganizationalChartBL.CallBL(b => b.OrganizationalChartInsert(request?.RequestDtoList),
                                request?.User, request?.Language); //OrganizationalChartBL.OrganizationalChartInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = OrganizationalChartBL.CallBL(b => b.OrganizationalChartInsert(request?.RequestDto),
                                request?.User, request?.Language);// OrganizationalChartBL.OrganizationalChartInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = OrganizationalChartBL.CallBL(b => b.OrganizationalChartUpdate(request?.RequestDto),
                            request?.User, request?.Language);// OrganizationalChartBL.OrganizationalChartUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = OrganizationalChartBL.CallBL(b => b.OrganizationalChartDelete(new OrganizationalChartDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = OrganizationalChartBL.CallBL(b => b.OrganizationalChartDelete(request.RequestDto),
                                request?.User, request?.Language);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = OrganizationalChartBL.CallBL(b => b.OrganizationalChartDeleteComplete(new OrganizationalChartDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            }), request?.UserID);
                    //        else
                    //            response.ResponseDto = OrganizationalChartBL.CallBL(b => b.OrganizationalChartDeleteComplete(request.RequestDto), request?.UserID);
                    //        break;
                    //    }
            }
            response.ValidationErrors = OrganizationalChartBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = OrganizationalChartBL.ResponseStatus;
                response.ErrorMessage = OrganizationalChartBL.ErrorMessage;
            }

            return response;
        }
        AreaBL AreaBL = new AreaBL();
        public AreaResponse Area(AreaRequest request)
        {
            AreaResponse response = new AreaResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new AreaResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = AreaBL.CallBL(b => b.AreaGet(new AreaDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = AreaBL.CallBL(b => b.AreaGet(request?.RequestDto, request?.AreaFilter),
                                request?.User, request?.Language);// AreaBL.AreaGet(request.RequestDto, request.AreaFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = AreaBL.CallBL(b => b.AreaInsert(request?.RequestDtoList),
                                request?.User, request?.Language);// AreaBL.AreaInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = AreaBL.CallBL(b => b.AreaInsert(request?.RequestDto),
                                request?.User, request?.Language);// AreaBL.AreaInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = AreaBL.CallBL(b => b.AreaUpdate(request?.RequestDto),
                            request?.User, request?.Language);// AreaBL.AreaUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = AreaBL.CallBL(b => b.AreaDelete(new AreaDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = AreaBL.CallBL(b => b.AreaDelete(request?.RequestDto),
                                request?.User, request?.Language);// AreaBL.AreaDelete(request.RequestDto);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = AreaBL.CallBL(b => b.AreaDeleteComplete(new AreaDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            }), request?.UserID);
                    //        else
                    //            response.ResponseDto = AreaBL.CallBL(b => b.AreaDeleteComplete(request?.RequestDto), request?.UserID);// AreaBL.AreaDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = AreaBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = AreaBL.ResponseStatus;
                response.ErrorMessage = AreaBL.ErrorMessage;
            }
            return response;
        }

        BranchBL BranchBL = new BranchBL();
        public BranchResponse Branch(BranchRequest request)
        {
            BranchResponse response = new BranchResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new BranchResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = BranchBL.CallBL(b => b.BranchGet(new BranchDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                        if (request.BranchFilter != null)
                            response.ResponseDtoList = BranchBL.CallBL(b => b.BranchGet(request.RequestDto, request.BranchFilter), request?.User, request?.Language);
                        else
                            response.ResponseDtoList = BranchBL.CallBL(b => b.BranchGet(request.RequestDto, request.BranchFilter ?? new BranchFilterDTO()), request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = BranchBL.CallBL(b => b.BranchInsert(request?.RequestDtoList),
                                request?.User, request?.Language);
                        }
                        else
                        {
                            response.ResponseDto = BranchBL.CallBL(b => b.BranchInsert(request?.RequestDto),
                                request?.User, request?.Language);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = BranchBL.CallBL(b => b.BranchUpdate(request?.RequestDto),
                            request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = BranchBL.CallBL(b => b.BranchDelete(new BranchDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = BranchBL.CallBL(b => b.BranchDelete(request?.RequestDto),
                                request?.User, request?.Language);
                        break;
                    }

            }
            response.ValidationErrors = BranchBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = BranchBL.ResponseStatus;
                response.ErrorMessage = BranchBL.ErrorMessage;
            }

            return response;
        }

        BranchAddressBL BranchAddressBL = new BranchAddressBL();
        public BranchAddressResponse BranchAddress(BranchAddressRequest request)
        {
            BranchAddressResponse response = new BranchAddressResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new BranchAddressResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = BranchAddressBL.CallBL(s => s.BranchAddressGet(new BranchAddressDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = BranchAddressBL.CallBL(s => s.BranchAddressGet(request.RequestDto, request.BranchAddressFilter), request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = BranchAddressBL.CallBL(s => s.BranchAddressInsert(request.RequestDtoList), request?.User, request?.Language);

                        }
                        else
                        {
                            response.ResponseDto = BranchAddressBL.CallBL(s => s.BranchAddressInsert(request.RequestDto), request?.User, request?.Language);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = BranchAddressBL.CallBL(s => s.BranchAddressUpdate(request.RequestDto), request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = BranchAddressBL.CallBL(s => s.BranchAddressDelete(new BranchAddressDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = BranchAddressBL.CallBL(s => s.BranchAddressDelete(request.RequestDto), request?.User, request?.Language);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = BranchAddressBL.BranchAddressDeleteComplete(new BranchAddressDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            });
                    //        else
                    //            response.ResponseDto = BranchAddressBL.BranchAddressDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = BranchAddressBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = BranchAddressBL.ResponseStatus;
                response.ErrorMessage = BranchAddressBL.ErrorMessage;
            }

            return response;

        }

        BranchRegionWorkBL BranchRegionWorkBL = new BranchRegionWorkBL();
        public BranchRegionWorkResponse BranchRegionWork(BranchRegionWorkRequest request)
        {
            BranchRegionWorkResponse response = new BranchRegionWorkResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new BranchRegionWorkResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = BranchRegionWorkBL.CallBL(b => b.BranchRegionWorkGet(new BranchRegionWorkDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = BranchRegionWorkBL.CallBL(b => b.BranchRegionWorkGet(request.RequestDto), request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDto != null)
                        {
                            response.ResponseDto = BranchRegionWorkBL.CallBL(b => b.BranchRegionWorkInsert(request?.RequestDto),
                                request?.User, request?.Language);
                        }
                        else if (request?.RequestDtoList?.Count() >= 0)
                        {
                            response.ResponseDtoList = BranchRegionWorkBL.CallBL(b => b.BranchRegionWorkInsert(request?.RequestDtoList),
                                request?.User, request?.Language);
                        }


                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = BranchRegionWorkBL.CallBL(b => b.BranchRegionWorkUpdate(request?.RequestDto),
                            request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = BranchRegionWorkBL.CallBL(b => b.BranchRegionWorkDelete(new BranchRegionWorkDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = BranchRegionWorkBL.CallBL(b => b.BranchRegionWorkDelete(request?.RequestDto),
                                request?.User, request?.Language);
                        break;
                    }

            }
            response.ValidationErrors = BranchRegionWorkBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = BranchRegionWorkBL.ResponseStatus;
                response.ErrorMessage = BranchRegionWorkBL.ErrorMessage;
            }

            return response;
        }

        CityBL CityBL = new CityBL();
        public CityResponse City(CityRequest request)
        {
            CityResponse response = new CityResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new CityResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = CityBL.CallBL(b => b.CityGet(new CityDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = CityBL.CallBL(b => b.CityGet(request?.RequestDto, request?.CityFilter),
                                request?.User, request?.Language);// CityBL.CityGet(request.RequestDto, request.CityFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = CityBL.CallBL(b => b.CityInsert(request?.RequestDtoList),
                                request?.User, request?.Language); //CityBL.CityInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = CityBL.CallBL(b => b.CityInsert(request?.RequestDto),
                                request?.User, request?.Language);// CityBL.CityInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = CityBL.CallBL(b => b.CityUpdate(request?.RequestDto),
                            request?.User, request?.Language);// CityBL.CityUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = CityBL.CallBL(b => b.CityDelete(new CityDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = CityBL.CallBL(b => b.CityDelete(request.RequestDto),
                                request?.User, request?.Language);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = CityBL.CallBL(b => b.CityDeleteComplete(new CityDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            }), request?.UserID);
                    //        else
                    //            response.ResponseDto = CityBL.CallBL(b => b.CityDeleteComplete(request.RequestDto), request?.UserID);
                    //        break;
                    //    }
            }
            response.ValidationErrors = CityBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = CityBL.ResponseStatus;
                response.ErrorMessage = CityBL.ErrorMessage;
            }

            return response;
        }

        CountryBL CountryBL = new CountryBL();
        public CountryResponse Country(CountryRequest request)
        {
            CountryResponse response = new CountryResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new CountryResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = CountryBL.CallBL(b => b.CountryGet(new CountryDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language, request.PagingInfo)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = CountryBL.CallBL(b => b.CountryGet(request?.RequestDto, request?.CountryFilter),
                                request?.User, request?.Language, request.PagingInfo);// CountryBL.CountryGet(request.RequestDto, request.CountryFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = CountryBL.CallBL(b => b.CountryInsert(request?.RequestDtoList),
                                request?.User, request?.Language);// CountryBL.CountryInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = CountryBL.CallBL(b => b.CountryInsert(request?.RequestDto),
                                request?.User, request?.Language);//CountryBL.CountryInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {
                        response.ResponseDto = CountryBL.CallBL(s => s.CountryUpdate(request?.RequestDto),
                            request?.User, request?.Language);// CountryBL.CountryUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = CountryBL.CallBL(s => s.CountryDelete(new CountryDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = CountryBL.CallBL(s => s.CountryDelete(request?.RequestDto),
                                request?.User, request?.Language);// CountryBL.CountryDelete(request.RequestDto);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = CountryBL.CountryDeleteComplete(new CountryDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            });
                    //        else
                    //            response.ResponseDto = CountryBL.CallBL(s => s.CountryDeleteComplete(request?.RequestDto), request?.UserID);// CountryBL.CountryDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = CountryBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResultCount = CountryBL.ResultCount;
                response.ResponseStatus = CountryBL.ResponseStatus;
                response.ErrorMessage = CountryBL.ErrorMessage;
            }

            return response;
        }

        CurrencyBL CurrencyBL = new CurrencyBL();
        public CurrencyResponse Currency(CurrencyRequest request)
        {

            CurrencyResponse response = new CurrencyResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User.UserID > 0))
            {
                response = new CurrencyResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Gostar.Setting.DTO.Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = CurrencyBL.CallBL(b => b.CurrencyGet(new CurrencyDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = CurrencyBL.CallBL(b => b.CurrencyGet(request?.RequestDto, request?.CurrencyFilter),
                                request?.User, request?.Language);// CurrencyBL.CurrencyGet(request.RequestDto, request.CurrencyFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = CurrencyBL.CallBL(b => b.CurrencyInsert(request?.RequestDtoList),
                                request?.User, request?.Language);// CurrencyBL.CurrencyInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = CurrencyBL.CallBL(b => b.CurrencyInsert(request?.RequestDto),
                                request?.User, request?.Language);// CurrencyBL.CurrencyInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = CurrencyBL.CallBL(b => b.CurrencyUpdate(request?.RequestDto),
                            request?.User, request?.Language);// CurrencyBL.CurrencyUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = CurrencyBL.CallBL(b => b.CurrencyDelete(new CurrencyDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = CurrencyBL.CallBL(b => b.CurrencyDelete(request?.RequestDto),
                                request?.User, request?.Language);// CurrencyBL.CurrencyDelete(request.RequestDto);
                        break;
                    }

            }
            response.ValidationErrors = CurrencyBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = CurrencyBL.ResponseStatus;
                response.ErrorMessage = CurrencyBL.ErrorMessage;
            }
            return response;
        }

        ExchangeRateBL ExchangeRateBL = new ExchangeRateBL();
        public ExchangeRateResponse ExchangeRate(ExchangeRateRequest request)
        {

            ExchangeRateResponse response = new ExchangeRateResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User.UserID > 0))
            {
                response = new ExchangeRateResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Gostar.Setting.DTO.Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = ExchangeRateBL.CallBL(b => b.ExchangeRateGet(new ExchangeRateDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = ExchangeRateBL.CallBL(b => b.ExchangeRateGet(request?.RequestDto, request?.ExchangeRateFilter),
                                request?.User, request?.Language);// ExchangeRateBL.ExchangeRateGet(request.RequestDto, request.ExchangeRateFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = ExchangeRateBL.CallBL(b => b.ExchangeRateInsert(request?.RequestDtoList),
                                request?.User, request?.Language);// ExchangeRateBL.ExchangeRateInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = ExchangeRateBL.CallBL(b => b.ExchangeRateInsert(request?.RequestDto),
                                request?.User, request?.Language);// ExchangeRateBL.ExchangeRateInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = ExchangeRateBL.CallBL(b => b.ExchangeRateUpdate(request?.RequestDto),
                            request?.User, request?.Language);// ExchangeRateBL.ExchangeRateUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = ExchangeRateBL.CallBL(b => b.ExchangeRateDelete(new ExchangeRateDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = ExchangeRateBL.CallBL(b => b.ExchangeRateDelete(request?.RequestDto),
                                request?.User, request?.Language);// ExchangeRateBL.ExchangeRateDelete(request.RequestDto);
                        break;
                    }

            }
            response.ValidationErrors = ExchangeRateBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = ExchangeRateBL.ResponseStatus;
                response.ErrorMessage = ExchangeRateBL.ErrorMessage;
            }
            return response;
        }
        //GroupBL GroupBl = new GroupBL();
        //public GroupResponse Group(GroupRequest request)
        //{
        //    GroupResponse response = new GroupResponse
        //    {
        //        ResponseStatus = Gostar.Common.ResponseStatus.Successful,
        //        ErrorMessage = Gostar.Common.ResponseStatus.Successful.GetDescription(),
        //    };

        //    if (!(request?.User?.UserID > 0))
        //    {
        //        response = new GroupResponse
        //        {
        //            ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
        //            ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
        //        };
        //        return null;
        //    }
        //    switch (request.ActionType)
        //    {
        //        case ActionType.Select:
        //            {
        //                if (request?.RequestID > 0)
        //                    response.ResponseDto = GroupBl.CallBL(b => b.GroupGet(new GroupDTO
        //                    {
        //                        ID = request.RequestID ?? 0
        //                    }), request.User)?.FirstOrDefault();
        //                else
        //                    response.ResponseDtoList = GroupBl.CallBL(b => b.GroupGet(request.RequestDto), request.User);
        //                break;
        //            }
        //        case ActionType.Insert:
        //            {
        //                response.ResponseDto = GroupBl.CallBL(b => b.GroupInsert(request.RequestDto), request.User);
        //                break;
        //            }
        //        case ActionType.Update:
        //            {
        //                response.ResponseDto = GroupBl.CallBL(b => b.GroupUpdate(request.RequestDto), request.User);
        //                break;
        //            }
        //        case ActionType.Delete:
        //            {
        //                response.ResponseDto = GroupBl.CallBL(b => b.GroupDelete(request.RequestDto), request.User);
        //                break;
        //            }
        //    }
        //    if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
        //    {
        //        response.ResponseStatus = GroupBl.ResponseStatus;
        //        response.ErrorMessage = GroupBl.ErrorMessage;
        //    }
        //    return response;
        //}

        //UserBL UserBl = new UserBL();
        //public UserResponse User(UserRequest request)
        //{
        //    UserResponse response = new UserResponse
        //    {
        //        ResponseStatus = Gostar.Common.ResponseStatus.Successful,
        //        ErrorMessage = Gostar.Common.ResponseStatus.Successful.GetDescription(),
        //    };

        //    if (!(request?.User?.UserID > 0) && request.ActionType != ActionType.Login)
        //    {
        //        response = new UserResponse
        //        {
        //            ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
        //            ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
        //        };
        //        return null;
        //    }
        //    switch (request.ActionType)
        //    {
        //        case ActionType.Login:
        //            response.ResponseDto = UserBl.CallBL(b => b.UserLogin(new UserDTO
        //            {
        //                MemberUserName = request.RequestDto.MemberUserName,
        //                MemberPassword = hashing.CalculateMD5Hash(request.RequestDto.MemberPassword + hashing.keyHashAdd),
        //                GroupRoleType = request.RequestDto.GroupRoleType,
        //            }), request.User);
        //            break;
        //        case ActionType.Select:
        //            {
        //                if (request?.RequestID > 0)
        //                    response.ResponseDto = UserBl.CallBL(b => b.UserGet(new UserDTO
        //                    {
        //                        ID = request.RequestID ?? 0
        //                    }), request.User)?.FirstOrDefault();
        //                else
        //                    response.ResponseDtoList = UserBl.CallBL(b => b.UserGet(request.RequestDto, request.Filter), request.User);
        //                break;
        //            }
        //        case ActionType.Insert:
        //            {
        //                response.ResponseDto = UserBl.CallBL(b => b.UserInsert(request.RequestDto), request.User);
        //                break;
        //            }
        //        case ActionType.Update:
        //            {
        //                response.ResponseDto = UserBl.CallBL(b => b.UserUpdate(request.RequestDto), request.User);
        //                break;
        //            }
        //        case ActionType.Delete:
        //            {
        //                if (request.RequestID > 0)
        //                    response.ResponseDto = UserBl.CallBL(b => b.UserDelete(new UserDTO { ID = request.RequestID }), request.User);
        //                else
        //                    response.ResponseDto = UserBl.CallBL(b => b.UserDelete(request.RequestDto), request.User);
        //                break;
        //            }
        //    }
        //    if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
        //    {
        //        response.ResponseStatus = UserBl.ResponseStatus;
        //        response.ErrorMessage = UserBl.ErrorMessage;
        //    }
        //    return response;
        //}

        FormTypeBL FormTypeBL = new FormTypeBL();
        public FormTypeResponse FormType(FormTypeRequest request)
        {

            FormTypeResponse response = new FormTypeResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User.UserID > 0))
            {
                response = new FormTypeResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Gostar.Setting.DTO.Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = FormTypeBL.CallBL(b => b.FormTypeGet(new FormTypeDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = FormTypeBL.CallBL(b => b.FormTypeGet(request?.RequestDto, request?.FormTypeFilter),
                                request?.User, request?.Language);// FormTypeBL.FormTypeGet(request.RequestDto, request.FormTypeFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = FormTypeBL.CallBL(b => b.FormTypeInsert(request?.RequestDtoList),
                                request?.User, request?.Language);// FormTypeBL.FormTypeInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = FormTypeBL.CallBL(b => b.FormTypeInsert(request?.RequestDto),
                                request?.User, request?.Language);// FormTypeBL.FormTypeInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = FormTypeBL.CallBL(b => b.FormTypeUpdate(request?.RequestDto),
                            request?.User, request?.Language);// FormTypeBL.FormTypeUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = FormTypeBL.CallBL(b => b.FormTypeDelete(new FormTypeDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = FormTypeBL.CallBL(b => b.FormTypeDelete(request?.RequestDto),
                                request?.User, request?.Language);// FormTypeBL.FormTypeDelete(request.RequestDto);
                        break;
                    }

            }
            response.ValidationErrors = FormTypeBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = FormTypeBL.ResponseStatus;
                response.ErrorMessage = FormTypeBL.ErrorMessage;
            }
            return response;
        }

        GeneratedFormBL GeneratedFormBL = new GeneratedFormBL();
        public GeneratedFormResponse GenerateForm(GeneratedFormRequest request)
        {
            GeneratedFormResponse response = new GeneratedFormResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User?.UserID > 0))
            {
                response = new GeneratedFormResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            if (request?.ActionType == ActionType.Select)
            {
                response.ResponseDTOList = GeneratedFormBL.CallBL(b => b.GeneratedFormGet(request.RequestDTO),
                                                  request?.User);
            }
            else
            {
                response.GeneratedForm = GeneratedFormBL.CallBL(b => b.GenerateForm(request.FormTypeID),
                                   request?.User);
            }

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = GeneratedFormBL.ResponseStatus;
                response.ErrorMessage = GeneratedFormBL.ErrorMessage;
            }

            return response;

        }

        LogBL LogBL = new LogBL();
        public LogResponse Log(LogRequest request)
        {
            LogResponse response = new LogResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User?.UserID > 0))
            {
                response = new LogResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                default:
                    response.ResponseDtoList = LogBL.CallBL(b => b.LogGet(request.RequestDto),
                        request?.User, request?.Language, request?.PagingInfo);
                    break;
            }
            response.ValidationErrors = LogBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResultCount = LogBL.ResultCount;
                response.ResponseStatus = LogBL.ResponseStatus;
                response.ErrorMessage = LogBL.ErrorMessage;
            }
            return response;
        }

        PrefixBL PrefixBL = new PrefixBL();
        public PrefixResponse Prefix(PrefixRequest request)
        {
            PrefixResponse response = new PrefixResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new PrefixResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = PrefixBL.CallBL(b => b.PrefixGet(new PrefixDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request.Language, request?.PagingInfo)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = PrefixBL.CallBL(b => b.PrefixGet(request?.RequestDto),
                                request?.User, request.Language, request?.PagingInfo);// PrefixBL.PrefixGet(request.RequestDto, request.PrefixFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = PrefixBL.CallBL(b => b.PrefixInsert(request?.RequestDtoList),
                                request?.User, request.Language);// PrefixBL.PrefixInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = PrefixBL.CallBL(b => b.PrefixInsert(request?.RequestDto),
                                request?.User, request.Language);//PrefixBL.PrefixInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {
                        response.ResponseDto = PrefixBL.CallBL(s => s.PrefixUpdate(request?.RequestDto),
                            request?.User, request.Language);// PrefixBL.PrefixUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = PrefixBL.CallBL(s => s.PrefixDelete(new PrefixDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request.Language);
                        else
                            response.ResponseDto = PrefixBL.CallBL(s => s.PrefixDelete(request?.RequestDto),
                                request?.User, request.Language);// PrefixBL.PrefixDelete(request.RequestDto);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = PrefixBL.PrefixDeleteComplete(new PrefixDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            });
                    //        else
                    //            response.ResponseDto = PrefixBL.CallBL(s => s.PrefixDeleteComplete(request?.RequestDto), request?.UserID);// PrefixBL.PrefixDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = PrefixBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResultCount = PrefixBL.ResultCount;
                response.ResponseStatus = PrefixBL.ResponseStatus;
                response.ErrorMessage = PrefixBL.ErrorMessage;
            }
            response.ValidationErrors = PrefixBL.ValidationErrors;

            return response;
        }

        RegionBL RegionBL = new RegionBL();
        public RegionResponse Region(RegionRequest request)
        {
            RegionResponse response = new RegionResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new RegionResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = RegionBL.CallBL(b => b.RegionGet(new RegionDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = RegionBL.CallBL(b => b.RegionGet(request.RequestDto, request.RegionFilter), request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = RegionBL.CallBL(b => b.RegionInsert(request?.RequestDtoList),
                                request?.User, request?.Language); //RegionBL.RegionInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = RegionBL.CallBL(b => b.RegionInsert(request?.RequestDto),
                                request?.User, request?.Language);// RegionBL.RegionInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = RegionBL.CallBL(b => b.RegionUpdate(request?.RequestDto),
                            request?.User, request?.Language);// RegionBL.RegionUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = RegionBL.CallBL(b => b.RegionDelete(new RegionDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = RegionBL.CallBL(b => b.RegionDelete(request?.RequestDto),
                                request?.User, request?.Language);// RegionBL.RegionDelete(request.RequestDto);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = RegionBL.RegionDeleteComplete(new RegionDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            });
                    //        else
                    //            response.ResponseDto = RegionBL.CallBL(b => b.RegionDeleteComplete(request?.RequestDto), request?.UserID);// RegionBL.RegionDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = RegionBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = RegionBL.ResponseStatus;
                response.ErrorMessage = RegionBL.ErrorMessage;
            }

            return response;
        }

        RegionAgentBL RegionAgentBL = new RegionAgentBL();
        public RegionAgentResponse RegionAgent(RegionAgentRequest request)
        {
            RegionAgentResponse response = new RegionAgentResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User?.UserID > 0))
            {
                response = new RegionAgentResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = RegionAgentBL.CallBL(b => b.RegionAgentGet(new RegionAgentDTO
                            {
                                ID = request.RequestID ?? 0
                            })?.FirstOrDefault(), request?.User, request?.Language);
                        else
                            response.ResponseDtoList = RegionAgentBL.CallBL(b => b.RegionAgentGet(request.RequestDto, request.RegionAgentFilter),
                                request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = RegionAgentBL.CallBL(b => b.RegionAgentInsert(request.RequestDtoList),
                                request?.User, request?.Language);
                        }
                        else
                        {
                            response.ResponseDto = RegionAgentBL.CallBL(b => b.RegionAgentInsert(request.RequestDto),
                                request?.User, request?.Language);
                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {
                        response.ResponseDto = RegionAgentBL.CallBL(b => b.RegionAgentUpdate(request.RequestDto),
                            request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = RegionAgentBL.CallBL(b => b.RegionAgentDelete(new RegionAgentDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = RegionAgentBL.CallBL(b => b.RegionAgentDelete(request.RequestDto),
                                request?.User, request?.Language);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = RegionAgentBL.CallBL(b => b.RegionAgentDeleteComplete(new RegionAgentDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            }), request?.UserID);
                    //        else
                    //            response.ResponseDto = RegionAgentBL.CallBL(b => b.RegionAgentDeleteComplete(request.RequestDto), request?.UserID);
                    //        break;
                    //    }
            }
            response.ValidationErrors = RegionAgentBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = RegionAgentBL.ResponseStatus;
                response.ErrorMessage = RegionAgentBL.ErrorMessage;
            }

            return response;
        }

        RuleBL RuleBL = new RuleBL();
        public RuleResponse Rule(RuleRequest request)
        {

            RuleResponse response = new RuleResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User.UserID > 0))
            {
                response = new RuleResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Gostar.Setting.DTO.Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = RuleBL.CallBL(b => b.RuleGet(new RuleDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language, request?.PagingInfo)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = RuleBL.CallBL(b => b.RuleGet(request?.RequestDto, request?.RuleFilter),
                                request?.User, request?.Language, request?.PagingInfo);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = RuleBL.CallBL(b => b.RuleInsert(request?.RequestDtoList),
                                request?.User, request?.Language);
                        }
                        else
                        {
                            response.ResponseDto = RuleBL.CallBL(b => b.RuleInsert(request?.RequestDto),
                                request?.User, request?.Language);// RuleBL.RuleInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = RuleBL.CallBL(b => b.RuleUpdate(request?.RequestDto),
                            request?.User, request?.Language);// RuleBL.RuleUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = RuleBL.CallBL(b => b.RuleDelete(new RuleDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = RuleBL.CallBL(b => b.RuleDelete(request?.RequestDto),
                                request?.User, request?.Language);// RuleBL.RuleDelete(request.RequestDto);
                        break;
                    }

            }
            response.ValidationErrors = RuleBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResultCount = RuleBL.ResultCount;
                response.ResponseStatus = RuleBL.ResponseStatus;
                response.ErrorMessage = RuleBL.ErrorMessage;
            }
            return response;
        }

        RuleTagBL RuleTagBL = new RuleTagBL();
        public RuleTagResponse RuleTag(RuleTagRequest request)
        {

            RuleTagResponse response = new RuleTagResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User.UserID > 0))
            {
                response = new RuleTagResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Gostar.Setting.DTO.Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = RuleTagBL.CallBL(b => b.RuleTagGet(new RuleTagDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = RuleTagBL.CallBL(b => b.RuleTagGet(request?.RequestDto, request?.RuleTagFilter),
                                request?.User, request?.Language);// RuleTagBL.RuleTagGet(request.RequestDto, request.RuleTagFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = RuleTagBL.CallBL(b => b.RuleTagInsert(request?.RequestDtoList),
                                request?.User, request?.Language);// RuleTagBL.RuleTagInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = RuleTagBL.CallBL(b => b.RuleTagInsert(request?.RequestDto),
                                request?.User, request?.Language);// RuleTagBL.RuleTagInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = RuleTagBL.CallBL(b => b.RuleTagUpdate(request?.RequestDto),
                            request?.User, request?.Language);// RuleTagBL.RuleTagUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = RuleTagBL.CallBL(b => b.RuleTagDelete(new RuleTagDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = RuleTagBL.CallBL(b => b.RuleTagDelete(request?.RequestDto),
                                request?.User, request?.Language);// RuleTagBL.RuleTagDelete(request.RequestDto);
                        break;
                    }

            }
            response.ValidationErrors = RuleTagBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = RuleTagBL.ResponseStatus;
                response.ErrorMessage = RuleTagBL.ErrorMessage;
            }
            return response;
        }

        SectorBL SectorBL = new SectorBL();
        public SectorResponse Sector(SectorRequest request)
        {
            SectorResponse response = new SectorResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new SectorResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = SectorBL.CallBL(b => b.SectorGet(new SectorDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = SectorBL.CallBL(b => b.SectorGet(request.RequestDto, request.SectorFilter), request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = SectorBL.CallBL(b => b.SectorInsert(request?.RequestDtoList),
                                request?.User, request?.Language); //SectorBL.SectorInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = SectorBL.CallBL(b => b.SectorInsert(request?.RequestDto),
                                request?.User, request?.Language);// SectorBL.SectorInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = SectorBL.CallBL(b => b.SectorUpdate(request?.RequestDto),
                            request?.User, request?.Language);// SectorBL.SectorUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = SectorBL.CallBL(b => b.SectorDelete(new SectorDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = SectorBL.CallBL(b => b.SectorDelete(request?.RequestDto),
                                request?.User, request?.Language);// SectorBL.SectorDelete(request.RequestDto);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = SectorBL.SectorDeleteComplete(new SectorDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            });
                    //        else
                    //            response.ResponseDto = SectorBL.CallBL(b => b.SectorDeleteComplete(request?.RequestDto), request?.UserID);// SectorBL.SectorDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = SectorBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = SectorBL.ResponseStatus;
                response.ErrorMessage = SectorBL.ErrorMessage;
            }

            return response;
        }

        StatementBL StatementBL = new StatementBL();
        public StatementResponse Statement(StatementRequest request)
        {
            StatementResponse response = new StatementResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User?.UserID > 0))
            {
                response = new StatementResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = StatementBL.CallBL(b => b.StatementGet(new DTO.StatementDTO
                            {
                                ID = (int)(request.RequestID ?? 0),
                            }), request?.User, request?.Language, request.PagingInfo)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = StatementBL.CallBL(b => b.StatementGet(request?.RequestDto),
                                request?.User, request?.Language, request.PagingInfo);// StatementBL.StatementGet(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = StatementBL.CallBL(b => b.StatementInsert(request?.RequestDtoList),
                                request?.User, request?.Language);//StatementBL.StatementInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = StatementBL.CallBL(b => b.StatementInsert(request?.RequestDto),
                                request?.User, request?.Language);// StatementBL.StatementInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {

                        response.ResponseDto = StatementBL.CallBL(b => b.StatementUpdate(request?.RequestDto),
                            request?.User, request?.Language);//StatementBL.StatementUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = StatementBL.CallBL(b => b.StatementDelete(new DTO.StatementDTO
                            {
                                ID = (int)(request.RequestID ?? 0)
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = StatementBL.CallBL(b => b.StatementDelete(request?.RequestDto),
                                request?.User, request?.Language);// StatementBL.StatementDelete(request.RequestDto);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = StatementBL.CallBL(b => b.StatementDeleteComplete(new StatementDTO
                    //            {
                    //                ID = (int)(request.RequestID ?? 0),
                    //            }), request?.UserID);
                    //        else
                    //            response.ResponseDto = StatementBL.CallBL(b => b.StatementDeleteComplete(request?.RequestDto), request?.UserID);// StatementBL.StatementDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = StatementBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResultCount = StatementBL.ResultCount;
                response.ResponseStatus = StatementBL.ResponseStatus;
                response.ErrorMessage = StatementBL.ErrorMessage;
            }

            return response;
        }

        SubpartBL SubpartBL = new SubpartBL();
        public SubpartResponse Subpart(SubpartRequest request)
        {
            SubpartResponse response = new SubpartResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User?.UserID > 0))
            {
                response = new SubpartResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = SubpartBL.CallBL(b => b.SubpartGet(new SubpartDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = SubpartBL.CallBL(b => b.SubpartGet(request.RequestDto),
                                request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = SubpartBL.CallBL(b => b.SubpartInsert(request.RequestDtoList),
                                request?.User, request?.Language);// SubpartBL.SubpartInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = SubpartBL.CallBL(b => b.SubpartInsert(request.RequestDto),
                                request?.User, request?.Language);//SubpartBL.SubpartInsert(request.RequestDto);
                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {
                        response.ResponseDto = SubpartBL.CallBL(b => b.SubpartUpdate(request.RequestDto),
                            request?.User, request?.Language); //SubpartBL.SubpartUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = SubpartBL.CallBL(b => b.SubpartDelete(new SubpartDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = SubpartBL.CallBL(b => b.SubpartDelete(request.RequestDto),
                                request?.User, request?.Language);// SubpartBL.SubpartDelete(request.RequestDto);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = SubpartBL.CallBL(b => b.SubpartDeleteComplete(new SubpartDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            }), request?.UserID);
                    //        else
                    //            response.ResponseDto = SubpartBL.CallBL(b => b.SubpartDeleteComplete(request.RequestDto), request?.UserID);// SubpartBL.SubpartDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = SubpartBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = SubpartBL.ResponseStatus;
                response.ErrorMessage = SubpartBL.ErrorMessage;
            }

            return response;

        }

        SubsystemBL SubsystemBL = new SubsystemBL();
        public SubsystemResponse Subsystem(SubsystemRequest request)
        {
            SubsystemResponse response = new SubsystemResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User?.UserID > 0))
            {
                response = new SubsystemResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = SubsystemBL.CallBL(b => b.SubsystemGet(new SubsystemDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = SubsystemBL.CallBL(b => b.SubsystemGet(request.RequestDto),
                                request?.User, request?.Language);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = SubsystemBL.CallBL(b => b.SubsystemInsert(request.RequestDtoList),
                                request?.User, request?.Language);// SubsystemBL.SubsystemInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = SubsystemBL.CallBL(b => b.SubsystemInsert(request.RequestDto),
                                request?.User, request?.Language);//SubsystemBL.SubsystemInsert(request.RequestDto);
                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {
                        response.ResponseDto = SubsystemBL.CallBL(b => b.SubsystemUpdate(request.RequestDto),
                            request?.User, request?.Language); //SubsystemBL.SubsystemUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = SubsystemBL.CallBL(b => b.SubsystemDelete(new SubsystemDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = SubsystemBL.CallBL(b => b.SubsystemDelete(request.RequestDto),
                                request?.User, request?.Language);// SubsystemBL.SubsystemDelete(request.RequestDto);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = SubsystemBL.CallBL(b => b.SubsystemDeleteComplete(new SubsystemDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            }), request?.UserID);
                    //        else
                    //            response.ResponseDto = SubsystemBL.CallBL(b => b.SubsystemDeleteComplete(request.RequestDto), request?.UserID);// SubsystemBL.SubsystemDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = SubsystemBL.ValidationErrors;
            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = SubsystemBL.ResponseStatus;
                response.ErrorMessage = SubsystemBL.ErrorMessage;
            }

            return response;

        }

        TypeoforganizationBL TypeoforganizationBL = new TypeoforganizationBL();
        public TypeoforganizationResponse Typeoforganization(TypeoforganizationRequest request)
        {
            TypeoforganizationResponse response = new TypeoforganizationResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new TypeoforganizationResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = TypeoforganizationBL.CallBL(b => b.TypeoforganizationGet(new TypeoforganizationDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language, request?.PagingInfo)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = TypeoforganizationBL.CallBL(b => b.TypeoforganizationGet(request?.RequestDto),
                                request?.User, request?.Language, request?.PagingInfo);// TypeoforganizationBL.TypeoforganizationGet(request.RequestDto, request.TypeoforganizationFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = TypeoforganizationBL.CallBL(b => b.TypeoforganizationInsert(request?.RequestDtoList),
                                request?.User, request?.Language);// TypeoforganizationBL.TypeoforganizationInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = TypeoforganizationBL.CallBL(b => b.TypeoforganizationInsert(request?.RequestDto),
                                request?.User, request?.Language);//TypeoforganizationBL.TypeoforganizationInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {
                        response.ResponseDto = TypeoforganizationBL.CallBL(s => s.TypeoforganizationUpdate(request?.RequestDto),
                            request?.User, request?.Language);// TypeoforganizationBL.TypeoforganizationUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = TypeoforganizationBL.CallBL(s => s.TypeoforganizationDelete(new TypeoforganizationDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = TypeoforganizationBL.CallBL(s => s.TypeoforganizationDelete(request?.RequestDto),
                                request?.User, request?.Language);// TypeoforganizationBL.TypeoforganizationDelete(request.RequestDto);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = TypeoforganizationBL.TypeoforganizationDeleteComplete(new TypeoforganizationDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            });
                    //        else
                    //            response.ResponseDto = TypeoforganizationBL.CallBL(s => s.TypeoforganizationDeleteComplete(request?.RequestDto), request?.UserID);// TypeoforganizationBL.TypeoforganizationDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = TypeoforganizationBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResultCount = TypeoforganizationBL.ResultCount;
                response.ResponseStatus = TypeoforganizationBL.ResponseStatus;
                response.ErrorMessage = TypeoforganizationBL.ErrorMessage;
            }

            return response;
        }

        ZoneBL ZoneBL = new ZoneBL();
        public ZoneResponse Zone(ZoneRequest request)
        {
            ZoneResponse response = new ZoneResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new ZoneResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = ZoneBL.CallBL(b => b.ZoneGet(new ZoneDTO
                            {
                                ID = request.RequestID ?? 0
                            }, null), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = ZoneBL.CallBL(b => b.ZoneGet(request?.RequestDto, request?.ZoneFilter),
                                request?.User, request?.Language);// ZoneBL.ZoneGet(request.RequestDto, request.ZoneFilter);
                        break;
                    }
                case Gostar.Common.ActionType.Insert:
                    {
                        if (request?.RequestDtoList?.Count() > 0)
                        {
                            response.ResponseDtoList = ZoneBL.CallBL(b => b.ZoneInsert(request?.RequestDtoList),
                                request?.User, request?.Language);// ZoneBL.ZoneInsert(request.RequestDtoList);
                        }
                        else
                        {
                            response.ResponseDto = ZoneBL.CallBL(b => b.ZoneInsert(request?.RequestDto),
                                request?.User, request?.Language);//ZoneBL.ZoneInsert(request.RequestDto);

                        }
                        break;
                    }
                case Gostar.Common.ActionType.Update:
                    {
                        response.ResponseDto = ZoneBL.CallBL(s => s.ZoneUpdate(request?.RequestDto),
                            request?.User, request?.Language);// ZoneBL.ZoneUpdate(request.RequestDto);
                        break;
                    }
                case Gostar.Common.ActionType.Delete:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDto = ZoneBL.CallBL(s => s.ZoneDelete(new ZoneDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language);
                        else
                            response.ResponseDto = ZoneBL.CallBL(s => s.ZoneDelete(request?.RequestDto),
                                request?.User, request?.Language);// ZoneBL.ZoneDelete(request.RequestDto);
                        break;
                    }
                    //case Gostar.Common.ActionType.DeleteComplete:
                    //    {
                    //        if (request?.RequestID > 0)
                    //            response.ResponseDto = ZoneBL.ZoneDeleteComplete(new ZoneDTO
                    //            {
                    //                ID = request.RequestID ?? 0
                    //            });
                    //        else
                    //            response.ResponseDto = ZoneBL.CallBL(s => s.ZoneDeleteComplete(request?.RequestDto), request?.UserID);// ZoneBL.ZoneDeleteComplete(request.RequestDto);
                    //        break;
                    //    }
            }
            response.ValidationErrors = ZoneBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = ZoneBL.ResponseStatus;
                response.ErrorMessage = ZoneBL.ErrorMessage;
            }

            return response;
        }
        ZoneBranchBL ZoneBranchBL = new ZoneBranchBL();
        public ZoneBranchResponse ZoneBranch(ZoneBranchRequest request)
        {
            ZoneBranchResponse response = new ZoneBranchResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,

            };
            if (!(request.User?.UserID > 0))
            {
                response = new ZoneBranchResponse
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {
                        if (request?.RequestID > 0)
                            response.ResponseDtoList = ZoneBranchBL.CallBL(b => b.ZoneBranchGet(request?.RequestID ?? 0), request?.User, request?.Language);
                        else if (request?.RequestDto > 0)
                        {
                            response.ResponseDtoList = ZoneBranchBL.CallBL(b => b.ZoneBranchGet(request?.RequestDto ?? 0), request?.User, request?.Language);

                        }
                        break;
                    }
            }
            response.ValidationErrors = ZoneBranchBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = ZoneBranchBL.ResponseStatus;
                response.ErrorMessage = ZoneBranchBL.ErrorMessage;
            }

            return response;
        }
        public ZoneResponse UpdateAllZone(ZoneRequest request)
        {
            ZoneResponse response = new ZoneResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User?.UserID > 0))
            {
                response = new ZoneResponse()
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Update:
                    {
                        response.ResponseDtoList = ZoneBL.CallBL(b => b.UpdateAllZone(), request?.User, request?.Language);
                        break;
                    }
            }
            response.ValidationErrors = ZoneBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = ZoneBL.ResponseStatus;
                response.ErrorMessage = ZoneBL.ErrorMessage;
            }

            return response;
        }
        public ZoneResponse SearchZone(ZoneRequest request)
        {
            ZoneResponse response = new ZoneResponse
            {
                ResponseStatus = Gostar.Common.ResponseStatus.Successful,
            };
            if (!(request.User?.UserID > 0))
            {
                response = new ZoneResponse()
                {
                    ResponseStatus = Gostar.Common.ResponseStatus.InvalidRequest,
                    ErrorMessage = Enums.ErrorType.UnknownUserError.GetDescription(),
                };
                return response;
            }

            switch (request.ActionType)
            {
                case Gostar.Common.ActionType.Select:
                    {

                        if (request?.RequestID > 0)
                            response.ResponseDto = ZoneBL.CallBL(b => b.ZoneSearch(new ZoneDTO
                            {
                                ID = request.RequestID ?? 0
                            }), request?.User, request?.Language)?.FirstOrDefault();
                        else
                            response.ResponseDtoList = ZoneBL.CallBL(b => b.ZoneSearch(request?.RequestDto),
                                request?.User, request?.Language);
                        break;
                    }
            }
            response.ValidationErrors = ZoneBL.ValidationErrors;

            if (response.ResponseStatus == Gostar.Common.ResponseStatus.Successful)
            {
                response.ResponseStatus = ZoneBL.ResponseStatus;
                response.ErrorMessage = ZoneBL.ErrorMessage;
            }

            return response;
        }

        public BaseResponse<DateTime> ServerTime()
        {
            var response = new BaseResponse<DateTime>();

            response.ResponseDto = DateTime.Now;
            response.ResponseStatus = ResponseStatus.Successful;
            return response;

        }
    }
}