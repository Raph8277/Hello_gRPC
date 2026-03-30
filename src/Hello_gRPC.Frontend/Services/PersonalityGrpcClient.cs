namespace HelloGrpc.Frontend.Services;

/// <summary>
/// Client gRPC pour la gestion des personnalités.
/// </summary>
public class PersonalityGrpcClient(PersonalityGrpc.PersonalityGrpcClient client)
{
    /// <summary>
    /// Récupère les personnalités avec pagination et filtrage.
    /// </summary>
    public async Task<GetPersonalitiesResponse> GetPersonalitiesAsync(
        string? searchTerm = null, string? category = null, int skip = 0, int take = 20)
    {
        var request = new GetPersonalitiesRequest
        {
            SearchTerm = searchTerm ?? "",
            Category = category ?? "",
            Skip = skip,
            Take = take
        };
        return await client.GetPersonalitiesAsync(request);
    }

    /// <summary>
    /// Récupère une personnalité par son identifiant.
    /// </summary>
    public async Task<PersonalityMessage> GetPersonalityAsync(int id)
    {
        return await client.GetPersonalityAsync(new GetPersonalityRequest { Id = id });
    }

    /// <summary>
    /// Crée une nouvelle personnalité.
    /// </summary>
    public async Task<PersonalityMessage> CreatePersonalityAsync(CreatePersonalityRequest request)
    {
        return await client.CreatePersonalityAsync(request);
    }

    /// <summary>
    /// Met à jour une personnalité existante.
    /// </summary>
    public async Task<PersonalityMessage> UpdatePersonalityAsync(UpdatePersonalityRequest request)
    {
        return await client.UpdatePersonalityAsync(request);
    }

    /// <summary>
    /// Supprime une personnalité.
    /// </summary>
    public async Task<DeletePersonalityResponse> DeletePersonalityAsync(int id)
    {
        return await client.DeletePersonalityAsync(new DeletePersonalityRequest { Id = id });
    }
}
