namespace Timma.Operations.Admin
{
    class Reconciliation : Administration
    {
        public Reconciliation(string printText = "", string payload = "{}") : base(printText, payload) { }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.Reconciliation;
            }
        }
    }
}
