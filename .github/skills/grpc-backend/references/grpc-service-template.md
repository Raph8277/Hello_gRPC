# gRPC Service Template

The gRPC service lives in the **Backend layer** (`Hello_gRPC.Backend/Services/`).
It delegates business logic to the **Service layer** and uses **mapping extensions** to convert between protobuf messages and EF Core entities.

## Service Implementation Pattern

```csharp
namespace HelloGrpc.Backend.Services;

/// <summary>
/// Service gRPC pour la gestion des personnalites.
/// Delegue la logique metier a la couche Service.
/// </summary>
public class PersonalityService(HelloGrpc.Service.PersonalityService personalityService)
    : PersonalityGrpc.PersonalityGrpcBase
{
    /// <summary>
    /// Recupere toutes les personnalites avec pagination et filtrage.
    /// </summary>
    public override async Task<GetPersonalitiesResponse> GetPersonalities(
        GetPersonalitiesRequest request, ServerCallContext context_call)
    {
        var items = await personalityService.GetAllAsync();

        // Filtrage
        var query = items.AsQueryable();
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var term = request.SearchTerm.ToLower();
            query = query.Where(p =>
                p.FirstName.ToLower().Contains(term) ||
                p.LastName.ToLower().Contains(term) ||
                p.Bio.ToLower().Contains(term));
        }
        if (!string.IsNullOrWhiteSpace(request.Category))
            query = query.Where(p => p.Category == request.Category);

        var totalCount = query.Count();
        var paged = query
            .OrderBy(p => p.LastName).ThenBy(p => p.FirstName)
            .Skip(request.Skip)
            .Take(request.Take > 0 ? request.Take : 20)
            .ToList();

        var response = new GetPersonalitiesResponse { TotalCount = totalCount };
        response.Personalities.AddRange(paged.Select(p => p.ToProto()));
        return response;
    }

    /// <summary>
    /// Recupere une personnalite par son identifiant.
    /// </summary>
    public override async Task<PersonalityMessage> GetPersonality(
        GetPersonalityRequest request, ServerCallContext context_call)
    {
        var entity = await personalityService.GetByIdAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound,
                `Personnalite avec l'ID {request.Id} introuvable.`));

        return entity.ToProto();
    }

    /// <summary>
    /// Cree une nouvelle personnalite.
    /// </summary>
    public override async Task<PersonalityMessage> CreatePersonality(
        CreatePersonalityRequest request, ServerCallContext context_call)
    {
        ValidateRequest(request.FirstName, request.LastName, request.Bio, request.Category);

        var entity = request.ToEntity();
        var created = await personalityService.AddAsync(entity);
        return created.ToProto();
    }

    /// <summary>
    /// Met a jour une personnalite existante.
    /// </summary>
    public override async Task<PersonalityMessage> UpdatePersonality(
        UpdatePersonalityRequest request, ServerCallContext context_call)
    {
        var entity = await personalityService.GetByIdAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound,
                `Personnalite avec l'ID {request.Id} introuvable.`));

        ValidateRequest(request.FirstName, request.LastName, request.Bio, request.Category);

        entity.UpdateFrom(request);
        var updated = await personalityService.UpdateAsync(entity);
        return updated.ToProto();
    }

    /// <summary>
    /// Supprime une personnalite.
    /// </summary>
    public override async Task<DeletePersonalityResponse> DeletePersonality(
        DeletePersonalityRequest request, ServerCallContext context_call)
    {
        var success = await personalityService.DeleteAsync(request.Id);
        if (!success)
            throw new RpcException(new Status(StatusCode.NotFound,
                `Personnalite avec l'ID {request.Id} introuvable.`));

        return new DeletePersonalityResponse { Success = true };
    }

    private static void ValidateRequest(string firstName, string lastName, string bio, string category)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Le prenom est requis."));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Le nom est requis."));
        if (string.IsNullOrWhiteSpace(bio))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "La biographie est requise."));
        if (string.IsNullOrWhiteSpace(category))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "La categorie est requise."));
    }
}
```

## Mapping Extensions Pattern (Backend Layer)

```csharp
namespace HelloGrpc.Backend.Extensions;

/// <summary>
/// Extensions de mapping entre entites (Data) et messages protobuf (Shared).
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
```

## Error Handling

Always use `RpcException` with appropriate `StatusCode`:
- `NotFound` --- entity doesn't exist
- `InvalidArgument` --- validation failure
- `AlreadyExists` --- duplicate constraint violation
- `Internal` --- unexpected errors (wrap in try-catch at service level)

## Layer Interaction

```
Frontend --gRPC--> Backend.Services.PersonalityService
                       |
                       +--> Backend.Extensions.MappingExtensions (proto <-> entity)
                       |
                       +--> Service.PersonalityService (CRUD business logic)
                                |
                                +--> Data.AppDbContext (EF Core SQLite)
```