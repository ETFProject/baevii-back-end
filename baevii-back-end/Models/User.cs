namespace baevii_back_end.Models;

public class User
{
    public int Id { get; set; }
    public string? PrivyId { get; set; }
    public string? Type { get; set; }
    public string? Address { get; set; }
    public DateTime? CreatedAt { get; set; }
    public Account[]? Accounts { get; set; }
}