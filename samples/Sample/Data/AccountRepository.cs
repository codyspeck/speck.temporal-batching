namespace Sample.Data;

public class AccountRepository
{
    private readonly List<Account> _accounts = [];

    public void AddBatch(IEnumerable<Account> accounts) => _accounts.AddRange(accounts);

    public IReadOnlyCollection<Account> Accounts => _accounts;
}
