# HelloWorldApp

Prosty serwer minimal API napisany w .NET 9 — przykładowe endpointy, zarządzanie użytkownikami i strona kontaktowa.

## Spis treści
- [Uruchomienie](#uruchomienie)
- [Endpointy](#endpointy)
- [Modele danych](#modele-danych)
- [Przykładowe polecenia](#przykładowe-polecenia-curl)
- [Uwagi techniczne](#uwagi-techniczne)

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

### Podstawowe
- GET `/` — JSON z wiadomością powitalną
- GET `/api/hello` — prosty tekst powitalny
- GET `/api/spec` — informacje o środowisku uruchomieniowym (.NET, OS)

### Zarządzanie elementami (Items)
- GET `/api/items` — lista wszystkich elementów
- GET `/api/items/{id}` — szczegóły pojedynczego elementu
- POST `/api/items` — utworzenie nowego elementu
- PUT `/api/items/{id}` — aktualizacja elementu
- DELETE `/api/items/{id}` — usunięcie elementu

### Zarządzanie użytkownikami (Users)
- GET `/api/users` — lista wszystkich użytkowników
- GET `/api/users/search?username={query}` — wyszukiwanie użytkowników po nazwie
- GET `/api/users/{id}` — szczegóły pojedynczego użytkownika
- POST `/api/users` — utworzenie nowego użytkownika
- PUT `/api/users/{id}` — aktualizacja użytkownika
- DELETE `/api/users/{id}` — usunięcie użytkownika

### Formularz kontaktowy
- GET `/contact` — formularz kontaktowy (HTML)
- POST `/contact` — przyjmowanie zgłoszeń (obsługuje form-data i JSON)
- GET `/api/contacts` — lista otrzymanych zgłoszeń

## Modele danych

### Item
```json
{
    "id": 1,
    "name": "Nazwa elementu",
    "description": "Opis elementu"
}
```

### User
```json
{
    "id": 1,
    "username": "nazwa_użytkownika",
    "email": "user@example.com"
}
```

### ContactSubmission
```json
{
    "id": 1,
    "name": "Imię",
    "email": "kontakt@example.com",
    "message": "Treść wiadomości",
    "receivedAt": "2025-10-15T10:30:00Z"
}
```

## Przykładowe polecenia (curl)

### Items
```bash
# Lista elementów
curl http://localhost:5000/api/items

# Dodanie nowego elementu
curl -X POST http://localhost:5000/api/items \
  -H "Content-Type: application/json" \
  -d '{"name":"Nowy element","description":"Opis elementu"}'

# Aktualizacja elementu
curl -X PUT http://localhost:5000/api/items/1 \
  -H "Content-Type: application/json" \
  -d '{"name":"Zmieniona nazwa","description":"Nowy opis"}'
```

### Users
```bash
# Lista użytkowników
curl http://localhost:5000/api/users

# Wyszukiwanie użytkowników
curl http://localhost:5000/api/users/search?username=alice

# Dodanie użytkownika
curl -X POST http://localhost:5000/api/users \
  -H "Content-Type: application/json" \
  -d '{"username":"carol","email":"carol@example.com"}'
```

### Contact
```bash
# Wysłanie zgłoszenia (JSON)
curl -X POST http://localhost:5000/contact \
  -H "Content-Type: application/json" \
  -d '{"name":"Jan","email":"jan@example.com","message":"Cześć!"}'

# Lista zgłoszeń
curl http://localhost:5000/api/contacts
```

Formularz kontaktowy dostępny w przeglądarce: `http://localhost:5000/contact`

## Uwagi techniczne

- Architektura:
  - Minimal API w .NET 9
  - Pojedynczy projekt
  - Brak zewnętrznych zależności
  - Endpointy zdefiniowane w `Program.cs`

- Przechowywanie danych:
  - Wszystkie dane przechowywane w pamięci (in-memory)
  - Restart aplikacji czyści stan
  - Domyślne dane testowe dla Items i Users

- Walidacja:
  - Unikalne nazwy użytkowników (case-insensitive)
  - Podstawowa walidacja emaili i wymaganych pól
  - Odpowiednie kody HTTP dla błędów (400, 404, 409)

- Możliwe rozszerzenia:
  - Swagger UI (Swashbuckle)
  - Persystencja danych (baza danych)
  - Autoryzacja i autentykacja
  - Testy jednostkowe i integracyjne
