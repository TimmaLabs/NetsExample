namespace Timma.Operations.Admin
{
    internal class EOTReport : Administration
    {
        public EOTReport(string printText = "") : base(printText) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.EOT;
            }
        }
    }
}