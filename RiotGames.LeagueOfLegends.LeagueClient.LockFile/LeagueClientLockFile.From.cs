using System.Diagnostics;

namespace RiotGames.LeagueOfLegends.LeagueClient;

/// <summary>
///     The lock file is used by the League Client to store connection values, like port and username.
/// </summary>
[DebuggerDisplay(
    "ProcessName = {ProcessName} ProcessId = {ProcessId} Port = {Port} Password = {Password} Protocol = {Protocol}")]
public readonly partial struct LeagueClientLockFile
{
    /// <summary>
    ///     Read the lock file from the specified path. It's preferable to use the <see cref="FromProcess(string)" /> method.
    /// </summary>
    /// <param name="path">The full path to the lock file.</param>
    /// <returns>The lock file.</returns>
    public static LeagueClientLockFile FromPath(string path = LEAGUE_CLIENT_DEFAULT_LOCKFILE_PATH)
    {
        try
        {
            var content = _fileReadAllText(path);
            var splitContent = content.Split(':');
            return new LeagueClientLockFile(path, splitContent[0], int.Parse(splitContent[1]),
                ushort.Parse(splitContent[2]), splitContent[3], splitContent[4]);
        }
        catch (Exception ex) when (ex is not LeagueClientLockFilePathException)
        {
            throw new LeagueClientLockFilePathException(
                "An error occurred trying the get the lock file from a path, see the inner exception.", path, ex);
        }
    }

    private static string _fileReadAllText(string path)
    {
        try
        {
            using var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        catch (Exception ex)
        {
            throw new LeagueClientLockFilePathException(
                "An error occurred trying to read the contents of the lock file, see the inner exception.", path, ex);
        }
    }

    /// <summary>
    ///     Finds the path of the LeagueClient process and checks its executables directory for the lockfile.
    /// </summary>
    /// <param name="processName">The name of the LeagueClient process.</param>
    /// <returns>The lock file.</returns>
    /// <exception cref="ArgumentException">Thrown if something went wrong with finding the League Client process.</exception>
    public static LeagueClientLockFile FromProcess(string processName = LEAGUE_CLIENT_DEFAULT_PROCESS_NAME)
    {
        try
        {
            using var process = Process.GetProcessesByName(processName).Single();

            if (process.MainModule == null)
                throw new LeagueClientLockFileProcessException(
                    "The League Client process doesn't have any main module.", processName);

            var processDirectory = System.IO.Path.GetDirectoryName(process.MainModule.FileName);

            if (processDirectory == null)
                throw new LeagueClientLockFileProcessException(
                    "Unable to get the directory name for the League Client process.", processName);

            return FromPath(System.IO.Path.Combine(processDirectory, LEAGUE_CLIENT_DEFAULT_LOCKFILE_NAME));
        }
        catch (Exception ex) when (ex is not LeagueClientLockFileProcessException)
        {
            throw new LeagueClientLockFileProcessException(
                "An error occurred trying to get the lock file using a LeagueClient process name, see the inner exception.",
                processName, ex);
        }
    }
}