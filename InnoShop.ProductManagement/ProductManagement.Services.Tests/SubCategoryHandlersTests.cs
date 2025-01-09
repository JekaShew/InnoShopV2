using Microsoft.EntityFrameworkCore;
using Moq;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Application.Commands.ProductStatusCommands;
using ProductManagement.Application.Commands.SubCategoryCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductStatusQueries;
using ProductManagement.Application.Queries.SubCategoryQueries;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Handlers.CategoryHandlers.CommandHandlers;
using ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.CommandHandlers;
using ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.QueryHandlers;
using ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.CommandHandlers;
using ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.QueryHandlers;
using ProductManagement.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Services.Tests
{
    public class SubCategoryHandlersTests
    {
        private ProductManagementDBContext InitDBContext()
        {
            var options = new DbContextOptionsBuilder<ProductManagementDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ProductManagementDBContext(options);

            return (dbContext);
        }

        private (CategoryRepository, SubCategoryRepository, ProductStatusRepository, ProductRepository) InitRepositories(ProductManagementDBContext dbContext)
        {
            var categoryRepository = new CategoryRepository(dbContext);
            var subCategoryRepository = new SubCategoryRepository(dbContext);
            var productStatusRepository = new ProductStatusRepository(dbContext);
            var productRepository = new ProductRepository(dbContext);

            return (categoryRepository, subCategoryRepository, productStatusRepository, productRepository);
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
                    Title = "Furniture",
                    Description = "Furniture Category"
                },
                new CategoryDTO
                {
                    Id = null,
                    Title = "Men products",
                    Description = "Products for men"
                }
            };
        }

        public List<SubCategoryDTO> InitSubCategoryDTOList()
        {
            return new List<SubCategoryDTO>()
            {
                new SubCategoryDTO
                {
                    Id = null,
                    Title = "Phones",
                    Description = "Different phones",
                    CategoryId = Guid.NewGuid(),
                },
                new SubCategoryDTO
                {
                    Id = null,
                    Title = "Laptops",
                    Description = "Different Laptops",
                    CategoryId = Guid.NewGuid(),
                },
                new SubCategoryDTO
                {
                    Id = null,
                    Title = "Chairs",
                    Description = "Different Chairs",
                    CategoryId = Guid.NewGuid(),
                },
                new SubCategoryDTO
                {
                    Id = null,
                    Title = "Sofas",
                    Description = "Different Sofas",
                    CategoryId = Guid.NewGuid(),
                }
            };
        }

        public async Task<ProductManagementDBContext> InitMockDB()
        {
            var dbContext = InitDBContext();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);
            var categoryDTOs = InitCategoryDTOList();
            var subCategoryDTOs = InitSubCategoryDTOList();

            foreach (var categoryDto in categoryDTOs)
            {
                var commandAddCat = new AddCategoryCommand() { CategoryDTO = categoryDto };

                var handlerAddCat = new AddCategoryHandler(categoryRepository);
                
                await handlerAddCat.Handle(commandAddCat, default);
            }

            var eCategoryId = (await dbContext.Categories.FirstOrDefaultAsync(c => c.Title == "Electronics")).Id;
            var fCategoryId = (await dbContext.Categories.FirstOrDefaultAsync(c => c.Title == "Furniture")).Id;

            foreach (var subCategoryDto in subCategoryDTOs)
            {
                if(subCategoryDto.Title == "Laptops" || subCategoryDto.Title == "Phones")
                    subCategoryDto.CategoryId = eCategoryId;
                
                if(subCategoryDto.Title == "Sofas" || subCategoryDto.Title == "Chairs")
                    subCategoryDto.CategoryId = fCategoryId;

                var commandAddSubCat = new AddSubCategoryCommand() { SubCategoryDTO = subCategoryDto };

                var handlerAddSubCat = new AddSubCategoryHandler(subCategoryRepository);
                
                await handlerAddSubCat.Handle(commandAddSubCat, default);
            }

            return dbContext;
        }

        [Fact]
        public async void AddSubCategoryHandler()
        {
            //Arrange 
            var dbContext = InitDBContext();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);
            var commandAddCat = new AddCategoryCommand() 
            { 
                CategoryDTO = new CategoryDTO
                {
                    Id = null,
                    Title = "Electronics",
                    Description = "Electronics Category"
                }
            };

            var handlerAddCat = new AddCategoryHandler(categoryRepository);
            await handlerAddCat.Handle(commandAddCat, default);

            var categoryId = (await dbContext.Categories.FirstOrDefaultAsync(c => c.Title == "Electronics")).Id;

            var command = new AddSubCategoryCommand() 
            { 
                SubCategoryDTO = new SubCategoryDTO
                {
                    Id = null,
                    Title = "Laptops",
                    Description = "Different Laptops",
                    CategoryId = categoryId,
                }
            };

            var handler = new AddSubCategoryHandler(subCategoryRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && await dbContext.SubCategories.AnyAsync(ps => ps.Title == "Laptops"));

        }

        [Fact]
        public async void DeleteSubCategoryByIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var deleteId = (await dbContext.SubCategories.FirstOrDefaultAsync(ps => ps.Title == "Laptops")).Id;

            var command = new DeleteSubCategoryByIdCommand() { Id = deleteId };

            var handler = new DeleteSubCategoryByIdHandler(subCategoryRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && !await dbContext.SubCategories.AnyAsync(ps => ps.Title == "Laptops"));
        }

        [Fact]
        public async void TakeSubCategoryDTOByIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var selectedId = (await dbContext.SubCategories.AsNoTracking().FirstOrDefaultAsync(ps => ps.Title == "Laptops")).Id;

            var command = new TakeSubCategoryDTOByIdQuery() { Id = selectedId };

            var handler = new TakeSubCategoryDTOByIdHandler(subCategoryRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Id == selectedId
                    && result.Title == "Laptops"
                    && result != null);
        }

        [Fact]
        public async void TakeSubCategoryDTOListHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var command = new TakeSubCategoryDTOListQuery() { };

            var handler = new TakeSubCategoryDTOListHandler(subCategoryRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Count == dbContext.SubCategories.Count()
                        && result.Any(r => r.Title == "Laptops"));
        }

        [Fact]
        public async void UpdateSubCategoryHandler()
        {
            //Arrange

            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var updatedId = (await dbContext.SubCategories.FirstOrDefaultAsync(ps => ps.Title == "Laptops")).Id;
            var categoryId = (await dbContext.Categories.FirstOrDefaultAsync(c => c.Title == "Electronics")).Id;
            var updatedProductStatusDTO = new SubCategoryDTO()
            {
                Id = updatedId,
                Title = "Super Laptops",
                Description = "Powerfull Laptops",
                CategoryId = categoryId,
            };

            var command = new UpdateSubCategoryCommand() { SubCategoryDTO = updatedProductStatusDTO };

            var handler = new UpdateSubCategoryHandler(subCategoryRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true
                            && await dbContext.SubCategories
                                    .AnyAsync(ps => ps.Title == "Super Laptops" && ps.Description == "Powerfull Laptops")
                            && !await dbContext.SubCategories.AnyAsync(ps => ps.Title == "Laptops"));
        }
    }
}
