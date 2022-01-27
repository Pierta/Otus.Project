namespace Otus.Project.Domain.Model
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public decimal Cost { get; set; }
    }
}
