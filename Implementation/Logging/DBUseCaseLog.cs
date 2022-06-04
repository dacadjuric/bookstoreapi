using Application;
using DataAccess;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Logging
{
    public class DBUseCaseLog : IUseCaseLogger
    {
        private readonly BookstoreContext _context;

        public DBUseCaseLog(BookstoreContext context)
        {
            _context = context;
        }

        public void Log(IUseCase userCase, IApplicatioActor actor, object useCaseData)
        {
            _context.AuditLogs.Add(new AuditLog
            {
                Actor = actor.Identity,
                Data = JsonConvert.SerializeObject(useCaseData),
                Date = DateTime.UtcNow,
                UseCaseName = userCase.Name
            });

            _context.SaveChanges();
        }
    }
}
