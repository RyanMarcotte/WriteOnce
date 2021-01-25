using Microsoft.AspNetCore.Components;

namespace WriteOnceContainer.Blazor
{
	public partial class WriteOnceMatch<T> : ComponentBase
	{
		[Parameter] public WriteOnce<T> WriteOnce { get; set; }
		[Parameter] public RenderFragment<T> ValueSet { get; set; }
		[Parameter] public RenderFragment ValueNotSet { get; set; }

		private RenderFragment GetRenderFragment()
		{
			return WriteOnce.HasValue
				? ValueSet(WriteOnce.Value)
				: ValueNotSet;
		}
	}
}
