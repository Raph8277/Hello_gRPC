---
name: proto-definitions
description: "Use when creating or modifying protobuf .proto files, gRPC service contracts, message definitions, RPC methods. Use for adding new fields, new RPC operations, or changing the gRPC API contract."
argument-hint: "Describe the proto change (new message, new RPC method, field change)"
---

# Proto Definitions Skill

## When to Use
- Creating or modifying `.proto` files
- Adding new gRPC service methods
- Adding or changing message fields
- Defining request/response contracts

## Architecture

```
Hello_gRPC.Shared/
├── Protos/
│   └── personality.proto        # Main proto file
├── Hello_gRPC.Shared.csproj
└── GlobalUsings.cs
```

## Procedure

### 1. Define Proto
Follow the conventions in [Proto conventions reference](./references/proto-conventions.md).

### 2. Build Shared Project
```bash
dotnet build src/Hello_gRPC.Shared/
```
This generates C# classes from the `.proto` files automatically.

### 3. Update Dependent Projects
After proto changes, update:
- **Backend**: gRPC service implementation (`override` methods)
- **Frontend**: gRPC client wrapper calls
