namespace Timma.Operations.Transactions
{
    internal class Purchase : Transaction
    {
        public Purchase(int amount, string printText = "", string payload = "{}") : base(amount, printText, payload) {

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