using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Design.PluralizationServices;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using BinaryDigit.DataAccess;

public static class CommonExtensions
{
    #region Delegates

    public delegate void FileFoundDelegate(FileInfo fi);

    #endregion

    #region Public Methods and Operators

    /// <summary>
    ///     Perform a deep Copy of the object.
    /// </summary>
    /// <typeparam name="T">The type of object being copied.</typeparam>
    /// <param name="source">The object instance to copy.</param>
    /// <returns>The copied object.</returns>
    public static T Clone<T>(this T source) where T : ISerializable
    {
        if (!typeof(T).IsSerializable)
        {
            throw new ArgumentException("The type must be serializable.", "source");
        }

        // Don't serialize a null object, simply return the default for that object
        if (ReferenceEquals(source, null))
        {
            return default(T);
        }

        IFormatter formatter = new BinaryFormatter();
        Stream stream = new MemoryStream();
        using (stream)
        {
            formatter.Serialize(stream, source);
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }
    }

    public static byte[] GetAsBytes(this object input, byte[] defaultIfNullOrEmpty = null)
    {
        byte[] result = defaultIfNullOrEmpty;
        if (!input.IsNullOrEmpty())
        {
            Type inputType = input.GetType();
            if (inputType == typeof(String))
            {
                return Encoding.ASCII.GetBytes(input.ToString());
            }
            if (inputType == typeof(Boolean))
            {
                result = BitConverter.GetBytes((Boolean)input);
            }
            else if (inputType == typeof(Char))
            {
                result = BitConverter.GetBytes((Char)input);
            }
            else if (inputType == typeof(Double))
            {
                result = BitConverter.GetBytes((Double)input);
            }
            else if (inputType == typeof(Single))
            {
                result = BitConverter.GetBytes((Single)input);
            }
            else if (inputType == typeof(Int32))
            {
                result = BitConverter.GetBytes((Int32)input);
            }
            else if (inputType == typeof(Int64))
            {
                result = BitConverter.GetBytes((Int64)input);
            }
            else if (inputType == typeof(Int16))
            {
                result = BitConverter.GetBytes((Int16)input);
            }
            else if (inputType == typeof(UInt32))
            {
                result = BitConverter.GetBytes((UInt32)input);
            }
            else if (inputType == typeof(UInt64))
            {
                result = BitConverter.GetBytes((UInt64)input);
            }
            else if (inputType == typeof(UInt16))
            {
                result = BitConverter.GetBytes((UInt16)input);
            }
            else if (inputType == typeof(Stream))
            {
                result = GetBytes_Stream((Stream)input);
            }
            else if (inputType is ISerializable)
            {
                result = GetBytes_ISerializable(input);
            }
        }
        return result;
    }

    public static List<string> GetColumnNames(this IDataRecord dr)
    {
        var result = new List<string>();
        for (int i = 0; i < dr.FieldCount; i++)
        {
            result.Add(dr.GetName(i));
        }
        return result;
    }

    public static Dictionary<string, Type> GetColumnNamesWithTypes(this IDataRecord dr)
    {
        var result = new Dictionary<string, Type>();
        for (int i = 0; i < dr.FieldCount; i++)
        {
            result.Add(dr.GetName(i), dr.GetFieldType(i));
        }
        return result;
    }

    public static object GetDefault(this Type type)
    {
        if (type != null && type.IsValueType)
        {
            return Activator.CreateInstance(type);
        }
        return null;
    }

