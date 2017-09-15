namespace NetsExample.Operations.Admin
{
    internal class ZReport : Administration
    {
        public ZReport(string baxiArgs = "{}") : base(baxiArgs) {}

        protected override int AdmCode
        {
            get
            {
                return (int)Type.ZReport;
            }
        }
    }
}