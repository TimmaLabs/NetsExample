namespace NetsExample.Operations.Admin
{
    class HardCancel : Administration
    {
        public HardCancel() : base() {}

        protected override int AdmCode
        {
            get
            {
                return (int)Type.HardCancel;
            }
        }
    }
}
