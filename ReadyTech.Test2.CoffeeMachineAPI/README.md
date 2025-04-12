# Coffee Machine API

This project simulates an imaginary internet-connected coffee machine with HTTP endpoints. It is implemented in **.NET 6 Web API**, using clean architecture, dependency injection, and unit testing.

---

## Base Requirements

### Endpoint: `GET /brew-coffee`

Returns a hot coffee message and timestamp, with the following logic:

### Behavior

1. **Normal Call**

   - Returns: `200 OK`
   - Response:
     ```json
     {
       "message": "Your piping hot coffee is ready",
       "prepared": "2025-03-15T12:00:00+0100"
     }
     ```

2. **Every 5th Call**

   - Returns: `503 Service Unavailable`
   - Body: _(empty)_
   - Reason: Simulates "out of coffee"

3. **April 1st (Fools’ Day)**
   - Returns: `418 I'm a teapot`
   - Body: _(empty)_
   - Reason: Coffee machine is joking

---

### Getting Started

#### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download/dotnet/6.0)

#### Build and Run

1. **Restore and Build:**

   ```bash
   dotnet restore
   dotnet build
   dotnet run
   ```

---
