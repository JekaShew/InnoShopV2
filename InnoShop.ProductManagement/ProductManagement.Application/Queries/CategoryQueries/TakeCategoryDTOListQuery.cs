using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries.CategoryQueries
{
    public class TakeCategoryDTOListQuery : IRequest<List<CategoryDTO>>
    {

    }
}
