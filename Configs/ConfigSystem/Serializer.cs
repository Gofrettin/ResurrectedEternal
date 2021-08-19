using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResurrectedEternal.Configs.ConfigSystem
{
    public static class Serializer
    {

        ///// <summary>
        ///// Creates a binary json
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="obj"></param>
        ///// <returns></returns>
        //public static byte[] EncryptedJson<T>(T obj)
        //{
        //    return Encoding.UTF8.GetBytes(StringCipher.Pack(JsonConvert.SerializeObject(obj, new CustomWrapConverter())));
        //}

        ///// <summary>
        ///// Deserializes a json from binary.
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //public static T DecryptedJson<T>(byte[] data)
        //{
        //    var _unpack = StringCipher.UnPack(Encoding.UTF8.GetString(data));
        //    //var _decompress = Compressor.Decompress(data);
        //    //var _decompressedData = Encoding.UTF8.GetString(_decompress);
        //    return JsonConvert.DeserializeObject<T>(_unpack, new CustomWrapConverter());
        //}


        //public static T UnsafeJson<T>(string data)
        //{
        //    return JsonConvert.DeserializeObject<T>(data);
        //}
        //public static string SafeJson<T>(T data)
        //{
        //    return JsonConvert.SerializeObject(data, Formatting.Indented);
        //}
        //public static T DJson<T>(byte[] data)
        //{
        //    var _decompressedData = Compressor.Decompress(data);
        //    var _decryption = StringCipher.Decrypt(_decompressedData, HardwareGenerator.m_dwHardwareIdentification);
        //    var _string = Encoding.UTF8.GetString(_decryption);
        //    return JsonConvert.DeserializeObject<T>(_string, new CustomWrapConverter());
        //}

        public static T LoadJson<T>(string pth)
        {
            if (File.Exists(pth))
                return JsonConvert.DeserializeObject<T>(File.ReadAllText(pth), new CustomWrapConverter());
            return default;
        }

        public static void SaveJson<T>(T ob, string pth)
        {
            //var _p = ;
            if (!Directory.Exists(Path.GetDirectoryName(pth)))
                Directory.CreateDirectory(Path.GetDirectoryName(pth));
            File.WriteAllText(pth, JsonConvert.SerializeObject(ob, Formatting.Indented, new CustomWrapConverter()));
        }

    }
}
