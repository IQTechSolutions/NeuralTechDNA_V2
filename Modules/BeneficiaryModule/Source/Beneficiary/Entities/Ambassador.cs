using NeuralTech.Entities;

namespace Beneficiary.Entities
{
    public class Ambassador : EntityBase<string>
    {
        public string Name { get; set; } = null!;
        public string Surname { get; set; } = null!;
        public string PhoneNr { get; set; } = null!; 
        public string Email { get; set; } = null!;
        public double CommissionPercentage { get; set; }

        public ICollection<Benificiary> Beneficiaries { get; set; } = [];
    }
}
