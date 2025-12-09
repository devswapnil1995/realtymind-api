# ?? RealtyMind API - Postman Testing Package

## ? Complete Package Contents

You now have a complete API testing package with:

### Files Created:
1. **Postman_Collection.json** - Full API collection with 19 pre-configured requests
2. **POSTMAN_GUIDE.md** - Comprehensive setup & usage guide (detailed)
3. **QUICK_REFERENCE.md** - Quick reference card with examples
4. **TEST_REPORT_TEMPLATE.json** - Template for test reports
5. **TESTING_SUMMARY.md** - This file

---

## ?? What's Included

### ? 19 API Requests Across 6 Categories

#### 1?? **Health & Ping** (2 endpoints)
- `GET /health` - Health check
- `GET /ping` - Connectivity test

#### 2?? **Authentication** (2 endpoints)
- `POST /api/auth/register` - Create account
- `POST /api/auth/login` - Login & get JWT

#### 3?? **Geo Services** (2 endpoints)
- `GET /api/geo/geocode` - Address to coordinates
- `GET /api/geo/reverse` - Coordinates to address

#### 4?? **Neighborhood Analysis** (4 endpoints)
- `GET /api/neighborhood/pois` - Get POIs (Google Campus)
- `GET /api/neighborhood/score` - Score area (Google Campus)
- `GET /api/neighborhood/pois` - Get POIs (NYC)
- `GET /api/neighborhood/score` - Score area (NYC)

#### 5?? **Property Provider** (2 endpoints)
- `GET /api/property/provider/zillow` - Property details (Mountain View)
- `GET /api/property/provider/zillow` - Property details (Brooklyn)

#### 6?? **Finance & Mortgages** (4 endpoints)
- `GET /api/finance/rates` - Get current rates
- `POST /api/finance/mortgage/calc` - 30-year mortgage
- `POST /api/finance/mortgage/calc` - 20-year mortgage
- `POST /api/finance/mortgage/calc` - Low-rate scenario

---

## ?? Mock Data Included

### User Authentication
```json
Email: buyer@realtymind.com
Password: SecurePass123!
```

### Test Locations
| Location | Lat | Lng |
|----------|-----|-----|
| Google Campus, CA | 37.4224764 | -122.0842499 |
| New York City | 40.7128 | -74.0060 |
| San Francisco | 37.7749 | -122.4194 |
| Los Angeles | 34.0522 | -118.2437 |
| Chicago | 41.8781 | -87.6298 |

### Loan Scenarios
1. **30-Year Mortgage**: $300K @ 6.5%
2. **20-Year Mortgage**: $500K @ 5.8% + $100 extra
3. **Low-Rate Loan**: $250K @ 4.5% + $500 extra

### Property Addresses
1. Google HQ: 1600 Amphitheatre Parkway, Mountain View, CA 94043
2. Brooklyn: 123 Main Street, Brooklyn, NY 11201

---

## ?? Quick Start (3 Steps)

### Step 1: Import Collection
```
Postman ? Import ? Select Postman_Collection.json
```

### Step 2: Configure Environment
```
Environment Variables:
- base_url = https://localhost:7073
- auth_token = (auto-populated)
```

### Step 3: Run Your API
```bash
cd RealtyMind.Api
dotnet run
# API runs at https://localhost:7073
```

---

## ?? Test Execution Flow

### Recommended Execution Order:

