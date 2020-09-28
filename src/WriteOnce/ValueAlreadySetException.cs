using System;

namespace WriteOnce
{
	/// <summary>
	/// Exception thrown from <see cref="WriteOnce{T}"/> when the value has already been set.
	/// </summary>
	public class ValueAlreadySetException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValueAlreadySetException"/> class.
		/// </summary>
		public ValueAlreadySetException()
			: this($"Attempted to set the value in '{typeof(WriteOnce<>)}', but value already set!")
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValueAlreadySetException"/> class.
		/// </summary>
		/// <param name="message">The error message.</param>
		public ValueAlreadySetException(string message)
			: base(message)
		{

		}
	}
}