using FlowStage.Interfaces;
using FlowStage.Models;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ShopWizard.Controllers.Abstract
{
	public abstract class FlowControllerBase<TContext> : Controller
		where TContext : IFlowContextChangeable, new()
	{
		private readonly IFlowStageOrchestrator _flowOrchestrator;
		private readonly IFlowStagePresenterOrchestrator _presenterOrchestrator;

		protected abstract FlowStageIdentifier InitialStage { get; }

		public FlowControllerBase(IFlowStageOrchestrator flowOrchestrator, IFlowStagePresenterOrchestrator presenterOrchestrator)
		{
			_flowOrchestrator = flowOrchestrator;
			_presenterOrchestrator = presenterOrchestrator;
		}

		[HttpGet]
		public async Task<IActionResult> Index()
		{
			ResetFlowContext();

			var stageActionName = GetViewActionName(InitialStage);

			return RedirectToAction(stageActionName);
		}

		public async Task<IActionResult> ProcessCommand(IFlowCommand command)
		{
			var context = GetFlowContext();

			await _flowOrchestrator.ProcessCommand(context, command);

			SaveFlowContext(context);

			var stageActionName = GetViewActionName(context.CurrentStage);

			return RedirectToAction(stageActionName);
		}

		public async Task<IActionResult> ShowView<TViewModel>()
		{
			var context = GetFlowContext();

			var viewName = context.CurrentStage.ToString().Replace("FlowStage", string.Empty);
			var viewModel = await _presenterOrchestrator.ShowView<TContext, TViewModel>(context);

			return View(viewName, viewModel);
		}

		private string GetViewActionName(FlowStageIdentifier stage)
		{
			var method = GetType().GetMethods()
				.Where(m => m.IsDefined(typeof(HttpGetAttribute), false))
				.Where(m => m.IsDefined(typeof(RouteAttribute), false))
				.Where(m => m.GetCustomAttributes(typeof(RouteAttribute), inherit: false)
					.OfType<RouteAttribute>()
					.Any(a => a.Template == stage))
				.SingleOrDefault();

			if (method == null)
			{
				throw new InvalidOperationException($"No GET method defined for {stage}");
			}

			return method.Name;
		}

		#region FlowContext
		protected void ResetFlowContext()
		{
			HttpContext
				.Response
				.Cookies
				.Delete(GetFlowContextKey());
		}

		private TContext GetFlowContext()
		{
			var flowStateFromCoockie = HttpContext.Request.Cookies[GetFlowContextKey()];

			if (string.IsNullOrWhiteSpace(flowStateFromCoockie))
			{
				var context = new TContext();

				context.SetCurrentStage(InitialStage);

				return context;
			}

			return DeserializeContext(flowStateFromCoockie);
		}

		private void SaveFlowContext(TContext context)
		{
			var serializedContext = SerializeContext(context);

			HttpContext
				.Response
				.Cookies
				.Append(GetFlowContextKey(), serializedContext);
		}

		private static string GetFlowContextKey()
		{
			return typeof(TContext).Name;
		}

		private string SerializeContext(TContext context)
		{
			var dataProtectionProvider = HttpContext.RequestServices.GetService<IDataProtectionProvider>();
			var protector = dataProtectionProvider.CreateProtector(GetFlowContextKey());

			var serializedContext = JsonConvert.SerializeObject(context);

			return protector.Protect(serializedContext);
		}

		private TContext DeserializeContext(string serializedContext)
		{
			var dataProtectionProvider = HttpContext.RequestServices.GetService<IDataProtectionProvider>();
			var protector = dataProtectionProvider.CreateProtector(GetFlowContextKey());

			var unprotectedContext = protector.Unprotect(serializedContext);

			return JsonConvert.DeserializeObject<TContext>(unprotectedContext);
		}
		#endregion
	}
}
