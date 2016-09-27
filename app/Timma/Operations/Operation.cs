using Timma.Print;

namespace Timma.Operations
{
    abstract class Operation<T> : IOperation
    {
        protected readonly string DEFAULT_OPERATOR_ID = "0000";
        public abstract string PrintText { get; protected set; }
        public abstract T Args { get; protected set; }

        public virtual Document GenerateDocument(string baxiTxt)
        {
            baxiTxt = string.IsNullOrWhiteSpace(baxiTxt) ? EmptyPrintText : baxiTxt;
            return string.IsNullOrWhiteSpace(PrintText) ? DefaultDocument(baxiTxt) : CustomDocument(baxiTxt);
        }

        private Document CustomDocument(string baxiTxt)
        {
            // TODO: Implement support for custom print templates...
            throw new System.NotImplementedException("TODO: implement support for custom print templates.");
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
        Document GenerateDocument(string baxiTxt);
    }
}
