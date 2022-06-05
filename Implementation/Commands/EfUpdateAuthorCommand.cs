using Application.Commands;
using Application.DataTransfer;
using Application.Exceptions;
using AutoMapper;
using DataAccess;
using Domain;
using FluentValidation;
using Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Commands
{
    public class EfUpdateAuthorCommand : IUpdateAuthorCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly UpdateAuthorValidator _validations;

        public EfUpdateAuthorCommand(BookstoreContext context, IMapper mapper, UpdateAuthorValidator validations)
        {
            _context = context;
            _mapper = mapper;
            _validations = validations;
        }
        public int Id => 1;

        public string Name => "update author";

        public void Execute(AuthorDTO request)
        {
            if(_context.Authors.Find(request) == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Author));
            }

            _validations.ValidateAndThrow(request);

            var author = _context.Authors.Find(request.Id);

            _mapper.Map(request, author);
            _context.SaveChanges();
        }
    }
}
