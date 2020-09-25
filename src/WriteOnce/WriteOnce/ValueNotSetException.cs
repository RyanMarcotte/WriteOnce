using System;

namespace WriteOnce
{
	/// <summary>
	/// Exception thrown from <see cref="WriteOnce{T}"/> when the value has not been set yet.
	/// </summary>
	public class ValueNotSetException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValueNotSetException"/> class.
		/// </summary>
		internal ValueNotSetException()
			: base($"Attempted to retrieve a value from '{typeof(WriteOnce<>)}', but value not set!")
		{

		}
	}
}