```
???????????????????????????????????????????????
? 1. GET /health                              ?
?    Purpose: Verify API is responding        ?
?    Expected: 200 OK with status             ?
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
? 2. POST /api/auth/register                  ?
?    Purpose: Create test user                ?
?    Expected: 200 OK with JWT token          ?
?    Auto-saves: Token to auth_token var      ?
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
? 3. GET /api/geo/geocode?address=...         ?
?    Purpose: Convert address to coordinates  ?
?    Expected: 200 OK with lat/lng            ?
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
? 4. GET /api/neighborhood/pois?lat=X&lng=Y   ?
?    Purpose: Get nearby Points of Interest   ?
?    Expected: 200 OK with POI array          ?
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
? 5. GET /api/neighborhood/score?lat=X&lng=Y  ?
?    Purpose: Calculate neighborhood score    ?
?    Expected: 200 OK with 0-100 score        ?
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
? 6. GET /api/property/provider/zillow?addr=..?
?    Purpose: Get property market data        ?
?    Expected: 200 OK with property details   ?
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
? 7. GET /api/finance/rates?region=IN         ?
?    Purpose: Get current mortgage rates      ?
?    Expected: 200 OK with rate percentage    ?
???????????????????????????????????????????????
                    ?
???????????????????????????????????????????????
? 8. POST /api/finance/mortgage/calc          ?
?    Purpose: Calculate mortgage payment      ?
?    Expected: 200 OK with payment schedule   ?
???????????????????????????????????????????????
```

---

## ?? Expected Test Results Summary

### Success Metrics
| Metric | Expected |
|--------|----------|
| Total Requests | 19 |
| Success Rate | 100%* |
| Avg Response Time | <500ms |
| Error Rate | 0%* |

*Assuming API & all external dependencies are properly configured

### Response Time Benchmarks
| Endpoint | Expected Time |
|----------|---------------|
| Health/Ping | <10ms |
| Auth (Register/Login) | 100-200ms |
| Geo (Geocode) | 200-500ms |
| Neighborhood (POIs) | 500-2000ms** |
| Neighborhood (Score) | 1000-3000ms** |
| Property (Zillow) | 500-2000ms |
| Finance (Rates) | 100-200ms |
| Finance (Mortgage Calc) | <100ms |

**Overpass API latency varies based on data size

---

## ?? What Each Test Validates

### ? Health Checks
- API is running and responsive
- Basic connectivity works
- JSON parsing functions

### ? Authentication
- User registration creates database record
- Password hashing works securely
- JWT token generation works
- Login validates credentials
- Token can be used for protected endpoints

### ? Geo Services
- Google Maps API integration works
- Geocoding returns valid coordinates
- Reverse geocoding returns valid addresses
- API key is properly configured

### ? Neighborhood Analysis
- Overpass API integration works
- POI queries return data
- Neighborhood scoring algorithm works
- Results save to database
- Caching works for repeated queries

### ? Property Provider
- Zillow API (via RapidAPI) integration works
- Property data retrieval works
- Rate limiting handled gracefully

### ? Finance
- Mortgage rate service works
- Loan calculations are accurate
- Amortization schedules generate correctly
- Complex financial math functions properly

---

## ?? How to Generate Test Report

### Option 1: Postman Collection Runner
```
1. Click "Run" (or use Collection Runner)
2. Select all requests
3. Let run to completion
4. View Summary tab
5. Export results as JSON
```

### Option 2: Newman (CLI)
```bash
npm install -g newman

newman run Postman_Collection.json \
  --environment your-environment.json \
  --reporters cli,json \
  --reporter-json-export results.json
```

### Option 3: Manual Testing
```
1. Run each request one by one
2. Document response status
3. Check response data
4. Verify expected results
5. Export as TEST_REPORT_TEMPLATE.json
```

---

## ?? Customization Guide

### Add New Test Location
Edit any Neighborhood request:
```json
Parameters:
{
  "lat": "YOUR_LATITUDE",
  "lng": "YOUR_LONGITUDE",
  "radius": 2000
}
```

### Change Base URL
```
Environment ? base_url = https://localhost:YOUR_PORT
```

### Modify Mortgage Scenarios
Edit Finance request body:
```json
{
  "principal": YOUR_LOAN_AMOUNT,
  "annualRatePercent": YOUR_RATE,
  "termMonths": YOUR_TERM,
  "paymentsPerYear": 12,
  "extraMonthlyPayment": YOUR_EXTRA
}
```

### Add Custom Headers
1. Open request
2. Headers tab
3. Add custom header (e.g., `X-Custom-Header: value`)

---

## ?? Troubleshooting Checklist

