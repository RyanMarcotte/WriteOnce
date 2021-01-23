using System.Diagnostics;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;

namespace WriteOnceContainer.FluentAssertions
{
	/// <summary>
	/// Defines assertions for <see cref="WriteOnce{TValue}"/> type.
	/// </summary>
	/// <typeparam name="T">The contained type.</typeparam>
	[DebuggerNonUserCode]
	public class WriteOnceTypeAssertions<T>
	{
		private const string IDENTIFIER = "container";

		private readonly WriteOnce<T> _subject;

		/// <summary>
		/// Initializes a new instance of the <see cref="WriteOnceTypeAssertions{T}"/> class.
		/// </summary>
		/// <param name="subject">The <see cref="WriteOnce{T}"/> instance to verify.</param>
		public WriteOnceTypeAssertions(WriteOnce<T> subject)
		{
			_subject = subject;
		}

		/// <summary>
		/// Verifies that the subject <see cref="WriteOnce{T}"/> holds a value.
		/// </summary>
		/// <param name="because">Additional information for if the assertion fails.</param>
		/// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
		[CustomAssertion]
		public AndWriteOnceValueConstraint<T> HaveValue(string because = "", params object[] becauseArgs)
		{
			Execute.Assertion
				.ForCondition(_subject.HasValue)
				.BecauseOf(because, becauseArgs)
				.WithDefaultIdentifier(IDENTIFIER)
				.FailWith($"Expected {{context:{IDENTIFIER}}} to have value set{{reason}}, but value has not been set yet.", _subject);

			return new AndWriteOnceValueConstraint<T>(_subject.Value);
		}

		/// <summary>
		/// Verifies that the subject <see cref="WriteOnce{T}"/> does not hold a value.
		/// </summary>
		/// <param name="because">Additional information for if the assertion fails.</param>
		/// <param name="becauseArgs">Zero or more objects to format using the placeholders in <paramref name="because"/>.</param>
		[CustomAssertion]
		public void NotHaveValue(string because = "", params object[] becauseArgs)
		{
			Execute.Assertion
				.ForCondition(!_subject.HasValue)
				.BecauseOf(because, becauseArgs)
				.WithDefaultIdentifier(IDENTIFIER)
				.FailWith(FailReasonForNotHaveValue);

			FailReason FailReasonForNotHaveValue()
			{
				var builder = new StringBuilder();
				builder.AppendLine($"Expected {{context:{IDENTIFIER}}} to not have value set{{reason}}, but the value has been set:");
				builder.AppendLine(_subject.Value.ToString());

				return new FailReason(builder.ToString());
			}
		}
	}
}