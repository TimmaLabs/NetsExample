namespace NetsExample.Operations.Admin
{
    class SoftCancel : Administration
    {
        public SoftCancel(string baxiArgs = "{}") : base(baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.SoftCancel;
            }
        }
    }
}
