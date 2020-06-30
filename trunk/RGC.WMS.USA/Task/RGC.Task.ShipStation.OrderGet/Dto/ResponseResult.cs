namespace RGC.Task.ShipStation.OrderGet.Dto
{
    public class ResponseResult<T>
    {
        /// <summary>
        /// 是否成功 0：成功，1：默认失败，其他值 都是失败
        /// 1：操作失败
        /// 2：系统异常
        /// 3：未登录
        /// 4：请求参数错误
        /// 5：对象不存在
        /// 9：用户需要先设置密码
        /// 31:登录失败次数大于3次
        /// 32:登录验证码输入错误
        /// 33:当前账号无法登陆，canLoign = false
        /// </summary>
        public int Code { get; set; }

        public string Msg { get; set; }

        public bool Success { get; set; }

        public T Data { get; set; }

        public ResponseResult()
        {
            Code = 1;
            Msg = "Fail";
            Success = false;
        }
    }
}
