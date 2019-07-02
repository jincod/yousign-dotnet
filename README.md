# YouSign .NET Client

## Usage

```
services
  .AddHttpClient<IYouSignService, YouSignService>(client =>
  {
    client.BaseAddress = new Uri(appSettings.YouSignSettings.Url);
    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {appSettings.YouSignSettings.ApiKey}");
  });
```
