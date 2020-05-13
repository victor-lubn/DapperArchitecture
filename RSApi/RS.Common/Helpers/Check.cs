using System;
using System.Collections.Generic;

namespace RS.Common.Helpers
{
    /// <summary>
    /// The check.
    /// </summary>
    public class Check
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Check"/> class.
        /// </summary>
        internal Check()
        {
        }

        /// <summary>
        /// Subclass that allow to check method's arguments.
        /// </summary>
        public class Argument
        {
            /// <summary>
            /// Initializes a new instance of the <see cref="Argument"/> class.
            /// </summary>
            internal Argument()
            {
            }

            /// <summary>
            /// Determines whether Is true.
            /// </summary>
            /// <param name="condition">If set to <c>true</c> then condition.</param>
            /// <param name="message">The message.</param>
            /// <exception cref="System.ArgumentException"></exception>
            public static void IsTrue(bool condition, string message)
            {
                if (!condition)
                {
                    throw new ArgumentException(message);
                }
            }

            /// <summary>
            /// Determines whether Is false.
            /// </summary>
            /// <param name="condition">If set to <c>true</c> then condition.</param>
            /// <param name="message">The message.</param>
            /// <exception cref="System.ArgumentException"></exception>
            public static void IsFalse(bool condition, string message)
            {
                if (condition)
                {
                    throw new ArgumentException(message);
                }
            }

            /// <summary>
            /// Determines whether Is not empty.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentException"></exception>
            public static void IsNotEmpty(Guid argument, string argumentName)
            {
                if (argument == Guid.Empty)
                {
                    throw new ArgumentException(String.Format("\"{0}\" cannot be empty guid.", argumentName));
                }
            }

            /// <summary>
            /// Determines whether Is not empty.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentException"></exception>
            public static void IsNotEmpty(Guid? argument, string argumentName)
            {
                if (!argument.HasValue || argument == Guid.Empty)
                {
                    throw new ArgumentException(String.Format("\"{0}\" cannot be empty guid.", argumentName));
                }
            }

            /// <summary>
            /// Determines whether Is not empty.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentException"></exception>
            public static void IsNotEmpty(string argument, string argumentName)
            {
                if (String.IsNullOrEmpty((argument ?? String.Empty).Trim()))
                {
                    throw new ArgumentException(String.Format("\"{0}\" cannot be blank.", argumentName));
                }
            }

            /// <summary>
            /// Determines whether Is not out of length.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="length">The length.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentException"></exception>
            public static void IsNotOutOfLength(string argument, int length, string argumentName)
            {
                if (argument.Trim().Length > length)
                {
                    throw new ArgumentException(String.Format("\"{0}\" cannot be more than {1} character.", argumentName, length));
                }
            }

            /// <summary>
            /// Determines whether Is not null.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentNullException"></exception>
            public static void IsNotNull(object argument, string argumentName)
            {
                if (argument == null)
                {
                    throw new ArgumentNullException(argumentName);
                }
            }

            /// <summary>
            /// Determines whether Is not negative.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentOutOfRangeException"></exception>
            public static void IsNotNegative(int argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Determines whether Is not negative or zero.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentOutOfRangeException"></exception>
            public static void IsNotNegativeOrZero(int argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Determines whether Is not negative.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentOutOfRangeException"></exception>
            public static void IsNotNegative(long argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Determines whether Is not negative or zero.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentOutOfRangeException"></exception>
            public static void IsNotNegativeOrZero(long argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Determines whether Is not negative.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentOutOfRangeException"></exception>
            public static void IsNotNegative(float argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Determines whether Is not negative or zero.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentOutOfRangeException"></exception>
            public static void IsNotNegativeOrZero(float argument, string argumentName)
            {
                if (argument <= 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Determines whether Is not negative.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentOutOfRangeException"></exception>
            public static void IsNotNegative(decimal argument, string argumentName)
            {
                if (argument < 0)
                {
                    throw new ArgumentOutOfRangeException(argumentName);
                }
            }

            /// <summary>
            /// Determines whether Is not empty.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="argument">The argument.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentException">Collection cannot be empty.</exception>
            public static void IsNotEmpty<T>(ICollection<T> argument, string argumentName)
            {
                IsNotNull(argument, argumentName);

                if (argument.Count == 0)
                {
                    throw new ArgumentException("Collection cannot be empty.", argumentName);
                }
            }

            /// <summary>
            /// Determines whether Is not out of range.
            /// </summary>
            /// <param name="argument">The argument.</param>
            /// <param name="min">The min.</param>
            /// <param name="max">The max.</param>
            /// <param name="argumentName">Name of the argument.</param>
            /// <exception cref="System.ArgumentOutOfRangeException"></exception>
            public static void IsNotOutOfRange(int argument, int min, int max, string argumentName)
            {
                if ((argument < min) || (argument > max))
                {
                    throw new ArgumentOutOfRangeException(argumentName, String.Format("{0} must be between \"{1}\"-\"{2}\".", argumentName, min, max));
                }
            }
        }
    }
}
