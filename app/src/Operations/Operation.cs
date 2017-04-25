using NetsExample.Print;

namespace NetsExample.Operations
{
    abstract class Operation<T> : IOperation
    {
        protected readonly string DEFAULT_OPERATOR_ID = "0000";
        public abstract string PrintText { get; protected set; }
        public abstract T Args { get; protected set; }

        public virtual string GenerateDocument(string baxiTxt)
        {
            baxiTxt = string.IsNullOrWhiteSpace(baxiTxt) ? EmptyPrintText : baxiTxt;
            // TODO: ideally this logic would be encapsulated by Document
            if (string.IsNullOrWhiteSpace(PrintText))
            {
                var doc = DefaultDocument(baxiTxt);
                return Newtonsoft.Json.JsonConvert.SerializeObject(doc);
            }
            return CustomDocument(baxiTxt);
        }

        private string CustomDocument(string baxiTxt)
        {
            return PrintText.Replace("{{baxiTxt}}", baxiTxt);
        }

        private Document DefaultDocument(string baxiTxt)
        {
            var logo = new Logo();
            var smallBlank = new BlankRow(BlankRow.SMALL);
            var baxi = new TextRow(baxiTxt);
            var blank = new BlankRow();
            return new Document(logo, smallBlank, baxi, blank);
        }

        protected virtual string EmptyPrintText { get; } = "";
    }

    internal interface IOperation
    {
        string GenerateDocument(string baxiTxt);
    }
}
