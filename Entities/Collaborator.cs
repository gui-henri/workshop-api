namespace WorkshopApi.Entities
{
    public class Collaborator(string name)
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; private set; } = name;
    }
}