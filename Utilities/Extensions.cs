using System;

namespace JMMT.Utilities
{

    public static class Extensions
    {
        public static T CloneObject<T>(this T obj) where T : class
        {
            if (obj == null) return null;
            System.Reflection.MethodInfo inst = obj.GetType().GetMethod("MemberwiseClone",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (inst != null)
                return (T)inst.Invoke(obj, null);
            else
                return null;
        }

        public static string GetAllInner(this Exception ex)
        {
            string message = string.Empty;
            Exception innerException = ex;

            do
            {
                message += (string.IsNullOrEmpty(innerException.Message) ? string.Empty : innerException.Message) + "\r\n";
                message += (string.IsNullOrEmpty(innerException.StackTrace) ? string.Empty : innerException.StackTrace) + "\r\n";
                innerException = innerException.InnerException;
            }
            while (innerException != null);

            return message;
        }
    }
}