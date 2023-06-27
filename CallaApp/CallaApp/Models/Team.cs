namespace CallaApp.Models
{
    public class Team: BaseEntity
    {
        public string Name { get; set; }
        public string Image { get; set; }
        public string Testimontial { get; set; }
        public int PositionId { get; set; }
        public Position Position { get; set; }
    }
}
