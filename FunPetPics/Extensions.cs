using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FunPetPics
{
    public static class Extensions
    {
        /// <summary>
        /// When creating a new model, changes nulls to default values. Intended to be used just before uploading a new entity to database
        /// </summary>
        /// <param name="model">The Model to Initialize</param>
        public static void SetDefaults<T>(this T model) where T : class
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var info in properties)
            {
                // if a string and null, set to String.Empty
                if (info.PropertyType == typeof(string) &&
                   info.GetValue(model) == null)
                {
                    info.SetValue(model, String.Empty);
                }
                if (info.PropertyType == typeof(int) &&
                info.GetValue(model) == null)
                {
                    info.SetValue(model, 0);
                }
            }
        }

        public static void SetDefaultsAndAdd<T>(this ICollection<T> collection, T model)
        {
            if (collection == null)
                collection = new List<T>();

            PropertyInfo[] properties = typeof(T).GetProperties();
            foreach (var info in properties)
            {
                // if a string and null, set to String.Empty
                if (info.PropertyType == typeof(string) &&
                   info.GetValue(model) == null)
                {
                    info.SetValue(model, String.Empty);
                }
                if (info.PropertyType == typeof(double) &&
                info.GetValue(model) == null)
                {
                    info.SetValue(model, 0);
                }
            }

            collection.Add(model);
        }
    }
}