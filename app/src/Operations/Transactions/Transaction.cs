using BBS.BAXI;
using Newtonsoft.Json;
using System;

namespace NetsExample.Operations.Transactions
{
    abstract class Transaction : Operation<TransferAmountArgs>
    {
        public override TransferAmountArgs Args { get; protected set; }

        public Transaction (string baxiArgs = "{}") : this(0, baxiArgs) { }

        public Transaction(int amount, string baxiArgs = "{}")
        {
            TransferAmountArgs args = getTransactionArgs(baxiArgs);
            args.Amount1 = args.Amount1 == 0 ? amount : args.Amount1;
            setup(args);
        }

        private TransferAmountArgs getTransactionArgs(string baxiArgs)
        {
            baxiArgs = String.IsNullOrWhiteSpace(baxiArgs) ? "{}" : baxiArgs;
            return JsonConvert.DeserializeObject<TransferAmountArgs>(baxiArgs);
        }

        private void setup (TransferAmountArgs args)
        {
            args.OperID = String.IsNullOrWhiteSpace(args.OperID) ? DEFAULT_OPERATOR_ID : args.OperID;
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