using AutoMapper;
using Chef.Common.Types;
using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration.Mapping;

public class SalesReturnCreditMappingProfile : Profile
{
    public SalesReturnCreditMappingProfile()
    {
        CreateMap<SalesReturnCreditDto, CustomerCreditNote>()
                        .ForMember(d => d.TransactionDate, opt => opt.MapFrom(x => x.SalesCreditDate))
                        .ForMember(d => d.BusinessPartnerId, opt => opt.MapFrom(x => x.CustomerId))
                        .ForMember(d => d.BusinessPartnerCode, opt => opt.MapFrom(x => x.CustomerCode))
                        .ForMember(d => d.BusinessPartnerName, opt => opt.MapFrom(x => x.CustomerName))
                        .ForMember(d => d.TransactionCurrencyCode, opt => opt.MapFrom(x => x.SalesCreditCurrency))
                        .ForMember(d => d.ExchangeRate, opt => opt.MapFrom(x => x.ExRate))
                        .ForMember(d => d.TotalAmount, opt => opt.MapFrom(x => x.NetAmount))
                        .ForMember(d => d.TotalBalanceAmount, opt => opt.MapFrom(x => x.NetAmount))
                        .ForMember(d => d.Narration, opt => opt.MapFrom(x => "dss"))
                        .ForMember(d => d.BranchId, opt => opt.MapFrom(x => x.BranchId))
                        .ForMember(d => d.DocumentStatus, opt => opt.MapFrom(x => "1"))
                        .ForMember(d => d.ApproveStatus, opt => opt.MapFrom(x => "1"))
                        .ForMember(d => d.DocumentType, opt => opt.MapFrom(x => DocumentType.CustomerCreditNote));

        CreateMap<SalesReturnCreditDto, CustomerCreditNoteDetail>()
                        .ForMember(d => d.NetAmount, opt => opt.MapFrom(x => x.GrossTotal))
                        .ForMember(d => d.TaxAmount, opt => opt.MapFrom(x => x.TaxAmount))
                        .ForMember(d => d.TotalAmount, opt => opt.MapFrom(x => x.NetAmount))
                        .ForMember(d => d.TotalBalanceAmountInBasecurrency, opt => opt.MapFrom(x => x.NetAmount * x.ExRate))
                        .ForMember(d => d.BranchId, opt => opt.MapFrom(x => x.BranchId));

    }
}
