namespace E_Commerce.Data.ViewModels
{
    public abstract class IBaseViewModel<Type>
    {
        public abstract Type Id { get; set; }

    }
}
