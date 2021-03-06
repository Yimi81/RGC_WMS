﻿using System;
using System.Collections.Generic;
using System.Text;

namespace RGC.Task.ShipStation.OrderGet
{
    /// <summary>
    /// shane 2020/4/16
    /// </summary>
    public class EncryptHelper
    {
        #region Base64相关

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Code(string Message)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Message);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static string Base64Decode(string Message)
        {
            byte[] bytes = Convert.FromBase64String(Message);
            return Encoding.UTF8.GetString(bytes);
        }

        #endregion
    }
}
