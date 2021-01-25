using System;
using Bunit;
using FluentAssertions;
using Xunit;

namespace WriteOnceContainer.Blazor.Tests
{
	public class WriteOnceMatchTests
	{
		private const string VALUE = "the container is set";
		private const string NO_VALUE = "the container is not set";

		[Fact]
		public void DisplaysValueWhenContainerHasValueSet()
		{
			using var context = new TestContext();
			var cut = context.RenderComponent<WriteOnceMatch<string>>(parameters => parameters
				.Add(c => c.WriteOnce, new WriteOnce<string>().WithValue(VALUE))
				.Add(c => c.ValueSet, s => $"<p>{s}</p>")
				.Add(c => c.ValueNotSet, $"<p>{NO_VALUE}</p>"));

			cut.Markup.Should().Contain(VALUE);
		}

		[Fact]
		public void DisplaysNoValueWhenContainerHasNoValueSet()
		{
			using var context = new TestContext();
			var cut = context.RenderComponent<WriteOnceMatch<string>>(parameters => parameters
				.Add(c => c.WriteOnce, new WriteOnce<string>())
				.Add(c => c.ValueSet, s => $"<p>{s}</p>")
				.Add(c => c.ValueNotSet, $"<p>{NO_VALUE}</p>"));

			cut.Markup.Should().Contain(NO_VALUE);
		}
	}

	internal static class WriteOnceExtensions
	{
		public static WriteOnce<T> WithValue<T>(this WriteOnce<T> source, T value)
		{
			source.Value = value;
			return source;
		}
	}
}
