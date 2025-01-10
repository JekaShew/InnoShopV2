using InnoShop.CommonLibrary.CommonDTOs;
using Microsoft.EntityFrameworkCore;
using ProductManagement.Application.Commands.CategoryCommands;
using ProductManagement.Application.Commands.ProductCommands;
using ProductManagement.Application.Commands.ProductStatusCommands;
using ProductManagement.Application.Commands.SubCategoryCommands;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Queries.ProductQueries;
using ProductManagement.Infrastructure.Data;
using ProductManagement.Infrastructure.Handlers.CategoryHandlers.CommandHandlers;
using ProductManagement.Infrastructure.Handlers.ProductHandlers.CommandHandlers;
using ProductManagement.Infrastructure.Handlers.ProductHandlers.QueryHandlers;
using ProductManagement.Infrastructure.Handlers.ProductStatusHandlers.CommandHandlers;
using ProductManagement.Infrastructure.Handlers.SubCategoryHandlers.CommandHandlers;
using ProductManagement.Infrastructure.Repositories;

namespace ProductManagement.Services.Tests
{
    public class ProductHandlersTests
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

        public async Task<ProductManagementDBContext> InitMockDB()
        {
            var dbContext = InitDBContext();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var categoryDTOs = InitCategoryDTOList();
            var subCategoryDTOs = InitSubCategoryDTOList();
            var productStatusDTOs = InitProductStatusDTOList();

            foreach (var categoryDto in categoryDTOs)
            {
                var commandAddCat = new AddCategoryCommand() { CategoryDTO = categoryDto };

                var handlerAddCat = new AddCategoryHandler(categoryRepository);
                
                await handlerAddCat.Handle(commandAddCat, default);
            }

            var eCategoryId = (await dbContext.Categories.FirstOrDefaultAsync(c => c.Title == "Electronics")).Id;
            var fCategoryId = (await dbContext.Categories.FirstOrDefaultAsync(c => c.Title == "Furniture")).Id;

            foreach (var productStatusDto in productStatusDTOs)
            {
                var commandAddProdStat = new AddProductStatusCommand() { ProductStatusDTO = productStatusDto };

                var handlerAddProdStat = new AddProductStatusHandler(productStatusRepository);
                
                await handlerAddProdStat.Handle(commandAddProdStat, default);
            }

            var eProductStatusId = (await dbContext.ProductStatuses.FirstOrDefaultAsync(c => c.Title == "Enabled")).Id;
            var dProductStatusId = (await dbContext.ProductStatuses.FirstOrDefaultAsync(c => c.Title == "Disabled")).Id;
            var bProductStatusId = (await dbContext.ProductStatuses.FirstOrDefaultAsync(c => c.Title == "Blocked")).Id;

            foreach (var subCategoryDto in subCategoryDTOs)
            {
                if (subCategoryDto.Title == "Laptops" || subCategoryDto.Title == "Phones")
                    subCategoryDto.CategoryId = eCategoryId;

                if (subCategoryDto.Title == "Sofas" || subCategoryDto.Title == "Chairs")
                    subCategoryDto.CategoryId = fCategoryId;

                var commandAddSubCat = new AddSubCategoryCommand() { SubCategoryDTO = subCategoryDto };

                var handlerAddSubCat = new AddSubCategoryHandler(subCategoryRepository);
                
                await handlerAddSubCat.Handle(commandAddSubCat, default);
            }

            var lSubCategoryId = (await dbContext.SubCategories.FirstOrDefaultAsync(c => c.Title == "Laptops")).Id;
            var pSubCategoryId = (await dbContext.SubCategories.FirstOrDefaultAsync(c => c.Title == "Phones")).Id;

            var sSubCategoryId = (await dbContext.SubCategories.FirstOrDefaultAsync(c => c.Title == "Sofas")).Id;
            var chSubCategoryId = (await dbContext.SubCategories.FirstOrDefaultAsync(c => c.Title == "Chairs")).Id;
            
            var user1Id = Guid.NewGuid();
            var user2Id = Guid.NewGuid();
            
            var productDTOs = new List<ProductDTO>()
            {
                new ProductDTO()
                {
                    Id = null,
                    Title = "Asus",
                    Price = 1500,
                    Description = "Super PowerFULL Laptop",
                    UserId = user1Id,
                    ProductStatusId = eProductStatusId,
                    SubCategoryId = lSubCategoryId
                },
                new ProductDTO()
                {
                    Id = null,
                    Title = "Lenovo",
                    Price = 2500,
                    Description = "Super Resilient Laptop",
                    UserId = user2Id,
                    ProductStatusId = eProductStatusId,
                    SubCategoryId = lSubCategoryId
                },
                new ProductDTO()
                {
                    Id = null,
                    Title = "Honor",
                    Price = 1000,
                    Description = "Super Android Robot",
                    UserId = user2Id,
                    ProductStatusId = eProductStatusId,
                    SubCategoryId = pSubCategoryId
                },
                new ProductDTO()
                {
                    Id = null,
                    Title = "IPhone",
                    Price = 3000,
                    Description = "Super IPhone Camera",
                    UserId = user1Id,
                    ProductStatusId = eProductStatusId,
                    SubCategoryId = pSubCategoryId
                },
                new ProductDTO()
                {
                    Id = null,
                    Title = "Kitchen Chair",
                    Price = 1600,
                    Description = "Chair for eating",
                    UserId = user2Id,
                    ProductStatusId = eProductStatusId,
                    SubCategoryId = chSubCategoryId
                },
                new ProductDTO()
                {
                    Id = null,
                    Title = "Bedroom Chair",
                    Price = 1000,
                    Description = "Trainig chair",
                    UserId = user1Id,
                    ProductStatusId = eProductStatusId,
                    SubCategoryId = chSubCategoryId
                },
                new ProductDTO()
                {
                    Id = null,
                    Title = "Living room sofa",
                    Price = 3900,
                    Description = "Comfortable sofa",
                    UserId = user2Id,
                    ProductStatusId = eProductStatusId,
                    SubCategoryId = sSubCategoryId
                },
                new ProductDTO()
                {
                    Id = null,
                    Title = "Bedroom sofa",
                    Price = 4200,
                    Description = "Soft sofa",
                    UserId = user1Id,
                    ProductStatusId = eProductStatusId,
                    SubCategoryId = sSubCategoryId
                }
            };

            foreach (var productDTO in productDTOs)
            {
                var command = new AddProductCommand()
                {
                    ProductDTO = productDTO
                };

                var handler = new AddProductHandler(productRepository);
                
                await handler.Handle(command, default);
            }

            return dbContext;
        }

