namespace MarsRover.Executer
{
    public interface IExecuter
    {
        void Execute(string inputs);
        string GetOutputs();
    }
}