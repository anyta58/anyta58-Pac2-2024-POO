using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogUNAH.API.Database.Entities
{
    public class BaseEntity
    {
        [Key]
        [Column("id")]
        //Esto esta en comun en todas las tablas
        public Guid Id { get; set; }

        [Column("created_by")]
        public string CreatedBty { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }

        [Column("updated_by")]
        public string UpdateBy { get; set; }

        [Column("updated_date")]
        public DateTime UpdatedDate { get; set; }

        public bool IsDeleted { get; set; }

    }
}
