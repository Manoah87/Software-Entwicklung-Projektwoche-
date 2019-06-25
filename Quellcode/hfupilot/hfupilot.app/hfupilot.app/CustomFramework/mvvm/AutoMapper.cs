using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace hfupilot.app.CustomFramework.mvvm
{
    class AutoMapper
    {
        public R CastObject<T, R>(T rhs)
        where T : class
        where R : new()
        {
            R result = new R();

            var type = rhs.GetType().GetFields();
            var objType = result.GetType();
            var properties = objType.GetProperties();

            try
            {
                properties.ToList().ForEach(
                    (p) =>
                    {
                        if (p != null && p.CanWrite && rhs.GetType().GetProperty(p.Name) != null && rhs.GetType().GetProperty(p.Name).CanRead)
                        {
                            p.SetValue(result, rhs.GetType().GetProperty(p.Name).GetValue(rhs, null), null);
                        }
                    }

                );
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }

            return result;
        }

        public void CastObjectByRef<T, R>(T refObj, R rhs)
        where T : class
        where R : new()
        {
            var type = rhs.GetType().GetFields();


            var objType = refObj.GetType();
            var properties = objType.GetProperties();

            try
            {
                properties.ToList().ForEach(
                    (p) =>
                    {
                        if (p != null && p.CanWrite && rhs.GetType().GetProperty(p.Name) != null && rhs.GetType().GetProperty(p.Name).CanRead)
                        {
                            if (p.PropertyType.Name.IndexOf("ICollection") == -1)
                            {
                                p.SetValue(refObj, rhs.GetType().GetProperty(p.Name).GetValue(rhs, null), null);
                            }
                            else
                            {
                                //List<object> newList = null;
                                //newList = rhs.GetType().GetProperty(p.Name).GetValue(rhs, null) as List<object>;
                                p.SetValue(refObj, rhs.GetType().GetProperty(p.Name).GetValue(rhs, null), null);
                                //Debug.WriteLine("ICollection: " + p.Name);
                                //p.SetValue  = 
                            }
                        }
                    }

                );

                // properties.ToList().ForEach(p => objType.InvokeMember(p.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty, null, objType, type));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        public void CastObjectForUpdate<T, R>(T refObj, R rhs)
        where T : class
        where R : new()
        {
            var type = rhs.GetType().GetFields();

            var objType = refObj.GetType();
            var properties = objType.GetProperties();

            try
            {
                properties.ToList().ForEach(
                    (p) =>
                    {
                        if (p != null && p.CanWrite && rhs.GetType().GetProperty(p.Name) != null && rhs.GetType().GetProperty(p.Name).CanRead)
                        {
                            p.SetValue(refObj, rhs.GetType().GetProperty(p.Name).GetValue(rhs, null), null);
                        }
                    }

                );

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
