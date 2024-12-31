using Microsoft.EntityFrameworkCore;
using Moq;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Application.Commands.ProductStatusCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.CategoryQueries;
using ProductManagement.Application.Queries.ProductStatusQueries;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Handlers.CategoryHandlers.CommandHandlers;
using ProductManagement.Infrastructure.Handlers.CategoryHandlers.QueryHandlers;
using ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.CommandHandlers;
using ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Services.Tests
{
    public class CategoryHandlersTests
    {
        private ProductManagementDBContext Init()
        {
            var options = new DbContextOptionsBuilder<ProductManagementDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ProductManagementDBContext(options);

            return (dbContext);
        }

        public List<CategoryDTO> InitCategoryDTOList()
        {
            return new List<CategoryDTO>()
            {

                new CategoryDTO
                {
                    Id = null,
                    Title = "Electronics",
                    Description = "Electronics Category"
                },
                new CategoryDTO
                {
                    Id = null,
                    Title = "Garden",
                    Description = "Garden Category"
                },
                new CategoryDTO
                {
                    Id = null,
                    Title = "Men products",
                    Description = "Products for men"
                }
            };
        }


        [Fact]
        public async void AddCategoryHandler()
        {
            var dbContext = Init();
            var categoryDTOs = InitCategoryDTOList();

            //Arrange
            var command = new AddCategoryCommand() { CategoryDTO = categoryDTOs.FirstOrDefault(ps => ps.Title == "Electronics") };

            var handler = new AddCategoryHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && await dbContext.Categories.AnyAsync(ps => ps.Title == "Electronics"));

        }
        //Need some fixes
        //[Fact]
        //public async void UpdateCategoryHandler()
        //{
        //    //Arrange

        //    var dbContext = Init();
        //    var categoryDTOs = InitCategoryDTOList();

        //    foreach (var categoryDto in categoryDTOs)
        //    {
        //        var commandAdd = new AddCategoryCommand() { CategoryDTO = categoryDto };

        //        var handlerAdd = new AddCategoryHandler(dbContext);
        //        await handlerAdd.Handle(commandAdd, default);

        //    }

        //    Assert.True(dbContext.Categories.Count() == categoryDTOs.Count);

        //    var updatedId = (await dbContext.Categories.FirstOrDefaultAsync(ps => ps.Title == "Electronics")).Id;

        //    var updatedCategoryDTO = new CategoryDTO()
        //    {
        //        Id = updatedId,
        //        Title = "Bricks",
        //        Description = "Super Bricks"
        //    };

        //    var command = new UpdateCategoryCommand() { CategoryDTO = updatedCategoryDTO };

        //    var handler = new UpdateCategoryHandler(dbContext);


        //    //Act
        //    var result = await handler.Handle(command, default);

        //    //Assert
        //    Assert.True(result.Flag == true
        //                    && await dbContext.Categories
        //                            .AnyAsync(ps => ps.Title == "Bricks" && ps.Description == "Super Bricks")
        //                    && !await dbContext.Categories.AnyAsync(ps => ps.Title == "Electronics"));

        //}

        [Fact]
        public async void DeleteCategoryByIdHandler()
        {
            //Arrange

            var dbContext = Init();
            var categoryDTOs = InitCategoryDTOList();

            foreach (var categoryDto in categoryDTOs)
            {
                var commandAdd = new AddCategoryCommand() { CategoryDTO = categoryDto };

                var handlerAdd = new AddCategoryHandler(dbContext);
                await handlerAdd.Handle(commandAdd, default);

            }

            Assert.True(dbContext.Categories.Count() == categoryDTOs.Count);


            var deleteId = (await dbContext.Categories.FirstOrDefaultAsync(ps => ps.Title == "Electronics")).Id;

            var command = new DeleteCategoryByIdCommand() { Id = deleteId };

            var handler = new DeleteCategoryByIdHandler(dbContext);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && !await dbContext.Categories.AnyAsync(ps => ps.Title == "Electronics"));

        }

        [Fact]
        public async void TakeCategoryDTOByIdHandler()
        {
            //Arrange

            var dbContext = Init();
            var categoryDTOs = InitCategoryDTOList();

            foreach (var categoryDto in categoryDTOs)
            {
                var commandAdd = new AddCategoryCommand() { CategoryDTO = categoryDto };

                var handlerAdd = new AddCategoryHandler(dbContext);
                await handlerAdd.Handle(commandAdd, default);

            }

            Assert.True(dbContext.Categories.Count() == categoryDTOs.Count);


            var selectedId = (await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(ps => ps.Title == "Electronics")).Id;

            var command = new TakeCategoryDTOByIdQuery() { Id = selectedId };

            var handler = new TakeCategoryDTOByIdHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Id == selectedId
                    && result.Title == "Electronics"
                    && result != null);

        }

        [Fact]
        public async void TakeCategoryDTOListHandler()
        {
            //Arrange

            var dbContext = Init();
            var categoryDTOs = InitCategoryDTOList();

            foreach (var categoryDto in categoryDTOs)
            {
                var commandAdd = new AddCategoryCommand() { CategoryDTO = categoryDto };

                var handlerAdd = new AddCategoryHandler(dbContext);
                await handlerAdd.Handle(commandAdd, default);

            }

            Assert.True(dbContext.Categories.Count() == categoryDTOs.Count);


            var command = new TakeCategoryDTOListQuery() { };

            var handler = new TakeCategoryDTOListHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Count == dbContext.Categories.Count()
                        && result.Any(r => r.Title == "Electronics"));

        }
    }
}
