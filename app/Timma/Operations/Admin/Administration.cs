using BBS.BAXI;
using Newtonsoft.Json;
using System;

namespace Timma.Operations.Admin
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
            baxiArgs = string.IsNullOrWhiteSpace(baxiArgs) ? "{}" : baxiArgs;
            return JsonConvert.DeserializeObject<AdministrationArgs>(baxiArgs);
        }

        private void setup(AdministrationArgs args)
        {
            args.OperID = args.OperID ?? DEFAULT_OPERATOR_ID;
            args.AdmCode = AdmCode;

            Args = args;
        }

    }
}