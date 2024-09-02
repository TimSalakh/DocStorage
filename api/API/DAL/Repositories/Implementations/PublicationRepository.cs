using API.DAL.Contexts;
using API.DAL.Entities;
using API.DAL.Repositories.Interfaces;

namespace API.DAL.Repositories.Implementations;

public class PublicationRepository : BaseRepository<Publication>, IPublicationRepository
{
    public PublicationRepository(DocStorageDbContext context)
        : base(context) { }

    public async Task ConfirmPublicationAsync(Publication publication, User confirmator)
    {
        publication.ConfirmatorId = confirmator.Id;
        publication.IsConfirmed = true;
        publication.ConfirmationTime = DateTime.UtcNow.AddHours(7);

        await UpdateAsync(publication);
    }
}

