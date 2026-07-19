using System.Text.Json.Serialization;

namespace GinasioVitaFit.Shared.Models;

public class InstrutorMod
{
    [JsonPropertyName("instrutorId")] 
    public int InstrutorId { get; set; }
    
    [JsonPropertyName("modalidadeId")] 
    public int ModalidadeId { get; set; }
    
    [JsonPropertyName("createdDate")]
    public DateTime CreatedDate { get; set; }
    
    [JsonPropertyName("updatedDate")]
    public DateTime UpdatedDate { get; set; }
    
    [JsonPropertyName("isDeleted")] 
    public bool IsDeleted { get; set; }
}