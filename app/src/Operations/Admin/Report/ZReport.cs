namespace NetsExample.Operations.Admin
{
    internal class ZReport : Administration
    {
        public ZReport(string printText = "", string baxiArgs = "{}") : base(printText, baxiArgs) {}

        protected override int AdmCode
        {
            get
            {
                return (int)Type.ZReport;
            }
        }
    }
}