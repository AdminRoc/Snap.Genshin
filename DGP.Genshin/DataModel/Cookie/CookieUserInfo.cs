﻿using DGP.Genshin.MiHoYoAPI.UserInfo;

namespace DGP.Genshin.DataModel.Cookie
{
    /// <summary>
    /// Cookie与对应的用户信息
    /// </summary>
    public class CookieUserInfo
    {
        public CookieUserInfo(string cookie, UserInfo userInfo)
        {
            Cookie = cookie;
            UserInfo = userInfo;
        }
        public string Cookie { get; init; }
        public UserInfo UserInfo { get; init; }
    }
}