namespace MyEShop.Utilities
{
	public static class Pagination
	{
		public static IEnumerable<TSource> ToPaged<TSource>(this IEnumerable<TSource> source, int page, int pageSize, out int totalPages)
		{
			totalPages =  (source.Count()) / pageSize;
			return source.Skip((page - 1 ) * pageSize).Take(pageSize);
		}

	}
}
