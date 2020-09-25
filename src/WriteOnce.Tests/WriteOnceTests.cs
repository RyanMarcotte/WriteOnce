using System;
using FluentAssertions;
using Functional;
using Functional.Primitives.FluentAssertions;
using Xunit;

namespace WriteOnce.Tests
{
	public class WriteOnceTests
	{
		private const int VALUE = 1337;
		private readonly WriteOnce<int> _notSetSUT = new WriteOnce<int>();
		private readonly WriteOnce<int> _alreadySetSUT = new WriteOnce<int> { Value = VALUE };

		[Fact]
		public void ShouldGetValueWhenAttemptingToAccessTheValueAndValueHasBeenSet()
		{
			_alreadySetSUT.HasValue.Should().BeTrue();
			_alreadySetSUT.Value.Should().Be(VALUE);
		}

		[Fact]
		public void ShouldThrowAnExceptionWhenAttemptingToAccessTheValueAndValueHasNotBeenSet()
		{
			_notSetSUT.HasValue.Should().BeFalse();
			Result.Try(() => _notSetSUT.Value)
				.Should()
				.BeFaulted()
				.AndFailureValue
				.Should()
				.BeOfType<ValueNotSetException>();
		}

		[Fact]
		public void ShouldThrowAnExceptionWhenAttemptingToSetTheValueAndValueHasAlreadyBeenSet()
		{
			Result.Try(() => _alreadySetSUT.Value = VALUE)
				.Should()
				.BeFaulted()
				.AndFailureValue
				.Should()
				.BeOfType<ValueAlreadySetException>();
		}
	}
}