namespace NetsExample.Operations.Admin
{
    internal class XReport : Administration
    {
        public XReport(string printText = "", string baxiArgs = "{}") : base(printText, baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.XReport;
            }
        }
    }
}