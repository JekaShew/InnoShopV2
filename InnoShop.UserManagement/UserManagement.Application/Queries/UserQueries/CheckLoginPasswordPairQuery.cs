using InnoShop.CommonLibrary.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Queries.UserQueries
{
    public class CheckLoginPasswordPairQuery : IRequest<Response>
    {
        public string Login { get; set; }
        public string PasswordHash { get; set; }
    }
}
