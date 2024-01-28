namespace Biblioteka.Models
{
    public class PseudoQueue
    {
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public string Date { get; set; }

        public PseudoQueue(int id, int count)
        {
            UserId = id;
            Quantity = count;
            Date = DateTime.Now.Date.ToShortDateString();
        }
    }
}