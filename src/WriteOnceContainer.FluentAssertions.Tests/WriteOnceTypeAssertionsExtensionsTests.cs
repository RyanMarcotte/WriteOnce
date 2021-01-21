using System;
using FluentAssertions;
using Xunit;

namespace WriteOnceContainer.FluentAssertions.Tests
{
	public class WriteOnceTypeAssertionsTests
	{
		public class ValueChecks
		{
			private const int VALUE = 3;
			private readonly WriteOnce<int> _sutWithValue = new WriteOnce<int>() { Value = VALUE };
			
			[Fact]
			public void ShouldNotThrowException() => new Action(() =>
			{
				_sutWithValue
					.Should()
					.HaveValue();

			}).Should().NotThrow();

			[Fact]
			public void ShouldNotThrowExceptionWhenAdditionalAssertionSucceeds() => new Action(() =>
			{
				_sutWithValue
					.Should()
					.HaveValue()
					.AndValue
					.Should()
					.Be(VALUE);

			}).Should().NotThrow();

			[Fact]
			public void ShouldThrowExceptionWithNameOfVariable()
			{
				var localVariable = new WriteOnce<int>();
				new Action(() => localVariable.Should().HaveValue())
					.Should()
					.Throw<Exception>()
					.And.Message.Should().ContainAll(nameof(localVariable));
			}

			[Fact]
			public void ShouldThrowExceptionWithNameOfClassMember()
			{
				var envelope = new WriteOnceEnvelope<int>(new WriteOnce<int>());
				new Action(() => envelope.Data.Should().HaveValue())
					.Should()
					.Throw<Exception>()
					.And.Message.Should().ContainAll(nameof(envelope), nameof(WriteOnceEnvelope<int>.Data));
			}
		}

		public class NoValueChecks
		{
			private readonly WriteOnce<int> _sutWithValue = new WriteOnce<int>();
			private readonly WriteOnce<int> _sutWithoutValue = new WriteOnce<int>();


			public NoValueChecks()
			{
				_sutWithValue.Value = 3;
			}

			[Fact]
			public void ShouldNotThrowException() => new Action(() => _sutWithoutValue.Should().NotHaveValue()).Should().NotThrow();

			[Fact]
			public void ShouldThrowExceptionWithNameOfVariable()
			{
				var localVariable = new WriteOnce<int> { Value = 3 };
				new Action(() => localVariable.Should().NotHaveValue())
					.Should()
					.Throw<Exception>()
					.And.Message.Should().Contain(nameof(localVariable));
			}

			[Fact]
			public void ShouldThrowExceptionWithNameOfClassMember()
			{
				var envelope = new WriteOnceEnvelope<int>(new WriteOnce<int> { Value = 15 });
				new Action(() => envelope.Data.Should().NotHaveValue())
					.Should()
					.Throw<Exception>()
					.And.Message.Should().ContainAll(nameof(envelope), nameof(WriteOnceEnvelope<int>.Data));
			}
		}

		private class WriteOnceEnvelope<T>
		{
			public WriteOnceEnvelope(WriteOnce<T> data)
			{
				Data = data;
			}

			public WriteOnce<T> Data { get; }
		}
	}
}
