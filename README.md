# League Client Lock File
Provides a struct for the League Client's lock file and methods for retrieving it using either a process name or a path.

[![.NET](https://github.com/mikaeldui/riotgames-leagueoflegends-leagueclient-lockfile-dotnet/actions/workflows/dotnet.yml/badge.svg)](https://github.com/mikaeldui/riotgames-leagueoflegends-leagueclient-lockfile-dotnet/actions/workflows/dotnet.yml)
[![CodeQL Analysis](https://github.com/mikaeldui/riotgames-leagueoflegends-leagueclient-lockfile-dotnet/actions/workflows/codeql-analysis.yml/badge.svg)](https://github.com/mikaeldui/riotgames-leagueoflegends-leagueclient-lockfile-dotnet/actions/workflows/codeql-analysis.yml)

You can install it using the following **.NET CLI** command:

    dotnet add package MikaelDui.RiotGames.LeagueOfLegends.LeagueClient.LockFile --version *

## Example
Getting the lock file for a running process

    LeagueClientLockFile lockFile = LeagueClientLockFile.FromProcess();
    Console.WriteLines("Process Name: " + lockfile.ProcessName);
    Console.WriteLine("Process ID: " + lockFile.ProcessId);
    Console.WriteLine("Port: " + lockFile.Port);
    Console.WriteLine("Password: " + lockFile.Password);
    Console.WriteLine("Protocol: " + lockFile.Protocol);
   
    // Prints something like
    // Process Name: LeagueClient
    // Process ID: 50956
    // Port: 57732
    // Password: ucxnwiXjvtP8hwwMNY8kGA
    // Protocol: https
