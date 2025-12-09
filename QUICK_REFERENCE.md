# RealtyMind API - Quick Reference Card

## ?? Quick Start
```bash
1. Import Postman_Collection.json into Postman
2. Set base_url = https://localhost:7073
3. Run API: dotnet run
4. Execute requests from Postman
```

## ?? All Endpoints (19 Total)

### Health & Ping (2)
```
? GET  /health
? GET  /ping
```

### Auth (2)
```
? POST /api/auth/register          ? Registers user, returns JWT
? POST /api/auth/login             ? Logs in, returns JWT
```

### Geo Services (2)
```
? GET  /api/geo/geocode?address=...        ? Get coordinates
? GET  /api/geo/reverse?lat=X&lng=Y        ? Get address from coords
```

### Neighborhood (4)
```
? GET  /api/neighborhood/pois?lat=X&lng=Y&radius=Z     ? Get POIs
? GET  /api/neighborhood/score?lat=X&lng=Y             ? Get score
? GET  /api/neighborhood/pois?lat=40.7128&lng=-74.0060 ? NYC POIs
? GET  /api/neighborhood/score?lat=40.7128&lng=-74.0060 ? NYC score
```

### Property Provider (2)
```
? GET  /api/property/provider/zillow?address=...   ? Get property details
? GET  /api/property/provider/zillow?address=...   ? Get property (Brooklyn)
```

### Finance (4)
```
? GET  /api/finance/rates?region=IN                ? Get rates
? POST /api/finance/mortgage/calc                  ? 30-year mortgage
? POST /api/finance/mortgage/calc                  ? 20-year mortgage
? POST /api/finance/mortgage/calc                  ? Low-rate mortgage
```

---

## ?? Mock Data Summary

### Register/Login
```json
{
  "email": "buyer@realtymind.com",
  "password": "SecurePass123!"
}
```

### Neighborhood Analysis Coordinates
| Location | Latitude | Longitude |
|----------|----------|-----------|
| Google Campus, CA | 37.4224764 | -122.0842499 |
| New York City | 40.7128 | -74.0060 |
| San Francisco | 37.7749 | -122.4194 |
| Los Angeles | 34.0522 | -118.2437 |
| Chicago | 41.8781 | -87.6298 |

### Mortgage Examples
| Scenario | Principal | Rate | Term | Extra Payment |
|----------|-----------|------|------|----------------|
| 30-year | $300,000 | 6.5% | 360 months | $0 |
| 20-year | $500,000 | 5.8% | 240 months | $100 |
| Low-rate | $250,000 | 4.5% | 180 months | $500 |

### Addresses
| Type | Address |
|------|---------|
| Google HQ | 1600 Amphitheatre Parkway, Mountain View, CA |
| Brooklyn | 123 Main Street, Brooklyn, NY 11201 |

---

## ?? Authentication

After running `/api/auth/register` or `/api/auth/login`:
- JWT token is automatically saved to `auth_token` variable
- Use in Authorization header: `Bearer {{auth_token}}`

---

## ?? Response Examples

### Neighborhood Score Response
```json
{
  "score": 82.5,
  "breakdownJson": "{\"transit\":85,\"schools\":90,\"health\":75,\"grocery\":80,\"park\":88,\"crime\":50}",
  "calculatedAt": "2024-01-15T10:35:22.456Z"
}
```

### Mortgage Calculation Response
```json
{
  "monthlyPayment": 1896.2,
  "totalPayment": 682632.0,
  "totalInterest": 382632.0,
  "amortizationSchedule": [
    {
      "month": 1,
      "payment": 1896.2,
      "principal": 546.2,
      "interest": 1350,
      "balance": 299453.8
    }
  ]
}
```

### POIs Response
```json
[
  {
    "externalId": "node/123456",
    "provider": "osm",
    "name": "Lincoln High School",
    "category": "school",
    "lat": 37.4225,
    "lng": -122.0843,
    "address": ""
  }
]
```

---

## ? Test Workflow

```
1. Health Check (/health)
   ? (Verify API is running)
   
2. Register (/api/auth/register)
   ? (Get JWT token)
   
3. Geocode (/api/geo/geocode)
   ? (Convert address to coords)
   
4. Get POIs (/api/neighborhood/pois)
   ? (Fetch nearby points of interest)
   
5. Score Neighborhood (/api/neighborhood/score)
   ? (Calculate area quality score)
   
6. Property Details (/api/property/provider/zillow)
   ? (Get market data)
   
7. Calculate Mortgage (/api/finance/mortgage/calc)
   ? (Compute loan payments)
   
8. Export Results as JSON Report
```

---

## ?? Endpoint Categories

| Category | Count | Auth | Purpose |
|----------|-------|------|---------|
| Health | 2 | ? | API status checks |
| Auth | 2 | ? | User authentication |
| Geo | 2 | ? | Geocoding services |
| Neighborhood | 4 | ? Optional | Area analysis |
| Property | 2 | ? Optional | Property data |
| Finance | 4 | ? | Mortgage tools |
| **Total** | **19** | - | - |

---

## ??? Environment Variables

```
base_url = https://localhost:7073
auth_token = (auto-populated after login)
```

---

## ?? Export Results

In Postman:
1. Run all requests in collection
2. Click **?** (More actions) ? **Run collection**
3. Monitor results in **Collection Runner**
4. Export results as JSON from Summary tab

---

## ? Common Issues & Fixes

| Issue | Fix |
|-------|-----|
| SSL error | Settings ? Disable SSL certificate verification |
| Connection refused | Check `base_url` port matches `dotnet run` |
| Invalid token | Re-run login request |
| Rate limit | Wait 1-2 minutes before retrying |
| Missing API key | Check `appsettings.json` for Google Maps key |

---

## ?? Full Documentation

See **POSTMAN_GUIDE.md** for complete setup and usage instructions.

---

**Created:** January 2024  
**RealtyMind API v1.0** | .NET 10 Backend
