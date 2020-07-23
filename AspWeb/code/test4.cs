using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AspWeb.code
{

    public class UserIdentity : IdentityUser
    {
        //public string Id { get; set; }

        //public string UserName { get; set; }

        //public string PasswordHash { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }



        public override string ToString()
        {
            return string.Format("{0},{1},{2}", this.Id, this.UserName, this.PasswordHash);
        }

        public static UserIdentity FromString(string strUser)
        {
            if (string.IsNullOrWhiteSpace(strUser))
            {
                return null;
                throw new ArgumentNullException("user");
            }

            var arr = strUser.Split(',');
            if (arr.Length != 3)
            {
                return null;
                throw new InvalidOperationException("user is not valid");
            }
            var user = new UserIdentity();
            user.Id = arr[0];
            user.UserName = arr[1];
            user.PasswordHash = arr[2];

            return user;
        }
    }
    public class UserStore : Microsoft.AspNet.Identity.IUserStore<UserIdentity>, IUserPasswordStore<UserIdentity>
    {

        // 创建用户
        public async Task CreateAsync(UserIdentity user)
        {
            user.Id = Guid.NewGuid().ToString();
            using (var stream = new System.IO.StreamWriter(_filePath, true, Encoding.UTF8))
            {
                await stream.WriteLineAsync(user.ToString());
            }
        }

        // 根据用户名找用户
        public async Task<UserIdentity> FindByNameAsync(string userName)
        {
            using (var stream = new System.IO.StreamReader(_filePath))
            {
                string line;
                UserIdentity result = null;
                if (userName == "aaa")
                {
                    result = new UserIdentity() { Password = "YmJi", Id = "1", RememberMe = true, UserName = userName , PasswordHash= "YmJi" };
                }


                //while ((line = await stream.ReadLineAsync()) != null)
                //{
                //    var user = UserIdentity.FromString(line);
                //    if (user==null)
                //    {
                //        continue;
                //    }
                //    if (user.UserName == userName)
                //    {
                //        result = user;
                //        break;
                //    }
                //}
                return result;
            }
        }




        public string _filePath { get; private set; } = @"d:\a.txt";

        //// 创建用户
        //public async Task CreateAsync(UserIdentity user)
        //{
        //    user.Id = Guid.NewGuid().ToString();
        //    using (var stream = new System.IO.StreamWriter(_filePath, true, Encoding.UTF8))
        //    {
        //        await stream.WriteLineAsync(user.ToString());
        //    }
        //}

        //// 根据用户名找用户
        //public async Task<UserIdentity> FindByNameAsync(string userName)
        //{
        //    using (var stream = new System.IO.StreamReader(_filePath))
        //    {
        //        string line;
        //        UserIdentity result = null;
        //        while ((line = await stream.ReadLineAsync()) != null)
        //        {
        //            var user = UserIdentity.FromString(line);
        //            if (user.UserName == userName)
        //            {
        //                result = user;
        //                break;
        //            }
        //        }
        //        return result;
        //    }
        //}
        //public Task CreateAsync(IdentityUser user)
        //{
        //    throw new NotImplementedException();
        //}

        public Task DeleteAsync(UserIdentity user)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public Task<UserIdentity> FindByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }

        //public Task<IdentityUser> FindByNameAsync(string userName)
        //{
        //    throw new NotImplementedException();
        //}

        public Task UpdateAsync(UserIdentity user)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(UserIdentity user, string passwordHash)
        {
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(UserIdentity user)
        {
            // return "";
            return Task.FromResult((user.Password));
        }

        public Task<bool> HasPasswordAsync(UserIdentity user)
        {
            return Task.FromResult(true); ;
        }
    }
}