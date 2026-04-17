using emailapi.Data;
using emailapi.Models;
using EmailApi.Data;
using Microsoft.EntityFrameworkCore;

namespace emailapi.Services
{
    public class UserTaskAndServices
    {
        private readonly AppDbContext _context;
        public UserTaskAndServices(AppDbContext context)
        {
            _context = context;

        }

        public async Task<ResponseVM> CreateService(createServiceVM data)
        {
            try
            {
                UserServices record =new UserServices()
                {
                    Name= data.Name,
                    UserId= data.UserId,
                    Amount= data.Amount,
                    Status= data.Status,
                    Type= data.Type,
                    CreateDate = DateTime.UtcNow,
                    Modified= DateTime.UtcNow,
                    ModifiedBy =""

                };
               
                await _context.UserServices.AddAsync(record);
                await _context.SaveChangesAsync();
                var result =  await _context.UserServices.Where(x => x.UserId == data.UserId).ToListAsync();
                return new ResponseVM()
                {
                    status = 1,
                    Message = "Service created successfully",
                    data = result
                };
            }
            catch (Exception ex) { 
                return new ResponseVM()
                {
                    status=0,
                    Message = ex.Message,
                   
                };
            }
        }

        public async Task<ResponseVM> GetService(int userid)
        {
            try
            {   var data=await _context.UserServices.Where(x => x.UserId == userid).ToListAsync();
                return new ResponseVM()
                {
                    status = 1,
                    data=data
                };
            }
            catch (Exception ex)
            {
                return new ResponseVM()
                {
                    status = 0,
                    Message = ex.Message,

                };
            }
        }

        public async Task<ResponseVM> CreateTask(CreateTaskVM data)
        {
            try
            {
                SubTasks req = new SubTasks()
                {
                    ServiceId=data.ServiceId,
                    UserId=data.UserId,
                    Amount=data.Amount,
                    Title=data.Title,
                    Description=data.Description,
                    Status=data.Status,
                    Progress=data.Progress,
                    StartDate=data.StartDate,
                    EndDate= data.EndDate,
                    Modifiedby="",
                    ModifiedDate= DateTime.UtcNow,


                };
                await _context.SubTasks.AddAsync(req);
                await _context.SaveChangesAsync();
                var result = await _context.SubTasks.Where(x => x.ServiceId == data.ServiceId).ToListAsync();
                return new ResponseVM()
                {
                    status = 1,
                    Message = "Service created successfully",
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseVM()
                {
                    status = 0,
                    Message = ex.Message,

                };
            }
        }

        public async Task<ResponseVM> GetTask(int serviceid)
        {
            try
            {
                var data = await _context.SubTasks.Where(x => x.ServiceId == serviceid).ToListAsync();
                return new ResponseVM()
                {
                    status = 1,
                    data = data
                };
            }
            catch (Exception ex)
            {
                return new ResponseVM()
                {
                    status = 0,
                    Message = ex.Message,

                };
            }
        }

        public async Task<ResponseVM> UpdateTask(SubTasks data)
        {
            try
            {
                var existing = await _context.SubTasks.FirstOrDefaultAsync(x => x.Id == data.Id);
                if (existing == null)
                {
                    return new ResponseVM()
                    {
                        status = 0,
                        Message = "Task not found"
                    };
                }
                existing.Amount = data.Amount;
                existing.Title = data.Title;
                existing.Description = data.Description;
                existing.Status = data.Status;
                existing.StartDate = data.StartDate;
                existing.EndDate = data.EndDate;
                existing.Progress = data.Progress;
                existing.ModifiedDate = DateTime.UtcNow;
                existing.Modifiedby = data.Modifiedby;

                // Save changes
                await _context.SaveChangesAsync();
                var result = await _context.SubTasks
                    .Where(x => x.ServiceId == data.ServiceId)
                    .ToListAsync();

                return new ResponseVM()
                {
                    status = 1,
                    Message = "Task updated successfully",
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseVM()
                {
                    status = 0,
                    Message = ex.Message
                };
            }
        }

        public async Task<ResponseVM> UpdateService(UserServices data)
        {
            try
            {
                // Find existing record
                var existing = await _context.UserServices
                    .FirstOrDefaultAsync(x => x.Id == data.Id);

                if (existing == null)
                {
                    return new ResponseVM()
                    {
                        status = 0,
                        Message = "Service not found"
                    };
                }

                // Update fields
                existing.Name = data.Name;
                existing.Type = data.Type;
                existing.Amount = data.Amount;
                existing.Status= data.Status;
                existing.Modified = DateTime.UtcNow;
                existing.ModifiedBy = data.ModifiedBy;

                // Save changes
                await _context.SaveChangesAsync();

                // Return updated list for same user
                var result = await _context.UserServices
                    .Where(x => x.UserId == existing.UserId)
                    .ToListAsync();

                return new ResponseVM()
                {
                    status = 1,
                    Message = "Service updated successfully",
                    data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseVM()
                {
                    status = 0,
                    Message = ex.Message
                };
            }
        }
    } 
}
