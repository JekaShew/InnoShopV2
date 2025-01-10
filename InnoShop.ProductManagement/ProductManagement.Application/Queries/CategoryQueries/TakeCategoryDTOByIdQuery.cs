using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries.CategoryQueries
{
    public class TakeCategoryDTOByIdQuery : IRequest<CategoryDTO>
    {
        public Guid Id { get; set; }
    }
}
