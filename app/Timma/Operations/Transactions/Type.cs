namespace Timma.Operations.Transactions
{
    public enum Type
    {
        // Implemented operations
        Purchase = 0x30,
        Return = 0x31,
        Reversal = 0x32,
        // Type2 and Type3 specific types:
        VAT = 0x32,
        NotInUse = 0x30,

        //
        // Extra (not implemented)
        //
        CashbackPurchase = 0x33,
        Adjustment = 0x35,
        Balance = 0x36,
        Tip = 0x37,
        Deposit = 0x38, // useful for e.g. gift cards
        Withdrawal = 0x39,
        LoadECard = 0x3A,
        TopUpPurchase = 0x3B,
        TopUpReversal = 0x3C,
        TopUpCorrection = 0x3D,
        Offline = 0x40, // useful if web app is offline
        PrePurchaseIncremental = 0x41,
        // Pre-purchase
        PrePurchase = 0x34,
        PrePurchaseReversal = 0x42,
        PrePurchaseComplete = 0x43
    }
}
