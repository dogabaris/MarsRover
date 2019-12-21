namespace MarsRover.Executer
{
    public interface IOrder
    {
        void Run();
        OrderType GetOrderType();
    }
}
