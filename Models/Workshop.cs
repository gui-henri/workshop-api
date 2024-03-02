namespace WorkshopApi.Models
{
    public class Workshop(string name)
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; private set; } = name;
    }
}