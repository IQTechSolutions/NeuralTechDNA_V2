using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Filing.Entities;

namespace Beneficiary.Entities
{
    public class Benificiary : ImageFileCollection<Benificiary, int>
    {
        public string CoverImageUrl { get; set; } = "_content/Accomodation.Blazor/images/NoImage.jpg";
        [MaxLength(1000)] public string Name { get; set; } = null!;
        [MaxLength(5000)] public string Description { get; set; } = null!;
        [MaxLength(1000)] public string ContactPerson { get; set; } = null!;
        [MaxLength(1000)] public string ContactNumber { get; set; } = null!;
        [MaxLength(1000)] public string ContactEmail { get; set; } = null!;
        public double CommissionPercentage { get; set; }

        public string? ReasonForRegistration { get; set; }

        public BenificiaryStatus Status { get; set; } = BenificiaryStatus.Pending;

        public string? BankName { get; set; }
        public string? BranchCode { get; set; }
        public string? AccountNr { get; set; }   
        public string? AccountType { get; set; }

        [ForeignKey(nameof(Ambassador))] public string? AmbassadorId { get; set; }
        public Ambassador? Ambassador { get; set; }

        public ICollection<UserBenificiary> UserBeneficiaries { get; set; } = new List<UserBenificiary>();

        public override string ToString()
        {
            return $"Beneficiary";
        }
    }

    public enum BenificiaryStatus
    {
        Inactive = 0,
        Pending = 1,
        Active = 2
    }
}
