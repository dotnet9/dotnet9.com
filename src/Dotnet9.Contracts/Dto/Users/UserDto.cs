using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet9.Contracts.Dto.Users;

public record UserDto(Guid Id, string Account, string NickName, string PhoneNumber, string Email);