using InnoShop.CommonLibrary.CommonDTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagement.Application.Commands.RoleCommands;
using UserManagement.Application.Commands.UserCommands;
using UserManagement.Application.Commands.UserStatusCommands;
using UserManagement.Application.DTOs;
using UserManagement.Application.Queries.UserQueries;
using UserManagement.Application.Services;
using UserManagement.Domain.Data.Models;
using UserManagement.Infrastructure.Data;
using UserManagement.Infrastructure.Handlers.RoleHandlers.CommandHandlers;
using UserManagement.Infrastructure.Handlers.UserHandlers.CommandHandlers;
using UserManagement.Infrastructure.Handlers.UserHandlers.QueryHandlers;
using UserManagement.Infrastructure.Handlers.UserStatusHandlers.CommandHandlers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace UserManagement.Handlers.Tests
{
    public class UserHandlersTests
    {
        private UserManagementDBContext Init()
        {
            var options = new DbContextOptionsBuilder<UserManagementDBContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var dbContext = new UserManagementDBContext(options);

            return dbContext;
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

        public async Task<UserManagementDBContext> InitMockDB()
        {
            var dbContext = Init();
            var userStatusDTOs = InitUserStatusDTOList();
            var roleDTOs = InitRoleDTOList();

            foreach (var userStatusDto in userStatusDTOs)
            {
                var commandAddUS = new AddUserStatusCommand() { UserStatusDTO = userStatusDto };

                var handlerAddUS = new AddUserStatusHandler(dbContext);
                await handlerAddUS.Handle(commandAddUS, default);

            }

            var aUserStatusId = (await dbContext.UserStatuses.FirstOrDefaultAsync(c => c.Title == "Activated")).Id;
            var dUserStatusId = (await dbContext.UserStatuses.FirstOrDefaultAsync(c => c.Title == "Disabled")).Id;

            foreach (var roleDto in roleDTOs)
            {
                var commandAddRole = new AddRoleCommand() { RoleDTO = roleDto };

                var handlerAddRole = new AddRoleHandler(dbContext);
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

                var handler = new AddUserHandler(dbContext);
                await handler.Handle(command, default);
            }



            return dbContext;
        }

        //Common
        [Fact]
        public async void AddUserHandler()
        {
            //Arrange 
            var dbContext = Init();
            var commandAddUS = new AddUserStatusCommand()
            {
                UserStatusDTO = new UserStatusDTO
                {
                    Id = null,
                    Title = "Acitvated",
                    Description = "Activated user"
                }
            };

            var handlerAddUS = new AddUserStatusHandler(dbContext);
            await handlerAddUS.Handle(commandAddUS, default);

            var userStatusId = (await dbContext.UserStatuses.FirstOrDefaultAsync(c => c.Title == "Acitvated")).Id;

            var commandAddRole = new AddRoleCommand()
            {
                RoleDTO = new RoleDTO
                {
                    Id = null,
                    Title = "User",
                    Description = "Simple User"
                }
            };

            var handlerAddRole= new AddRoleHandler(dbContext);
            await handlerAddRole.Handle(commandAddRole, default);

            var uRoleId = (await dbContext.Roles.FirstOrDefaultAsync(c => c.Title == "User")).Id;

            var registrationInfoDTO = new RegistrationInfoDTO()
            {
                Id = null,
                FIO = "Ivan",
                Email = "ivan@mail.ru",
                Login = "ivan123",
                Password = "zxczxc",
                SecretWord = "21ivan12",
            };

            var command = new AddUserCommand()
            {
                RegistrationInfoDTO = registrationInfoDTO,
                PasswordHash = registrationInfoDTO.Password,
                SecretWordHash = registrationInfoDTO.SecretWord,
                SecurityStamp = "SecurityStamp",
            };

            var handler = new AddUserHandler(dbContext);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true 
                    && await dbContext.Users
                        .AnyAsync(ps => ps.FIO == "Ivan" 
                            && ps.Login =="ivan123"
                            && ps.RoleId == uRoleId
                            && ps.UserStatusId == userStatusId)); 
        }

        //Need some fixes
        //[Fact]
        //public async void UpdateUserHandler()
        //{
        //    //Arrange

        //    var dbContext = await InitMockDB();


        //    var updatedUser = await dbContext.Users
        //                        .FirstOrDefaultAsync(ps => ps.Login == "ivan123");
        //    var updatedId = updatedUser.Id;
        //    var uRoleId = (await dbContext.Roles
        //                    .FirstOrDefaultAsync(r=> r.Title == "User")).Id;
        //    var userStatusId = (await dbContext.UserStatuses
        //                        .FirstOrDefaultAsync(us=> us.Title == "Activated")).Id;
        //    var updatedUsertDTO = new UserDTO()
        //    {
        //        Id = updatedId,
        //        FIO = "Vanya Popkov",
        //        Email = "ivanDDD@yandex.by",
        //        Login = "111ivan111",
        //        RoleId = uRoleId,
        //        UserStatusId = userStatusId,
        //    };

        //    var command = new UpdateUserCommand() { UserDTO = updatedUsertDTO};

        //    var handler = new UpdateUserHandler(dbContext);

        //    //Act
        //    var result = await handler.Handle(command, default);

        //    //Assert
        //    Assert.True(result.Flag == true
        //                    && await dbContext.Users
        //                            .AnyAsync(ps => ps.FIO == "Vanya Popkov" && ps.Login == "111ivan111")
        //                    && !await dbContext.Users.AnyAsync(ps => ps.Login == "ivan123"));
        //}

        [Fact]
        public async void DeleteUserByIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();

            var deleteId = (await dbContext.Users
                        .FirstOrDefaultAsync(ps => ps.Login == "ivan123")).Id;

            var command = new DeleteUserByIdCommand() { Id = deleteId };

            var handler = new DeleteUserByIdHandler(dbContext);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true && !await dbContext.Users
                                                    .AnyAsync(ps => ps.Login == "ivan123"));

        }

        [Fact]
        public async void TakeUserDTOByIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();

            var selectedId = (await dbContext.Users
                                .AsNoTracking()
                                .FirstOrDefaultAsync(ps => ps.Login == "ivan123")).Id;

            var command = new TakeUserDTOByIdQuery() { Id = selectedId };

            var handler = new TakeUserDTOByIdHandler(dbContext);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Id == selectedId
                    && result.Login == "ivan123"
                    && result != null);
        }

        [Fact]
        public async void TakeUserDTOListHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();

            var command = new TakeUserDTOListQuery() { };

            var handler = new TakeUserDTOListHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Count == dbContext.Users.Count()
                        && result.Any(r => r.Login == "ivan123")
                        && result.Any(r => r.Login == "oleg123")
                        && result.Any(r => r.Login == "boris123"));
        }
        //Special
        [Fact]
        public async void ChangePasswordHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();

            var user = await dbContext.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Login == "ivan123");
            var userId = user.Id;
            var oldPassword = user.PasswordHash;
            var newPasswprdHash = "asdasd";

            var command = new ChangePasswordCommand()
            {
                UserId = userId,
                NewPasswordHash = newPasswprdHash,
            };

            var handler = new ChangePasswordHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true
                    && await dbContext.Users
                            .AnyAsync(p => p.Login == user.Login && p.PasswordHash == newPasswprdHash));
        }

        [Fact]
        public async void ChangeRoleOfUserHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();

            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(p => p.Login == "ivan123");
            var aRoleId = (await dbContext.Roles
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Title == "Administrator")).Id;
            var uRoleId = (await dbContext.Roles
                        .AsNoTracking()
                        .FirstOrDefaultAsync(p => p.Title == "User")).Id;
            var command = new ChangeRoleOfUserCommand()
            {
                UserId = user.Id,
                RoleId = aRoleId
            };

            var handler = new ChangeRoleOfUserHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Flag == true
                    && await dbContext.Users
                        .AnyAsync(u => u.Login == "ivan123" && u.RoleId == aRoleId)
                    && !await dbContext.Users
                        .AnyAsync(u=> u.Login == "ivan123" && u.RoleId == uRoleId));
        }

        [Fact]
        public async void ChangeUserStatusOfUserhandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var dUserStatusId = (await dbContext.UserStatuses
                        .AsNoTracking()
                        .FirstOrDefaultAsync(us=> us.Title == "Disabled")).Id;
            var user = await dbContext.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Login == "ivan123");

            var command = new ChangeUserStatusOfUserCommand()
            {
                UserId = user.Id,
                UserStatusId = dUserStatusId
            };

            
            var handler = new ChangeUserStatusOfUserHandler(dbContext);


            //Act
            var result = await handler.Handle(command, default);
        
            //Assert
            Assert.True(result.Flag == true 
                        && await dbContext.Users
                            .AsNoTracking()
                            .AnyAsync(u=> u.Login == "ivan123" && u.UserStatusId == dUserStatusId) != null);
        }

        [Fact]
        public async void CheckLoginPasswordPairHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();

            var command1 = new CheckLoginPasswordPairQuery()
            {
              Login = "ivan123",
              PasswordHash = "qweqwe",
            };

            var command2 = new CheckLoginPasswordPairQuery()
            {
                Login = "ivan123",
                PasswordHash = "aasdasd"
            };

            var command3 = new CheckLoginPasswordPairQuery()
            {
                Login = "boris123",
                PasswordHash="qweqwe"
            };

            var command4 = new CheckLoginPasswordPairQuery()
            {
                Login = "boris123",
                PasswordHash = "asdasd"
            };

            var handler = new CheckLoginPasswordPairHandler(dbContext);


            //Act
            var result1 = await handler.Handle(command1, default);

            var result2 = await handler.Handle(command2, default);

            var result3 = await handler.Handle(command3, default);
            var result4 = await handler.Handle(command4, default);

            //Assert
            Assert.True(result1.Flag == true );
            Assert.True(result2.Flag == false);
            Assert.True(result3.Flag == false);
            Assert.True(result4.Flag == true);
        }

        [Fact]
        public async void ChackLoginSecretWordPairHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();

            var command1 = new CheckLoginSecretWordPairQuery()
            {
                Login = "ivan123",
                SecretWordHash = "1ivan1"
            };

            var command2 = new CheckLoginSecretWordPairQuery()
            {
                Login = "ivan123",
                SecretWordHash = "1boris1"
            };

            var command3 = new CheckLoginSecretWordPairQuery()
            {
                Login = "boris123",
                SecretWordHash = "1ivan1"
            };

            var command4 = new CheckLoginSecretWordPairQuery()
            {
                Login = "boris123",
                SecretWordHash = "1boris1"
            };

            var handler = new CheckLoginSecretWordPairHandler(dbContext);


            //Act
            var result1 = await handler.Handle(command1, default);

            var result2 = await handler.Handle(command2, default);

            var result3 = await handler.Handle(command3, default);
            var result4 = await handler.Handle(command4, default);

            //Assert
            Assert.True(result1.Flag == true);
            Assert.True(result2.Flag == false);
            Assert.True(result3.Flag == false);
            Assert.True(result4.Flag == true);
        }

        [Fact]
        public async void IsUserLoginRegisteredHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var login1 = "ivan123";
            var login2 = "boris123";
            var login3 = "Gosha12";
            var login4 = "Boxer89";


            var command1 = new IsLoginRegisteredQuery()
            {
                EnteredLogin = login1
            };

            var command2 = new IsLoginRegisteredQuery()
            {
                EnteredLogin = login2
            };

            var command3 = new IsLoginRegisteredQuery()
            {
                EnteredLogin = login3
            };

            var command4 = new IsLoginRegisteredQuery()
            {
                EnteredLogin = login4
            };

            var handler = new IsUserLoginRegisteredHandler(dbContext);


            //Act
            var result1 = await handler.Handle(command1, default);
            var result2 = await handler.Handle(command2, default);
            var result3 = await handler.Handle(command3, default);
            var result4 = await handler.Handle(command4, default);


            //Assert
            Assert.True(result1.Flag == true);
            Assert.True(result2.Flag == true);
            Assert.True(result3.Flag == false);
            Assert.True(result4.Flag == false);
        }

        [Fact]
        public async void TakeAuthorizationInfoDTOByLoginHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u=> u.Login == "ivan123");

            var command = new TakeAuthorizationInfoDTOByLoginQuery()
            {
                EnteredLogin = user.Login
            };

            var handler = new TakeAuthorizationInfoDTOByLoginHandler(dbContext);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Id == user.Id 
                    && result.Login == user.Login
                    && result.SecurityStamp == user.SecurityStamp);
        }
        [Fact]
        public async void TakeAuthorizationInfoDTOByUserIdHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == "ivan123");

            var command = new TakeAuthorizationInfoDTOByUserIdQuery()
            {
                UserId = user.Id
            };

            var handler = new TakeAuthorizationInfoDTOByUserIdHandler(dbContext);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result.Id == user.Id
                    && result.Login == user.Login
                    && result.SecurityStamp == user.SecurityStamp);
        }

        [Fact]
        public async void TakeUserIdByLoginHandler()
        {
            //Arrange
            var dbContext = await InitMockDB();
            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Login == "ivan123");

            var command = new TakeUserIdByLoginQuery()
            {
                Login = user.Login
            };

            var handler = new TakeUserIdByLoginHandler(dbContext);

            //Act
            var result = await handler.Handle(command, default);

            //Assert
            Assert.True(result == user.Id);
        }
    }
}
