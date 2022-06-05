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
    public class EfUpdateGenreCommand : IUpdateGenreCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly UpdateGenreValidator _validations;

        public EfUpdateGenreCommand(BookstoreContext context, IMapper mapper, UpdateGenreValidator validations)
        {
            _context = context;
            _mapper = mapper;
            _validations = validations;
        }
        public int Id => 1;
        public string Name => "update genre";

        public void Execute(GenreDTO request)
        {
            if (_context.Genres.Find(request.Id) == null)
            {
                throw new EntityNotFoundException(request.Id, typeof(Genre));
            }

            _validations.ValidateAndThrow(request);

            var genre = _context.Genres.Find(request.Id);

            _mapper.Map(request, genre);
            _context.SaveChanges();
        }
    }
}
