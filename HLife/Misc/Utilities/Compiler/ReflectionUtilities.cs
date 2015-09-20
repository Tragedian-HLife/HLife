using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using System.Collections;

namespace HLife
{
    /// <summary>
    /// Utilities for System.Reflection.
    /// </summary>
    public static class ReflectionUtilities
    {
        /// <summary>
        /// Get all the types within a given namespace.
        /// </summary>
        /// <param name="assembly">Assembly in which to look.</param>
        /// <param name="nameSpace">Namespace in which to look.</param>
        /// <returns>Array of Types.</returns>
        public static Type[] GetTypesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes().Where(t => String.Equals(t.Namespace, nameSpace, StringComparison.Ordinal)).ToArray();
        }

        /// <summary>
        /// Gets all of the namespaces and their classes within an assembly.
        /// </summary>
        /// <param name="assembly">Assembly in which to look.</param>
        /// <param name="nameSpace">Namespace in which to look.</param>
        /// <returns>IEnumerable of IGroupings of Types.</returns>
        public static IEnumerable GetNamespacesInNamespace(Assembly assembly, string nameSpace)
        {
            return assembly.GetTypes()
                        .Where(t => t.IsClass) // Only include classes
                        .GroupBy(t => t.Namespace);
        }

        public static object GetPropertyValue(object src, string propName)
        {
            var props = propName.Split('.');

            var realProp = src;
            
            foreach(string prop in props)
            {
                realProp = realProp.GetType().GetProperty(prop).GetValue(realProp, null);
            }

            return realProp;
        }
    }
}
