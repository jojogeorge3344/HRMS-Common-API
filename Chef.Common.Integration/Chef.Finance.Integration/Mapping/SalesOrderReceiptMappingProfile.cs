
using AutoMapper;
using Chef.Common.Types;
using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration.Mapping;

public class SalesOrderReceiptMappingProfile : Profile
{
    public SalesOrderReceiptMappingProfile()
    {
        CreateMap<SalesOrderReceiptDto, ReceiptRegister>()
                 .ForMember(d => d.ReceiptCategory, opt => opt.MapFrom(x => x.IsRetail == true ? ReceiptCategory.OneTimeReceipt : ReceiptCategory.CustomerReceiptAgainstInvoices))
                .ForMember(d => d.PaymentMethodType, opt => opt.MapFrom(x =>PaymentMethodType.Cash))
                .ForMember(d => d.DocumentDate, opt => opt.MapFrom(x => x.ReceiptDate))
                .ForMember(d => d.TransactionDate, opt => opt.MapFrom(x => x.ReceiptDate))
                .ForMember(d => d.IsCorporateGroup, opt => opt.MapFrom(x =>false))
                .ForMember(d => d.IsLinkSalesOrder, opt => opt.MapFrom(x => false))
                .ForMember(d => d.IsHoldReceipt, opt => opt.MapFrom(x =>false))
                .ForMember(d => d.BalanceAmount, opt => opt.MapFrom(x => x.TotalAmount))
                .ForMember(d => d.BalanceAmountInBaseCurrency, opt => opt.MapFrom(x => x.TotalAmountInBaseCurrency))
                .ForMember(d => d.ProcessedStatus, opt => opt.MapFrom(x => ReceiptStatusType.Unprocessed))
                .ForMember(d => d.ApproveStatus, opt => opt.MapFrom(x => ApproveStatus.Draft))
                .ForMember(d => d.DocumentType, opt => opt.MapFrom(x =>DocumentType.ReceiptRegister))
                .ForMember(d => d.AmountInBankCurrency, opt => opt.MapFrom(x => x.TotalAmountInBaseCurrency)) 
                .ForMember(d => d.BalanceAmountInBaseCurrency, opt => opt.MapFrom(x => x.TotalAmountInBaseCurrency));

        CreateMap<SalesOrderReceiptDto, CustomerCashReceipt>()
               .ForMember(d => d.Name, opt => opt.MapFrom(x => x.BusinessPartnerName == null || x.BusinessPartnerName == "" ? x.CustomerName : x.BusinessPartnerName));

               


        CreateMap<SalesOrderReceiptDto, PaymentAdvice>()
            .ForMember(d => d.PaymentAdviceType, opt => opt.MapFrom(x =>
             ReceiptCategory.OnaccountRepaymenttoCustomers))
            .ForMember(d => d.PaymentMethodType, opt => opt.MapFrom(x => PaymentMethodType.Cash))
            .ForMember(d => d.TransactionDate, opt => opt.MapFrom(x => x.ReceiptDate))
            .ForMember(d => d.CurrencyCode, opt => opt.MapFrom(x => x.TransactionCurrencyCode))
            .ForMember(d => d.DocumentDate, opt => opt.MapFrom(x => x.ReceiptDate))
            .ForMember(d => d.DocumentStatus, opt => opt.MapFrom(x => DocumentStatus.Pending))
            .ForMember(d => d.DocumentStatusName, opt => opt.MapFrom(x => DocumentStatus.Pending.ToString()))
            .ForMember(d => d.DocumentType, opt => opt.MapFrom(x => DocumentType.SupplierOtherPayments))
            .ForMember(d => d.IsMaximumBudgetApplicable, opt => opt.MapFrom(x => false))
            .ForMember(d => d.TransactionDate, opt => opt.MapFrom(x => DateTime.UtcNow));
    }
}