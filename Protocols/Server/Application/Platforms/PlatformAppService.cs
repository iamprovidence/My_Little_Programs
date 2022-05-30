using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Server.Application.Common;
using Server.Application.Platforms.ViewModels.AddPlatform;
using Server.Domain;
using Server.Infrastructure;

namespace Server.Application.Platforms
{
    public class PlatformAppService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IJobService _jobService;
        private readonly IEnumerable<IEventSender> _eventSenders;

        public PlatformAppService(
            AppDbContext appDbContext,
            IJobService jobService,
            IEnumerable<IEventSender> eventSenders)
        {
            _appDbContext = appDbContext;
            _jobService = jobService;
            _eventSenders = eventSenders;
        }

        public Task SyncPlatforms(CancellationToken cancellationToken)
        {
            // use case logic

            return Task.CompletedTask;
        }

        public async Task<AddPlatformResponse> AddPlatform(AddPlatformRequest request, CancellationToken cancellationToken)
        {
            var platform = new Platform
            {
                Name = request.Name,
            };

            _appDbContext.Add(platform);

            await _appDbContext.SaveChangesAsync(cancellationToken);

            await _jobService.QueueJob<LogTimeJob>(cancellationToken);

            foreach (var eventSender in _eventSenders)
            {
                await eventSender.Publish(new PlatformAddedEvent
                {
                    Id = platform.Id,
                    Name = platform.Name,
                }, cancellationToken);
            }

            return new AddPlatformResponse
            {
                Id = platform.Id,
                Name = platform.Name,
            };
        }
    }
}
