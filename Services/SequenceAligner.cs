using Backend.Models;
using System;
using System.Collections.Generic;

namespace Backend.Services
{
    public class SequenceAligner
    {
        // Method to perform Needleman-Wunsch alignment and return step-by-step matrix filling
        public IEnumerable<AlignmentStep> AlignStepByStep(string seq1, string seq2)
        {
            int m = seq1.Length;
            int n = seq2.Length;
            int[,] dp = new int[m + 1, n + 1];

            // Initialize the scoring matrix (base cases for insertion and deletion)
            for (int i = 0; i <= m; i++) dp[i, 0] = i;  // Deletions
            for (int j = 0; j <= n; j++) dp[0, j] = j;  // Insertions

            // Initial step: matrix is initialized
            yield return new AlignmentStep
            {
                Matrix = ConvertToJaggedArray(dp),
                CurrentCell = null,
                Explanation = "Initialized the first row and column with gap penalties."
            };

            // Fill the matrix step-by-step
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    // Determine match or mismatch cost (0 for match, 1 for mismatch)
                    int matchScore = (seq1[i - 1] == seq2[j - 1]) ? 0 : 1;

                    // Compute insertion, deletion, and substitution costs
                    int insertion = dp[i, j - 1] + 1;
                    int deletion = dp[i - 1, j] + 1;
                    int substitution = dp[i - 1, j - 1] + matchScore;

                    // Choose the minimum of the three possible moves
                    dp[i, j] = Math.Min(substitution, Math.Min(insertion, deletion));

                    // Generate explanation based on which operation was chosen
                    string explanation = $"Filling cell ({i}, {j}): ";

                    if (dp[i, j] == insertion)
                        explanation += $"Insertion (gap in seq1) from cell ({i}, {j - 1}), cost = {insertion}.";
                    else if (dp[i, j] == deletion)
                        explanation += $"Deletion (gap in seq2) from cell ({i - 1}, {j}), cost = {deletion}.";
                    else
                        explanation += $"Substitution/Match from cell ({i - 1}, {j - 1}), cost = {substitution}.";

                    yield return new AlignmentStep
                    {
                        Matrix = ConvertToJaggedArray(dp),
                        CurrentCell = new int[] { i, j },
                        Explanation = explanation
                    };
                }
            }
        }

        // Helper method to convert multi-dimensional array to jagged array
        private int[][] ConvertToJaggedArray(int[,] multiArray)
        {
            int rows = multiArray.GetLength(0);
            int cols = multiArray.GetLength(1);
            int[][] jaggedArray = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                jaggedArray[i] = new int[cols];
                for (int j = 0; j < cols; j++)
                {
                    jaggedArray[i][j] = multiArray[i, j];
                }
            }
            return jaggedArray;
        }
    }
}
