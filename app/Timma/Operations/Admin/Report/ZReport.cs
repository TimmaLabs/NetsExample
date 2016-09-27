namespace Timma.Operations.Admin
{
    internal class ZReport : Administration
    {
        public ZReport(string printText = "") : base(printText) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.ZReport;
            }
        }
    }
}