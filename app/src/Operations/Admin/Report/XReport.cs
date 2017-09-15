namespace NetsExample.Operations.Admin
{
    internal class XReport : Administration
    {
        public XReport(string baxiArgs = "{}") : base(baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.XReport;
            }
        }
    }
}