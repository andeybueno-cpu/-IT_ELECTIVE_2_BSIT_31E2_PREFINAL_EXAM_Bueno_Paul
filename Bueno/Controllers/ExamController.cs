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
            new ExamQuestion {
                Number = 2,
                Question = "Which approach is being used when an existing database is used to generate EF Core entity classes?",
                Options = new() { "A. Code-First", "B. Database-First", "C. Model-First", "D. Controller-First" },
                CorrectAnswer = "B. Database-First",
                Explanation = "Database-First reverse engineers entities from schema.",
                Category = "EF Core Architecture"
            },
            new ExamQuestion {
                Number = 3,
                Question = "What is the primary purpose of Entity Framework Core?",
                Options = new() { "A. To create HTML pages automatically", "B. To replace the MVC Controller", "C. To map objects in code to relational database data", "D. To replace the C# compiler" },
                CorrectAnswer = "C. To map objects in code to relational database data",
                Category = "Relational Data Modeling"
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