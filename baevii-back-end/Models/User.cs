namespace baevii_back_end.Models;

public class User
{
    public int Id { get; set; }
    public string? PrivyId { get; set; }
    public DateTimeOffset? CreatedAt { get; set; }
    public Account[]? Accounts { get; set; }
}