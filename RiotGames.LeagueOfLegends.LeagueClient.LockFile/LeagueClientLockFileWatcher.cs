namespace RiotGames.LeagueOfLegends.LeagueClient;

#if false
internal class LeagueClientLockFileWatcher : IDisposable
{
    private readonly FileSystemWatcher _watcher;

    public LeagueClientLockFileWatcher(string lockFilePath)
    {
        _watcher = new FileSystemWatcher(Path.GetDirectoryName(lockFilePath)!, Path.GetFileName(lockFilePath))
        {
            EnableRaisingEvents = true
        };
    }

    public void Dispose()
    {
        _watcher.Dispose();
    }

    /// <devdoc>
    ///     Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path' /> is changed.
    /// </devdoc>
    public event FileSystemEventHandler? Changed
    {
        add => _watcher.Changed += value;
        remove => _watcher.Changed -= value;
    }

    /// <devdoc>
    ///     Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path' /> is created.
    /// </devdoc>
    public event FileSystemEventHandler? Created
    {
        add => _watcher.Created += value;
        remove => _watcher.Created -= value;
    }

    /// <devdoc>
    ///     Occurs when a file or directory in the specified <see cref='System.IO.FileSystemWatcher.Path' /> is deleted.
    /// </devdoc>
    public event FileSystemEventHandler? Deleted
    {
        add => _watcher.Deleted += value;
        remove => _watcher.Deleted -= value;
    }

    /// <devdoc>
    ///     Occurs when the internal buffer overflows.
    /// </devdoc>
    public event ErrorEventHandler? Error
    {
        add => _watcher.Error += value;
        remove => _watcher.Error -= value;
    }
}
#endif