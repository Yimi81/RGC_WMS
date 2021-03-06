﻿using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Do
{
    public class ResponsePageDo<T>
    {
        /// <summary>
        /// 是否成功 0：成功，1：默认失败，其他值 都是失败
        /// 1：操作失败
        /// 2：系统异常
        /// 3：未登录
        /// 4：请求参数错误
        /// 5：对象不存在
        /// </summary>
        public int Code { get; set; }

        public string Msg { get; set; }

        public bool Success { get; set; }

        public PageDo Page { get; set; }

        public List<T> Data { get; set; }

        public ResponsePageDo()
        {
            Data = new List<T>();
            Page = new PageDo();
            Code = 1;
            Msg = "Fail";
            Success = false;
        }
    }
}
