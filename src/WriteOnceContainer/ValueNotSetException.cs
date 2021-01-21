using System;

namespace WriteOnceContainer
{
	/// <summary>
	/// Exception thrown from <see cref="WriteOnce{T}"/> when the value has not been set yet.
	/// </summary>
	public class ValueNotSetException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValueNotSetException"/> class.
		/// </summary>
		public ValueNotSetException()
			: this($"Attempted to retrieve a value from '{typeof(WriteOnce<>)}', but value not set!")
		{

		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ValueNotSetException"/> class.
		/// </summary>
		/// <param name="message"></param>
		public ValueNotSetException(string message)
			: base(message)
		{

		}
	}
}