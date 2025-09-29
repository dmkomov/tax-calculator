using Microsoft.EntityFrameworkCore;

namespace TaxCalculator.Persistence.Extensions
{
    public static class EfExtensions
    {
        /// <summary>
        ///     Required to avoid exception during tests execution.
        ///     Mocked data does not implement IAsyncEnumerable.
        /// </summary>
        public static Task<List<TSource>> ToListAsyncSafe<TSource>(this IQueryable<TSource> source,
            CancellationToken cancellationToken)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source is not IAsyncEnumerable<TSource>)
                return Task.FromResult(source.ToList());

            return source.ToListAsync(cancellationToken: cancellationToken);
        }
    }
}
