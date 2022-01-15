﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RiotGames.LeagueOfLegends.LeagueClient.Tests
{
    /// <summary>
    /// Requires a running League Client instance, else all tests are inconclusive.
    /// The League Client should be in the default directory for the path tests.
    /// </summary>
    [TestClass]
    public class TestsWithLeagueClientRunning
    {
        private static readonly bool _isLeagueClientRunning = Process.GetProcesses().Any(p => p.ProcessName == LeagueClientLockFile.LEAGUECLIENT_DEFAULT_PROCESS_NAME);

        [TestMethod]
        public void FromProcess()
        {
            if (!_isLeagueClientRunning)
            {
                Assert.Inconclusive();
                return;
            }

            var lockFile = LeagueClientLockFile.FromProcess();

            Assert.IsNotNull(lockFile);
        }

        [TestMethod]
        public void FromPath()
        {
            if (!_isLeagueClientRunning)
            {
                Assert.Inconclusive();
                return;
            }

            var lockFile = LeagueClientLockFile.FromPath();

            Assert.IsNotNull(lockFile);
        }
    }
}
