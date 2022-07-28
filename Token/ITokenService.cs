using BlogIdentity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogIdentity.Token
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
