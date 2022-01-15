using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RiotGames.LeagueOfLegends.LeagueClient.Tests
{
    [TestClass]
    public class ExceptionTests
    {
        [TestMethod]
        [ExpectedException(typeof(LeagueClientLockFileProcessException))]
        public void FromProcess()
        {
            _ = LeagueClientLockFile.FromProcess("addsadjsadjjdfskkjffsjkjfskjfdkjsfkjfsddfkjfsdjksfdjenmenmmnr");
        }

        [TestMethod]
        [ExpectedException(typeof(LeagueClientLockFilePathException))]
        public void FromPath()
        {
            _ = LeagueClientLockFile.FromPath("addsadjsadjjdfskkjffsjkjfskjfdkjsfkjfsddfkjfsdjksfdjenmenmmnr");
        }
    }
}
