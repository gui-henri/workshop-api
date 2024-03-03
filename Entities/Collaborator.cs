using WorkshopApi.Dtos;
using System.Text.Json.Serialization;

namespace WorkshopApi.Entities
{
    public class Collaborator
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        [JsonIgnore]
        public ICollection<CollaboratorWorkshop> CollaboratorWorkshops { get; }

        private Collaborator(string name)
        {
            Id = Guid.NewGuid();
            CollaboratorWorkshops = [];
            Name = name;
        }

        public static Collaborator? Create(string name)
        {

            if (string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            return new Collaborator(name);
        }

        public void Update(string? name)
        {
            if (name != null)
            {
                Name = name;
            }
        }


        public static Collaborator? FromDTO(CollaboratorDTO dto)
        {
            return Create(dto.Name);
        }
    }
}