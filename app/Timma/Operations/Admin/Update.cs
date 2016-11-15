namespace Timma.Operations.Admin
{
    class Update : Administration
    {
        public Update() : base() { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.SoftwareDownload;
            }
        }
    }
}
