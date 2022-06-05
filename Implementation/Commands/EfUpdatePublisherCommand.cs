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
    public class EfUpdatePublisherCommand : IUPdatePublisherCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly UpdatePublisherValidator _validations;

        public EfUpdatePublisherCommand(BookstoreContext context, IMapper mapper, UpdatePublisherValidator validations)
        {
            _context = context;
            _mapper = mapper;
            _validations = validations;
        }

        public int Id => 1;

        public string Name => "update publisher";

        public void Execute(PublisherDTO request)
        {
            if (_context.Publishers.Find(request.Id) == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Publisher));
            }

            _validations.ValidateAndThrow(request);

            var publisher = _context.Publishers.Find(request.Id);

            _mapper.Map(request, publisher);
            _context.SaveChanges();
        }
    }
}