        //Common Handlers
        [Fact]
        public async void AddProductHandler()
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

            var commandAddProdStat = new AddProductStatusCommand()
            {
                ProductStatusDTO = new ProductStatusDTO
                {
                    Id = null,
                    Title = "Enabled",
                    Description = "Product is Enabled"
                }
            };

            var handlerProdStat = new AddProductStatusHandler(productStatusRepository);
            await handlerProdStat.Handle(commandAddProdStat, default);

            var productStatusId = (await dbContext.ProductStatuses.FirstOrDefaultAsync(c => c.Title == "Enabled")).Id;

            var commandAddSubCat = new AddSubCategoryCommand()
            {
                SubCategoryDTO = new SubCategoryDTO
                {
                    Id = null,
                    Title = "Laptops",
                    Description = "Different Laptops",
                    CategoryId = categoryId,
                }
            };

            var handlerAddSubCat = new AddSubCategoryHandler(subCategoryRepository);
            await handlerAddSubCat.Handle(commandAddSubCat, default);

            var subCategoryId = (await dbContext.SubCategories.FirstOrDefaultAsync(c => c.Title == "Laptops")).Id;

            var userId = Guid.NewGuid();

            var command = new AddProductCommand()
            {
                ProductDTO = new ProductDTO()
                {
                    Id = null,
                    Title = "Asus",
                    Price = 1500,
                    Description = "Super PowerFULL Laptop",
                    UserId = userId,
                    ProductStatusId = productStatusId,
                    SubCategoryId = subCategoryId
                }
            };

