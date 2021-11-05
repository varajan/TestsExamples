# TestsExamples

Main branch contains only the WebSite application. Its functionality pretty simple: 
- Login screen
  - Login field
  - Password field
  - Login button 
  - Remind Password button (view is opened in an iFrame)
  - Register link in a corner -> redirects to Register page
- Deposit screen
  - Input fields:
    - Amount
    - Percent
    - Term
    - Financial year (two radio buttons: 360 and 365)
    - Start Date (three seperate dropdowns for day, month and year)
  - Calculate button (disabled till all mandatory fields are populated or during calculation)
  - Output fields (all read-only):
    - Interest
    - Income
    - End Date
  - Settings link
  - History link 
- Settings screen:
  - Date format (drop-down with four options)
  - Number format (four options)
  - Currency (three ones)
  - Save/Cancel buttons
  - Logout link
- History screen:
  - Hitory table (last 9 calculations)
  - Clear button
  - Go back to Calculator link

# How to run
- Install Visual Studio with _ASP.NET_ and _web development_ modules
- Open the _WebSite.sln_, build, run
- If you don't see the Login page, but some error open NuGet Package console and run:
```
  Update-Package Microsoft.CodeDom.Providers.DotNetCompilerPlatform -r
```
- Run project again. 
