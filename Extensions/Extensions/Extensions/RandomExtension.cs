using System;
using System.Collections.Generic;

namespace Extensions
{
    /// <summary>
    /// Some extension methods for <see cref="Random"/> for creating a few more kinds of random stuff.
    /// </summary>
    public static class RandomExtensions
    {
        /// <summary>
        ///   Generates normally distributed numbers.
        /// </summary>
        /// <param name="r">The inctance of <see cref="System.Random"/> that has been extended.</param>
        /// <param name = "mu">Mean of the distribution</param>
        /// <param name = "sigma">Standard deviation</param>
        /// <returns>A random value in range.</returns>
        public static double NextGaussian(this Random r, double mu = 0, double sigma = 1)
        {
            double u1 = r.NextDouble();
            double u2 = r.NextDouble();

            double rand_std_normal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                                Math.Sin(2.0 * Math.PI * u2);

            return mu + sigma * rand_std_normal;
        }

        /// <summary>
        ///   Generates values from a triangular distribution.
        /// </summary>
        /// <remarks>
        /// See http://en.wikipedia.org/wiki/Triangular_distribution for a description of the triangular probability distribution and the algorithm for generating one.
        /// </remarks>
        /// <param name="r">The inctance of <see cref="System.Random"/> that has been extended.</param>
        /// <param name = "a">Minimum</param>
        /// <param name = "b">Maximum</param>
        /// <param name = "c">Mode (most frequent value)</param>
        /// <returns>Values from a triangular distribution</returns>
        public static double NextTriangular(this Random r, double a, double b, double c)
        {
            double u = r.NextDouble();

            return u < (c - a) / (b - a)
                        ? a + Math.Sqrt(u * (b - a) * (c - a))
                        : b - Math.Sqrt((1 - u) * (b - a) * (b - c));
        }

        /// <summary>
        ///   Equally likely to return true or false. Uses <see cref="Random.Next()"/>.
        /// </summary>
        /// <param name="r">The inctance of <see cref="System.Random"/> that has been extended.</param>
        /// <returns> Random value of true or false</returns>
        public static bool NextBoolean(this Random r)
        {
            return r.Next(2) > 0;
        }

        /// <summary>
        /// Shuffles a list in O(n) time by using the Fisher-Yates/Knuth algorithm.
        /// </summary>
        /// <param name="r">The inctance of <see cref="System.Random"/> that has been extended.</param>
        /// <param name = "list">The list that will be shuggled.</param>
        /// <typeparam name="T">Type of collection values.</typeparam>
        public static void Shuffle<T>(this Random r, IList<T> list)
        {
            for (int i = 0; i < list.Count; ++i)
            {
                int j = r.Next(0, i + 1);

                T temp = list[j];
                list[j] = list[i];
                list[i] = temp;
            }
        }

        /// <summary>
        /// Returns n unique random numbers in the range [1, n], inclusive. 
        /// This is equivalent to getting the first n numbers of some random permutation of the sequential numbers from 1 to max. 
        /// Runs in O(k^2) time.
        /// </summary>
        ///<param name="rand">The inctance of <see cref="System.Random"/> that has been extended.</param>
        /// <param name="n">Maximum number possible.</param>
        /// <param name="k">How many numbers to return.</param>
        /// <returns>An array of integer.</returns>
        public static int[] Permutation(this Random rand, int n, int k)
        {
            List<int> result = new List<int>();
            SortedSet<int> sorted = new SortedSet<int>();

            for (int i = 0; i < k; ++i)
            {
                int r = rand.Next(1, n + 1 - i);

                foreach (int q in sorted)
                    if (r >= q) r++;

                result.Add(r);
                sorted.Add(r);
            }

            return result.ToArray();
        }
    }
}