            var handler = new AddProductHandler(productRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && await dbContext.Products.AnyAsync(ps => ps.Title == "Asus"));
        }

        [Fact]
        public async void DeleteProductByIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var deleteId = (await dbContext.Products.FirstOrDefaultAsync(ps => ps.Title == "Asus")).Id;

            var command = new DeleteProductByIdCommand() { Id = deleteId };

            var handler = new DeleteProductByIdHandler(productRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && !await dbContext.Products.AnyAsync(ps => ps.Title == "Asus"));
        }

        [Fact]
        public async void TakeProductDTOByIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var selectedId = (await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(ps => ps.Title == "Asus")).Id;

            var command = new TakeProductDTOByIdQuery() { Id = selectedId };

            var handler = new TakeProductDTOByIdHandler(productRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Id == selectedId
                    && result.Title == "Asus"
                    && result != null);
        }

        [Fact]
        public async void TakeProductDTOListHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var command = new TakeProductDTOListQuery() { };

            var handler = new TakeProductDTOListHandler(productRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Count == dbContext.Products.Count()
                        && result.Any(r => r.Title == "Asus")
                        && result.Any(r => r.Title == "IPhone"));
        }

        [Fact]
        public async void UpdateProductHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var updatedProduct = await dbContext.Products.FirstOrDefaultAsync(ps => ps.Title == "Asus");
            var updatedId = updatedProduct.Id;
            var userId = updatedProduct.UserId;
            var subCategoryId = updatedProduct.SubCategoryId;
            var productStatusId = updatedProduct.ProductStatusId;
            var updatedProductDTO = new ProductDTO()
            {
                Id = updatedId,
                Title = "Bad Asus",
                Description = "Not so powerfull laptop",
                SubCategoryId = subCategoryId,
                ProductStatusId = productStatusId,
                UserId = userId,
                Price = 900,
            };

            var command = new UpdateProductCommand() { ProductDTO = updatedProductDTO };

            var handler = new UpdateProductHandler(productRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true
                            && await dbContext.Products
                                    .AnyAsync(ps => ps.Title == "Bad Asus" && ps.Description == "Not so powerfull laptop")
                            && !await dbContext.Products.AnyAsync(ps => ps.Title == "Asus"));
        }

        //Special Handlers
        [Fact]
        public async void ChangeProductStatusesOfProductsByUserIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var userId = (await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Title == "Asus")).Id;
            var eProductStatusId = (await dbContext.ProductStatuses.AsNoTracking().FirstOrDefaultAsync(ps=> ps.Title == "Enabled")).Id;
            var command = new ChangeProductStatusesOfProductsByUserIdCommand() 
            {
                UserId = userId
            };

            var handler = new ChangeProductStatusesOfProductsByUserIdHandler(productRepository, productStatusRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true 
                    && !await dbContext.Products.AnyAsync(p => p.UserId == userId && p.ProductStatusId == eProductStatusId));
        }

        [Fact]
        public async void ChangeProductStatusOfProductHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var productId = (await dbContext.Products.FirstOrDefaultAsync(p => p.Title == "Asus")).Id;
            var dProductStatusId = (await dbContext.ProductStatuses.AsNoTracking().FirstOrDefaultAsync(p => p.Title == "Disabled")).Id;

            var command = new ChangeProductStatusOfProductCommand() 
            {
                ProductId = productId,
                ProductStatusId = dProductStatusId
            };

            var handler = new ChangeProductStatusOfProductHandler(productRepository, productStatusRepository);

            //Act
            
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true
                    && (await dbContext.Products.FirstOrDefaultAsync(p=> p.Title =="Asus" && p.Id == productId)).ProductStatusId == dProductStatusId);
        }

        [Fact]
        public async void TakeProductDTOListByUserIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var user1Id = (await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Title == "Asus")).UserId;
            var user2Id = (await dbContext.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Title == "Lenovo")).UserId;

            var command1 = new TakeProductDTOListByUserIdQuery()
            {
                UserId = user1Id
            };
            
            var command2 = new TakeProductDTOListByUserIdQuery()
            {
                UserId = user2Id
            };
            var handler = new TakeProductDTOListByUserIdHandler(productRepository);

            //Act
            var result1 = await handler.Handle(command1, default);
            var result2 = await handler.Handle(command2, default);

            //Assert
            Assert.True(result1 != null && result2 != null
                        && !result1.Any(r => r.UserId == user2Id)
                        && !result2.Any(r => r.UserId == user1Id)); 
        }

        [Fact]
        public async void TakeSearchedProductDTOListHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var query1 = "chair";
            var query2 = "sofa";

            var command1 = new TakeSearchedProductDTOListQuery() 
            {
                QueryString = query1
            };

            var command2 = new TakeSearchedProductDTOListQuery()
            {
                QueryString = query2
            };

            var handler = new TakeSearchedProductDTOListHandler(productRepository);

            //Act
            var result1 = await handler.Handle(command1, default);

            var result2 = await handler.Handle(command2, default);

            //Assert
            Assert.True(result1.Count == 2 && result2.Count == 2
                && result1.TrueForAll(r=> r.Title.ToLower().Contains(query1) || r.Description.ToLower().Contains(query1))
                && result2.TrueForAll(r => r.Title.ToLower().Contains(query2) || r.Description.ToLower().Contains(query2)));
        }

        [Fact]
        public async void TakeFilteredProductDTOListHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (categoryRepository, subCategoryRepository, productStatusRepository, productRepository) = InitRepositories(dbContext);

            var eCategory = await dbContext.Categories.AsNoTracking().FirstOrDefaultAsync(c=> c.Title == "Electronics");
            var pSubCategory = await dbContext.SubCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Title == "Phones");

            var sSubCategory = await dbContext.SubCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Title == "Sofas");
            var chSubCategory = await dbContext.SubCategories.AsNoTracking().FirstOrDefaultAsync(c => c.Title == "Chairs");

            var productFilterDTO1 = new ProductFilterDTO()
            {
                MinPrice = 1300M,
                MaxPrice = 2800M
            };

            var productFilterDTO2 = new ProductFilterDTO()
            {
                Category = new List<ParameterDTO>()
                {
                    new ParameterDTO()
                    {
                        Id = eCategory.Id,
                        Text = eCategory.Title
                    } 
                }
            };

            var productFilterDTO3 = new ProductFilterDTO()
            {
                MinPrice = 1500M,
                SubCategory = new List<ParameterDTO>()
                {
                    new ParameterDTO()
                    {
                        Id = pSubCategory.Id,
                        Text = pSubCategory.Title
                    }                
                }
            };

            var command1 = new TakeFilteredProductDTOListQuery() 
            {
                ProductFilterDTO = productFilterDTO1
            };

            var command2 = new TakeFilteredProductDTOListQuery()
            {
                ProductFilterDTO = productFilterDTO2
            };

            var command3 = new TakeFilteredProductDTOListQuery()
            {
                ProductFilterDTO = productFilterDTO3
            };

            var handler = new TakeFilteredProductDTOListHandler(productRepository, subCategoryRepository, categoryRepository);

            //Act
            var result1 = await handler.Handle(command1, default);
            var result2 = await handler.Handle(command2, default);
            var result3 = await handler.Handle(command3, default);

            //Assert
            Assert.True(!result1.Any(r => r.Price < 1300 && r.Price > 2800)
                        && !result2.Any(r => r.SubCategoryId == sSubCategory.Id && r.SubCategoryId == chSubCategory.Id)
                        && !result3.Any(r => r.Price < 1500 && r.SubCategoryId != pSubCategory.Id));
        }
    }
}