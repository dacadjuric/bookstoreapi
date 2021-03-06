using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(int id, Type type)
            : base($"Entity of type {type.Name} with an id {id} was not found.")
        {

        }
    }
}
