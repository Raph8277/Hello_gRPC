---
description: "Add a new gRPC method to an existing service: proto definition, backend implementation, frontend client wrapper."
agent: "blazor-grpc-fullstack"
argument-hint: "Service name, method name, and purpose (e.g., 'PersonalityGrpc.GetByCategory to filter by category')"
---

Add a new gRPC method to an existing service in the Hello_gRPC stack.

## Steps

1. **Proto**: Add the new RPC method, request message, and response message in the `.proto` file
2. **Backend**: Implement the `override` method in the gRPC service class
3. **Frontend**: Add the method to the gRPC client wrapper
4. **Build**: Run `dotnet build` to validate

Follow conventions:
- Validate inputs and throw `RpcException` with proper `StatusCode`
- Use manual mapping extensions
- Use async/await for all I/O
