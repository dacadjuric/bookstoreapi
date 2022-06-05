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
    public class EfCreateImageCommand : ICreateImageCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly AddImageValidator _validations;

        public EfCreateImageCommand(BookstoreContext context, IMapper mapper, AddImageValidator validations)
        {
            _context = context;
            _mapper = mapper;
            _validations = validations;
        }
        public int Id => 1;

        public string Name => "create image ";

        public void Execute(ImageDTO request)
        {
            _validations.ValidateAndThrow(request);

            var image = _mapper.Map<Image>(request);

            _context.Images.Add(image);
            _context.SaveChanges();
        }
    }
}
