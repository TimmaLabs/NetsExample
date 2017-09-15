namespace NetsExample.Operations.Admin
{
    class HardCancel : Administration
    {
        public HardCancel(string baxiArgs = "{}") : base(baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.HardCancel;
            }
        }
    }
}
