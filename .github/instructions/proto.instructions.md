---
description: "Use when editing .proto protobuf files, defining gRPC service contracts, messages, and RPC methods."
applyTo: "**/*.proto"
---

# Proto File Guidelines

## Syntax
- Always use `syntax = "proto3";`
- Set `option csharp_namespace = "HelloGrpc.Shared";`
- Use `package personality;` for the personality domain

## Naming
- Service: `{Entity}Grpc` (e.g., `PersonalityGrpc`)
- Messages: PascalCase with descriptive suffixes (`Request`, `Response`, `Message`)
- Fields: snake_case (auto-converted to PascalCase in C#)

## CRUD Pattern
- `Get{Entity}s` — list with pagination (skip/take) and filtering
- `Get{Entity}` — single by ID
- `Create{Entity}` — create, returns created entity
- `Update{Entity}` — update by ID, returns updated entity
- `Delete{Entity}` — delete by ID, returns success bool

## Types
- Use `string` for dates (format `yyyy-MM-dd`)
- Use `int32` for IDs and counts
- Use `repeated` for collections
- Use empty string for optional fields (not `optional` keyword)
