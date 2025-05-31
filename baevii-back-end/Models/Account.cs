namespace baevii_back_end.Models;

public class Account
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string? ChainId { get; set; }
    public string? ChainType { get; set; }
    public string? ConnectorType { get; set; }
    public string? Type { get; set; }
    public string? WalletClient { get; set; }
    public string? WalletClientType { get; set; }
}