namespace Timma.Operations.Admin
{
    internal class LatestTransactionReport : Administration
    {
        public LatestTransactionReport(string printText = "") : base(printText) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.LatestFinancialTransactionReceipt;
            }
        }

        protected override string EmptyPrintText { get; } = "No recent transactions found.";
    }
}
