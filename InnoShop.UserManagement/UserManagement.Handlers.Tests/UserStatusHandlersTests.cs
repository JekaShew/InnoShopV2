﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Queries.UserStatusQueries;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Handlers.UserStatusHandlers.CommandHandlers;
using UserManagement.Infrastructure.Handlers.UserStatusHandlers.QueryHandlers;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Handlers.Tests
{
    public class UserStatusHandlersTests
    {
        private UserManagementDBContext InitDBContext()
        {
            var options = new DbContextOptionsBuilder<UserManagementDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new UserManagementDBContext(options);

            return (dbContext);
        }

        private (RoleRepository, UserStatusRepository, RefreshTokenRepository, UserRepository) InitRepositories(UserManagementDBContext dbContext)
        {
            var roleRepository = new RoleRepository(dbContext);
            var userStatusRepository = new UserStatusRepository(dbContext);
            var refreshTokenRepository = new RefreshTokenRepository(dbContext);
            var userRepository = new UserRepository(dbContext);

            return (roleRepository, userStatusRepository, refreshTokenRepository, userRepository);
        }

        private List<UserStatusDTO> InitUserStatusDTOList()
        {
            return new List<UserStatusDTO>()
            {
                new UserStatusDTO
                {
                    Id = null,
                    Title = "Activated",
                    Description = "User has been already activated"
                },
                new UserStatusDTO
                {
                    Id = null,
                    Title = "Disabled",
                    Description = "User was disabled"
                },
                new UserStatusDTO
                {
                    Id = null,
                    Title = "Blocked",
                    Description = "Blocked/ Banned user"
                },
            };
        }

        [Fact]
        public async void AddUserStatusHandler()
        {
            var dbContext = InitDBContext();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var userStatusDTOs = InitUserStatusDTOList();

            //Arrange
            var command = new AddUserStatusCommand() { UserStatusDTO = userStatusDTOs.FirstOrDefault(ps => ps.Title == "Activated") };

            var handler = new AddUserStatusHandler(userStatusRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && await dbContext.UserStatuses.AnyAsync(ps => ps.Title == "Activated"));
        }

        [Fact]
        public async void UpdateUsereStatusHandler()
        {
            //Arrange
            var dbContext = InitDBContext();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var userStatusDTOs = InitUserStatusDTOList();

            foreach (var userStatusDto in userStatusDTOs)
            {
                var commandAdd = new AddUserStatusCommand() { UserStatusDTO = userStatusDto };

                var handlerAdd = new AddUserStatusHandler(userStatusRepository);
                
                await handlerAdd.Handle(commandAdd, default);
            }

            Assert.True(dbContext.UserStatuses.Count() == userStatusDTOs.Count);

            var updatedId = (await dbContext.UserStatuses.FirstOrDefaultAsync(ps => ps.Title == "Activated")).Id;

            var updatedUserStatusDTO = new UserStatusDTO()
            {
                Id = updatedId,
                Title = "Already Activated",
                Description = "Already activated"
            };

            var command = new UpdateUserStatusCommand() { UserStatusDTO = updatedUserStatusDTO };

            var handler = new UpdateUserStatusHandler(userStatusRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true
                            && await dbContext.UserStatuses
                                    .AnyAsync(ps => ps.Title == "Already Activated" && ps.Description == "Already activated")
                            && !await dbContext.UserStatuses.AnyAsync(ps => ps.Title == "Activated"));
        }

        [Fact]
        public async void DeleteUserStatusByIdHandler()
        {
            //Arrange
            var dbContext = InitDBContext();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var userStatusDTOs = InitUserStatusDTOList();

            foreach (var userStatusDto in userStatusDTOs)
            {
                var commandAdd = new AddUserStatusCommand() { UserStatusDTO = userStatusDto };

                var handlerAdd = new AddUserStatusHandler(userStatusRepository);
                
                await handlerAdd.Handle(commandAdd, default);
            }

            Assert.True(dbContext.UserStatuses.Count() == userStatusDTOs.Count);

            var deleteId = (await dbContext.UserStatuses.FirstOrDefaultAsync(ps => ps.Title == "Disabled")).Id;

            var command = new DeleteUserStatusByIdCommand() { Id = deleteId };

            var handler = new DeleteUserStatusByIdHandler(userStatusRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && !await dbContext.UserStatuses.AnyAsync(ps => ps.Title == "Disabled"));
        }

        [Fact]
        public async void TakeUserStatusDTOByIdHandler()
        {
            //Arrange
            var dbContext = InitDBContext();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var userStatusDTOs = InitUserStatusDTOList();

            foreach (var userStatusDto in userStatusDTOs)
            {
                var commandAdd = new AddUserStatusCommand() { UserStatusDTO = userStatusDto };

                var handlerAdd = new AddUserStatusHandler(userStatusRepository);
                
                await handlerAdd.Handle(commandAdd, default);
            }

            Assert.True(dbContext.UserStatuses.Count() == userStatusDTOs.Count);

            var selectedId = (await dbContext.UserStatuses.AsNoTracking().FirstOrDefaultAsync(ps => ps.Title == "Activated")).Id;

            var command = new TakeUserStatusDTOByIdQuery() { Id = selectedId };

            var handler = new TakeUserStatusDTOByIdHandler(userStatusRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Id == selectedId
                    && result.Title == "Activated"
                    && result != null);
        }

        [Fact]
        public async void TakeUserStatusDTOListHandler()
        {
            //Arrange
            var dbContext = InitDBContext();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var userStatusDTOs = InitUserStatusDTOList();

            foreach (var userStatusDto in userStatusDTOs)
            {
                var commandAdd = new AddUserStatusCommand() { UserStatusDTO = userStatusDto };

                var handlerAdd = new AddUserStatusHandler(userStatusRepository);
                
                await handlerAdd.Handle(commandAdd, default);
            }

            Assert.True(dbContext.UserStatuses.Count() == userStatusDTOs.Count);

            var command = new TakeUserStatusDTOListQuery() { };

            var handler = new TakeUserStatusDTOListHandler(userStatusRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Count == dbContext.UserStatuses.Count()
                        && result.Any(r => r.Title == "Activated")
                        && result.Any(r => r.Title == "Disabled"));
        }
    }
}
