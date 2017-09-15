namespace NetsExample.Operations.Admin
{
    class Reconciliation : Administration
    {
        public Reconciliation(string baxiArgs = "{}") : base(baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.Reconciliation;
            }
        }
    }
}
