using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public static class CommonExtensions
{
    #region Methods

    internal static byte[] GetAsBytes(object input, byte[] defaultIfNullOrEmpty = null)
    {
        byte[] result = defaultIfNullOrEmpty;
        if (!IsNullOrEmtpy(input))
        {
            Type inputType = input.GetType();
            if (inputType == typeof(String) || inputType is String)
            {
                return Encoding.ASCII.GetBytes(input.ToString());
            }
            if (inputType == typeof(Boolean) || inputType is Boolean)
            {
                result = BitConverter.GetBytes((Boolean)input);
            }
            else if (inputType == typeof(Char) || inputType is Char)
            {
                result = BitConverter.GetBytes((Char)input);
            }
            else if (inputType == typeof(Double) || inputType is Double)
            {
                result = BitConverter.GetBytes((Double)input);
            }
            else if (inputType == typeof(Single) || inputType is Single)
            {
                result = BitConverter.GetBytes((Single)input);
            }
            else if (inputType == typeof(Int32) || inputType is Int32)
            {
                result = BitConverter.GetBytes((Int32)input);
            }
            else if (inputType == typeof(Int64) || inputType is Int64)
            {
                result = BitConverter.GetBytes((Int64)input);
            }
            else if (inputType == typeof(Int16) || inputType is Int16)
            {
                result = BitConverter.GetBytes((Int16)input);
            }
            else if (inputType == typeof(UInt32) || inputType is UInt32)
            {
                result = BitConverter.GetBytes((UInt32)input);
            }
            else if (inputType == typeof(UInt64) || inputType is UInt64)
            {
                result = BitConverter.GetBytes((UInt64)input);
            }
            else if (inputType == typeof(UInt16) || inputType is UInt16)
            {
                result = BitConverter.GetBytes((UInt16)input);
            }
            else if (inputType == typeof(Stream) || inputType is Stream)
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

    internal static bool HasValueThatIsNot(byte[] input, byte value)
    {
        return HasValueThatIsNot<byte>(input, value);
    }

    internal static bool HasValueThatIsNot<T>(T[] input, T value)
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

    internal static bool IsNullOrEmptyNot(object input)
    {
        return !IsNullOrEmtpy(input);
    }

    internal static bool IsNullOrEmtpy(object input)
    {
        return !(input != DBNull.Value && input != null && !string.IsNullOrEmpty(input.ToString()));
    }

    internal static byte[] ReadBetween(byte[] input, int startPos, int endPos)
    {
        return ReadBetween<byte>(input, startPos, endPos);
    }

    internal static T[] ReadBetween<T>(T[] input, int startPos, int endPos)
    {
        var result = new List<T>();

        for (int i = startPos; i <= endPos; i++)
        {
            result.Add(input[i]);
        }

        return result.ToArray();
    }

    internal static string ReadBetween(string input, int startPos, int endPos)
    {
        string result = string.Empty;

        for (int i = startPos; i <= endPos; i++)
        {
            result += input[i];
        }

        return result;
    }

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