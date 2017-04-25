using BBS.BAXI;
using Newtonsoft.Json;
using System;

namespace NetsExample.Operations.Admin
{
    abstract class Administration : Operation<AdministrationArgs>
    {
        public override AdministrationArgs Args { get; protected set; }
        public override string PrintText { get; protected set; }

        public Administration(string printText = "", string baxiArgs = "{}")
        {
            AdministrationArgs args = getAdministrationArgs(baxiArgs);
            PrintText = printText;
            setup(args);
        }

        protected abstract int AdmCode { get; }

        private AdministrationArgs getAdministrationArgs(string baxiArgs)
        {
            baxiArgs = String.IsNullOrWhiteSpace(baxiArgs) ? "{}" : baxiArgs;
            return JsonConvert.DeserializeObject<AdministrationArgs>(baxiArgs);
        }

        private void setup(AdministrationArgs args)
        {
            args.OperID = String.IsNullOrWhiteSpace(args.OperID) ? DEFAULT_OPERATOR_ID : args.OperID;
            args.AdmCode = AdmCode;

            Args = args;
        }

    }
}