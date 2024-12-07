namespace Backend.Models
{
    public class AlignmentStep
    {
        public int[][] Matrix { get; set; }  // Current state of the matrix
        public int[] CurrentCell { get; set; }  // Cell being filled [row, column]
        public string Explanation { get; set; }  // Explanation of the decision made (insertion, deletion, substitution)
    }
}
