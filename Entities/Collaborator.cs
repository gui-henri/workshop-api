using WorkshopApi.Dtos;

namespace WorkshopApi.Entities
{
    public class Collaborator(string name)
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = name;

        public CollaboratorDTO ToDTO()
        {
            return new CollaboratorDTO(Name);
        }

        static public Collaborator FromDTO(CollaboratorDTO dto)
        {
            return new Collaborator(dto.Name);
        }
    }
}