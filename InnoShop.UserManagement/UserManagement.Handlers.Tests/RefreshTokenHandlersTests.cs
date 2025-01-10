using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;
using UserManagement.Application.Commands.RefreshTokenCommands;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Queries.RefreshQueries;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.CommandHandlers;
using UserManagement.Infrastructure.Handlers.RefreshTokenHandlers.QueryHandlers;
using UserManagement.Infrastructure.Handlers.RoleHandlers.CommandHandlers;
using UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers;
using UserManagement.Infrastructure.Handlers.UserStatusHandlers.CommandHandlers;
using UserManagement.Infrastructure.Repositories;

namespace UserManagement.Handlers.Tests
{
    public class RefreshTokenHandlersTests
    {
        private UserManagementDBContext InitDBContext()
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
        private (RoleRepository, UserStatusRepository, RefreshTokenRepository, UserRepository) InitRepositories(UserManagementDBContext dbContext)
        {
            var roleRepository = new RoleRepository(dbContext);
            var userStatusRepository = new UserStatusRepository(dbContext);
            var refreshTokenRepository = new RefreshTokenRepository(dbContext);
            var userRepository = new UserRepository(dbContext);

            return (roleRepository, userStatusRepository, refreshTokenRepository, userRepository);
        }

        public async Task<UserManagementDBContext> InitMockDB()
        {
            var dbContext = InitDBContext();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var userStatusDTOs = InitUserStatusDTOList();
            var roleDTOs = InitRoleDTOList();

            foreach (var userStatusDto in userStatusDTOs)
            {
                var commandAddUS = new AddUserStatusCommand() { UserStatusDTO = userStatusDto };

                var handlerAddUS = new AddUserStatusHandler(userStatusRepository);
                
                await handlerAddUS.Handle(commandAddUS, default);
            }

            var aUserStatusId = (await dbContext.UserStatuses.FirstOrDefaultAsync(c => c.Title == "Activated")).Id;
            var dUserStatusId = (await dbContext.UserStatuses.FirstOrDefaultAsync(c => c.Title == "Disabled")).Id;

            foreach (var roleDto in roleDTOs)
            {
                var commandAddRole = new AddRoleCommand() { RoleDTO = roleDto };

                var handlerAddRole = new AddRoleHandler(roleRepository);
                
                await handlerAddRole.Handle(commandAddRole, default);
            }

            var uRoleId = (await dbContext.Roles.FirstOrDefaultAsync(c => c.Title == "User")).Id;
            var aRoleId = (await dbContext.Roles.FirstOrDefaultAsync(c => c.Title == "Administrator")).Id;

            var registrationInfoDTOs = new List<RegistrationInfoDTO>()
            {
                new RegistrationInfoDTO()
                {
                    Id = null,
                    FIO = "Ivan IO QW",
                    Email = "ivan@mail.ru",
                    Login = "ivan123",
                    Password = "qweqwe",
                    SecretWord = "1ivan1",
                },
                new RegistrationInfoDTO()
                {
                    Id = null,
                    FIO = "Boris RT YO",
                    Email = "boris@mail.ru",
                    Login = "boris123",
                    Password = "asdasd",
                    SecretWord = "1boris1",
                },
                new RegistrationInfoDTO()
                {
                    Id = null,
                    FIO = "Igor DK RT",
                    Email = "igor@mail.ru",
                    Login = "igor123",
                    Password = "zxczxc",
                    SecretWord = "1igor1",
                },
                new RegistrationInfoDTO()
                {
                    Id = null,
                    FIO = "Vasiliy OP GLHF",
                    Email = "vasya@mail.ru",
                    Login = "vasya123",
                    Password = "qweqwe",
                    SecretWord = "1vasya1",
                },
                new RegistrationInfoDTO()
                {
                    Id = null,
                    FIO = "Chnops IT DC",
                    Email = "chnops@mail.ru",
                    Login = "chnops123",
                    Password = "asdasd",
                    SecretWord = "1chnops1",
                },
                new RegistrationInfoDTO()
                {
                    Id = null,
                    FIO = "Oleg PT PK",
                    Email = "oleg@mail.ru",
                    Login = "oleg123",
                    Password = "zxczxc",
                    SecretWord = "1oleg1",
                },
            };

            foreach (var registrationInfoDto in registrationInfoDTOs)
            {
                var command = new AddUserCommand()
                {
                    RegistrationInfoDTO = registrationInfoDto,
                    PasswordHash = registrationInfoDto.Password,
                    SecretWordHash = registrationInfoDto.SecretWord,
                    SecurityStamp = "SecurityStamp",

                };

                var handler = new AddUserHandler(userRepository);
                
                await handler.Handle(command, default);
            }

            var user1Id = (await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == "ivan123")).Id;
            var user2Id = (await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == "boris123")).Id;
            var user3Id = (await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == "igor123")).Id;

            var refreshTokenDTOs = new List<RefreshTokenDTO>()
            {
                new RefreshTokenDTO
                {
                    Id = Guid.NewGuid(),
                    IsRevoked = false,
                    ExpireDate = DateTime.UtcNow.AddMinutes(120),
                    UserId = user1Id,
                },
                new RefreshTokenDTO
                {
                    Id = Guid.NewGuid(),
                    IsRevoked = false,
                    ExpireDate = DateTime.UtcNow.AddMinutes(120),
                    UserId = user2Id,
                },
                new RefreshTokenDTO
                {
                    Id = Guid.NewGuid(),
                    IsRevoked = false,
                    ExpireDate = DateTime.UtcNow.AddMinutes(120),
                    UserId = user3Id,
                },
            };

            foreach (var refreshTokenDto in refreshTokenDTOs)
            {
                var command = new AddRefreshTokenCommand()
                {
                    RefreshTokenDTO = refreshTokenDto
                };

                var handler = new AddRefreshTokenHandler(refreshTokenRepository);
                
                await handler.Handle(command, default);
            }

            return dbContext;
        }

        [Fact]
        public async void AddRefreshTokenHandler()
        {
            var dbContext = await InitMockDB();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var user1Id = (await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u=> u.Login == "ivan123")).Id;
            var refreshTokenDTO = new RefreshTokenDTO()
            {
                Id = Guid.NewGuid(),
                IsRevoked = false,
                ExpireDate = DateTime.UtcNow.AddMinutes(120),
                UserId = user1Id,
            };

            //Arrange
            var command = new AddRefreshTokenCommand() { RefreshTokenDTO = refreshTokenDTO };

            var handler = new AddRefreshTokenHandler(refreshTokenRepository);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true 
                        && await dbContext.RefreshTokens.AnyAsync(rt => rt.UserId == user1Id));
        }

        [Fact]
        public async void DeleteRefreshTokenByRTokenIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var user1Id = (await dbContext.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Login == "ivan123")).Id;
            var rTokenId = (await dbContext.RefreshTokens
                            .AsNoTracking()
                            .FirstOrDefaultAsync(rt => rt.UserId == user1Id)).Id;

            var command = new DeleteRefreshTokenByRTokenIdCommand() { RTokenId = rTokenId};

            var handler = new DeleteRefreshTokenByRTokenIdHandler(refreshTokenRepository);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true
                            && !await dbContext.RefreshTokens
                                    .AnyAsync(ps => ps.Id == rTokenId));
        }

        [Fact]
        public async void RevokeRefreshTokenByRTokenIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var user1Id = (await dbContext.Users
                            .AsNoTracking()
                            .FirstOrDefaultAsync(u => u.Login == "ivan123")).Id;
            var rTokenId = (await dbContext.RefreshTokens
                            .AsNoTracking()
                            .FirstOrDefaultAsync(rt => rt.UserId == user1Id)).Id;

            var command = new RevokeRefreshTokenByRTokenIdCommand() { RTokenId = rTokenId};

            var handler = new RevokeRefreshTokenByRTokenIdHandler(refreshTokenRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true 
                        && !await dbContext.RefreshTokens.AnyAsync(rt => rt.Id == rTokenId
                                                                   && rt.IsRevoked == false));
        }

        [Fact]
        public async void IsRefreshTokenCorrectByRTokenIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var user1Id = (await dbContext.Users
                           .AsNoTracking()
                           .FirstOrDefaultAsync(u => u.Login == "ivan123")).Id;
            var rTokenId = (await dbContext.RefreshTokens
                            .AsNoTracking()
                            .FirstOrDefaultAsync(rt => rt.UserId == user1Id)).Id;

            var command = new IsRefreshTokenCorrectByRTokenIdQuery() { RTokenId = rTokenId};

            var handler = new IsRefreshTokenCorrectByRTokenIdHandler(refreshTokenRepository);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true);
        }

        [Fact]
            public async void TakeRefreshTokenDTOListHandlerHandler()
            {
                //Arrange
                var dbContext = await InitMockDB();
                var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

                var command = new TakeRefreshTokenDTOListQuery() { };

                var handler = new TakeRefreshTokenDTOListHandler(refreshTokenRepository);

                //Act
                var result = await handler.Handle(command, default);

                //Assert
                Assert.True(result.Count == dbContext.RefreshTokens.Count());
            }

        [Fact]
        public async void TakeUserIdByRTokenIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var (roleRepository, userStatusRepository, refreshTokenRepository, userRepository) = InitRepositories(dbContext);

            var user1Id = (await dbContext.Users
                           .AsNoTracking()
                           .FirstOrDefaultAsync(u => u.Login == "ivan123")).Id;
            var rTokenId = (await dbContext.RefreshTokens
                            .AsNoTracking()
                            .FirstOrDefaultAsync(rt => rt.UserId == user1Id)).Id;


            var command = new TakeUserIdByRTokenIdQuery() { RtokenId = rTokenId };

            var handler = new TakeUserIdByRTokenIdHandler(refreshTokenRepository);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result == user1Id);
        }
    }
}