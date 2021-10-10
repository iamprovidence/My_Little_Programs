using Messages.Application.Contracts.SendSms;
using System.Threading.Tasks;

namespace Messages.Application.Contracts
{
	public interface IMessageApiClient
	{
		Task Execute(SendSmsCommand command);
	}
}
