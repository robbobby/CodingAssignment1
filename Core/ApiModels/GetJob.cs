namespace Core.ApiModels;

public class Job
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public required string Status { get; set; }
    public required DateTime CreatedAt { get; set; }
}
