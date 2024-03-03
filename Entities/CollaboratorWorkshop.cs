namespace WorkshopApi.Entities
{
    public class CollaboratorWorkshop
    {
        public Guid CollaboratorId { get; set; }
        public Collaborator Collaborator { get; set; }
        public Guid WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
    }
}
