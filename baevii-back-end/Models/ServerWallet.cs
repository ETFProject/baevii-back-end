namespace baevii_back_end.Models
{
    public class ServerWallet
    {
        public int Id { get; set; }
        public string? PrivyId { get; set; }
        public string? ChainType { get; set; }
        public string? Address { get; set; }
        public DateTimeOffset? CreatedAt { get; set; }
        public User? User { get; set; }
    }
}
