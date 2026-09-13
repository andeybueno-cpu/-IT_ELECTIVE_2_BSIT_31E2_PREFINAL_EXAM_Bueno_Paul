namespace ExamApp.Models
{
    public class ExamQuestion
    {
        public int Number { get; set; }
        public string Question { get; set; } = string.Empty;
        public List<string> Options { get; set; } = new();
        public string CorrectAnswer { get; set; } = string.Empty;
        public string Explanation { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}