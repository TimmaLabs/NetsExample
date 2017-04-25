namespace NetsExample.Operations.Admin
{
    internal class EOTReport : Administration
    {
        public EOTReport(string printText = "", string baxiArgs = "{}") : base(printText, baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.EOT;
            }
        }
    }
}