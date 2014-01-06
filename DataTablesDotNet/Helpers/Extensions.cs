using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace DataTablesDotNet.Helpers {

    internal static class Extensions {

        /// <summary>
        /// Convert the value of each property of an object to a
        /// string and concatenate to a list
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">The object</param>
        /// <returns>Properties as List of string</returns>
        public static List<string> PropertiesToList<T>(this T obj) {
            var propertyList = new List<string>();
            var properties = typeof(T).GetProperties();

            propertyList = properties.Select(prop => (prop.GetValue(obj, new object[0]) ?? string.Empty).ToString())
                                        .ToList();

            return propertyList;
        }

        /// <summary>
        /// Given a list of property names for an object,
        /// convert the value of each property of an object to a
        /// string and concatenate to a list
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="obj">The object</param>
        /// <param name="columns"></param>
        /// <returns>Properties as List of string</returns>
        public static List<string> PropertiesToList<T>(this T obj, string columns) {
            var propertyNames = columns.Split(',').ToList();
            var propertyList = new List<string>();
            var properties = typeof(T).GetProperties();
            var props = new List<PropertyInfo>();

            //  Find all "" in propertyNames and insert empty value into list at
            //  corresponding position
            var blankIndexes = new List<int>();
            int i = 0;

            //  Select and order filterProperties.  Record index position where there is
            //  no property
            propertyNames.ForEach(name => {
                var property = properties.Where(prop => prop.Name == name.Trim())
                                    .SingleOrDefault();

                if (property == null) {
                    blankIndexes.Add(i);
                } else {
                    props.Add(properties.Where(prop => prop.Name == name.Trim())
                                    .SingleOrDefault());
                }
                i++;
            });

            propertyList = props.Select(prop => (prop.GetValue(obj, new object[0]) ?? string.Empty).ToString())
                                        .ToList();

            //  Add "" to List<string> as client expects blank value in array
            foreach (var index in blankIndexes) {
                propertyList.Insert(index, string.Empty);
            }

            return propertyList;
        }

        /// <summary>
        /// Author:  Marc Gravell & others from StackOverflow
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">List of T as Queryable</param>
        /// <param name="property">Name of propertu as string</param>
        /// <param name="sortDirection">ASC or DESC as string</param>
        /// <param name="initial">First Ordered operation indicator as bool</param>
        /// <returns>Order collection as IQueryable of T</returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string property, string sortDirection, bool initial) {
            string[] props = property.Split('.');
            var type = typeof(T);
            var arg = Expression.Parameter(type, "x");
            Expression expr = arg;
            foreach (string prop in props) {
                // use reflection (not ComponentModel) to mirror LINQ
                PropertyInfo pi = type.GetProperty(prop);
                expr = Expression.Property(expr, pi);
                type = pi.PropertyType;
            }

            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), type);
            var lambda = Expression.Lambda(delegateType, expr, arg);

            string methodName = string.Empty;

            //  Asc or Desc
            if (sortDirection.ToLower() == "asc") {
                //  First clause?
                if (initial && source is IOrderedQueryable<T>) {
                    methodName = "OrderBy";
                } else {
                    methodName = "ThenBy";
                }
            } else {
                if (initial && source is IOrderedQueryable<T>) {
                    methodName = "OrderByDescending";
                } else {
                    methodName = "ThenByDescending";
                }
            }

            object result = typeof(Queryable).GetMethods().Single(
                    method => method.Name == methodName
                            && method.IsGenericMethodDefinition
                            && method.GetGenericArguments().Length == 2
                            && method.GetParameters().Length == 2)
                    .MakeGenericMethod(typeof(T), type)
                    .Invoke(null, new object[] { source, lambda });
            return (IOrderedQueryable<T>)result;
        }
    }
}