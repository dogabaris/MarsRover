using Autofac;
using MarsRover.Executer;
using System;
using System.Reflection;
using System.Text;

namespace MarsRover
{
    public class Program
    {
        static void Main(string[] args)
        {
            var containerBuilder = RegisterServices();
            using (var container = containerBuilder.Build())
            {
                var executer = container.Resolve<IExecuter>();
                var caseStudyInputs = CreateCaseStudyInput();
                executer.Execute(caseStudyInputs);
            }  
        }

        public static void WriteCaseStudyToConsole(string inputs, string outputs)
        {
            Console.WriteLine("Inputs :");
            Console.WriteLine(inputs);
            Console.WriteLine();
            Console.WriteLine("Outputs :");
            Console.WriteLine(outputs);
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