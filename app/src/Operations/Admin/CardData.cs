namespace NetsExample.Operations.Admin
{
    class CardData : Administration
    {
        public CardData(string baxiArgs = "{}") : base(baxiArgs)
        {
        }

        protected override int AdmCode
        {
            get
            {
                return (int)Type.DatasetDownload;
            }
        }
    }
}
