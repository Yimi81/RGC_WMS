using System.Collections.Generic;

namespace RGC.WMS.USA.Application.Dto
{
    public class TreeDto<TEntity> where TEntity : class
    {
        public TEntity Parent { get; set; }
        public List<TreeDto<TEntity>> Children { get; set; }

        public int ChildrenCount { get; set; }

        public bool Disabled { get; set; }
        public TreeDto()
        {
            Children = new List<TreeDto<TEntity>>();
            ChildrenCount = 0;
            Disabled = false;

        }
    }
}
