using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/publication")]
public class PublicationController : ControllerBase
{
    [HttpGet("publications")]
    public IActionResult GetPublications()
    {
        return Ok(FetchPublications());
    }

    private List<Pub> FetchPublications()
    {
        return new List<Pub>
        {
            new Pub { Id = Guid.NewGuid(), Date = "28.07 12:36", Author = "Сергей Белов", Name = "Доклад на тему ML", Type = "Доклад", Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?,Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?" },
            new Pub { Id = Guid.NewGuid(), Date = "28.07 12:36", Author = "Сергей Белов", Name = "Доклад на тему ML", Type = "Доклад", Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?,Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?" },
            new Pub { Id = Guid.NewGuid(), Date = "28.07 12:36", Author = "Сергей Белов", Name = "Доклад на тему ML", Type = "Доклад", Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?,Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?" },
            new Pub { Id = Guid.NewGuid(), Date = "28.07 12:36", Author = "Сергей Белов", Name = "Доклад на тему ML", Type = "Доклад", Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?,Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?" },
            new Pub { Id = Guid.NewGuid(), Date = "28.07 12:36", Author = "Сергей Белов", Name = "Доклад на тему ML", Type = "Доклад", Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?,Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?" },
            new Pub { Id = Guid.NewGuid(), Date = "28.07 12:36", Author = "Сергей Белов", Name = "Доклад на тему ML", Type = "Доклад", Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?,Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?" },
            new Pub { Id = Guid.NewGuid(), Date = "28.07 12:36", Author = "Сергей Белов", Name = "Доклад на тему ML", Type = "Доклад", Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?,Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?" },
            new Pub { Id = Guid.NewGuid(), Date = "28.07 12:36", Author = "Сергей Белов", Name = "Доклад на тему ML", Type = "Доклад", Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?,Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?" },
            new Pub { Id = Guid.NewGuid(), Date = "28.07 12:36", Author = "Сергей Белов", Name = "Доклад на тему ML", Type = "Доклад", Description = "Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?,Lorem ipsum dolor sit amet consectetur adipisicing elit. Sapiente, eos?" }
        };
    }
}

public class Pub
{
    public Guid Id { get; set; }
    public string Date { get; set; }
    public string Author { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
}
