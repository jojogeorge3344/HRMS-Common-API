using System.ComponentModel;

namespace Chef.Common.Models;

/// <summary>
/// Holds Expense Policy
/// </summary>
public enum ExpensePolicyType
{
    [Description("Accommodation Claims")]
    AccommodationClaims = 1,

    [Description("Food")]
    Food,

    [Description("Flight Tickets")]
    FlightTickets,
}
