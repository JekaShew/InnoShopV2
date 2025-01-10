using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries.SubCategoryQueries
{
    public class TakeSubCategoryDTOByIdQuery : IRequest<SubCategoryDTO>
    {
        public Guid Id { get; set; }
    }
}
