using AutoMapper;
using Chef.Finance.Integration.Models;

namespace Chef.Finance.Integration.Mapping;

public  class ItemTransactionMappingProfile:Profile
{
    public ItemTransactionMappingProfile()
    {
        CreateMap<IntegrationDetailDimension, ItemTransactionFinanceDetailsDimension>();

        CreateMap<IntegrationDetailDimension, DetailDimension>().ForMember(d => d.allocatedAmount, opt => opt.MapFrom(x => x.CreditAmount != 0 && x.CreditAmount != null ? x.CreditAmount : x.DebitAmount));


        CreateMap<ItemTransactionFinanceDTO, ItemViewModel>().ForMember(d => d.ItemCategoryId, opt => opt.MapFrom(x => x.ItemCategory))
        .ForMember(d => d.ItemTypeId, opt => opt.MapFrom(x => x.ItemType));





        CreateMap<ItemTransactionFinanceLineCost, ItemViewModel>().ForMember(d => d.ItemCategoryId, opt => opt.MapFrom(x => x.ItemCategory))
         .ForMember(d => d.ItemTypeId, opt => opt.MapFrom(x => x.ItemType));

        CreateMap<List<ItemTransactionFinanceDTO>, TradingIntegrationHeader>().ForMember(d => d.businesspartnerid, opt => opt.MapFrom(x => x.First().BpId))
            .ForMember(d => d.businesspartnername, opt => opt.MapFrom(x => x.First().BpName))
            .ForMember(d => d.businesspartnercode, opt => opt.MapFrom(x => x.First().BpName))
            .ForMember(d => d.BranchId, opt=>opt.MapFrom(x => x.First().BranchId))
            .ForMember(d => d.exchangerate, opt=>opt.MapFrom(x =>x.First().ExRate))
            .ForMember(d => d.transorginid, opt=>opt.MapFrom(x=>x.First().TransOrginId))
            .ForMember(d => d.transorgin, opt => opt.MapFrom(x => x.First().TransOrgin))
            .ForMember(d => d.TransactionDate, opt => opt.MapFrom(x => x.First().TrasnDate))
            .ForMember(d => d.transtypeid, opt => opt.MapFrom(x => x.First().TransTypeId))
            .ForMember(d => d.transtype, opt => opt.MapFrom(x => x.First().TransType))
            .ForMember(d => d.transid, opt => opt.MapFrom(x => x.First().TransId))
            .ForMember(d => d.company, opt => opt.MapFrom(x => x.First().Company))
            .ForMember(d => d.currency, opt => opt.MapFrom(x => x.First().Currency))
            .ForMember(d => d.referencenumber, opt => opt.MapFrom(x => x.First().TrasnOrderNum))
            .ForMember(d => d.transtypeslno, opt => opt.MapFrom(x => x.First().TrasnTypeSlNo))
            .ForMember(d => d.remark, opt => opt.MapFrom(x => x.First().TransRemark))
            .ForMember(d => d.totalamount, opt => opt.MapFrom(x => x.Select(j => j.TransAmount).Sum()));




        CreateMap<TradingIntegrationHeaderDetailsViewModel, GeneralLedger>().ForMember(d => d.RefenceDocumentId, opt => opt.MapFrom(x => x.TradingIntegrationHeaderId))
                    .ForMember(d => d.BusinessPartnerId, opt => opt.MapFrom(x => x.businesspartnerid))
                    .ForMember(d => d.DocumentType, opt => opt.MapFrom(x => x.documentType))
                    .ForMember(d => d.DocumentNumber, opt => opt.MapFrom(x => x.documentnumber))
                    .ForMember(d => d.DocumentDate, opt => opt.MapFrom(x => x.TransactionDate))
                    .ForMember(d => d.TransactionDate, opt => opt.MapFrom(x => x.TransactionDate))
                    .ForMember(d => d.JournalBookCode, opt => opt.MapFrom(x => x.journalbookcode))
                    .ForMember(d => d.JournalBookId, opt => opt.MapFrom(x => x.journalbookid))
                    .ForMember(d => d.TransactionCurrencyCode, opt => opt.MapFrom(x => x.currency))
                    .ForMember(d => d.ExchangeRate, opt => opt.MapFrom(x => x.exchangerate))
                    .ForMember(d => d.Narration, opt => opt.MapFrom(x => x.narration))
                    .ForMember(d => d.DebitAmountInBaseCurrency, opt => opt.MapFrom(x => x.debitamountinbasecurrency))
                    .ForMember(d => d.CreditAmountInBaseCurrency, opt => opt.MapFrom(x => x.creditamountinbasecurrency))
                    .ForMember(d => d.BranchId, opt => opt.MapFrom(x => x.BranchId))
                    .ForMember(d => d.LedgerAccountId, opt => opt.MapFrom(x => x.ledgeraccountid))
                    .ForMember(d => d.LedgerAccountCode, opt => opt.MapFrom(x => x.ledgeraccountcode))
                    .ForMember(d => d.BusinessPartnerCode, opt => opt.MapFrom(x => x.businesspartnercode))
                    .ForMember(d => d.LedgerAccountName, opt => opt.MapFrom(x => x.ledgeraccountname))
                    .ForMember(d => d.BusinessPartnerCode, opt => opt.MapFrom(x => x.businesspartnercode))
                    .ForMember(d => d.CreditAmount, opt => opt.MapFrom(x => x.creditamount))
                    .ForMember(d => d.DebitAmount, opt => opt.MapFrom(x => x.debitamount))
                    .ForMember(d => d.RefenceDocumentDetailId, opt => opt.MapFrom(x => x.integrationheaderid))
                    .ForMember(d => d.FinancialYearId, opt => opt.MapFrom(x => x.FinancialYearId));


    }
}
