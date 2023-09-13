namespace Dotnet9Tools.Helper;

public class FileTypeCheck
{
    private static readonly Dictionary<string, List<byte[]>> fileSignature = new()
    {
        { ".DOC", new List<byte[]> { new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 } } },
        { ".DOCX", new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } } },
        { ".PDF", new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } } },
        {
            ".ZIP", new List<byte[]>
            {
                new byte[] { 0x50, 0x4B, 0x03, 0x04 },
                new byte[] { 0x50, 0x4B, 0x4C, 0x49, 0x54, 0x55 },
                new byte[] { 0x50, 0x4B, 0x53, 0x70, 0x58 },
                new byte[] { 0x50, 0x4B, 0x05, 0x06 },
                new byte[] { 0x50, 0x4B, 0x07, 0x08 },
                new byte[] { 0x57, 0x69, 0x6E, 0x5A, 0x69, 0x70 }
            }
        },

        //图片格式
        { ".PNG", new List<byte[]> { new byte[] { 0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A } } },
        {
            ".JPG", new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE8 }
            }
        },
        {
            ".JPEG", new List<byte[]>
            {
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE0 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE2 },
                new byte[] { 0xFF, 0xD8, 0xFF, 0xE3 }
            }
        },
        { ".GIF", new List<byte[]> { new byte[] { 0x47, 0x49, 0x46, 0x38 } } },
        { ".BMP", new List<byte[]> { new byte[] { 0x42, 0x4D } } },
        { ".JFIF", new List<byte[]> { new byte[] { 0xFF, 0xD8, 0xFF, 0xE0, 0x00, 0x10, 0x4A, 0x46, 0x49, 0x46 } } },


        //视频格式
        //高清，高占用空间
        {
            ".MP4", new List<byte[]>
            {
                new byte[] { 0x00, 0x00, 0x00, 0x18, 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32 },
                new byte[] { 0x00, 0x00, 0x00, 0x1C, 0x66, 0x74, 0x79, 0x70, 0x6D, 0x70, 0x34, 0x32 },
                new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x69, 0x73, 0x6F, 0x6D }
            }
        },
        //高清，中度占用空间
        {
            ".MKV", new List<byte[]>
            {
                new byte[] { 0x1A, 0x45, 0xDF, 0xA3, 0xA3, 0x42, 0x86, 0x81, 0x01, 0x42, 0xF7 }
            }
        },
        //高清，中度占用空间
        {
            ".MOV", new List<byte[]>
            {
                new byte[] { 0x00, 0x00, 0x00, 0x14, 0x66, 0x74, 0x79, 0x70, 0x71, 0x74 }
            }
        },
        //高清，低占用空间
        {
            ".M4V", new List<byte[]>
            {
                new byte[] { 0x00, 0x00, 0x00, 0x20, 0x66, 0x74, 0x79, 0x70, 0x4D, 0x34, 0x56 }
            }
        },
        //高清，低占用空间
        {
            ".WEBM", new List<byte[]>
            {
                new byte[] { 0x1A, 0x45, 0xDF, 0xA3, 0x9F, 0x42, 0x86, 0x81, 0x01, 0x42, 0xF7, 0x81, 0x01, 0x42, 0xF2 }
            }
        },
        //低质量
        {
            ".WMV", new List<byte[]>
            {
                new byte[] { 0x30, 0x26, 0xB2, 0x75, 0x8E, 0x66, 0xCF, 0x11 }
            }
        },
        //低质量
        {
            ".AVI", new List<byte[]>
            {
                new byte[] { 0x52, 0x49, 0x46, 0x46, 0x84, 0x4A, 0x1E, 0x00, 0x41, 0x56, 0x49 }
            }
        },
        //低质量
        {
            ".FLV", new List<byte[]>
            {
                new byte[] { 0x46, 0x4C, 0x56 }
            }
        },


        //电子表格
        {
            ".XLS", new List<byte[]>
            {
                new byte[] { 0xD0, 0xCF, 0x11, 0xE0, 0xA1, 0xB1, 0x1A, 0xE1 },
                new byte[] { 0x09, 0x08, 0x10, 0x00, 0x00, 0x06, 0x05, 0x00 },
                new byte[] { 0xFD, 0xFF, 0xFF, 0xFF }
            }
        },
        { ".XLSX", new List<byte[]> { new byte[] { 0x50, 0x4B, 0x03, 0x04 } } }
    };

    /// <summary>
    ///     文件真实格式检查
    /// </summary>
    /// <param name="fileName">文件名，包含后缀</param>
    /// <param name="fileData">文件头字节数组，请传200个以内</param>
    /// <param name="allowedChars">允许的例外字节数组</param>
    /// <returns></returns>
    public static bool IsValidFileExtension(string fileName, byte[] fileData, byte[]? allowedChars = null)
    {
        if (string.IsNullOrEmpty(fileName) || fileData == null || fileData.Length == 0)
        {
            return false;
        }

        bool flag = false;
        string ext = Path.GetExtension(fileName);
        if (string.IsNullOrEmpty(ext))
        {
            return false;
        }

        ext = ext.ToUpperInvariant();

        //if (ext.Equals(".TXT") || ext.Equals(".CSV") || ext.Equals(".PRN"))
        //{
        //    foreach (byte b in fileData)
        //    {
        //        if (b > 0x7F)
        //        {
        //            if (allowedChars != null)
        //            {
        //                if (!allowedChars.Contains(b))
        //                {
        //                    return false;
        //                }
        //            }
        //            else
        //            {
        //                return false;
        //            }
        //        }
        //    }
        //    return true;
        //}

        if (!fileSignature.ContainsKey(ext))
        {
            return true;
        }

        List<byte[]> sig = fileSignature[ext];
        foreach (byte[] b in sig)
        {
            byte[] curFileSig = new byte[b.Length];
            Array.Copy(fileData, curFileSig, b.Length);
            if (curFileSig.SequenceEqual(b))
            {
                //验证格式通过
                flag = true;
                break;
            }
        }

        return flag;
    }
}