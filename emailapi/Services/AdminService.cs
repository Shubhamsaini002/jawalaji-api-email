using emailapi.Data;
using emailapi.Models;
using EmailApi.Data;
using Microsoft.EntityFrameworkCore;

namespace emailapi.Business.Services
{
    public class AdminService 
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context, IConfiguration config)
        {
            _context = context;
        }

        public async Task<ResponseVM> database(adminVM data)
        {
            using var conn = _context.Database.GetDbConnection();
            await conn.OpenAsync();

            using var cmd = conn.CreateCommand();
            cmd.CommandText = data.Query;

            // SELECT
            if (data.Query.TrimStart().StartsWith("SELECT", StringComparison.OrdinalIgnoreCase))
            {
                using var reader = await cmd.ExecuteReaderAsync();

                var result = new List<Dictionary<string, object>>();

                while (await reader.ReadAsync())
                {
                    var row = new Dictionary<string, object>();

                    for (int i = 0; i < reader.FieldCount; i++)
                        row[reader.GetName(i)] = reader.GetValue(i);

                    result.Add(row);
                }

                return new ResponseVM()
                {
                    status = 1,
                    data = result
                };
            }
            else
            {
                // INSERT / UPDATE / DELETE
                var affected = await cmd.ExecuteNonQueryAsync();

                return new ResponseVM()
                {
                    status = 1,
                    data = new { rowsAffected = affected }
                };
            }
        }

        public async Task<ResponseVM> checkLogin(string email ,string password)
        {
            var existingUser = await _context.AdminLogin.FirstOrDefaultAsync(x => x.Email == email && x.Password == password );
            if (existingUser == null)
            {
                return new ResponseVM()
                {
                    status = 0,
                    Message = "Please Check Your Email Or Password!",
                };
            }

            return new ResponseVM()
            {
                status = 1,
                Message = "Verified successfully",
                data = new UserDetailsVM()
                {
                    Name = existingUser.Name,
                    Email = existingUser.Email,
                }
            };
        }

        public async Task<ResponseVM> getUsers()
        {
            var data = await _context.Users.ToListAsync();

            return new ResponseVM()
            {
                status = 1,
                data = data
            };
        }
    }
}