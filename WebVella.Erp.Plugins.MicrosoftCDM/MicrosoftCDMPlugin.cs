using System;
using Newtonsoft.Json;
using WebVella.Erp;
using WebVella.Erp.Api;

namespace WebVella.Erp.Plugins.MicrosoftCDM
{
	public partial class MicrosoftCdmPlugin : ErpPlugin
	{

		[JsonProperty(PropertyName = "name")]
		public override string Name { get; protected set; } = "MicrosoftCdmPlugin";

		public override void Initialize(IServiceProvider serviceProvider)
		{
			using (var ctx = SecurityContext.OpenSystemScope())
			{
				ProcessPatches();
			}
		}

	}
}
