namespace NetsExample.Operations.Transactions
{
    internal class Return : Transaction
    {
        public Return(int amount, string baxiArgs = "{}") : base(amount, baxiArgs) { }

        override protected int Type1
        {
            get
            {
                return (int)Type.Return;
            }
        }
    }
}