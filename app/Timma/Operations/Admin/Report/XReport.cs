namespace Timma.Operations.Admin
{
    internal class XReport : Administration
    {
        public XReport(string printText = "") : base(printText) {}

        protected override int AdmCode
        {
            get
            {
                return (int)Type.XReport;
            }
        }
    }
}