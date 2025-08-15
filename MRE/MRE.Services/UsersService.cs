using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MRE.Models;
using MRE.Models.Requests;
using MRE.Models.SearchObjects;
using MRE.Services.Database;

using Microsoft.EntityFrameworkCore;


namespace MRE.Services
{
    public class UsersService : BaseCRUDService<Users, User, BaseSearchObject, UserInsertRequest>, IUsersService
    {
        public UsersService(MovieRatingEngineContext context, IMapper mapper) : base(context, mapper)
        {
        }

        public static string GenerateSalt()
        {
            var provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[16];
            provider.GetBytes(byteArray);

            return Convert.ToBase64String(byteArray);
        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dist = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dist, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dist, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dist);
            return Convert.ToBase64String(inArray);
        }


        public async Task<Users> Login(string username, string password)
        {
            var entity = await _context.Users.FirstOrDefaultAsync(x => x.Username == username);

            if (entity == null)
            {
                return null;
            }
          
            var hash = GenerateHash(entity.PasswordSalt, password);

            if (hash != entity.PasswordHash)
            {
                return null;
            }
            return _mapper.Map<Users>(entity);
        }
    }
}
