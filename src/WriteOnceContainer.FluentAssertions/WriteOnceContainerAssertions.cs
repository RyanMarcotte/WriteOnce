namespace WriteOnceContainer.FluentAssertions
{
	/// <summary>
	/// Defines additional fluent assertion gateways for types defined in Functional.Primitives namespace.
	/// </summary>
	public static class WriteOnceContainerAssertions
	{
		/// <summary>
		/// Returns a <see cref="WriteOnceTypeAssertions{T}"/> object that is used to assert the current <see cref="WriteOnce{T}"/>.
		/// </summary>
		/// <typeparam name="T">The type.</typeparam>
		/// <param name="source">The source.</param>
		/// <returns></returns>
		public static WriteOnceTypeAssertions<T> Should<T>(this WriteOnce<T> source)
		{
			return new WriteOnceTypeAssertions<T>(source);
		}
	}
}