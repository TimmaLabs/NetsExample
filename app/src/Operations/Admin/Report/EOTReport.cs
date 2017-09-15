namespace NetsExample.Operations.Admin
{
    internal class EOTReport : Administration
    {
        public EOTReport(string baxiArgs = "{}") : base(baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.EOT;
            }
        }
    }
}