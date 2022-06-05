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
    public class EfUpdateBookCommand : IUpdateBookCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly UpdateBookValidator _validations;

        public EfUpdateBookCommand(BookstoreContext context, IMapper mapper, UpdateBookValidator validations)
        {
            _context = context;
            _mapper = mapper;
            _validations = validations;
        }
        public int Id => 1;

        public string Name => "update books ";

        public void Execute(BookDTO request)
        {
            if(_context.Books.Find(request.Id) == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Book));
            }

            _validations.ValidateAndThrow(request);

            var book = _context.Books.Find(request.Id);

            _mapper.Map(request, book);
            _context.SaveChanges();
        }
    }
}
