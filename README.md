# Unit Conversion API

## Description
A lightweight RESTful API built with ASP.NET Core (.NET 8) that converts numerical values between different units of measurement including length, temperature, and weight through a single clean REST endpoint.

---

## Features
-  Length conversion (meter, kilometer, mile, foot, inch, yard, cm, mm)
-  Temperature conversion (celsius, fahrenheit, kelvin)
-  Weight conversion (kilogram, gram, pound, ounce, ton)
-  Case-insensitive unit names (`KG`, `kg`, `Kg` all work)
-  Common abbreviations supported (`km`, `ft`, `lbs`, `cm`)
-  Interactive Swagger UI for easy testing

---

## Project Structure
```
UnitConversionAPI/
├── Controllers/
│   └── ConversionController.cs   # Handles HTTP requests
├── Models/
│   └── ConversionRequest.cs      # Request input model
├── Services/
│   └── ConversionService.cs      # All conversion logic
├── Properties/
│   └── launchSettings.json       # Launch configuration
├── Program.cs                    # App setup and DI
├── appsettings.json              # App configuration
├── UnitConversionAPI.http        # Sample HTTP requests
└── README.md
```

---

## Prerequisites
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- Git

---

## Configuration
No extra configuration required. The app runs on:
```
http://localhost:5000
```
Swagger UI available at:
```
http://localhost:5000/swagger
```

---

## How to Run Locally

1. **Clone the repository**
```bash
git clone https://github.com/Preetamcse/UnitConversionAPI.git
```

2. **Open the solution**
```
Open UnitConversionAPI.sln in Visual Studio 2022
```

3. **Run the project**
```
Press Ctrl+F5
```

4. **Open Swagger in browser**
```
http://localhost:5000/swagger
```

---

## API Usage

**Endpoint:** `POST /api/conversion/convert`

**Request Body:**
```json
{
  "value": 100,
  "fromUnit": "celsius",
  "toUnit": "fahrenheit"
}
```

**Response:**
```json
{
  "value": 100,
  "fromUnit": "celsius",
  "toUnit": "fahrenheit",
  "result": 212
}
```

---

## Example Conversions

| Input                      | Result  |
|----------------------------|---------|
| 100 celsius → fahrenheit   | 212     |
| 1 mile → kilometer         | 1.609   |
| 1 kilogram → pound         | 2.204   |
| 0 celsius → kelvin         | 273.15  |
| 100 cm → meter             | 1       |
| 1 foot → meter             | 0.3048  |

---

## Design Decisions & Challenges

The API uses a single endpoint for all unit categories (length, temperature, weight). Length and weight conversions work by converting to a base unit first (meters/kilograms), then to the target unit. Temperature uses direct formulas since it needs offset math, not just multiplication — this was the main challenge as it could not follow the same pattern as other units.

The conversion logic lives in one file (`ConversionService.cs`) so adding new units in the future only requires changes in one place. Making unit names case-insensitive and supporting abbreviations like `km`, `ft`, `lbs` was a small challenge but improves usability greatly. Swagger is enabled by default so anyone can test the API instantly after running it locally.

---

