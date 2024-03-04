using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using WorkshopApi.Dto;

namespace WorkshopApi.Entities
{
    public class Workshop
    {
        public Guid Id { get; init; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public DateOnly Date { get; private set; }

        [JsonIgnore]
        public ICollection<CollaboratorWorkshop> CollaboratorWorkshops { get; }

        private Workshop(string name, string description, DateOnly date)
        {
            Id = Guid.NewGuid();
            CollaboratorWorkshops = [];
            Name = name;
            Description = description;
            Date = date;
        }

        public static Workshop? Create(string name, string description, DateOnly date)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(description))
            {
                return null;
            }

            return new Workshop(name, description, date);
        }

        public void Update(string? name, string? description, DateOnly? date)
        {
            if (name != null)
            {
                Name = name;
            }

            if (description != null)
            {
                Description = description;
            }

            if (date != null)
            {
                Date = date.Value;
            }
            
        }

        public static Workshop? FromDTO(WorkshopDTO workshopDTO)
        {
            return Create(workshopDTO.Name, workshopDTO.Description, workshopDTO.Date);
        }
    }
}