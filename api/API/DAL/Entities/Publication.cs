namespace API.DAL.Entities;

public class Publication
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public PublicationType PublicationType { get; set; }
    public string Description { get; set; } = null!;
    public Guid AuthorId { get; set; }
    public User? Author { get; set; }
    public bool IsConfirmed { get; set; } = false;
    public Guid ConfirmatorId { get; set; }
    public User? Confirmator { get; set; }
    public DateTime ConfirmationTime { get; set; }
    public DateTime CreationTime { get; set; }
    //public ICollection<Document>? Documents { get; set; }
}

public enum PublicationType
{
    Coursework,         // Курсовая работа
    Summary,            // Реферат 
    IndependentStudy,   // Самостоятельная работа
    LaboratoryWork,     // Лабораторная работа 
    ResearchPaper,      // Научная статья
    Essay,              // Эссе 
    Report              // Доклад  
}