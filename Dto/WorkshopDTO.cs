namespace WorkshopApi.Dto
{
    public class WorkshopDTO(string name, string description, DateOnly date)
    {
        public string Name { get; set; } = name;
        public string Description { get; set; } = description;
        public DateOnly Date { get; set; } = date;
    }
}