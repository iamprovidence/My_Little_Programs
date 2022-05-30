using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Client.Contracts.ViewModels;
using Client.Contracts.ViewModels.AddPlatform;

namespace Client.Contracts
{
    internal interface IApiClient
    {
        Task<IReadOnlyCollection<PlatformViewModel>> GetPlatforms();
        Task<IReadOnlyCollection<CommandViewModel>> GetCommands();

        Task<AddPlatformResponse> AddPlatform(AddPlatformRequest request, CancellationToken cancellationToken);
        IDisposable SubscribeToAddPlatformStream(Action<PlatformAddedEvent> action, CancellationToken cancellationToken);
    }
}
