namespace RGC.WMS.USA.Domain.Entities.Product.Enum
{
    public enum ProductStatus
    {
        /// <summary>
        /// 录入中
        /// </summary>
        Initial = 0,

        /// <summary>
        /// 待审核
        /// </summary>
        AuditPending = 1,

        /// <summary>
        /// 审核不通过
        /// </summary>
        AuditFailed = 2,

        /// <summary>
        /// 正常
        /// </summary>
        Normal = 3,

        /// <summary>
        /// 临时冻结，转正常
        /// </summary>
        TempFrozen = 6,

        /// <summary>
        /// 冻结，转淘汰
        /// </summary>
        Frozen = 7,

        /// <summary>
        /// 淘汰的
        /// </summary>
        Eliminated = 8,

        /// <summary>
        /// 资料录入异常等原因伪删除
        /// </summary>
        ///I = 9
    }
}
