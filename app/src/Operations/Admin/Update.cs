namespace NetsExample.Operations.Admin
{
    class Update : Administration
    {
        public Update(string baxiArgs = "{}") : base(baxiArgs) {}

        protected override int AdmCode
        {
            get
            {
                return (int)Type.SoftwareDownload;
            }
        }
    }
}
