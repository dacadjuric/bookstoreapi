using Application;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Logging
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public void Log(IUseCase userCase, IApplicatioActor actor, object data)
        {
            Console.WriteLine($"{DateTime.Now}: {actor.Identity} is trying to execute {userCase.Name} using data: " +
                $"{JsonConvert.SerializeObject(data)}");
        }
    }
}
