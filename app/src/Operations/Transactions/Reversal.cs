namespace NetsExample.Operations.Transactions
{
    internal class Reversal : Transaction
    {
        public Reversal(int amount, string baxiArgs) : base(amount, baxiArgs) { }

        override protected int Type1
        {
            get
            {
                return (int)Type.Reversal;
            }
        }
    }
}