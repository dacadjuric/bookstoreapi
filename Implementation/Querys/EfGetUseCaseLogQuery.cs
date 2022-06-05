using Application.DataTransfer;
using Application.Queries;
using Application.Search;
using AutoMapper;
using DataAccess;
using Domain;
using Implementation.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Implementation.Querys
{
    public class EfGetUseCaseLogQuery : IGetUseCaseLogQuery
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;

        public EfGetUseCaseLogQuery(BookstoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public int Id => 1;

        public string Name => "Get use case logs";

        public PaginationResponse<UseCaseLogDTO> Execute(UseCaseLogSearch search)
        {
            var query = _context.AuditLogs.AsQueryable();

            switch (search.SearchBy)
            {
                case "datetime":break;
                case "actor":
                    if (!string.IsNullOrEmpty(search.Text) || !string.IsNullOrWhiteSpace(search.Text))
                    {
                        query = query.Where(x => x.Actor.ToLower().Contains(search.Text.ToLower()));
                    }
                    break;
                case "usecasename":
                    if (!string.IsNullOrEmpty(search.Text) || !string.IsNullOrWhiteSpace(search.Text))
                    {
                        query = query.Where(x => x.UseCaseName.ToLower().Contains(search.Text.ToLower()));
                    }
                    break;
            }

            //return query.Paged<UseCaseLogDTO, UseCaseLog>(search, _mapper);
            return null;
        }
    }
}
