namespace Model
{
    public class Review
    {
        public int ReviewID { get; set; }

        public string ReviewText { get; set; }

        public Restaurant Restaurant { get; set; }

        public User User { get; set; }
    }
}