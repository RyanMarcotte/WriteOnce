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
		internal ValueAlreadySetException()
			: base($"Attempted to set the value in '{typeof(WriteOnce<>)}', but value already set!")
		{

		}
	}
}