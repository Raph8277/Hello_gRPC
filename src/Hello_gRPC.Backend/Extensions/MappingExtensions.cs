namespace HelloGrpc.Backend.Extensions;

/// <summary>
/// Extensions de mapping entre entités et messages protobuf.
/// </summary>
public static class MappingExtensions
{
    public static PersonalityMessage ToProto(this Personality entity) => new()
    {
        Id = entity.Id,
        FirstName = entity.FirstName,
        LastName = entity.LastName,
        Bio = entity.Bio,
        Category = entity.Category,
        Nationality = entity.Nationality,
        BirthDate = entity.BirthDate.ToString("yyyy-MM-dd"),
        DeathDate = entity.DeathDate?.ToString("yyyy-MM-dd") ?? "",
        ImageUrl = entity.ImageUrl ?? ""
    };

    public static Personality ToEntity(this CreatePersonalityRequest request) => new()
    {
        FirstName = request.FirstName,
        LastName = request.LastName,
        Bio = request.Bio,
        Category = request.Category,
        Nationality = request.Nationality,
        BirthDate = DateOnly.Parse(request.BirthDate),
        DeathDate = string.IsNullOrEmpty(request.DeathDate) ? null : DateOnly.Parse(request.DeathDate),
        ImageUrl = string.IsNullOrEmpty(request.ImageUrl) ? null : request.ImageUrl
    };

    public static void UpdateFrom(this Personality entity, UpdatePersonalityRequest request)
    {
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Bio = request.Bio;
        entity.Category = request.Category;
        entity.Nationality = request.Nationality;
        entity.BirthDate = DateOnly.Parse(request.BirthDate);
        entity.DeathDate = string.IsNullOrEmpty(request.DeathDate) ? null : DateOnly.Parse(request.DeathDate);
        entity.ImageUrl = string.IsNullOrEmpty(request.ImageUrl) ? null : request.ImageUrl;
    }
}
