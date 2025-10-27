namespace API_PARCIAL_3.DataTransferObjects
{
    public class StartGameResponse
    {
       
        public int GameId { get; set; }
        public int PlayerId { get; set; }
        public DateTime CreateAt { get; set; }
    }
}