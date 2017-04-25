namespace NetsExample.Print
{
    class BlankRow : TextRow
    {
        internal static readonly int LARGE = 15; // Max is 20 as per Baxi API spec
        internal static readonly int SMALL = 5;

        public BlankRow(int lines = -1) : base("")
        {
            Blank = lines < 0 ? LARGE : lines;
        }
    }
}
