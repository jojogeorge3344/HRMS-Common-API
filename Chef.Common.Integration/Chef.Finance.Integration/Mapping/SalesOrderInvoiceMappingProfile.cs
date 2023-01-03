using AutoMapper;
using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration.Mapping;

public class SalesOrderInvoiceMappingProfile : Profile
{
    public SalesOrderInvoiceMappingProfile()
    {
        CreateMap<SalesInvoicePaymentTermLineDto, SalesInvoicePaymentTermInstallment>();

        CreateMap<SalesInvoicePaymentTermsDto, SalesInvoicePaymentTerm>()
            .ForMember(d => d.Installments, opt => opt.MapFrom(x => x.salesInvoicePaymentTermLineDtos));

        CreateMap<SalesInvoiceItemDto, SalesInvoiceLineItem>()
            .ForMember(d => d.Description, opt => opt.MapFrom(x => x.ItemSpec))
            .ForMember(d => d.Amount, opt => opt.MapFrom(x => x.TotalAmount))
            .ForMember(d => d.DiscountPercentage, opt => opt.MapFrom(x => x.DiscountPer))
            .ForMember(d => d.DiscountAmount, opt => opt.MapFrom(x => x.DiscountAmt))
            .ForMember(d => d.NetAmount, opt => opt.MapFrom(x => x.GrossAmt))
            .ForMember(d => d.TaxPercentage, opt => opt.MapFrom(x => x.TotalTaxPer))
            .ForMember(d => d.TaxAmount, opt => opt.MapFrom(x => x.TotalTaxAmt))
            .ForMember(d => d.TotalAmount, opt => opt.MapFrom(x => x.NetAmount));

        CreateMap<SalesInvoiceDto, SalesInvoice>()
            .ForMember(d => d.InvoiceDate, opt => opt.MapFrom(x => x.SalesInvoiceDate))
            .ForMember(d => d.BusinessPartnerId, opt => opt.MapFrom(x => x.CustomerId))
            .ForMember(d => d.BusinessPartnerCode, opt => opt.MapFrom(x => x.CustomerCode))
            .ForMember(d => d.BusinessPartnerName, opt => opt.MapFrom(x => x.CustomerName))
            .ForMember(d => d.TransactionCurrencyCode, opt => opt.MapFrom(x => x.SalesInvoiceCurrency))
            .ForMember(d => d.ExchangeRate, opt => opt.MapFrom(x => x.ExRate))
            .ForMember(d => d.DiscountAmount, opt => opt.MapFrom(x => x.TotalDiscount))
            .ForMember(d => d.PaymentTerm, opt => opt.MapFrom(x => x.SalesInvoicePaymentTermsDto.FirstOrDefault()))
            .ForMember(d => d.LineItems, opt => opt.MapFrom(x => x.SalesInvoiceItemDto))
            .ForMember(d => d.TransactionDate, opt => opt.MapFrom(x => x.SalesInvoiceDate));
    }
}
