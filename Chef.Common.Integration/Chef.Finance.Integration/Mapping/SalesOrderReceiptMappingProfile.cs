
using AutoMapper;
using Chef.Common.Types;
using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration.Mapping;

public class SalesOrderReceiptMappingProfile : Profile
{
    public SalesOrderReceiptMappingProfile()
    {
        CreateMap<SalesOrderReceiptDto, ReceiptRegister>()
                .ForMember(d => d.ReceiptCategory, opt => opt.MapFrom(x => ReceiptCategory.CustomerReceiptAgainstInvoices))
                .ForMember(d => d.BusinessPartnerId, opt => opt.MapFrom(x => x.CustomerId))
                .ForMember(d => d.BusinessPartnerCode, opt => opt.MapFrom(x => x.CustomerCode))
                .ForMember(d => d.BusinessPartnerName, opt => opt.MapFrom(x => x.CustomerName))
                .ForMember(d => d.PaymentMethodType, opt => opt.MapFrom(x =>PaymentMethodType.Cash))
                .ForMember(d => d.DocumentDate, opt => opt.MapFrom(x => x.ReceiptDate))
                .ForMember(d => d.TransactionDate, opt => opt.MapFrom(x => x.ReceiptDate))
                .ForMember(d => d.IsCorporateGroup, opt => opt.MapFrom(x =>false))
                .ForMember(d => d.IsHoldReceipt, opt => opt.MapFrom(x =>false))
                .ForMember(d => d.BalanceAmount, opt => opt.MapFrom(x => x.TotalAmount))
                .ForMember(d => d.BalanceAmountInBaseCurrency, opt => opt.MapFrom(x => x.TotalAmountInBaseCurrency))
                .ForMember(d => d.DocumentType, opt => opt.MapFrom(x =>DocumentType.ReceiptRegister)) ;

        CreateMap<SalesOrderReceiptDto, CustomerCashReceipt>()
               .ForMember(d => d.Name, opt => opt.MapFrom(x => x.CustomerName));
    

    }
}