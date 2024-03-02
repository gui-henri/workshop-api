using WorkshopApi.Dtos;

namespace WorkshopApi.Entities
{
    public class Collaborator(string name)
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; private set; } = name;

        public void Update(string name)
        {
            Name = name;
        }

        static public Collaborator FromDTO(CollaboratorDTO dto)
        {
            return new Collaborator(dto.Name);
        }
    }
}