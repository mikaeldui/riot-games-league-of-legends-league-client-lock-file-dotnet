using System.Diagnostics;

namespace RiotGames.LeagueOfLegends.LeagueClient
{
    /// <summary>
    /// The lock file is used by the League Client to store connection values, like port and username.
    /// </summary>
    [DebuggerDisplay("ProcessName = {ProcessName} ProcessId = {ProcessId} Port = {Port} Password = {Password} Protocol = {Protocol}")]
    public struct LeagueClientLockFile
    {
        public const string LEAGUECLIENT_DEFAULT_LOCKFILE_NAME = "lockfile";
        public const string LEAGUECLIENT_DEFAULT_LOCKFILE_PATH = @"C:\Riot Games\League of Legends\lockfile";
        public const string LEAGUECLIENT_DEFAULT_LOCKFILE_DIRECTORY = @"C:\Riot Games\League of Legends";
        public const string LEAGUECLIENT_DEFAULT_PROCESS_NAME = "LeagueClient";

        private LeagueClientLockFile(string processName, ulong processId, ushort port, string password, string protocol)
        {
            ProcessName = processName;
            ProcessId = processId;
            Port = port;
            Password = password;
            Protocol = protocol;
        }

        /// <summary>
        /// The name of the League Client process, e.g. "LeagueClient".
        /// </summary>
        public string ProcessName { get; }

        /// <summary>
        /// The process ID of the League Client, e.g. "50956".
        /// </summary>
        public ulong ProcessId { get; }

        /// <summary>
        /// The port for communicating the League Client, e.g. "57732".
        /// </summary>
        public ushort Port { get; }

        /// <summary>
        /// The password for communicating the League Client, e.g. "ucxnwiXjvtP8hwwMNY8kGA".
        /// </summary>
        public string Password { get; }

        /// <summary>
        /// The protocol for communicating with the League Client, e.g. "https".
        /// </summary>
        public string Protocol { get; }

        /// <summary>
        /// A string representation of the lock file in its original representation, e.g. "LeagueClient:50956:57732:ucxnwiXjvtP8hwwMNY8kGA:https".
        /// </summary>
        /// <returns>The lock file in its original representation, e.g. "LeagueClient:50956:57732:ucxnwiXjvtP8hwwMNY8kGA:https".</returns>
        public override string ToString() => $"{ProcessName}:{ProcessId}:{Port}:{Password}:{Protocol}";

        /// <summary>
        /// Read the lock file from the specified path. It's preferable to use the <see cref="FromProcess(string)"/> method.
        /// </summary>
        /// <param name="path">The full path to the lock file.</param>
        /// <returns>The lock file.</returns>
        public static LeagueClientLockFile FromPath(string path = LEAGUECLIENT_DEFAULT_LOCKFILE_PATH)
        {
            try
            {
                var content = _fileReadAllText(path);
                var splitContent = content.Split(':');
                return new LeagueClientLockFile(splitContent[0], UInt64.Parse(splitContent[1]), UInt16.Parse(splitContent[2]), splitContent[3], splitContent[4]);
            }
            catch (Exception ex) when (ex is not LeagueClientLockFilePathException)
            {
                throw new LeagueClientLockFilePathException("An error occured trying the get the lock file from a path, see the inner exception.", path, ex);
            }
        }

        private static string _fileReadAllText(string path)
        {
            try
            {
                string text;
                using (var stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }
                return text;
            }
            catch (Exception ex)
            {
                throw new LeagueClientLockFilePathException("An error occured trying to read the contents of the lock file, see the inner exception.", path, ex);
            }
        }

        /// <summary>
        /// Finds the path of the LeagueClient process and checks its executables directory for the lockfile.
        /// </summary>
        /// <param name="processName">The name of the LeagueClient process.</param>
        /// <returns>The lock file.</returns>
        /// <exception cref="ArgumentException">Thrown if something went wrong with finding the League Client process.</exception>
        public static LeagueClientLockFile FromProcess(string processName = LEAGUECLIENT_DEFAULT_PROCESS_NAME)
        {
            try
            {
                var process = Process.GetProcessesByName(processName).Single();

                if (process.MainModule == null)
                    throw new LeagueClientLockFileProcessException($"The League Client process doesn't have any main module.", processName);

                var processDirectory = Path.GetDirectoryName(process.MainModule.FileName);

                if (processDirectory == null)
                    throw new LeagueClientLockFileProcessException($"Unable to get the directory name for the League Client process.", processName);

                return FromPath(Path.Combine(processDirectory, LEAGUECLIENT_DEFAULT_LOCKFILE_NAME));
            }
            catch (Exception ex) when (ex is not LeagueClientLockFileProcessException)
            {
                throw new LeagueClientLockFileProcessException("An error occured trying to get the lock file using a LeagueClient process name, see the inner exception.", processName, ex);
            }
        }
    }
}