using Chef.Common.Authentication.Models;
using Chef.Common.Data.Repositories;
using Chef.Common.Models;

namespace Chef.Common.Data.Services;

public class CommonDataService : ICommonDataService
{
    private readonly ICommonDataRepository commonDataRepository;

    public CommonDataService(ICommonDataRepository commonDataRepository)
    {
        this.commonDataRepository = commonDataRepository;
    }

    public Task<IEnumerable<BranchViewModel>> GetBranches()
    {
        return commonDataRepository.GetBranches();
    }

    public async Task<IEnumerable<UserBranchDto>> GetMyBranches()
    {
        return await commonDataRepository.GetBranches(HttpHelper.Username);
    }
    public async Task<Company> GetMyCompany()
    {
        var company = await commonDataRepository.GetMyCompany();

        if (company != null && company.Logo != null)
        {
            company.LogoEncoded = System.Text.Encoding.UTF8.GetString(company.Logo);
            company.Logo = null;
        }

        return company;
    }

    public Task<IEnumerable<ReasonCodeMaster>> GetAllReasonCode()
    {
        return commonDataRepository.GetAllReasonCode();
    }

    public Task<CompanyDetails> GetCompanyDetailsForSalesInvoicePrint()
    {
        return commonDataRepository.GetCompanyDetailsForSalesInvoicePrint();
    }
    public async Task<int> UpdateCompanyLogo(Company company)
    {
        if (company.LogoEncoded != null)
            company.Logo = System.Text.Encoding.UTF8.GetBytes(company.LogoEncoded);

        return await commonDataRepository.UpdateCompanyLogo(company);
    }

    public async Task<int> UpdateCompany(Company company)
    {
        if (company.LogoEncoded != null)
            company.Logo = System.Text.Encoding.UTF8.GetBytes(company.LogoEncoded);

        return await commonDataRepository.UpdateCompany(company);
    }

    public  Task<Company> GetCompanyDetailsForVoucherPrint()
    {
        return commonDataRepository.GetCompanyDetailsForVoucherPrint();
    }

    public async Task<IEnumerable<Uom>> GetAllUom()
    {
        return await commonDataRepository.GetAllUom();
    }

    public string ConvertToWords(string numb, string currency)
    {
        string val = "";
        String wholeNo = numb, points = "", andStr = "", pointStr = "";
        String endStr = "Only";
        try
        {
            int decimalPlace = numb.IndexOf(".");
            if (currency == "INR")
            {
                andStr = "Rupees";
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);


                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "Rupees and";// just to separate whole numbers from points/cents    
                        endStr = "Paisa " + endStr;//Cents    
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
            else if(currency == "AED")
            {
                andStr = "Dirham";
                if (decimalPlace > 0)
                {
                    wholeNo = numb.Substring(0, decimalPlace);
                    points = numb.Substring(decimalPlace + 1);

                    if (Convert.ToInt32(points) > 0)
                    {
                        andStr = "Dirham and";// just to separate whole numbers from points/cents    
                        endStr = "Fills " + endStr;//Cents    
                        pointStr = ConvertDecimals(points);
                    }
                }
                val = String.Format("{0} {1}{2} {3}", ConvertWholeNumber(wholeNo).Trim(), andStr, pointStr, endStr);
            }
        }
        catch { }
        return val;
    }

    private string ConvertDecimals(String number)
    {
        String cd = "", digit = "", engOne = "";
        for (int i = 0; i < number.Length; i++)
        {
            digit = number[i].ToString();
            if (i == 1)
            {
                engOne = ones(digit);
            }
            else
            {
                engOne = tens(digit);
            }

            if (!string.IsNullOrEmpty(engOne))
                cd += " " + engOne;
        }
        return cd;
    }

    private string ones(String Number)
    {
        String name = "";
        if (Number != "")
        {
            //}
            int _Number = Convert.ToInt32(Number);

            switch (_Number)
            {

                case 1:
                    name = "One";
                    break;
                case 2:
                    name = "Two";
                    break;
                case 3:
                    name = "Three";
                    break;
                case 4:
                    name = "Four";
                    break;
                case 5:
                    name = "Five";
                    break;
                case 6:
                    name = "Six";
                    break;
                case 7:
                    name = "Seven";
                    break;
                case 8:
                    name = "Eight";
                    break;
                case 9:
                    name = "Nine";
                    break;
            }
        }
        return name;
    }

