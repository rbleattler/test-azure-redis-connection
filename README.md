# test-azure-redis-connection

Console app to test the connection to an Azure Redis

## Steps

Clone repository
```bash
git clone https://github.com/rbleattler/test-azure-redis-connection.git && cd test-azure-redis-connection
```

~~Edit the connection parameters in the Program.cs file~~

```c#
//Update values
//const string OUR_NODE = "YOUR_NODE_HERE";
//const string PRIMARY_ACCESS_KEY = "YOUR_PRIMARY_KEY_HERE";
//const string REDIS_CONNECTION_STRING = "YOUR_CONNECTION_STRING_HERE";
```

### Build and execute the application

Run the tool with the connection details 
TODO: Update to support secure strings
```powershell
cachedebugger.exe my-redis-resourcename $(Read-Host -AsSecureString | ConvertFrom-SecureString -AsPlainText)
```


## License

MIT - See [LICENSE](LICENSE) for more information.
