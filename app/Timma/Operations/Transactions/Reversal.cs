namespace Timma.Operations.Transactions
{
    internal class Reversal : Transaction
    {
        public Reversal(int amount, string printText, string baxiArgs) : base(amount, printText, baxiArgs) { }

        override protected int Type1
        {
            get
            {
                return (int)Type.Reversal;
            }
        }
    }
}