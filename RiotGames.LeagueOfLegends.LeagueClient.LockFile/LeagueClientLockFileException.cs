namespace RiotGames.LeagueOfLegends.LeagueClient;

public class LeagueClientLockFileException : Exception
{
    public LeagueClientLockFileException(string message) : base(message)
    {
    }

    public LeagueClientLockFileException(string message, Exception innerException) : base(message, innerException)
    {
    }
}

public class LeagueClientLockFilePathException : LeagueClientLockFileException
{
    public LeagueClientLockFilePathException(string message, string path) : base(message)
    {
        Path = path;
    }

    public LeagueClientLockFilePathException(string message, string path, Exception innerException) : base(message,
        innerException)
    {
        Path = path;
    }

    public string Path { get; }
}

public class LeagueClientLockFileProcessException : LeagueClientLockFileException
{
    public LeagueClientLockFileProcessException(string message, string processName) : base(message)
    {
        ProcessName = processName;
    }

    public LeagueClientLockFileProcessException(string message, string processName, Exception innerException) : base(
        message, innerException)
    {
        ProcessName = processName;
    }

    public string ProcessName { get; }
}