using UrlShortener.Domain.Common;

namespace UrlShortener.Domain.Entities;

public class User : BaseEntity
{


    public string EntraObjectId { get; set; }

    public string Email { get; set; }

    public string DisplayName { get; set; }

    public ICollection<ShortLink> Links { get; set; }
        = new List<ShortLink>();

    //private User() { }

    //public User(string entraObjectId, string email)
    //{
    //    Id = Guid.NewGuid();
    //    EntraObjectId = entraObjectId;
    //    Email = email;
    //}
}

