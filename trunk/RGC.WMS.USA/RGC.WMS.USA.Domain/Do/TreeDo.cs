using System.Collections.Generic;

namespace RGC.WMS.USA.Domain.Do
{
    public class TreeDo<TEntity> where TEntity : class
    {
        public TEntity Parent { get; set; }
        public List<TreeDo<TEntity>> Children { get; set; }

        public int ChildrenCount { get; set; }

        public bool Disabled { get; set; }
        public TreeDo()
        {
            Children = new List<TreeDo<TEntity>>();
            ChildrenCount = 0;
            Disabled = false;

        }
    }
}
