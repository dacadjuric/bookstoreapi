using Application.Commands;
using Application.DataTransfer;
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
    public class EfCreateAuthorCommand : ICreateAuthorCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly AddAuthorValidator _validations;

        public EfCreateAuthorCommand(BookstoreContext context, IMapper mapper, AddAuthorValidator validations)
        {
            _context = context;
            _mapper = mapper;
            _validations = validations;
        }

        public int Id => 1;

        public string Name => "create authro";
        public void Execute(AuthorDTO request)
        {
            _validations.ValidateAndThrow(request);

            var author = _mapper.Map<Author>(request);

            _context.Authors.Add(author);
            _context.SaveChanges();
        }

    }
}
