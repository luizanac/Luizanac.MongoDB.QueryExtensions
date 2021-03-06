namespace Luizanac.MongoDB.QueryExtensions.Extensions;

public static class QueryExtensions
{
	/// <summary>
	/// Create a query from <see cref="IMongoCollection{TDocument}"/>
	/// </summary>
	/// <param name="collection"><see cref="IMongoCollection{TDocument}"/></param>
	/// <param name="caseType"><see cref="ECaseType"/></param>
	/// <typeparam name="T">Entity type</typeparam>
	/// <returns>The query</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public static Query<T> Query<T>(this IMongoCollection<T> collection, ECaseType caseType = ECaseType.PascalCase)
			where T : class
	{
		_ = collection ?? throw new ArgumentNullException(nameof(collection), "collection can't be null");
		return new Query<T>(collection, caseType);
	}

	/// <summary>
	/// Create a query from <see cref="IMongoCollection{TDocument}"/>
	/// </summary>
	/// <param name="collection"><see cref="IMongoCollection{TDocument}"/></param>
	/// <param name="options"><see cref="FindOptions{TDocument}"/></param>
	/// <param name="caseType"><see cref="ECaseType"/></param>
	/// <typeparam name="T">Eentity type</typeparam>
	/// <returns>The query</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public static Query<T> Query<T>(this IMongoCollection<T> collection, FindOptions<T> options, ECaseType caseType = ECaseType.PascalCase)
			where T : class
	{
		_ = collection ?? throw new ArgumentNullException(nameof(collection), "collection can't be null");
		return new Query<T>(collection, options, caseType);
	}

	/// <summary>
	/// Fetch data from your <see cref="Query{T}"/>
	/// </summary>
	/// <param name="query"><see cref="Query{T}"/></param>
	/// <typeparam name="T">Your entity type</typeparam>
	/// <returns>The fetched data</returns>
	/// <exception cref="ArgumentNullException"></exception>
	public static async Task<IList<T>> ToListAsync<T>(this Query<T> query)
			where T : class
	{
		_ = query ?? throw new ArgumentNullException(nameof(query), "query can't be null");
		return await query.Collection.FindAsync(query.GetFilterDefinition(), query.Options).ToListAsync();
	}

	/// <summary>
	/// Wrapper to call ToListAsync
	/// </summary>
	/// <param name="cursor"><see cref="IAsyncCursor{TDocument}"/></param>
	/// <typeparam name="T">Your entity type</typeparam>
	/// <returns>The fetched data</returns>
	public static async Task<IList<T>> ToListAsync<T>(this Task<IAsyncCursor<T>> cursor) =>
			await (await cursor).ToListAsync();

	/// <summary>
	/// Paginates an IQueryable
	/// </summary>
	/// <param name="query">The IQueriable</param>
	/// <param name="page">Currrent page</param>
	/// <param name="size">Number of data to get</param>
	/// <typeparam name="T">Type of the data</typeparam>
	/// <returns></returns>
	public static async Task<IPagination<IList<T>>> PaginateAsync<T>(this Query<T> query, int page, int size)
			where T : class
	{
		page = page <= 0 ? 1 : page;

		// var entries = query.Skip((page - 1) * size).Take(size);
		// var count = await query.CountAsync();
		// var totalPages = (int) Math.Ceiling(count / (float) size);
		//
		// var firstPage = 1;
		// var lastPage = totalPages;
		// var prevPage = page > firstPage ? page - 1 : firstPage;
		// var nextPage = page < lastPage ? page + 1 : lastPage;
		// return new Pagination<IList<T>>(await entries.ToListAsync(),
		// 		totalPages,
		// 		page,
		// 		size,
		// 		prevPage,
		// 		nextPage,
		// 		count);
		return null;
	}

	// public static HttpContext SetPaginationHeader<T>(this HttpContext httpContext, string route, Pagination<T> pagination){
	//     httpContext.Response.Headers.Add ("X-Total-Count", pagination.TotalPages.ToString());
	//     httpContext.Response.Headers.Add ("Link",
	//         $"<{route}&page={pagination.CurrentPage}>; rel=\"first\", <{route}&page={pagination.NextPage}>; rel=\"next\", <{route}&page={pagination.TotalDataCount}>; rel=\"last\""
	//     );
	//     return httpContext;
	// }
}