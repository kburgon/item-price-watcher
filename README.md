# Item Price Watcher

## How to run:
1. Set the following environment variables:
	- `SMTP_UNAME`: The username email address of the email account used for sending notifications
	- `STMP_PASS`: The password of the email account used for sending notifications
	- `CONN_STRING`: The connection string of the MySQL or MariaDB database that is used
2. Navigate to the ItemPriceWatcher directory
3. Run `dotnet build`
4. run `dotnet run ItemPriceWatcher.dll`

If using Visual Studio/Visual Studio Code, the following entry to the launch.json file can be used:
```json
{
    "name": ".NET Core Launch (console)",
    "type": "coreclr",
    "request": "launch",
    "preLaunchTask": "build",
    // If you have changed target frameworks, make sure to update the program path.
    "program": "${workspaceFolder}/ItemPriceWatcher/bin/Debug/netcoreapp3.0/ItemPriceWatcher.dll",
    "args": [],
    "cwd": "${workspaceFolder}/ItemPriceWatcher",
    // For more information about the 'console' field, see https://aka.ms/VSCode-CS-LaunchJson-Console
    "console": "internalConsole",
    "stopAtEntry": false,
    "env": {
        "SMTP_UNAME": "", // Notification email address goes here
        "SMTP_PASS": "", // Notification email password goes here
        "CONN_STRING": "" // MySQL/MariaDB connection string goes here
    }
}
```

