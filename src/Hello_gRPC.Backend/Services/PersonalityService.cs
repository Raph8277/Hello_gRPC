using HelloGrpc.Service;
using HelloGrpc.Shared;
using Grpc.Core;

namespace HelloGrpc.Backend.Services;

/// <summary>
/// Service gRPC pour la gestion des personnalités, basé sur la couche métier.
/// </summary>
public class PersonalityService(HelloGrpc.Service.PersonalityService service) : PersonalityGrpc.PersonalityGrpcBase
{
    public override async Task<PersonalityMessage> UpdatePersonality(UpdatePersonalityRequest request, ServerCallContext context)
    {
        var entity = await service.GetByIdAsync(request.Id);
        if (entity is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Personnalité avec l'ID {request.Id} introuvable."));

        // Validation basique
        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Le prénom est requis."));
        if (string.IsNullOrWhiteSpace(request.LastName))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Le nom est requis."));
        if (string.IsNullOrWhiteSpace(request.Bio))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "La biographie est requise."));

        // Mise à jour des champs
        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Bio = request.Bio;
        entity.Category = request.Category;
        entity.Nationality = request.Nationality;
        entity.BirthDate = DateOnly.TryParse(request.BirthDate, out var birth) ? birth : entity.BirthDate;
        entity.DeathDate = DateOnly.TryParse(request.DeathDate, out var death) ? death : null;
        entity.ImageUrl = string.IsNullOrWhiteSpace(request.ImageUrl) ? null : request.ImageUrl;
        entity.UpdatedAt = DateTime.UtcNow;

        await service.UpdateAsync(entity);

        return new PersonalityMessage
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Bio = entity.Bio,
            Category = entity.Category,
            Nationality = entity.Nationality,
            BirthDate = entity.BirthDate.ToString("yyyy-MM-dd"),
            DeathDate = entity.DeathDate?.ToString("yyyy-MM-dd") ?? string.Empty,
            ImageUrl = entity.ImageUrl ?? string.Empty
        };
    }
    public override async Task<GetPersonalitiesResponse> GetPersonalities(GetPersonalitiesRequest request, ServerCallContext context)
    {
        var items = await service.GetAllAsync();
        var response = new GetPersonalitiesResponse();
        response.Personalities.AddRange(items.Select(p => new PersonalityMessage
        {
            Id = p.Id,
            FirstName = p.FirstName,
            LastName = p.LastName,
            Bio = p.Bio,
            Category = p.Category,
            Nationality = p.Nationality,
            BirthDate = p.BirthDate.ToString("yyyy-MM-dd"),
            DeathDate = p.DeathDate?.ToString("yyyy-MM-dd") ?? string.Empty,
            ImageUrl = p.ImageUrl ?? string.Empty
        }));
        response.TotalCount = items.Count;
        return response;
    }

    public override async Task<PersonalityMessage> GetPersonality(GetPersonalityRequest request, ServerCallContext context)
    {
        var entity = await service.GetByIdAsync(request.Id);
        if (entity is null)
            throw new RpcException(new Status(StatusCode.NotFound, $"Personnalité avec l'ID {request.Id} introuvable."));
        return new PersonalityMessage
        {
            Id = entity.Id,
            FirstName = entity.FirstName,
            LastName = entity.LastName,
            Bio = entity.Bio,
            Category = entity.Category,
            Nationality = entity.Nationality,
            BirthDate = entity.BirthDate.ToString("yyyy-MM-dd"),
            DeathDate = entity.DeathDate?.ToString("yyyy-MM-dd") ?? string.Empty,
            ImageUrl = entity.ImageUrl ?? string.Empty
        };
    }

    public override async Task<PersonalityMessage> CreatePersonality(CreatePersonalityRequest request, ServerCallContext context)
    {
        if (string.IsNullOrWhiteSpace(request.FirstName))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Le prénom est requis."));
        if (string.IsNullOrWhiteSpace(request.LastName))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Le nom est requis."));
        if (string.IsNullOrWhiteSpace(request.Bio))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "La biographie est requise."));
        var entity = new HelloGrpc.Data.Entities.Personality
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Bio = request.Bio,
            Category = request.Category,
            Nationality = request.Nationality,
            BirthDate = DateOnly.TryParse(request.BirthDate, out var birth) ? birth : default,
            DeathDate = DateOnly.TryParse(request.DeathDate, out var death) ? death : null,
            ImageUrl = string.IsNullOrWhiteSpace(request.ImageUrl) ? null : request.ImageUrl
        };
        var created = await service.AddAsync(entity);
        return new PersonalityMessage
        {
            Id = created.Id,
            FirstName = created.FirstName,
            LastName = created.LastName,
            Bio = created.Bio,
            Category = created.Category,
            Nationality = created.Nationality,
            BirthDate = created.BirthDate.ToString("yyyy-MM-dd"),
            DeathDate = created.DeathDate?.ToString("yyyy-MM-dd") ?? string.Empty,
            ImageUrl = created.ImageUrl ?? string.Empty
        };
    }

    public override async Task<DeletePersonalityResponse> DeletePersonality(DeletePersonalityRequest request, ServerCallContext context)
    {
        var success = await service.DeleteAsync(request.Id);
        if (!success)
            throw new RpcException(new Status(StatusCode.NotFound, $"Personnalité avec l'ID {request.Id} introuvable."));
        return new DeletePersonalityResponse { Success = true };
    }
}
