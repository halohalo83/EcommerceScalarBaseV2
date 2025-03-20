using System.ComponentModel;

namespace Domain.Common.Enums;

public enum PaymentMethod
{
    [Description("Cash")]
    Cash = 1,

    [Description("Credit Card")]
    CreditCard = 2,

    [Description("Debit Card")]
    DebitCard = 3,

    [Description("Bank Transfer")]
    BankTransfer = 4
}

public enum OrderStatus
{
    [Description("Processing")]
    Processing = 1,
    [Description("Shipped")]
    Shipped = 2,
    [Description("Delivered")]
    Delivered = 3,
    [Description("Cancelled")]
    Cancelled = 4
}