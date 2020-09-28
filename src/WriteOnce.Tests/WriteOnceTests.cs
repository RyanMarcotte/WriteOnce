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
		private const string CUSTOM_NOT_SET_ERROR_MESSAGE = "not set";
		private const string CUSTOM_ALREADY_SET_ERROR_MESSAGE = "already set";
		private static readonly Func<ValueNotSetException> _notSetExceptionFactory = () => new ValueNotSetException(CUSTOM_NOT_SET_ERROR_MESSAGE);
		private static readonly Func<ValueAlreadySetException> _alreadySetExceptionFactory = () => new ValueAlreadySetException(CUSTOM_ALREADY_SET_ERROR_MESSAGE);

		private readonly WriteOnce<int> _notSetSUT = new WriteOnce<int>();
		private readonly WriteOnce<int> _alreadySetSUT = new WriteOnce<int> { Value = VALUE };
		private readonly WriteOnce<int> _notSetCustomErrorMessageSUT = new WriteOnce<int>(_notSetExceptionFactory, _alreadySetExceptionFactory);
		private readonly WriteOnce<int> _alreadySetCustomErrorMessageSUT = new WriteOnce<int>(_notSetExceptionFactory, _alreadySetExceptionFactory) { Value = VALUE };

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

		[Fact]
		public void ShouldThrowAnExceptionWithCustomErrorMessageWhenAttemptingToAccessTheValueAndValueHasNotBeenSet()
		{
			_notSetSUT.HasValue.Should().BeFalse();
			Result.Try(() => _notSetCustomErrorMessageSUT.Value)
				.Should()
				.BeFaulted()
				.AndFailureValue
				.Should()
				.BeOfType<ValueNotSetException>()
				.And.Match<ValueNotSetException>(e => e.Message == CUSTOM_NOT_SET_ERROR_MESSAGE);
		}

		[Fact]
		public void ShouldThrowAnExceptionWithCustomErrorMessageWhenAttemptingToSetTheValueAndValueHasAlreadyBeenSet()
		{
			Result.Try(() => _alreadySetCustomErrorMessageSUT.Value = VALUE)
				.Should()
				.BeFaulted()
				.AndFailureValue
				.Should()
				.BeOfType<ValueAlreadySetException>()
				.And.Match<ValueAlreadySetException>(e => e.Message == CUSTOM_ALREADY_SET_ERROR_MESSAGE);
		}
	}
}