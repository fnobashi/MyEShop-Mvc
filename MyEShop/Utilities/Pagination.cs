namespace MyEShop.Utilities
{
	public static class Pagination
	{
		public static IQueryable<TSource> ToPaged<TSource>(this IQueryable<TSource> source, int page, int pageSize, out int totalPages)
		{
            int totalCount = source.Count();
            totalPages = (totalCount + pageSize - 1) / pageSize;

            return source.Skip((page - 1 ) * pageSize).Take(pageSize);
		}

    }
}
