using BBS.BAXI;
using Newtonsoft.Json;
using System;

namespace Timma.Operations.Admin
{
    abstract class Administration : Operation<AdministrationArgs>
    {
        public override AdministrationArgs Args { get; protected set; }
        public override string PrintText { get; protected set; }

        public Administration(string printText = "", string payload = "{}")
        {
            AdministrationArgs args = getAdministrationArgs(payload);
            PrintText = printText;
            setup(args);
        }

        protected abstract int AdmCode { get; }

        private AdministrationArgs getAdministrationArgs(string payload)
        {
            payload = String.IsNullOrWhiteSpace(payload) ? "{}" : payload;
            return JsonConvert.DeserializeObject<AdministrationArgs>(payload);
        }

        private void setup(AdministrationArgs args)
        {
            args.OperID = args.OperID ?? DEFAULT_OPERATOR_ID;
            args.AdmCode = AdmCode;

            Args = args;
        }

    }
}