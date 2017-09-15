namespace NetsExample.Operations.Transactions
{
    internal class Purchase : Transaction
    {
        public Purchase(int amount, string baxiArgs = "{}") : base(amount, baxiArgs) {

        }

        override protected int Type1
        {
            get
            {
                return (int)Type.Purchase;
            }
        }
    }
}