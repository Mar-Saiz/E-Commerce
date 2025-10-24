namespace E_Commerce.Data.Core
{
    public class OperationResult<TData> where TData : class //pattern based on Operations
    {
        public OperationResult()
        {
            this.Success = true;
        }

        public bool Success { get; set; }
        public string? Message { get; set; }
        public TData? Result { get; set; }
    }
}
