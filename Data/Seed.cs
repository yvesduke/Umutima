using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UMUTIMA.API.Models;

namespace UMUTIMA.API.Data
{
	public class Seed
	{
		/*private readonly DataContext _context;
		public Seed (DataContext context)
		{
			_context = context;
		}*/

		public static void SeedUsers(UserManager<User> userManager)
		{
			if(!userManager.Users.Any())
			{
				var userData = System.IO.File.ReadAllText("Data/UserSeedData.json");
				var users = JsonConvert.DeserializeObject<List<User>>(userData);
				foreach(var user in users)
				{
					//byte[] passwordhash, passwordSalt;
					//CreatePasswordHash("password", out passwordhash, out passwordSalt);
					/*user.PasswordHash = passwordhash;
					user.PasswordSalt = passwordSalt;*/
					//user.UserName = user.UserName.ToLower();
					//context.Users.Add(user);
					userManager.CreateAsync(user, "password").Wait();

				}
				//context.SaveChanges();
			}
		}
		private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
		{
			using (var hmac = new System.Security.Cryptography.HMACSHA512())
			{
				passwordSalt = hmac.Key;
				passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
			}
		}

	}
}
