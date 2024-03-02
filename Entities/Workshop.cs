using WorkshopApi.Dto;

namespace WorkshopApi.Entities
{
    public class Workshop(string name, string description, DateOnly date)
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public DateOnly Date { get; set; } = date;

        public void Update(string name, string description, DateOnly date)
        {
            Name = name;
            Description = description;
            Date = date;
        }

        public static Workshop FromDTO(WorkshopDTO workshopDTO)
        {
            return new Workshop(workshopDTO.Name, workshopDTO.Description, workshopDTO.Date);
        }
    }
}