    private string tens(String Number)
    {
        int _Number = Convert.ToInt32(Number);
        String name = null;
        switch (_Number)
        {
            case 10:
                name = "Ten";
                break;
            case 11:
                name = "Eleven";
                break;
            case 12:
                name = "Twelve";
                break;
            case 13:
                name = "Thirteen";
                break;
            case 14:
                name = "Fourteen";
                break;
            case 15:
                name = "Fifteen";
                break;
            case 16:
                name = "Sixteen";
                break;
            case 17:
                name = "Seventeen";
                break;
            case 18:
                name = "Eighteen";
                break;
            case 19:
                name = "Nineteen";
                break;
            case 20:
                name = "Twenty";
                break;
            case 30:
                name = "Thirty";
                break;
            case 40:
                name = "Fourty";
                break;
            case 50:
                name = "Fifty";
                break;
            case 60:
                name = "Sixty";
                break;
            case 70:
                name = "Seventy";
                break;
            case 80:
                name = "Eighty";
                break;
            case 90:
                name = "Ninety";
                break;
            default:
                if (_Number > 0)
                {
                    name = tens(Number.Substring(0, 1) + "0") + ones(Number.Substring(1));
                }
                break;
        }
        return name;
    }

    private string ConvertWholeNumber(String Number)
    {
        string word = "";
        try
        {
            bool beginsZero = false;//tests for 0XX    
            bool isDone = false;//test if already translated    
            double dblAmt = (Convert.ToDouble(Number));
            //if ((dblAmt > 0) && number.StartsWith("0"))    
            if (dblAmt > 0)
            {//test for zero or digit zero in a nuemric    
                beginsZero = Number.StartsWith("0");

                int numDigits = Number.Length;
                int pos = 0;//store digit grouping    
                String place = "";//digit grouping name:hundres,thousand,etc...    
                switch (numDigits)
                {
                    case 1://ones' range    

                        word = ones(Number);
                        isDone = true;
                        break;
                    case 2://tens' range    
                        word = tens(Number);
                        isDone = true;
                        break;
                    case 3://hundreds' range    
                        pos = (numDigits % 3) + 1;
                        place = " Hundred ";
                        break;
                    case 4://thousands' range    
                    case 5:
                    case 6:
                        pos = (numDigits % 4) + 1;
                        place = " Thousand ";
                        break;
                    case 7://millions' range    
                    case 8:
                    case 9:
                        pos = (numDigits % 7) + 1;
                        place = " Million ";
                        break;
                    case 10://Billions's range    
                    case 11:
                    case 12:

                        pos = (numDigits % 10) + 1;
                        place = " Billion ";
                        break;
                    //add extra case options for anything above Billion...    
                    default:
                        isDone = true;
                        break;
                }
                if (!isDone)
                {//if transalation is not done, continue...(Recursion comes in now!!)    
                    if (Number.Substring(0, pos) != "0" && Number.Substring(pos) != "0")
                    {
                        try
                        {
                            word = ConvertWholeNumber(Number.Substring(0, pos)) + place + ConvertWholeNumber(Number.Substring(pos));
                        }
                        catch { }
                    }
                    else
                    {
                        word = ConvertWholeNumber(Number.Substring(0, pos)) + ConvertWholeNumber(Number.Substring(pos));
                    }

                    //check for trailing zeros    
                    //if (beginsZero) word = " and " + word.Trim();    
                }
                //ignore digit grouping names    
                if (word.Trim().Equals(place.Trim())) word = "";
            }
        }
        catch { }
        return word.Trim();
    }

    public decimal GetChartLimitAmount(decimal maxAmount)
    {
        int startingNumber = maxAmount >= 0 ? Convert.ToInt32(maxAmount.ToString().Substring(0, 1)) + 1 : (Convert.ToInt32(maxAmount.ToString().Substring(1, 1)) + 1) * -1;
        IEnumerable<int> zeros = Enumerable.Repeat(0, Math.Abs(Math.Floor(maxAmount)).ToString().Length - 1);
        return Convert.ToDecimal($"{startingNumber}{string.Join("", zeros)}");
    }
}

