namespace API.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Class to copy properties from a object to another
    /// </summary>
    public static class Mapper
    {
        private static List<Type> _systemTypes;

        /// <summary>
        /// A simple copy of one object to another with at least one parameter equal
        /// </summary>
        public static void CopyPropertiesFrom(this object dest, object source, bool objOverride = true)
        {
            if (dest == null || source == null)
                return;

            PropertyInfo[] sourceProperties = source.GetType().GetProperties();
            PropertyInfo[] destProperties = dest.GetType().GetProperties();

            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                foreach (PropertyInfo destProperty in destProperties)
                {
                    if (sourceProperty.Name == destProperty.Name)
                    {
                        if (sourceProperty.PropertyType == destProperty.PropertyType)
                        {
                            if (objOverride)
                                destProperty.SetValue(dest, sourceProperty.GetValue(source));
                            else
                            {
                                object objDest = destProperty.GetValue(dest);
                                bool isNullOrDefault = Utility.IsNullOrDefault(objDest);

                                if (isNullOrDefault)
                                    destProperty.SetValue(dest, sourceProperty.GetValue(source));
                            }

                            break;
                        }

                        if (sourceProperty.PropertyType.IsGenericType && sourceProperty.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                                                                    && Nullable.GetUnderlyingType(sourceProperty.PropertyType) == destProperty.PropertyType)
                        {
                            SetValue(dest, source, objOverride, sourceProperty, destProperty);
                            break;
                        }

                        if (destProperty.PropertyType.IsGenericType && destProperty.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>)
                                                                    && Nullable.GetUnderlyingType(destProperty.PropertyType) == sourceProperty.PropertyType)
                        {
                            SetValue(dest, source, objOverride, sourceProperty, destProperty);
                            break;
                        }

                        if (_systemTypes == null)
                            _systemTypes = Assembly.GetExecutingAssembly().GetType().Module.Assembly.GetExportedTypes().ToList();

                        if (!_systemTypes.Contains(sourceProperty.PropertyType))
                        {
                            if (sourceProperty.PropertyType.IsGenericType)
                            {
                                CopyList(dest, source, objOverride, destProperty, sourceProperty);
                            }
                            else
                            {
                                object objSource = sourceProperty.GetValue(source);
                                object objDest = destProperty.GetValue(dest);
                                if (objSource == null || objDest == null)
                                    break;

                                objDest.CopyPropertiesFrom(objSource, objOverride);
                            }

                            break;
                        }
                    }
                }
            }
        }

        private static void CopyList(object dest, object source, bool objOverride, PropertyInfo destProperty, PropertyInfo sourceProperty)
        {
            IEnumerable<dynamic> listObjDest = destProperty.GetValue((dynamic)dest);

            Type objDestType = listObjDest.GetType().GetGenericArguments().Single();
            Type listType = typeof(List<>).MakeGenericType(objDestType);
            IList listDest = (IList)Activator.CreateInstance(listType);

            foreach (object objSource in (IEnumerable)sourceProperty.GetValue(source))
            {
                dynamic objDest = listObjDest.FirstOrDefault();

                if (objSource == null)
                    break;

                if (objDest == null)
                {
                    objDest = Activator.CreateInstance(objDestType);
                    CopyPropertiesFrom(objDest, objSource, objOverride);

                    listDest.Add(objDest);
                }
                else
                {
                    CopyPropertiesFrom(objDest, objSource, objOverride);
                    listDest.Add(objDest);
                }
            }

            destProperty.SetValue(dest, listDest);
        }

        /// <summary>
        /// Check and set the values for properties
        /// </summary>
        private static void SetValue(object dest, object source, bool objOverride, PropertyInfo sourceProperty, PropertyInfo destProperty)
        {
            object value = sourceProperty.GetValue(source);

            if (value != null && objOverride)
                destProperty.SetValue(dest, value);
            else if (value != null)
            {
                object objDest = destProperty.GetValue(dest);
                bool isNullOrDefault = Utility.IsNullOrDefault(objDest);

                if (isNullOrDefault)
                    destProperty.SetValue(dest, value);
            }
        }
    }
}
