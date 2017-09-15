namespace NetsExample.Operations
{
    abstract class Operation<T>
    {
        protected readonly string DEFAULT_OPERATOR_ID = "0000";
        public abstract T Args { get; protected set; }
    }
}
