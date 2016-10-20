namespace Timma.Operations.Transactions
{
    internal class Return : Transaction
    {
        public Return(int amount, string printText = "", string baxiArgs = "{}") : base(amount, printText, baxiArgs) { }

        override protected int Type1
        {
            get
            {
                return (int)Type.Return;
            }
        }
    }
}