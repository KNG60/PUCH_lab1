# Raport zmian — HelloWorldApp (PUCH_lab1)

Data: 2025-10-15

Krótki opis:

- Zmieniłem aplikację konsolową na prosty serwer minimal API oparty na ASP.NET Core (pliki w `HelloWorldApp/Program.cs`).
- Dodałem endpointy REST dla zasobu `items` oraz prostą stronę kontaktową `/contact` i API `/api/contacts` przechowujące zgłoszenia w pamięci.
- Dodałem minimalne typy w `HelloWorldApp/Models/OrderModels.cs` aby istniejące serwisy się kompilowały.
- Zaktualizowałem projekt (`HelloWorldApp/HelloWorldApp.csproj`) aby używał `Microsoft.NET.Sdk.Web`.
- Dodałem `HelloWorldApp/README.md` z instrukcjami uruchomienia i przykładowymi zapytaniami (PL).

Buduj i wyniki:

1) Budowa projektu:

```text
dotnet build HelloWorldApp/HelloWorldApp.csproj

Build succeeded. (HelloWorldApp/bin/Debug/net9.0/HelloWorldApp.dll)
```

2) Krótki smoke test (uruchomienie i przykładowe żądania):

- Uruchomiłem serwer w tle i wykonałem zapytania:
  - GET /  → 200 OK, JSON: {"message":"Hello from HelloWorldApp API"}
  - GET /api/items → 200 OK, zwrócono listę dwóch przykładowych elementów
  - POST /contact (JSON) → 201 Created, zwrócono obiekt ContactSubmission
  - GET /api/contacts → 200 OK, lista zgłoszeń (zawiera testowe wpisy)

Fragmenty odpowiedzi z testu:

```
GET / => {"message":"Hello from HelloWorldApp API"}

GET /api/items => [{"id":1,"name":"Item 1","description":"First item"},{"id":2,"name":"Item 2","description":"Second item"}]

POST /contact => {"id":2,"name":"Smoke","email":"smoke@example.com","message":"Hi","receivedAt":"2025-10-15T11:24:40.7094839Z"}

GET /api/contacts => [..., {"id":2,"name":"Smoke","email":"smoke@example.com","message":"Hi","receivedAt":"2025-10-15T11:24:40.7094839Z"}]
```

Instrukcja uruchomienia (skrót):

```bash
dotnet run --project HelloWorldApp/HelloWorldApp.csproj
# lub
dotnet ./HelloWorldApp/bin/Debug/net9.0/HelloWorldApp.dll
```

Uwagi i dalsze kroki:

- Dane kontaktowe są przechowywane tylko w pamięci — zaproponowałem dodanie EF Core + SQLite jeśli wymagana jest trwałość.
- Swagger nie został włączony — mogę dodać `Swashbuckle.AspNetCore` i włączyć Swagger UI, jeśli chcesz.
- Możemy dodać testy integracyjne (xUnit) oraz pipeline CI (GitHub Actions) dla automatycznej weryfikacji.

---

Jeśli chcesz, utworzę Pull Request z tym raportem teraz.
