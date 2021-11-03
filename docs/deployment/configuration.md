# Configuration

## appsettings.json

- `ConnectionStrings` - MySQL/MariaDB connection strings for application execution and design time (in docker application can use docker dns record, while on host/in editor/ host address can be used)
- `Jwt`
  - `Key` - JWT signing secret (should be randomly generated)
  - `Issuer` - Issuer of the JWT tokens
  - `ExpirationInMinutes` - Token expiration setting in minutes
- `Livekit` - connecting to a livekit instance
  - `host` - http://host:port/
  - `key` - Access key to use
  - `secret` - Secret key for the `key`
  - `webhook` - The `key` and `secret` to be used for webhook validation (as it can be different)
- `Migration.OnStart` - when enabled migration will be executed on application startup
- `ClientUrl` - url of the Angular dev mode app (required only in dev, http://frontend:4200 or http://localhost:4200)
- `DisableRegistration` - Disable registration feature
- `ExternalAuthentication` - External authentication settings
  - `Google` - Google settings
    - `ClientId` - Google app client Id
    - `ClientSecret` - Google app client secret
    - `LoginDisabled` - Disable Google external login feature
    - `RedirectUri` - Login redirect Uri
  - `Microsoft` - Microsoft settings
    - `ClientId` - Microsoft Azure app client Id
    - `GraphUrl` - Microsoft Graph url
    - `LoginDisabled` - Disable Microsoft external login feature
    - `RedirectUri` - Login redirect Uri

## Livekit

Example livekit config file (for development):

```yaml
port: 7880
grpc_port: 7886
disable_twirp: true

log_level: info

rtc:
  tcp_port: 7881
  use_external_ip: false
  udp_port: 7882

room:
  max_participants: 10
  enable_remote_unmute: false

webhook:
  api_key: test
  urls:
    - http://meethut-backend:5000/livekit_webhook

keys:
  test: EXAMPLElgnfIrYsA8C8vZ0n53rqKCWsm
```

### Webhook

### GRPC / Ports
