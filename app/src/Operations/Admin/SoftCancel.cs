namespace NetsExample.Operations.Admin
{
    class SoftCancel : Administration
    {
        public SoftCancel(string printText = "", string baxiArgs = "{}") : base(printText, baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.SoftCancel;
            }
        }
    }
}
