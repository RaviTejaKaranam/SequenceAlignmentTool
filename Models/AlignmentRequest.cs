namespace Backend.Models
{
    public class AlignmentRequest
    {
        public string Sequence1 { get; set; }
        public string Sequence2 { get; set; }
        public int Match { get; set; }
        public int Mismatch { get; set; }
        public int Gap { get; set; }
    }
}
