using Application.DataTransfer;
using Application.Search;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries
{
    public interface IGetGenresQuery : IQuery<GenreSearch, PaginationResponse<GenreDTO>>
    {
    }
}
