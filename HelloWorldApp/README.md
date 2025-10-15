# HelloWorldApp

Prosty serwer minimal API napisany w .NET 9 — przykładowe endpointy i strona kontaktowa.

## Uruchomienie

Z katalogu repo (root) uruchom:

```bash
dotnet run --project HelloWorldApp/HelloWorldApp.csproj
```

Lub z katalogu `HelloWorldApp`:

```bash
dotnet run
```

Możesz też uruchomić skompilowany plik:

```bash
dotnet ./bin/Debug/net9.0/HelloWorldApp.dll
```

Domyślnie aplikacja nasłuchuje na adresach wypisanych w konsoli (np. http://localhost:5000).

## Endpointy

- GET `/` — JSON z wiadomością powitalną
- GET `/api/hello` — prosty tekst powitalny
- GET `/api/items` — lista elementów (in-memory)
- GET `/api/items/{id}` — pojedynczy element
- POST `/api/items` — utworzenie elementu
- PUT `/api/items/{id}` — aktualizacja
- DELETE `/api/items/{id}` — usunięcie
- GET `/contact` — formularz kontaktowy (HTML)
- POST `/contact` — przyjmuje formularz (application/x-www-form-urlencoded) lub JSON
- GET `/api/contacts` — lista zgłoszeń kontaktowych (in-memory)
- GET `/login` — strona logowania z animacją skeleton loading (HTML)
- POST `/login` — obsługa logowania (sprawdza czy użytkownik istnieje)

## Przykładowe polecenia (curl)

Lista elementów:

```bash
curl http://localhost:5000/api/items
```

Wysłanie zgłoszenia kontaktowego (JSON):

```bash
curl -X POST http://localhost:5000/contact -H "Content-Type: application/json" -d '{"name":"Jan","email":"jan@example.com","message":"Cześć!"}'
```

Odwiedź formularz w przeglądarce: `http://localhost:5000/contact`

Testowanie logowania (użyj istniejącego użytkownika, np. "alice" lub "bob"):

Odwiedź stronę logowania w przeglądarce: `http://localhost:5000/login`

## Uwagi

- Dane są przechowywane w pamięci — restart aplikacji czyści je.
- Swagger nie jest włączony domyślnie. Jeśli chcesz, mogę dodać Swashbuckle i interfejs Swagger UI.
