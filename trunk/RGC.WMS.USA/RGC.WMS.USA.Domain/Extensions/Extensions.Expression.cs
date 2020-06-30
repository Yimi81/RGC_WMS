namespace RGC.WMS.USA.Domain
{
    /// <summary>
    /// 统一存储正则表达式的常量类 Meridian 2020/06/08
    /// </summary>
    public class RegexExpression
    {
        #region 验证信息
        /// <summary>
        /// 验证邮箱正则表达式
        /// </summary>
        public const string IS_VALID_EMAIL = @"^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$";

        /// <summary>
        /// 验证手机号码正则表达式
        /// </summary>
        public const string IS_VALID_PHONE = @"(^13\d{9}$)|(^14\d{9}$)|(^15\d{9}$)|(^16\d{9}$)|(^17\d{9}$)|(^18\d{9}$)|(^19\d{9}$)";

        /// <summary>
        /// 验证电话号码正则表达式
        /// </summary>
        public const string IS_VALID_TEL = @"^([0-9]{3,4}-)?[0-9]{7,8}$";

        /// <summary>
        /// 验证电话号码正则表达式
        /// </summary>
        public const string IS_VALID_TEL2 = @"^([0-9]{3,4})?[0-9]{7,8}$";

        /// <summary>
        /// 验证15位身份证号码正则表达式
        /// </summary>
        public const string IS_VALID_IDENTITY_CARD15 = @"^[1-9]\d{7}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}$";

        /// <summary>
        /// 验证18位身份证号码正则表达式
        /// </summary>
        public const string IS_VALID_IDENTITY_CARD18 = @"^[1-9]\d{5}[1-9]\d{3}((0\d)|(1[0-2]))(([0|1|2]\d)|3[0-1])\d{3}([0-9]|x|X)$";

        /// <summary>
        /// 验证登陆密码
        /// </summary>
        public const string IS_VALID_PASSWORD = @"^(?![a-zA-z]+$)(?!\d+$)(?![!@@#$%^&*]+$)[a-zA-Z\d!@@#$%^&*]+$";
        #endregion
    }
}
