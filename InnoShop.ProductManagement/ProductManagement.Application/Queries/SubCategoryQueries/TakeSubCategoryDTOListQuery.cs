using MediatR;
using ProductManagement.Application.DTOs;

namespace ProductManagement.Application.Queries.SubCategoryQueries
{
    public class TakeSubCategoryDTOListQuery : IRequest<List<SubCategoryDTO>>
    {

    }
}
