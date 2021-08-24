﻿namespace DGP.Snap.Framework.Data.Privacy
{
    /// <summary>
    /// 字符串掩码设置函数
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public delegate string StringMasker(string input);

    public class PrivateString
    {
        private readonly StringMasker masker;

        /// <summary>
        /// 构造一个新的隐私字符串对象
        /// </summary>
        /// <param name="data">源字符串</param>
        /// <param name="masker">掩码算法</param>
        public PrivateString(string data, StringMasker masker, bool shouldNotMask)
        {
            this.UnMaskedValue = data;
            this.masker = masker;
            this.ShouldNotMask = shouldNotMask;
        }

        /// <summary>
        /// 经过隐私设置处理后的字符串
        /// </summary>
        public string Value => this.ShouldNotMask ? this.UnMaskedValue : this.masker.Invoke(this.UnMaskedValue);

        public string UnMaskedValue { get; }

        /// <summary>
        /// 告知次隐私字符串是否需要设置掩码
        /// </summary>
        public bool ShouldNotMask { get; set; } = false;

        public void SetShouldNotMask(bool s) => this.ShouldNotMask = s;

        public static StringMasker DefaultMasker = u => u.Substring(0, 2) + "****" + u.Substring(6, 3);
    }
}