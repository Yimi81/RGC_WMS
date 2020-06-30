using System.ComponentModel.DataAnnotations.Schema;

namespace RGC.WMS.USA.Domain.Entities.System
{
    [Table("state")]
    public class State
    {
        public State()
        {
           
        }

        [Column("id")]
        public long Id { get; set; }

        [Column("e_capital")]
        public string ECapital { get; set; }

        [Column("c_capital")]
        public string CCapital { get; set; }

        [Column("c_name")]
        public string CName { get; set; }


        [Column("e_name")]
        public string EName { get; set; }

        [Column("code")]
        public string Code { get; set; }

        [Column("remark")]
        public string Remark { get; set; }

    }
}