    public static Guid GetUserId(this IIdentity identity)
    {
        return Sql.ExecuteScalar<Guid>(@"
select TOP 1 UserId
from Users 
where Username = @Username", new[] { new SqlParameter("Username", identity.Name) });
    }

    public static bool HasColumn(this IDataRecord dr, string columnName)
    {
        bool result = false;
        for (int i = 0; i < dr.FieldCount; i++)
        {
            if (dr.GetName(i).Equals(columnName, StringComparison.InvariantCultureIgnoreCase))
            {
                result = true;
                break;
            }
        }
        return result;
    }

    public static bool HasValueThatIsNot(this byte[] input, byte value)
    {
        return input.HasValueThatIsNot<byte>(value);
    }

    public static bool HasValueThatIsNot<T>(this T[] input, T value)
    {
        bool result = false;

        for (int i = 0; i < input.Length; i++)
        {
            if (EqualityComparer<T>.Default.Equals(input[i], value)) //input[i] == value)
            {
                result = true;
                break;
            }
        }

        return result;
    }

    public static bool IsNullOrEmpty(this object input)
    {
        return !(input != DBNull.Value && input != null && !string.IsNullOrEmpty(input.ToString()));
    }

    public static bool IsNullOrEmptyNot(this object input)
    {
        return !IsNullOrEmpty(input);
    }

    public static bool IsPlural(this string input)
    {
        return PluralizationService.CreateService(CultureInfo.CurrentCulture).IsPlural(input);
    }

    public static bool IsSingular(this string input)
    {
        return PluralizationService.CreateService(CultureInfo.CurrentCulture).IsSingular(input);
    }

    public static string Pluralize(this string input)
    {
        return PluralizationService.CreateService(CultureInfo.CurrentCulture).Pluralize(input);
    }

    public static byte[] ReadBetween(this byte[] input, int startPos, int endPos)
    {
        return input.ReadBetween<byte>(startPos, endPos);
    }

    public static T[] ReadBetween<T>(this T[] input, int startPos, int endPos)
    {
        var result = new List<T>();

        for (int i = startPos; i <= endPos; i++)
        {
            result.Add(input[i]);
        }

        return result.ToArray();
    }

    public static string ReadBetween(this string input, int startPos, int endPos)
    {
        string result = string.Empty;

        for (int i = startPos; i <= endPos; i++)
        {
            result += input[i];
        }

        return result;
    }

    public static string Singularize(this string input)
    {
        return PluralizationService.CreateService(CultureInfo.CurrentCulture).Singularize(input);
    }

    public static string SplitCamelCase(this string str)
    {
        return Regex.Replace(Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
    }

    public static Int16 ToInt16(this string input, Int16 defaultValue = 0)
    {
        Int16 result = -1;

        if (!Int16.TryParse(input, out result))
        {
            result = defaultValue;
        }

        return result;
    }

    public static Int32 ToInt32(this string input, Int32 defaultValue = 0)
    {
        Int32 result = -1;

        if (!Int32.TryParse(input, out result))
        {
            result = defaultValue;
        }

        return result;
    }

    public static Int64 ToInt64(this string input, Int64 defaultValue = 0)
    {
        Int64 result = -1;

        if (!Int64.TryParse(input, out result))
        {
            result = defaultValue;
        }

        return result;
    }

    public static string ToStringSafe(this object input, string valueIfNull = "", string valueIfDBNull = "")
    {
        string result = string.Empty;

        if (input.IsNullOrEmpty())
        {
            if (input == null)
            {
                input = valueIfNull;
            }
            else if (input == DBNull.Value)
            {
                input = valueIfDBNull;
            }
        }
        else
        {
            result = input.ToString();
        }

        return result;
    }

    public static string ToTitleCase(this string value)
    {
        //Get the culture property of the thread.
        CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
        //Create TextInfo object.
        TextInfo textInfo = cultureInfo.TextInfo;
        return textInfo.ToTitleCase(value);
    }

    public static string Trim(this string input, int trimAt, bool trimWhiteSpaceFirst = false, string addText = "...")
    {
        string result = input;
        if (result.Length > trimAt)
        {
            result = result.Remove(trimAt);
            result += addText;
        }
        return result;
    }

    #endregion

    #region Methods

    private static byte[] GetBytes_ISerializable(object input)
    {
        var result = new byte[0];

        using (var ms = new MemoryStream())
        {
            var bformatter = new BinaryFormatter();
            bformatter.Serialize(ms, input);
            result = ms.GetBuffer();
        }

        return result;
    }

    private static byte[] GetBytes_Stream(Stream input)
    {
        var result = new byte[0];

        var buffer = new byte[16 * 1024];
        using (var ms = new MemoryStream())
        {
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                ms.Write(buffer, 0, read);
            }
            result = ms.ToArray();
        }

        return result;
    }

    #endregion
}