using InnoShop.CommonLibrary.Response;
using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Application.Commands.SubCategoryCommands
{
    public class AddSubCategoryCommand : IRequest<Response> 
    {
        public SubCategoryDTO SubCategoryDTO { get; set; }
    }
}
