# test-azure-redis-connection

Console app to test the connection to an Azure Redis

## Steps

> Clone repository

```powershell
git clone https://github.com/rbleattler/test-azure-redis-connection.git; Set-Location .\test-azure-redis-connection
```

## How to Use This Tool

Run the tool with the connection details. All examples are in PowerShell. For PowerShell versions < 5.1 precede the exe call with a period. *(example: `. cachedebugger`)*

> Using a secure string as the Primary Key input with the default port (6380)

```powershell
cachedebugger.exe my-redis-resourcename $(Read-Host -AsSecureString | ConvertFrom-SecureString -AsPlainText)
```

> Use a custom port

```powershell
cachedebugger.exe my-redis-resourcename YOURREDISPRIMARYKEY 6381
```

> Use a custom endpoint

```powershell
cachedebugger.exe my-redis-resourcename YOURREDISPRIMARYKEY some.other.redis.endpoint
```

> Use a custom port and disable SSL (enabled by default)

```powershell
cachedebugger.exe my-redis-resourcename YOURREDISPRIMARYKEY 6399 false
```

## TODO

- [ ] Update to support secure strings

## License

MIT - See [LICENSE](LICENSE) for more information.
