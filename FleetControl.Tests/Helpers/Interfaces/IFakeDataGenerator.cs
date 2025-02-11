namespace FleetControl.Tests.Helpers.Interfaces
{
    public interface IFakeDataGenerator<T>
    {
        T Generate();
        IList<T> GenerateList(int quantity);
    };
}
