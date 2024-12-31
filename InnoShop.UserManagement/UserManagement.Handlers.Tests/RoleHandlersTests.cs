using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Queries.RoleQueries;
using UserManagement.Application.Queries.UserStatusQueries;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Handlers.RoleHandlers.CommandHandlers;
using UserManagement.Infrastructure.Handlers.RoleHandlers.QueryHandlers;
using UserManagement.Infrastructure.Handlers.UserStatusHandlers.CommandHandlers;
using UserManagement.Infrastructure.Handlers.UserStatusHandlers.QueryHandlers;

namespace UserManagement.Handlers.Tests
{
    public class RoleHandlersTests
    {
        private UserManagementDBContext Init()
        {
            var options = new DbContextOptionsBuilder<UserManagementDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new UserManagementDBContext(options);

            return (dbContext);
        }

        private List<RoleDTO> InitRoleDTOList()
        {
            return new List<RoleDTO>()
            {
                new RoleDTO
                {
                    Id = null,
                    Title = "User",
                    Description = "Simple user"
                },
                new RoleDTO
                {
                    Id = null,
                    Title = "Moderator",
                    Description = "User that helps administrator but limited"
                },
                new RoleDTO
                {
                    Id = null,
                    Title = "Administrator",
                    Description = "User that has all rights"
                },
            };
        }


        [Fact]
        public async void AddRoleHandler()
        {
            var dbContext = Init();
            var roleDTOs = InitRoleDTOList();

            //Arrange
            var command = new AddRoleCommand() { RoleDTO = roleDTOs.FirstOrDefault(ps => ps.Title == "User") };

            var handler = new AddRoleHandler(dbContext);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && await dbContext.Roles.AnyAsync(ps => ps.Title == "User"));

        }
        //Need some fixes
        //[Fact]
        //public async void UpdateRoleHandler()
        //{
        //    //Arrange

        //    var dbContext = Init();
        //    var roleDTOs = InitRoleDTOList();

        //    foreach (var roleDto in roleDTOs)
        //    {
        //        var commandAdd = new AddRoleCommand() { RoleDTO = roleDto };

        //        var handlerAdd = new AddRoleHandler(dbContext);
        //        await handlerAdd.Handle(commandAdd, default);

        //    }

        //    Assert.True(dbContext.Roles.Count() == roleDTOs.Count);

        //    var updatedId = (await dbContext.Roles.FirstOrDefaultAsync(ps => ps.Title == "User")).Id;

        //    var updatedRoleDTO = new RoleDTO()
        //    {
        //        Id = updatedId,
        //        Title = "User",
        //        Description = "No rights. simple user"
        //    };

        //    var command = new UpdateRoleCommand() { RoleDTO = updatedRoleDTO };

        //    var handler = new UpdateRoleHandler(dbContext);


        //    //Act
        //    var result = await handler.Handle(command, default);

        //    //Assert
        //    Assert.True(result.Flag == true
        //                    && await dbContext.Roles
        //                            .AnyAsync(ps => ps.Title == "User" && ps.Description == "No rights. simple user")
        //                    && !await dbContext.Roles.AnyAsync(ps => ps.Description == "Simple user"));
        //}

        [Fact]
        public async void DeleteRoleByIdHandler()
        {
            //Arrange

            var dbContext = Init();
            var roleDTOs = InitRoleDTOList();

            foreach (var roleDto in roleDTOs)
            {
                var commandAdd = new AddRoleCommand() { RoleDTO = roleDto };

                var handlerAdd = new AddRoleHandler(dbContext);
                await handlerAdd.Handle(commandAdd, default);

            }

            Assert.True(dbContext.Roles.Count() == roleDTOs.Count);


            var deleteId = (await dbContext.Roles.FirstOrDefaultAsync(ps => ps.Title == "Moderator")).Id;

            var command = new DeleteRoleByIdCommand() { Id = deleteId };

            var handler = new DeleteRoleByIdHandler(dbContext);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && !await dbContext.Roles.AnyAsync(ps => ps.Title == "Moderator"));

        }

        [Fact]
        public async void TakeRoleDTOByIdHandler()
        {
            //Arrange

            var dbContext = Init();
            var roleDTOs = InitRoleDTOList();

            foreach (var roleDto in roleDTOs)
            {
                var commandAdd = new AddRoleCommand() { RoleDTO = roleDto };

                var handlerAdd = new AddRoleHandler(dbContext);
                await handlerAdd.Handle(commandAdd, default);

            }

            Assert.True(dbContext.Roles.Count() == roleDTOs.Count);


            var selectedId = (await dbContext.Roles.AsNoTracking().FirstOrDefaultAsync(ps => ps.Title == "User")).Id;

            var command = new TakeRoleDTOByIdQuery() { Id = selectedId };

            var handler = new TakeRoleDTOByIdHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Id == selectedId
                    && result.Title == "User"
                    && result != null);

        }

        [Fact]
        public async void TakeRoleDTOListHandler()
        {
            //Arrange

            var dbContext = Init();
            var roleDTOs = InitRoleDTOList();

            foreach (var roleDto in roleDTOs)
            {
                var commandAdd = new AddRoleCommand() { RoleDTO = roleDto };

                var handlerAdd = new AddRoleHandler(dbContext);
                await handlerAdd.Handle(commandAdd, default);

            }

            Assert.True(dbContext.Roles.Count() == roleDTOs.Count);


            var command = new TakeRoleDTOListQuery() { };

            var handler = new TakeRoleDTOListHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Count == dbContext.Roles.Count()
                        && result.Any(r => r.Title == "User")
                        && result.Any(r => r.Title == "Administrator"));
        }
    }
}
