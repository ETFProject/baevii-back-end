namespace baevii_back_end.Services;

using baevii_back_end.Configuration;
using Microsoft.Extensions.Options;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;
using System.Numerics;

public class Funder(ILogger<Funder> logger, IOptions<PrivyConfiguration> privyOptions) : IFunder
{
    private const string FlowMainnetRpcUrl = "https://mainnet.evm.nodes.onflow.org";
    private const int FlowChainId = 747;

    public async Task Fund(string? serverWalletAddress)
    {
        var account = new Account(privyOptions.Value.FundingPrivateKey);
        logger.LogInformation($"Funding wallet balance before funding = {await GetFlowBalance(account.Address)}");
        string txHash = await SendEvmTransaction(privyOptions.Value.FundingPrivateKey, serverWalletAddress, (decimal)0.001);
        logger.LogInformation($"funding tx hash = {txHash}");
        logger.LogInformation($"Server wallet balance after funding = {await GetFlowBalance(serverWalletAddress)}");
        logger.LogInformation($"Funding wallet balance after funding = {await GetFlowBalance(account.Address)}");
    }

    private async Task<decimal> GetFlowBalance(string address)
    {
        var web3 = new Web3(FlowMainnetRpcUrl);
        var balanceWei = await web3.Eth.GetBalance.SendRequestAsync(address);
        var balanceFlow = Web3.Convert.FromWei(balanceWei.Value);
        return balanceFlow;
    }

    private async Task<string> SendEvmTransaction(
        string privateKey,
        string toAddress,
        decimal amountInFlow,
        BigInteger? gasLimit = null,
        BigInteger? gasPrice = null)
    {
        var account = new Account(privateKey, FlowChainId);
        var web3 = new Web3(account, FlowMainnetRpcUrl);
        gasLimit ??= 21000;
        decimal? gasPriceDecimal = gasPrice.HasValue ? (decimal)gasPrice.Value : null;
        var transactionHash = await web3.Eth.GetEtherTransferService()
            .TransferEtherAndWaitForReceiptAsync(toAddress, amountInFlow, gasPriceDecimal, gasLimit);
        return transactionHash.TransactionHash;
    }
}
