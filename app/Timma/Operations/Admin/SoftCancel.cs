namespace Timma.Operations.Admin
{
    class SoftCancel : Administration
    {
        public SoftCancel() : base() { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.SoftCancel;
            }
        }
    }
}
