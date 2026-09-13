using Microsoft.AspNetCore.Mvc;
using ExamApp.Models;

namespace ExamApp.Controllers
{
    public class ExamController : Controller
    {
        private static readonly List<ExamQuestion> Questions = new()
        {
            new ExamQuestion {
                Number = 1,
                Question = "What is the main problem solved by using a database instead of an in-memory collection?",
                Options = new() { "A. It makes C# code shorter", "B. It prevents the application from restarting", "C. It allows data to persist after the application stops", "D. It removes the need for MVC" },
                CorrectAnswer = "C. It allows data to persist after the application stops",
                Explanation = "Databases ensure persistent storage across restarts.",
                Category = "Database Fundamentals"
            },
        };

        public IActionResult Index(string? search, string? category)
        {
            var filtered = Questions.AsEnumerable();

            if (!string.IsNullOrEmpty(search))
                filtered = filtered.Where(q => q.Question.Contains(search, StringComparison.OrdinalIgnoreCase));

            if (!string.IsNullOrEmpty(category))
                filtered = filtered.Where(q => q.Category == category);

            ViewBag.Categories = Questions.Select(q => q.Category).Distinct().ToList();
            return View(filtered.ToList());
        }

        public IActionResult Details(int id)
        {
            var item = Questions.FirstOrDefault(q => q.Number == id);
            if (item == null) return NotFound();
            return View(item);
        }
    }
}