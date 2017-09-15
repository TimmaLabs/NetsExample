namespace NetsExample.Operations.Admin
{
    internal class LatestTransactionReport : Administration
    {
        public LatestTransactionReport(string baxiArgs = "{}") : base(baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.LatestFinancialTransactionReceipt;
            }
        }
    }
}