- [ ] API is running (`dotnet run` from RealtyMind.Api folder)
- [ ] Base URL matches your port (default: 7073)
- [ ] Google Maps API key is valid in appsettings.json
- [ ] RapidAPI key is valid for Zillow
- [ ] PostgreSQL database is running
- [ ] Database migrations have run
- [ ] SSL certificate verification is disabled in Postman (if using localhost)
- [ ] Network connectivity is good (check internet for external APIs)

---

## ?? Documentation Files

| File | Purpose |
|------|---------|
| **Postman_Collection.json** | Import into Postman - all 19 requests |
| **POSTMAN_GUIDE.md** | Complete setup & detailed documentation |
| **QUICK_REFERENCE.md** | Quick reference card |
| **TEST_REPORT_TEMPLATE.json** | Template for documenting results |
| **TESTING_SUMMARY.md** | This file |

---

## ?? Pro Tips

1. **Auto-Token Handling**: After login/register, token is automatically saved - no manual copy-paste needed

2. **Request Organization**: Requests are grouped by feature in the collection - easy to navigate

3. **Mock Data Built-In**: All requests have realistic mock data - no setup needed

4. **Multiple Test Cases**: Finance, Neighborhood, and Geo have multiple scenarios for comprehensive testing

5. **Response Inspection**: Check Postman's "Pretty" view for formatted JSON responses

6. **Performance Tracking**: Postman automatically tracks response times for each request

7. **Database Persistence**: Results are saved to your PostgreSQL database for audit trail

---

## ?? Learning Resources

### For API Testing:
- [Postman Learning Center](https://learning.postman.com/)
- [REST API Best Practices](https://restfulapi.net/)
- [HTTP Status Codes](https://developer.mozilla.org/en-US/docs/Web/HTTP/Status)

### For Your APIs:
- [Google Maps API](https://developers.google.com/maps)
- [OpenStreetMap Overpass API](https://wiki.openstreetmap.org/wiki/Overpass_API)
- [Zillow API](https://rapidapi.com/s/zillow)
- [JWT Authentication](https://jwt.io/)

### For .NET:
- [ASP.NET Core Docs](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [RESTful API Design](https://learn.microsoft.com/en-us/azure/architecture/best-practices/api-design)

---

## ?? Next Steps

### 1. Import & Configure
- [ ] Import Postman_Collection.json
- [ ] Set base_url in environment
- [ ] Start your API

### 2. Run Tests
- [ ] Execute Health/Ping endpoints
- [ ] Register test user
- [ ] Run all 19 requests

### 3. Verify Results
- [ ] Check response codes (should be 200/201)
- [ ] Inspect response data
- [ ] Verify database inserts

### 4. Generate Report
- [ ] Export collection run results
- [ ] Fill TEST_REPORT_TEMPLATE.json
- [ ] Share results with team

---

## ?? Sample Test Report Output

```json
{
  "date": "2024-01-15T14:30:00Z",
  "totalRequests": 19,
  "successful": 19,
  "failed": 0,
  "successRate": "100%",
  "avgResponseTime": "425ms",
  "endpoints": {
    "health": { "status": "pass", "time": "12ms" },
    "auth_register": { "status": "pass", "time": "125ms" },
    "geo_geocode": { "status": "pass", "time": "350ms" },
    "neighborhood_pois": { "status": "pass", "time": "1200ms" },
    "neighborhood_score": { "status": "pass", "time": "2100ms" },
    "property_zillow": { "status": "pass", "time": "800ms" },
    "finance_rates": { "status": "pass", "time": "150ms" },
    "finance_mortgage": { "status": "pass", "time": "50ms" }
  }
}
```

---

## ?? Summary

You now have everything needed to:
? Test all 19 RealtyMind API endpoints  
? Validate authentication flow  
? Check external API integrations  
? Verify business logic (scoring, calculations)  
? Generate comprehensive test reports  
? Document API behavior  
? Share results with stakeholders  

**Happy Testing! ??**

---

*Package Created: January 2024*  
*For: RealtyMind API v1.0*  
*Built with: .NET 10 & PostgreSQL*  
*GitHub: [devswapnil1995/realtymind-api](https://github.com/devswapnil1995/realtymind-api)*
