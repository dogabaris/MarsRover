using Autofac;
using MarsRover.Executer;
using System;
using System.Reflection;
using System.Text;

namespace MarsRover
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var containerBuilder = RegisterServices();
            using (var container = containerBuilder.Build())
            {
                var executer = container.Resolve<IExecuter>();
                var caseStudyInputs = CreateCaseStudyInput();
                executer.Execute(caseStudyInputs);
                var caseStudyOutputs = executer.GetOutputs();
                WriteCaseStudyToConsole(caseStudyInputs, caseStudyOutputs);
            }  
        }

        public static void WriteCaseStudyToConsole(string inputs, string outputs)
        {
            Console.WriteLine("Test Input:");
            Console.WriteLine(inputs);
            Console.WriteLine();
            Console.WriteLine("Expected Output:");
            Console.WriteLine(outputs);
            Console.ReadLine();
        }

        public static string CreateCaseStudyInput()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendLine("5 5");
            strBuilder.AppendLine("1 2 N");
            strBuilder.AppendLine("LMLMLMLMM");
            strBuilder.AppendLine("3 3 E");
            strBuilder.Append("MMRMMRMRRM");
            var inputs = strBuilder.ToString();

            return inputs;
        }

        public static ContainerBuilder RegisterServices()
        {
            var programAssembly = Assembly.GetExecutingAssembly();

            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(programAssembly)
                .AsImplementedInterfaces();

            return builder;
        }
    }
}