using API.DAL.Entities;

namespace API.DAL.Repositories.Interfaces;

public interface IPublicationRepository : IBaseRepository<Publication>
{
    Task ConfirmPublicationAsync(Publication publication, User confirmator);
}
