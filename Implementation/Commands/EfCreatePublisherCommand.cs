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
using static System.Net.Mime.MediaTypeNames;

namespace Implementation.Commands
{
    public class EfCreatePublisherCommand : ICreatePublisherCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly AddPublisherValidator _validations;

        public EfCreatePublisherCommand(BookstoreContext context, IMapper mapper, AddPublisherValidator validations)
        {
            _context = context;
            _mapper = mapper;
            _validations = validations;
        }

        public int Id => 1;

        public string Name => "create publisher";

        public void Execute(PublisherDTO request)
        {
            _validations.ValidateAndThrow(request);

            var publisher = _mapper.Map<Publisher>(request);

            _context.Publishers.Add(publisher);
            _context.SaveChanges();
        }
    }
}
