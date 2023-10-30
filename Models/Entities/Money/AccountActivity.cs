using Home.Models.Entities.Other;

namespace Home.Models.Entities.Money
{
    public class AccountActivity
    {
        /// <summary>
        /// Fields
        /// </summary>
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }


        /// <summary>
        /// Relationships
        /// </summary>

        public int Id { get; set; }   
        public int? AccountId { get; set; }
        public Account? Account { get; set; }
    }
}
