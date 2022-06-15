using System.Collections;

namespace Core.Entities {
  public class Configs {
    public string? AdditionalProp1 { get; set; }
    public string? AdditionalProp2 { get; set; }
    public string? AdditionalProp3 { get; set; }

    public ICollection<Widget>? Widgets { get; set; }
  }
}