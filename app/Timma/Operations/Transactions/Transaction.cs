using BBS.BAXI;
using Newtonsoft.Json;
using System;

namespace Timma.Operations.Transactions
{
    abstract class Transaction : Operation<TransferAmountArgs>
    {
        public override TransferAmountArgs Args { get; protected set; }
        public override string PrintText { get; protected set; }

        public Transaction (string printText = "", string baxiArgs = "{}") : this(0, printText, baxiArgs) { }

        public Transaction(int amount, string printText = "", string baxiArgs = "{}")
        {
            TransferAmountArgs args = getTransactionArgs(baxiArgs);
            args.Amount1 = args.Amount1 == 0 ? amount : args.Amount1;
            PrintText = printText;
            setup(args);
        }

        private TransferAmountArgs getTransactionArgs(string baxiArgs)
        {
            baxiArgs = string.IsNullOrWhiteSpace(baxiArgs) ? "{}" : baxiArgs;
            return JsonConvert.DeserializeObject<TransferAmountArgs>(baxiArgs);
        }

        private void setup (TransferAmountArgs args)
        {
            args.OperID = args.OperID ?? DEFAULT_OPERATOR_ID;

            args.Type1 = Type1;
            args.Type2 = Type2;
            args.Type3 = Type3;

            Args = args;
        }

        protected abstract int Type1 { get; }

        protected virtual int Type2
        {
            get
            {
                return (int)Type.NotInUse;
            }
        }

        protected virtual int Type3
        {
            get
            {
                return (int)Type.NotInUse;
            }
        }

    }
}