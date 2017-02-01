namespace XMLFeedsViewer.Services.Business.Contracts
{
    public interface INotifier
    {
        void Notify<T>(T data);
    }
}
