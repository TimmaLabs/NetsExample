namespace Timma.Operations.Transactions
{
    internal class Purchase : Transaction
    {
        public Purchase(int amount, string printText = "", string baxiArgs = "{}") : base(amount, printText, baxiArgs) {

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