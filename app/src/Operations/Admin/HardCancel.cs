namespace NetsExample.Operations.Admin
{
    class HardCancel : Administration
    {
        public HardCancel(string printText = "", string baxiArgs = "{}") : base(printText, baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.HardCancel;
            }
        }
    }
}
