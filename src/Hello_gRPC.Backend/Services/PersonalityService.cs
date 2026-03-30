namespace HelloGrpc.Backend.Services;

/// <summary>
/// Service gRPC pour la gestion des personnalités.
/// </summary>
public class PersonalityService(AppDbContext context) : PersonalityGrpc.PersonalityGrpcBase
{
    /// <summary>
    /// Récupère toutes les personnalités avec pagination et filtrage.
    /// </summary>
    public override async Task<GetPersonalitiesResponse> GetPersonalities(
        GetPersonalitiesRequest request, ServerCallContext callContext)
    {
        var query = context.Personalities.AsQueryable();

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

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .Skip(request.Skip)
            .Take(request.Take > 0 ? request.Take : 20)
            .ToListAsync();

        var response = new GetPersonalitiesResponse { TotalCount = totalCount };
        response.Personalities.AddRange(items.Select(p => p.ToProto()));
        return response;
    }

    /// <summary>
    /// Récupère une personnalité par son identifiant.
    /// </summary>
    public override async Task<PersonalityMessage> GetPersonality(
        GetPersonalityRequest request, ServerCallContext callContext)
    {
        var entity = await context.Personalities.FindAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound,
                $"Personnalité avec l'ID {request.Id} introuvable."));

        return entity.ToProto();
    }

    /// <summary>
    /// Crée une nouvelle personnalité.
    /// </summary>
    public override async Task<PersonalityMessage> CreatePersonality(
        CreatePersonalityRequest request, ServerCallContext callContext)
    {
        ValidateRequest(request.FirstName, request.LastName, request.Bio, request.Category);

        var entity = request.ToEntity();
        context.Personalities.Add(entity);
        await context.SaveChangesAsync();
        return entity.ToProto();
    }

    /// <summary>
    /// Met à jour une personnalité existante.
    /// </summary>
    public override async Task<PersonalityMessage> UpdatePersonality(
        UpdatePersonalityRequest request, ServerCallContext callContext)
    {
        var entity = await context.Personalities.FindAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound,
                $"Personnalité avec l'ID {request.Id} introuvable."));

        ValidateRequest(request.FirstName, request.LastName, request.Bio, request.Category);

        entity.UpdateFrom(request);
        await context.SaveChangesAsync();
        return entity.ToProto();
    }

    /// <summary>
    /// Supprime une personnalité.
    /// </summary>
    public override async Task<DeletePersonalityResponse> DeletePersonality(
        DeletePersonalityRequest request, ServerCallContext callContext)
    {
        var entity = await context.Personalities.FindAsync(request.Id)
            ?? throw new RpcException(new Status(StatusCode.NotFound,
                $"Personnalité avec l'ID {request.Id} introuvable."));

        context.Personalities.Remove(entity);
        await context.SaveChangesAsync();
        return new DeletePersonalityResponse { Success = true };
    }

    private static void ValidateRequest(string firstName, string lastName, string bio, string category)
    {
        if (string.IsNullOrWhiteSpace(firstName))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Le prénom est requis."));
        if (string.IsNullOrWhiteSpace(lastName))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Le nom est requis."));
        if (string.IsNullOrWhiteSpace(bio))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "La biographie est requise."));
        if (string.IsNullOrWhiteSpace(category))
            throw new RpcException(new Status(StatusCode.InvalidArgument, "La catégorie est requise."));
    }
}
