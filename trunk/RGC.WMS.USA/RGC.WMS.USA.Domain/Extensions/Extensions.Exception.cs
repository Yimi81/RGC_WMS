using System;

namespace RGC.WMS.USA.Domain
{
    /// <summary>
    /// 自定义异常，用于接口内的验证 Meridian 2020/06/08
    /// </summary>
    public class CustomException : Exception
    {
        /// <summary>
        /// 自定义异常初始化构造
        /// </summary>
        /// <param name="errorMessage">失败原因</param>
        /// <param name="errorCode">
        /// 是否成功 
        /// 0：成功
        /// 1：操作失败
        /// 2：系统异常
        /// 3：未登录
        /// 4：请求参数错误
        /// 5：对象不存在
        /// 9：用户需要先设置密码
        /// 31:登录失败次数大于3次
        /// 32:登录验证码输入错误
        /// 33:当前账号无法登陆，canLoign = false</param>
        public CustomException(string errorMessage, int errorCode)
            : base(errorMessage)
        {
            base.HResult = errorCode;
        }
    }
}
