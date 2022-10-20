# test-azure-redis-connection

Console app to test the connection to an Azure Redis

## Steps

>[!info] Clone repository
```powershell
git clone https://github.com/rbleattler/test-azure-redis-connection.git; Set-Location .\test-azure-redis-connection
```

### Build and execute the application

Run the tool with the connection details 
TODO: Update to support secure strings
```powershell
cachedebugger.exe my-redis-resourcename $(Read-Host -AsSecureString | ConvertFrom-SecureString -AsPlainText)

cachedebugger.exe my-redis-resourcename YOURREDISPRIMARYKEY
```


## License

MIT - See [LICENSE](LICENSE) for more information.
