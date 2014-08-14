﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nessos.Streams.Core;

namespace Nessos.Streams.Core.CSharp
{
    public static class ParStreams
    {
        /// <summary>Wraps array as a parallel stream.</summary>
        /// <param name="source">The input array.</param>
        /// <returns>The result parallel stream.</returns>
        public static ParStream<T> AsParStream<T>(this T[] source)
        {
            return ParStream.ofArray(source);
        }

        /// <summary>Wraps List as a parallel stream.</summary>
        /// <param name="source">The input array.</param>
        /// <returns>The result parallel stream.</returns>
        public static ParStream<T> AsParStream<T>(this List<T> source)
        {
            return ParStream.ofResizeArray(source);
        }

        /// <summary>Wraps IEnumerable as a parallel stream.</summary>
        /// <param name="source">The input seq.</param>
        /// <returns>The result parallel stream.</returns>
        public static ParStream<T> AsStream<T>(this IEnumerable<T> source)
        {
            return ParStream.ofSeq(source);
        }

        /// <summary>Transforms each element of the input parallel stream.</summary>
        /// <param name="f">A function to transform items from the input parallel stream.</param>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The result parallel stream.</returns>
        public static ParStream<TResult> Select<TSource, TResult>(this ParStream<TSource> stream, Func<TSource, TResult> f)
        {
            return CSharpProxy.Select(stream, f);
        }

        /// <summary>Filters the elements of the input parallel stream.</summary>
        /// <param name="predicate">A function to test each source element for a condition.</param>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The result parallel stream.</returns>
        public static ParStream<TSource> Where<TSource>(this ParStream<TSource> stream, Func<TSource, bool> predicate)
        {
            return CSharpProxy.Where(stream, predicate);
        }


        /// <summary>Transforms each element of the input parallel stream to a new stream and flattens its elements.</summary>
        /// <param name="f">A function to transform items from the input parallel stream.</param>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The result parallel stream.</returns>
        public static ParStream<TResult> SelectMany<TSource, TResult>(this ParStream<TSource> stream, Func<TSource, Stream<TResult>> f)
        {
            return CSharpProxy.SelectMany(stream, f);
        }

        /// <summary>Applies a function to each element of the parallel stream, threading an accumulator argument through the computation. If the input function is f and the elements are i0...iN, then this function computes f (... (f s i0)...) iN.</summary>
        /// <param name="state">A function that produces the initial state.</param>
        /// <param name="folder">A function that updates the state with each element from the parallel stream.</param>
        /// <param name="combiner">A function that combines partial states into a new state.</param>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The final result.</returns>
        public static TAccumulate Aggregate<TSource, TAccumulate>(this ParStream<TSource> stream, Func<TAccumulate> state, Func<TAccumulate, TSource, TAccumulate> folder, Func<TAccumulate, TAccumulate, TAccumulate> combiner)
        {
            return CSharpProxy.Aggregate(stream, state, folder, combiner);
        }

        /// <summary>Applies a key-generating function to each element of the input parallel stream and yields a parallel stream ordered by keys.</summary>
        /// <param name="projection">A function to transform items of the input parallel stream into comparable keys.</param>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The result parallel stream.</returns>    
        public static ParStream<TSource> OrderBy<TSource, TKey>(this ParStream<TSource> stream, Func<TSource, TKey> projection) where TKey : IComparable<TKey>
        {
            return CSharpProxy.OrderBy(stream, projection);
        }


        /// <summary>Applies a key-generating function to each element of the input parallel stream and yields a parallel stream of unique keys and a sequence of all elements that have each key.</summary>
        /// <param name="projection">A function to transform items of the input parallel stream into comparable keys.</param>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>A parallel stream of tuples where each tuple contains the unique key and a sequence of all the elements that match the key.</returns>    
        public static ParStream<Tuple<TKey, IEnumerable<TSource>>> GroupBy<TSource, TKey>(this ParStream<TSource> stream, Func<TSource, TKey> projection)
        {
            return CSharpProxy.GroupBy(stream, projection);
        }


        /// <summary>Returns the sum of the elements.</summary>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The sum of the elements.</returns>
        public static int Sum(this ParStream<int> stream)
        {
            return CSharpProxy.Sum(stream);
        }

        /// <summary>Returns the sum of the elements.</summary>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The sum of the elements.</returns>
        public static long Sum(this ParStream<long> stream)
        {
            return CSharpProxy.Sum(stream);
        }

        /// <summary>Returns the sum of the elements.</summary>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The sum of the elements.</returns>
        public static float Sum(this ParStream<float> stream)
        {
            return CSharpProxy.Sum(stream);
        }

        /// <summary>Returns the sum of the elements.</summary>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The sum of the elements.</returns>
        public static double Sum(this ParStream<double> stream)
        {
            return CSharpProxy.Sum(stream);
        }

        /// <summary>Returns the sum of the elements.</summary>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The sum of the elements.</returns>
        public static decimal Sum(this ParStream<decimal> stream)
        {
            return CSharpProxy.Sum(stream);
        }

        /// <summary>Returns the total number of elements of the parallel stream.</summary>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The total number of elements.</returns>
        public static int Count<TSource>(this ParStream<TSource> stream)
        {
            return CSharpProxy.Count(stream);
        }

        /// <summary>Creates an array from the given parallel stream.</summary>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The result array.</returns>    
        public static TSource[] ToArray<TSource>(this ParStream<TSource> stream)
        {
            return ParStream.toArray(stream);
        }

        /// <summary>Creates an List from the given parallel stream.</summary>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The result List.</returns>    
        public static List<TSource> ToList<TSource>(this ParStream<TSource> stream)
        {
            return ParStream.toResizeArray(stream);
        }


        /// <summary>Returns the first element for which the given function returns true. Raises KeyNotFoundException if no such element exists.</summary>
        /// <param name="predicate">A function to test each source element for a condition.</param>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>The first element for which the predicate returns true.</returns>
        /// <exception cref="System.KeyNotFoundException">Thrown if the predicate evaluates to false for all the elements of the parallel stream.</exception>
        public static TSource First<TSource>(this ParStream<TSource> stream, Func<TSource, bool> predicate)
        {
            return CSharpProxy.First(stream, predicate);
        }

        /// <summary>Tests if any element of the stream satisfies the given predicate.</summary>
        /// <param name="predicate">A function to test each source element for a condition.</param>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>true if any element satisfies the predicate. Otherwise, returns false.</returns>
        public static bool Any<TSource>(this ParStream<TSource> stream, Func<TSource, bool> predicate)
        {
            return CSharpProxy.Any(stream, predicate);
        }

        /// <summary>Tests if all elements of the parallel stream satisfy the given predicate.</summary>
        /// <param name="predicate">A function to test each source element for a condition.</param>
        /// <param name="stream">The input parallel stream.</param>
        /// <returns>true if all of the elements satisfies the predicate. Otherwise, returns false.</returns>
        public static bool All<TSource>(this ParStream<TSource> stream, Func<TSource, bool> predicate)
        {
            return CSharpProxy.All(stream, predicate);
        }
    }
}
