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
    public class EfCreateGenreCommand : ICreateGanreCommand
    {
        private readonly BookstoreContext _context;
        private readonly IMapper _mapper;
        private readonly AddGenreValidator _validations;

        public EfCreateGenreCommand(BookstoreContext context, IMapper mapper, AddGenreValidator validations)
        {
            _context = context;
            _mapper = mapper;
            _validations = validations;
        }

        public int Id => 1;

        public string Name => "create genre ef";
        public void Execute(GenreDTO request)
        {
            _validations.ValidateAndThrow(request);

            var genre = _mapper.Map<Genre>(request);

            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

    }
}
