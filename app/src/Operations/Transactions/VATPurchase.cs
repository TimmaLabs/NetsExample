using System;

namespace NetsExample.Operations.Transactions
{
    internal class VATPurchase : Transaction
    {
        public VATPurchase(int amount, int VATamount, string baxiArgs = "{}") : base(amount, baxiArgs)
        {
            Args.Amount3 = Args.Amount3 == 0 ? VATamount : Args.Amount3;
        }

        override protected int Type1
        {
            get
            {
                return (int)Type.Purchase;
            }
        }

        override protected int Type3
        {
            get
            {
                return (int)Type.VAT;
            }
        }
    }
}