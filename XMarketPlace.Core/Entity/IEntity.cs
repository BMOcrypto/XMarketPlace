namespace XMarketPlace.Core.Entity
{
    public interface IEntity<T>
    {
        T ID { get; set; } // Bu yazıma göre ID değeri artık gönderilen tip neyse o şekilde oluşacak.
    }
}