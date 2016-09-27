namespace Timma.Operations.Transactions
{
    internal class Reversal : Transaction
    {
        public Reversal(int amount, string printText, string payload) : base(amount, printText, payload) { }

        override protected int Type1
        {
            get
            {
                return (int)Type.Reversal;
            }
        }
    }
}