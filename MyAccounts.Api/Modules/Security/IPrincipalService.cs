namespace MyAccounts.Api.Modules.Security
{
    public interface IPrincipalService
    {
        public int UserId { get; }
        public DateTime RequestDate { get; }
    }
}
