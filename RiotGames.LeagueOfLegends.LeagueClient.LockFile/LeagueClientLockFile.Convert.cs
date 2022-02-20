using System.Diagnostics;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

// ReSharper disable UnusedMember.Global

namespace RiotGames.LeagueOfLegends.LeagueClient;

public readonly partial struct LeagueClientLockFile
{
    public Process GetProcess() => Process.GetProcessById(ProcessId);

    public Uri ToUri() => new($"{this.Protocol}://riot:{Password}@127.0.0.1:{Port}");

    public UriBuilder ToUriBuilder() => new(Protocol, "127.0.0.1", Port) { UserName = "riot", Password = Password};

    public NetworkCredential ToNetworkCredential() => new("riot", Password);

    public AuthenticationHeaderValue ToAuthenticationHeaderValue() => new("Basic",
        Convert.ToBase64String(Encoding.ASCII.GetBytes($"riot:{Password}")));
}