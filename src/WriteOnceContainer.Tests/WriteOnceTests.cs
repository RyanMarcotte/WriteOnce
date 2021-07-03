using System;
using FluentAssertions;
using Functional;
using Functional.Primitives.FluentAssertions;
using Xunit;

namespace WriteOnceContainer.Tests
{
	public class WriteOnceTests
	{
		private const int VALUE = 1337;
		private const string CUSTOM_NOT_SET_ERROR_MESSAGE = "not set";
		private const string CUSTOM_ALREADY_SET_ERROR_MESSAGE = "already set";
		private static readonly Func<ValueNotSetException> _notSetExceptionFactory = () => new ValueNotSetException(CUSTOM_NOT_SET_ERROR_MESSAGE);
		private static readonly Func<ValueAlreadySetException> _alreadySetExceptionFactory = () => new ValueAlreadySetException(CUSTOM_ALREADY_SET_ERROR_MESSAGE);

		private static readonly WriteOnce<int> _notSetSUT = new WriteOnce<int>();
		private static readonly WriteOnce<int> _alreadySetSUT = new WriteOnce<int> { Value = VALUE };
		private static readonly WriteOnce<int> _notSetCustomErrorMessageSUT = new WriteOnce<int>(_notSetExceptionFactory, _alreadySetExceptionFactory);
		private static readonly WriteOnce<int> _alreadySetCustomErrorMessageSUT = new WriteOnce<int>(_notSetExceptionFactory, _alreadySetExceptionFactory) { Value = VALUE };

		public class ValueIsSet
		{
			[Fact]
			public void GetValue_ReturnValue()
			{
				_alreadySetSUT.HasValue.Should().BeTrue();
				_alreadySetSUT.Value.Should().Be(VALUE);
			}

			[Fact]
			public void ThrowIfNotSet_DoesNotThrowException()
			{
				Result.Try(() => _alreadySetSUT.ThrowIfNotSet())
					.Should()
					.BeSuccessful();
			}

			[Fact]
			public void SetValue_ThrowException()
			{
				Result.Try(() => _alreadySetSUT.Value = VALUE)
					.Should()
					.BeFaulted()
					.AndFailureValue
					.Should()
					.BeOfType<ValueAlreadySetException>();
			}

			[Fact]
			public void SetValue_ThrowExceptionWithCustomMessage()
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

		public class ValueIsNotSet
		{
			[Fact]
			public void GetValue_ThrowException()
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
			public void GetValue_ThrowExceptionWithCustomErrorMessage()
			{
				_notSetCustomErrorMessageSUT.HasValue.Should().BeFalse();

				Result.Try(() => _notSetCustomErrorMessageSUT.Value)
					.Should()
					.BeFaulted()
					.AndFailureValue
					.Should()
					.BeOfType<ValueNotSetException>()
					.And.Match<ValueNotSetException>(e => e.Message == CUSTOM_NOT_SET_ERROR_MESSAGE);
			}

			[Fact]
			public void ThrowIfNotSet_ThrowException()
			{
				_notSetSUT.HasValue.Should().BeFalse();

				Result.Try(() => _notSetSUT.ThrowIfNotSet())
					.Should()
					.BeFaulted()
					.AndFailureValue
					.Should()
					.BeOfType<ValueNotSetException>();
			}

			[Fact]
			public void ThrowIfNotSet_ThrowExceptionWithCustomErrorMessage()
			{
				_notSetCustomErrorMessageSUT.HasValue.Should().BeFalse();

				Result.Try(() => _notSetCustomErrorMessageSUT.ThrowIfNotSet())
					.Should()
					.BeFaulted()
					.AndFailureValue
					.Should()
					.BeOfType<ValueNotSetException>()
					.And.Match<ValueNotSetException>(e => e.Message == CUSTOM_NOT_SET_ERROR_MESSAGE);
			}
		}
	}
}