namespace baevii_back_end.Services;

public interface IFunder
{
    Task Fund(string? serverWalletAddress);
}
