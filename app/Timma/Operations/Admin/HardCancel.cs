namespace Timma.Operations.Admin
{
    class HardCancel : Administration
    {
        public HardCancel(string printText = "") : base(printText) {}

        protected override int AdmCode
        {
            get
            {
                return (int)Type.HardCancel;
            }
        }
    }
}
