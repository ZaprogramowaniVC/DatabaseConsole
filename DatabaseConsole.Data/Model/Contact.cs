
namespace DatabaseConsole.Data.Model
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public SexDictionary Sex { get; set; }
        public string PhoneNumber { get; set; }

        public override string ToString()
        {
            return $"{Name} | {Surname} | {Sex} | {PhoneNumber}";
        }

    }

    public enum SexDictionary
    {
        Male = 1,
        Female
    }
}
