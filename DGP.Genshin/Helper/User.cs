﻿using DGP.Genshin.Helper.Extension;
using Microsoft.Win32;
using System;
using System.Security.Cryptography;
using System.Text;

namespace DGP.Genshin.Helper
{
    /// <summary>
    /// 用户信息
    /// </summary>
    public class User
    {
        private const string CryptographyKey = @"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography\";
        private const string MachineGuidValue = "MachineGuid";

        private static string? userId;
        public static string Id
        {
            get
            {
                userId ??= GetUniqueUserID();
                return userId;
            }
        }

        /// <summary>
        /// 获取设备的UUID
        /// </summary>
        /// <returns>设备的UUID</returns>
        private static string GetUniqueUserID()
        {
            string UserName = Environment.UserName;
            object? MachineGuid = Registry.GetValue(CryptographyKey, MachineGuidValue, UserName);
            byte[] bytes = Encoding.UTF8.GetBytes(UserName + MachineGuid);
            byte[] hash = MD5.Create().ComputeHash(bytes);
            return hash.Stringify();
        }
    }
}
