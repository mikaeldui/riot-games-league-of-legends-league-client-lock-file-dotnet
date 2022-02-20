using System.Diagnostics;

namespace RiotGames.LeagueOfLegends.LeagueClient;

/// <summary>
///     The lock file is used by the League Client to store connection values, like port and username.
/// </summary>
[DebuggerDisplay(
    "ProcessName = {ProcessName} ProcessId = {ProcessId} Port = {Port} Password = {Password} Protocol = {Protocol}")]
public readonly partial struct LeagueClientLockFile
{

}