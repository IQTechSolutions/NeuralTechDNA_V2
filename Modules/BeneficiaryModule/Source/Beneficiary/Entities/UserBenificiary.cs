using System.ComponentModel.DataAnnotations.Schema;
using Identity.Entities;
using NeuralTech.Entities;

namespace Beneficiary.Entities
{
    public class UserBenificiary : EntityBase<int>
    {
        [ForeignKey(nameof(Benificiary))]
        public int BenificiaryId { get; set; }
        public Benificiary Benificiary { get; set; }


        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public override string ToString()
        {
            return $"User Beneficiary";
        }
    }
}
