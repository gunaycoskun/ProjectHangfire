using Hangfire;
using Hangfire.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectHangfire.Entity;
using ProjectHangfire.Triggers;

namespace ProjectHangfire.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BackgroundJobController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BackgroundJobController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<OnlyDateEntity> CreateUser()
        {
            //Hangfire.BackgroundJob.Enqueue(() => TriggerClass.SendData());

            //Hangfire.BackgroundJob.Schedule(() => TriggerClass.SendData(),TimeSpan.FromSeconds(10));

            // Hangfire.RecurringJob.AddOrUpdate(() => TriggerClass.SendData(), Hangfire.Cron.MinuteInterval(1));
           Hangfire.RecurringJob.AddOrUpdate(() => TriggerClass.SendData(), "*/20 * * * * *");
            return await Task.FromResult(new OnlyDateEntity { CreatedDate=DateTime.UtcNow});
        }
         
        [HttpGet]
        public async Task<string> GetDate()
        {
            var date = new OnlyDateEntity
            {
                CreatedDate = DateTime.UtcNow,
            };
            await _context.OnlyDateEntities.AddAsync(date);
            await _context.SaveChangesAsync();
            return await Task.FromResult(date.CreatedDate.ToString());
        }

        [HttpGet]
        public async Task<string> ResetAllJobs()
        {
            using (var connection = JobStorage.Current.GetConnection())
            {
                foreach (var recurringJob in connection.GetRecurringJobs())
                {
                    RecurringJob.RemoveIfExists(recurringJob.Id);
                }
            }
            return await Task.FromResult("Success");
        }
       
       
    }
}
