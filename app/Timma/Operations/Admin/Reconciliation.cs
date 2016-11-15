namespace Timma.Operations.Admin
{
    class Reconciliation : Administration
    {
        public Reconciliation(string printText = "", string baxiArgs = "{}") : base(printText, baxiArgs) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.Reconciliation;
            }
        }
    }
}
