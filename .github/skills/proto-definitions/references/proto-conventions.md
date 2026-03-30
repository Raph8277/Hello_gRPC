# Proto Conventions

## personality.proto

```protobuf
syntax = "proto3";

option csharp_namespace = "HelloGrpc.Shared";

package personality;

// Service gRPC pour la gestion des personnalités
service PersonalityGrpc {
  rpc GetPersonalities (GetPersonalitiesRequest) returns (GetPersonalitiesResponse);
  rpc GetPersonality (GetPersonalityRequest) returns (PersonalityMessage);
  rpc CreatePersonality (CreatePersonalityRequest) returns (PersonalityMessage);
  rpc UpdatePersonality (UpdatePersonalityRequest) returns (PersonalityMessage);
  rpc DeletePersonality (DeletePersonalityRequest) returns (DeletePersonalityResponse);
}

// Représente une personnalité
message PersonalityMessage {
  int32 id = 1;
  string first_name = 2;
  string last_name = 3;
  string bio = 4;
  string category = 5;
  string nationality = 6;
  string birth_date = 7;       // Format: yyyy-MM-dd
  string death_date = 8;       // Format: yyyy-MM-dd, vide si vivant
  string image_url = 9;
}

// Requêtes
message GetPersonalitiesRequest {
  string search_term = 1;
  string category = 2;
  int32 skip = 3;
  int32 take = 4;
}

message GetPersonalitiesResponse {
  repeated PersonalityMessage personalities = 1;
  int32 total_count = 2;
}

message GetPersonalityRequest {
  int32 id = 1;
}

message CreatePersonalityRequest {
  string first_name = 1;
  string last_name = 2;
  string bio = 3;
  string category = 4;
  string nationality = 5;
  string birth_date = 6;
  string death_date = 7;
  string image_url = 8;
}

message UpdatePersonalityRequest {
  int32 id = 1;
  string first_name = 2;
  string last_name = 3;
  string bio = 4;
  string category = 5;
  string nationality = 6;
  string birth_date = 7;
  string death_date = 8;
  string image_url = 9;
}

message DeletePersonalityRequest {
  int32 id = 1;
}

message DeletePersonalityResponse {
  bool success = 1;
}
```

## .csproj for Shared Project

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <TargetFramework>net10.0</TargetFramework>
    <RootNamespace>HelloGrpc.Shared</RootNamespace>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="Grpc.Tools" Version="2.*" PrivateAssets="all" />
    <PackageReference Include="Google.Protobuf" Version="3.*" />
    <PackageReference Include="Grpc.Net.Client" Version="2.*" />
  </ItemGroup>

  <ItemGroup>
    <Protobuf Include="Protos\personality.proto" GrpcServices="Both" />
  </ItemGroup>
</Project>
```

## Conventions

- **Package**: Utiliser le nom de domaine (`personality`)
- **Namespace C#**: `HelloGrpc.Shared`
- **Nommage**: snake_case pour les champs proto (converti automatiquement en PascalCase en C#)
- **GrpcServices**: `Both` dans Shared pour générer client ET serveur
- **Dates**: Format string `yyyy-MM-dd` (protobuf n'a pas de type date natif)
- **Optionnel**: Utiliser des strings vides pour les valeurs optionnelles (pas de `optional` proto3)
- **Service naming**: `{Entity}Grpc` pour le nom du service
