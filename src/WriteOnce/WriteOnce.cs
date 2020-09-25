using System;
using System.Threading.Tasks;

namespace WriteOnce
{
	/// <summary>
	/// Encapsulates a value that can only be set once.
	/// </summary>
	/// <typeparam name="T">The value type.</typeparam>
	public class WriteOnce<T>
	{
		private readonly TaskCompletionSource<T> _tcs = new TaskCompletionSource<T>();

		/// <summary>
		/// Indicates if the value has been set.
		/// </summary>
		public bool HasValue => _tcs.Task.IsCompleted;

		/// <summary>
		/// Gets or sets the value of this <see cref="WriteOnce{T}"/>.
		/// </summary>
		public T Value
		{
			get => HasValue ? _tcs.Task.Result : throw new ValueNotSetException();
			set => _tcs.TrySetResult(value).EnsureTrue(() => throw new ValueAlreadySetException());
		}

		/// <summary>
		/// Gets the value of this <see cref="WriteOnce{T}"/>.
		/// </summary>
		/// <param name="source">The <see cref="WriteOnce{T}"/> instance.</param>
		public static implicit operator T(WriteOnce<T> source) => source.Value;

		/// <summary>
		/// Returns a string that represents the current object.
		/// </summary>
		/// <returns>A string that represents the current object.</returns>
		public override string ToString() => HasValue ? _tcs.Task.Result.ToString() : "{ }";

		/// <summary>
		/// Gets the hash code for the value.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode() => HasValue ? _tcs.Task.Result.GetHashCode() : 0;
	}

	internal static class BooleanExtensions
	{
		public static void EnsureTrue(this bool value, Func<Exception> exceptionFactory)
		{
			if (!value)
				throw exceptionFactory();
		}
	}
}
