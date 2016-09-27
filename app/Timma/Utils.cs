namespace Timma
{
    class Utils
    {
        public static string OptionalData(int autodcc = 1, int merch = -1, string txnref = "")
        {
            string autodccStr = @", ""autodcc"":" + autodcc;
            string merchStr = merch < 0 ? string.Empty : @", ""merch"":" + merch;
            string txnrefStr = string.IsNullOrWhiteSpace(txnref) ? string.Empty : @", ""txnref"":""" + txnref + @"""";
            string o = @"{ ""ver"": ""1.00""" + autodccStr + merchStr + txnrefStr + " }";
            return @"{ ""od"": { ""ver"": ""1.01"", ""nets"": { ""ver"": ""1.00"", ""ch13"": { ""ver"": ""1.00"", ""ta"": { ""ver"": ""1.00"", ""o"":" + o + " } } } } }";
        }
    }
}
