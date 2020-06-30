using System.Collections.Generic;
using HuigeTec.Core.Domain.Entities;

namespace RGC.WMS.USA.Domain.Entities.Bms.Do
{
    public class BmsMenuTreeDo
    {
        public BmsMenuTreeDo() {
            Children = new List<BmsMenuTreeDo>();

        }

        public long Id { get; set; }
        public long ParentId { get; set; }
        public BmsMenuType Type { get; set; }
        public string Name { get; set; }
        public string AuthorizeCode { get; set; }
        public string Path { get; set; }
        public string IcoName { get; set; }
        public int SeqNo { get; set; }
        public string Remark { get; set; }
        public List<BmsMenuTreeDo> Children { get; set; }

        /// <summary>
        /// 是否授权
        /// </summary>
        public bool IsGranted { get; set; }

        /// <summary>
        /// 是否用户单独授权
        /// </summary>
        public bool IsUserGranted { get; set; }

        /// <summary>
        /// 是否用户单独移除
        /// </summary>
        public bool IsUserRemoved { get; set; }
    }
}
