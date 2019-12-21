using System.ComponentModel.DataAnnotations.Schema;

namespace MineServer.Models
{
    public abstract class ModelClass
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }
    }
}
