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
            new ExamQuestion {
                Number = 4,
                Question = "Which EF Core component is primarily responsible for communicating with the database?",
                Options = new() { "A. DbContext", "B. DbSetView", "C. ControllerContext", "D. RazorContext" },
                CorrectAnswer = "A. DbContext",
                Category = "Relational Data Modeling"
            },
            new ExamQuestion {
                Number = 5,
                Question = "What does the following command primarily do? dotnet ef dbcontext scaffold \"ConnectionString\" Microsoft.EntityFrameworkCore.SqlServer -o Models",
                Options = new() { "A. Deletes the database", "B. Creates a new MVC project", "C. Generates EF Core models and a DbContext from an existing database", "D. Starts the MVC application" },
                CorrectAnswer = "C. Generates EF Core models and a DbContext from an existing database",
                Category = "Relational Data Modeling"
            },
            new ExamQuestion {
                Number = 6,
                Question = "Where is a database connection string commonly stored in an ASP.NET Core MVC application?",
                Options = new() { "A. Program.cs only", "B. appsettings.json", "C. Index.cshtml", "D. Student.cs" },
                CorrectAnswer = "B. appsettings.json",
                Category = "Conceptual Data Architecture"
            },
            new ExamQuestion {
                Number = 7,
                Question = "A Student belongs to exactly one Section, while a Section can contain many students. What type of relationship is this?",
                Options = new() { "A. One-to-One", "B. One-to-Many", "C. Many-to-Many", "D. Many-to-One only" },
                CorrectAnswer = "B. One-to-Many",
                Category = "Conceptual Data Architecture"
            },
            new ExamQuestion {
                Number = 8,
                Question = "In the following example, what is SectionId? public int SectionId { get; set; } public Section Section { get; set; }",
                Options = new() { "A. Primary key of Student", "B. Foreign key referencing Section", "C. Navigation property", "D. Database connection string" },
                CorrectAnswer = "B. Foreign key referencing Section",
                Category = "Conceptual Data Architecture"
            },
            new ExamQuestion {
                Number = 9,
                Question = "What is the purpose of a navigation property such as public Section Section { get; set; }?",
                Options = new() { "A. It stores the database password", "B. It represents a relationship to another entity", "C. It creates a new database", "D. It validates the student's name" },
                CorrectAnswer = "B. It represents a relationship to another entity",
                Category = "Conceptual Data Architecture"
            },
            new ExamQuestion {
                Number = 10,
                Question = "What does .Include() generally allow EF Core to do?",
                Options = new() { "A. Delete the Section table", "B. Load related Section data together with Students", "C. Create a new Student", "D. Validate Student input" },
                CorrectAnswer = "B. Load related Section data together with Students",
                Category = "Conceptual Data Architecture"
            },
            new ExamQuestion {
                Number = 11,
                Question = "Why might a ViewModel be used when displaying Student and Section information?",
                Options = new() { "A. To replace the database", "B. To combine or shape the data specifically needed by the view", "C. To automatically create database tables", "D. To prevent controllers from using LINQ" },
                CorrectAnswer = "B. To combine or shape the data specifically needed by the view",
                Category = "Data Normalization & Integrity"
            },
            new ExamQuestion {
                Number = 12,
                Question = "Consider this query: var students = _context.Students.Include(s => s.Section).ToList(); What is the main benefit of Include(s => s.Section)?",
                Options = new() { "A. It loads the related Section navigation property", "B. It creates a Section object manually", "C. It removes the foreign key", "D. It prevents the query from accessing the database" },
                CorrectAnswer = "A. It loads the related Section navigation property",
                Category = "Data Normalization & Integrity"
            },
            new ExamQuestion {
                Number = 13,
                Question = "Which type of validation occurs in the browser before a request is sent to the server?",
                Options = new() { "A. Database-level validation", "B. Client-side validation", "C. Server-side validation", "D. EF Core migration validation" },
                CorrectAnswer = "B. Client-side validation",
                Category = "Data Normalization & Integrity"
            },
            new ExamQuestion {
                Number = 14,
                Question = "Why is server-side validation still necessary if client-side validation exists?",
                Options = new() { "A. Client-side validation can be bypassed", "B. Client-side validation automatically modifies the database", "C. Server-side validation only works with SQLite", "D. Client-side validation cannot display messages" },
                CorrectAnswer = "A. Client-side validation can be bypassed",
                Category = "Data Normalization & Integrity"
            },
            new ExamQuestion {
                Number = 15,
                Question = "A school requires every student to have a unique Student Number. Which rule best represents this requirement?",
                Options = new() { "A. Student Number should always be nullable", "B. Student Number should be unique", "C. Student Number should always be the same", "D. Student Number should contain only spaces" },
                CorrectAnswer = "B. Student Number should be unique",
                Category = "Data Normalization & Integrity"
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