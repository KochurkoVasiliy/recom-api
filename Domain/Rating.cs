namespace Domain;

public class Rating
{
    public int Id { get; set; }
    public Guid UserId { get; set; }
    public User User { get; set; }
    public int FilmId { get; set; }
    public Film Film { get; set; }
    public byte Score { get; set; }
    public string Comment { get; set; }
}