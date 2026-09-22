# Waracle Store Test Automation

Automated acceptance tests for the Waracle Store, built using:

- C#
- .NET 8
- Microsoft Playwright
- Reqnroll
- NUnit

## Prerequisites

Install the following:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 or later, or another compatible IDE
- PowerShell

The Waracle Store application must also be running before executing the tests.

## Configuration

The application URL is stored in appsettings.json, for example:
```json
{
  "baseUrl": "http://localhost:5173/",
}
```

## Setup
in powershell, run: 

dotnet restore
dotnet build

Install the required Playwright browser: powershell -ExecutionPolicy Bypass -File .\bin\Debug\net8.0\playwright.ps1 install chromium

## Framework approach

The solution uses:

Reqnroll feature files for business-readable scenarios.
Page objects to separate page interactions from step definitions.
Playwright role- and text-based locators where possible.
Reqnroll dependency injection to share the Playwright page safely within each scenario.
Playwright assertions and automatic waiting instead of fixed delays.
Table-driven product data to support different items and quantities.

## Release assessment
The main customer journey is functional. Users can sign in, add products to the basket and apply a coupon.
However, the discount calculation does not appear to satisfy the acceptance criteria. 
For a subtotal of £24.99, the application displays a discount of -£0.25. 
A 25% discount should be around £6.25.