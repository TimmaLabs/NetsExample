namespace Timma.Operations.Transactions
{
    internal class Return : Transaction
    {
        public Return(int amount, string printText = "", string payload = "{}") : base(amount, printText, payload) { }

        override protected int Type1
        {
            get
            {
                return (int)Type.Return;
            }
        }
    }
}