using InnoShop.CommonLibrary.Response;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.Commands.ProductStatusCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Interfaces;
using ProductManagement.Application.Queries.ProductStatusQueries;
using ProductManagement.Application.Services;
using ProductManagement.Domain.Data.Models;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.CommandHandlers;
using ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.QueryHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Services.Tests
{
    public class ProductStatusHandlersTests
    {
        private readonly Mock<IProductServices> _productServicesMock;
        Mock<ProductManagementDBContext> _pmDBContextMock;
        public ProductStatusHandlersTests()
        {
            _productServicesMock = new();
            _pmDBContextMock = new Mock<ProductManagementDBContext>();
        }

        private ProductManagementDBContext Init()
        {
            //var config = new MapperConfiguration(cfg => cfg.AddProfile<Services.AutoMapper>());
            //var mapper = config.CreateMapper();
            var options = new DbContextOptionsBuilder<ProductManagementDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new ProductManagementDBContext(options);
            //var service = new ProductServices(dbContext );
            return (dbContext);
        }

        public List<ProductStatusDTO> InitProductStatusDTOList()
        {
            return new List<ProductStatusDTO>()
            {
               
                new ProductStatusDTO
                {
                    Id = null,
                    Title = "Sold",
                    Description = "Product has already been sold"
                },
                new ProductStatusDTO
                {
                    Id = null,
                    Title = "Disabled",
                    Description = "Product is disabled and not available"
                },
                new ProductStatusDTO
                {
                    Id = null,
                    Title = "Enabled",
                    Description = "Product is enabled and available"
                },
                new ProductStatusDTO
                {
                    Id = null,
                    Title = "Blocked",
                    Description = "Product is blocked and not accessible"
                }
            };
        }


        [Fact]
        public async void AddProductStatusHandler()
        {
            var dbContext = Init();
            var productStatusDTOs = InitProductStatusDTOList();

            //Arrange
            var command = new AddProductStatusCommand() { ProductStatusDTO = productStatusDTOs.FirstOrDefault(ps => ps.Title == "Enabled") };

            var handler = new AddProductStatusHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && await dbContext.ProductStatuses.AnyAsync(ps => ps.Title == "Enabled"));

        }

        [Fact]
        public async void UpdateProductStatusHandler()
        {
            //Arrange

            var dbContext = Init();
            var productStatusDTOs = InitProductStatusDTOList();

            foreach(var productStatusDto in productStatusDTOs)
            {
                var commandAdd = new AddProductStatusCommand() { ProductStatusDTO = productStatusDto };

                var handlerAdd = new AddProductStatusHandler(dbContext);
                await handlerAdd.Handle(commandAdd, default);

            }

            Assert.True(dbContext.ProductStatuses.Count() == productStatusDTOs.Count);
            var updatedId = (await dbContext.ProductStatuses.FirstOrDefaultAsync(ps => ps.Title == "Sold")).Id;
            
            var updatedProductStatusDTO = new ProductStatusDTO()
            {
                Id = updatedId,
                Title = "SoldOUT",
                Description = "Soldout Product"
            };
            
            var command = new UpdateProductStatusCommand() { ProductStatusDTO = updatedProductStatusDTO };

            var handler = new UpdateProductStatusHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true 
                            && await dbContext.ProductStatuses
                                    .AnyAsync(ps => ps.Title == "SoldOUT" && ps.Description == "Soldout Product")
                            && !await dbContext.ProductStatuses.AnyAsync(ps=> ps.Title == "Sold"));

        }

        [Fact]
        public async void DeleteProductStatusByIdHandler()
        {
            //Arrange
            var dbContext = Init();
            var productStatusDTOs = InitProductStatusDTOList();

            foreach (var productStatusDto in productStatusDTOs)
            {
                var commandAdd = new AddProductStatusCommand() { ProductStatusDTO = productStatusDto };

                var handlerAdd = new AddProductStatusHandler(dbContext);
                await handlerAdd.Handle(commandAdd, default);

            }

            Assert.True(dbContext.ProductStatuses.Count() == productStatusDTOs.Count);

            var deleteId = (await dbContext.ProductStatuses.FirstOrDefaultAsync(ps => ps.Title == "Sold")).Id;

            var command = new DeleteProductStatusByIdCommand() { Id = deleteId };

            var handler = new DeleteProductStatusByIdHandler(dbContext);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && !await dbContext.ProductStatuses.AnyAsync(ps => ps.Title == "Sold"));

        }

        [Fact]
        public async void TakeProductStatusDTOByIdHandler()
        {
            //Arrange
            var dbContext = Init();
            var productStatusDTOs = InitProductStatusDTOList();
            
            foreach (var productStatusDto in productStatusDTOs)
            {
                var commandAdd = new AddProductStatusCommand() { ProductStatusDTO = productStatusDto };

                var handlerAdd = new AddProductStatusHandler(dbContext);
                await handlerAdd.Handle(commandAdd, default);
            }

            Assert.True(dbContext.ProductStatuses.Count() == productStatusDTOs.Count);

            var selectedId = (await dbContext.ProductStatuses.AsNoTracking().FirstOrDefaultAsync(ps => ps.Title == "Enabled")).Id;
            
            var command = new TakeProductStatusDTOByIdQuery() { Id = selectedId };

            var handler = new TakeProductStatusDTOByIdHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Id == selectedId 
                    && result.Title == "Enabled"
                    && result != null);

        }

        [Fact]
        public async void TakeProductStatusDTOListHandler()
        {
            //Arrange
            var dbContext = Init();
            var productStatusDTOs = InitProductStatusDTOList();

            foreach (var productStatusDto in productStatusDTOs)
            {
                var commandAdd = new AddProductStatusCommand() { ProductStatusDTO = productStatusDto };

                var handlerAdd = new AddProductStatusHandler(dbContext);
                await handlerAdd.Handle(commandAdd, default);
            }

            Assert.True(dbContext.ProductStatuses.Count() == productStatusDTOs.Count);

            var command = new TakeProductStatusDTOListQuery() {};

            var handler = new TakeProductStatusDTOListHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Count == dbContext.ProductStatuses.Count() 
                        && result.Any(r => r.Title == "Enabled"));

        }


    }
}
