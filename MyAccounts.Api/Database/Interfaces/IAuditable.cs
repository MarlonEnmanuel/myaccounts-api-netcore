namespace MyAccounts.Api.Database.Interfaces
{
    public interface IAuditable
    {
        public int CreatedBy { get; set; }

        public int UpdatedBy { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }
    }
}
