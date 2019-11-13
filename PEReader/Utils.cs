using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PEReader
{
    public class Utils
    {
        public static string BytesToHexString(byte[] bytes, bool mutiLine = true)
        {
            var sb = new StringBuilder(2048);
            for (var i = 0; i < bytes.Length; i++)
            {
                if (mutiLine)
                {
                    if ((i + 1) % 16 == 0)
                    {
                        sb.Append($"{Convert.ToString(bytes[i], 16).PadLeft(2, '0').ToUpper()}{Environment.NewLine}");
                    }
                    else
                    {
                        sb.Append($"{Convert.ToString(bytes[i], 16).PadLeft(2, '0').ToUpper()} ");
                    }
                }
                else
                {
                    sb.Append($"{Convert.ToString(bytes[i], 16).PadLeft(2, '0').ToUpper()} ");
                }
            }
            if (bytes.Length > 0)
            {
                sb.Length--;
            }
            return sb.ToString();
        }

        /// <summary>
        /// 将byte[]还原为指定的struct,该函数的泛型仅用于自定义结构
        /// startIndex：数组中 Copy 开始位置的从零开始的索引。
        /// length：要复制的数组元素的数目。
        /// </summary>
        public static T BytesToStruct<T>(byte[] bytes, int startIndex, int length)
        {
            if (bytes == null) return default(T);
            if (bytes.Length <= 0) return default(T);
            IntPtr buffer = Marshal.AllocHGlobal(length);
            try//struct_bytes转换
            {
                Marshal.Copy(bytes, startIndex, buffer, length);
                return (T)Marshal.PtrToStructure(buffer, typeof(T));
            }
            catch (Exception ex)
            {
                throw new Exception("Error in BytesToStruct ! " + ex.Message);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }

        /// <summary>
        /// 将struct类型转换为byte[]
        /// </summary>
        public static byte[] StructToBytes(object structObj, int size)
        {
            IntPtr buffer = Marshal.AllocHGlobal(size);
            try//struct_bytes转换
            {
                Marshal.StructureToPtr(structObj, buffer, false);
                byte[] bytes = new byte[size];
                Marshal.Copy(buffer, bytes, 0, size);
                return bytes;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in StructToBytes ! " + ex.Message);
            }
            finally
            {
                Marshal.FreeHGlobal(buffer);
            }
        }
        /// <summary>
        /// 计算结构体的大小
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static int StructSize(object o)
        {
            return Marshal.SizeOf(o);
        }
    }
}
