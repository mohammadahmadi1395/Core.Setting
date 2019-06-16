using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Gostar.Setting.SC.Messages;

namespace Gostar.Setting.SC
{
    [ServiceContract]
    public interface ISettingService
    {
        [OperationContract]
        OrganizationalChartResponse OrganizationalChart(OrganizationalChartRequest request);

        [OperationContract]
        AreaResponse Area(AreaRequest request);

        [OperationContract]
        BranchResponse Branch(BranchRequest request);

        [OperationContract]
        BranchAddressResponse BranchAddress(BranchAddressRequest request);

        [OperationContract]
        BranchRegionWorkResponse BranchRegionWork(BranchRegionWorkRequest request);

        [OperationContract]
        CityResponse City(CityRequest request);

        [OperationContract]
        CountryResponse Country(CountryRequest request);

        [OperationContract]
        CurrencyResponse Currency(CurrencyRequest request);

        [OperationContract]
        ExchangeRateResponse ExchangeRate(ExchangeRateRequest request);

        [OperationContract]
        FormTypeResponse FormType(FormTypeRequest request);

        [OperationContract]
        GeneratedFormResponse GenerateForm(GeneratedFormRequest request);

        [OperationContract]
        LogResponse Log(LogRequest request);

        [OperationContract]
        PrefixResponse Prefix(PrefixRequest request);

        [OperationContract]
        RegionResponse Region(RegionRequest requset);

        [OperationContract]
        RegionAgentResponse RegionAgent(RegionAgentRequest requset);

        [OperationContract]
        RuleResponse Rule(RuleRequest request);

        [OperationContract]
        RuleTagResponse RuleTag(RuleTagRequest request);

        [OperationContract]
        SectorResponse Sector(SectorRequest Request);

        [OperationContract]
        StatementResponse Statement(StatementRequest request);

        [OperationContract]
        SubpartResponse Subpart(SubpartRequest request);

        [OperationContract]
        SubsystemResponse Subsystem(SubsystemRequest request);

        [OperationContract]
        TypeoforganizationResponse Typeoforganization(TypeoforganizationRequest request);

        [OperationContract]
        ZoneResponse UpdateAllZone(ZoneRequest request);
        [OperationContract]
        ZoneResponse SearchZone(ZoneRequest request);

        [OperationContract]
        ZoneResponse Zone(ZoneRequest Request);
        [OperationContract]
        ZoneBranchResponse ZoneBranch(ZoneBranchRequest Request);
        [OperationContract]
        BaseResponse<DateTime> ServerTime();

    }
}