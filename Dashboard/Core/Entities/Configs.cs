using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities
{
    public class Configs
    {
        public Guid Id { get; set; }
        [Column(TypeName = "jsonb")]
        public string? AdditionalProp1 { get; set; }
        [Column(TypeName = "jsonb")]
        public string? AdditionalProp2 { get; set; }
        [Column(TypeName = "jsonb")]
        public string? AdditionalProp3 { get; set; }
    }
}