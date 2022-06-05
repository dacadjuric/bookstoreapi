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
    public class EfUpdateImageCommand : IUpdateImageCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly UpdateImageValidator _validations;

        public EfUpdateImageCommand(BookstoreContext context, IMapper mapper, UpdateImageValidator validations)
        {
            _context = context;
            _mapper = mapper;
            _validations = validations;
        }

        public int Id => 1;

        public string Name => "update image";

        public void Execute(ImageDTO request)
        {
            if (_context.Images.Find(request.Id) == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Image));
            }

            _validations.ValidateAndThrow(request);

            var image = _context.Images.Find(request.Id);

            _mapper.Map(request, image);
            _context.SaveChanges();
        }
    }
}
