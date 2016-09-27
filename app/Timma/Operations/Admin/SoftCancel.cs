namespace Timma.Operations.Admin
{
    class SoftCancel : Administration
    {
        public SoftCancel(string printText = "") : base(printText) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.SoftCancel;
            }
        }
    }
}
