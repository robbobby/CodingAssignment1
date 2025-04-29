namespace Db.Entity;

public class ExportJobEntity 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public int OperationId { get; set; }
}
