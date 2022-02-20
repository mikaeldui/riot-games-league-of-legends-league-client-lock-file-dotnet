using System.Diagnostics;

namespace RiotGames.LeagueOfLegends.LeagueClient;

/// <summary>
///     The lock file is used by the League Client to store connection values, like port and username.
/// </summary>
[DebuggerDisplay(
    "ProcessName = {ProcessName} ProcessId = {ProcessId} Port = {Port} Password = {Password} Protocol = {Protocol}")]
public readonly partial struct LeagueClientLockFile
{
    public const string LEAGUE_CLIENT_DEFAULT_LOCKFILE_NAME = "lockfile";
    public const string LEAGUE_CLIENT_DEFAULT_LOCKFILE_PATH = @"C:\Riot Games\League of Legends\lockfile";
    public const string LEAGUE_CLIENT_DEFAULT_LOCKFILE_DIRECTORY = @"C:\Riot Games\League of Legends";
    public const string LEAGUE_CLIENT_DEFAULT_PROCESS_NAME = "LeagueClient";

    private LeagueClientLockFile(string path, string processName, int processId, ushort port, string password, string protocol)
    {
        Path = path;
        ProcessName = processName;
        ProcessId = processId;
        Port = port;
        Password = password;
        Protocol = protocol;
    }

    internal string Path { get; }

    /// <summary>
    ///     The name of the League Client process, e.g. "LeagueClient".
    /// </summary>
    public string ProcessName { get; }

    /// <summary>
    ///     The process ID of the League Client, e.g. "50956".
    /// </summary>
    public int ProcessId { get; }

    /// <summary>
    ///     The port for communicating the League Client, e.g. "57732".
    /// </summary>
    public ushort Port { get; }

    /// <summary>
    ///     The password for communicating the League Client, e.g. "ucxnwiXjvtP8hwwMNY8kGA".
    /// </summary>
    public string Password { get; }

    /// <summary>
    ///     The protocol for communicating with the League Client, e.g. "https".
    /// </summary>
    public string Protocol { get; }

    /// <summary>
    ///     A string representation of the lock file in its original representation, e.g.
    ///     "LeagueClient:50956:57732:ucxnwiXjvtP8hwwMNY8kGA:https".
    /// </summary>
    /// <returns>The lock file in its original representation, e.g. "LeagueClient:50956:57732:ucxnwiXjvtP8hwwMNY8kGA:https".</returns>
    public override string ToString()
    {
        return $"{ProcessName}:{ProcessId}:{Port}:{Password}:{Protocol}";
    }
}