using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class BandTests : IDisposable
  {
    public void Dispose()
    {
      Band.ClearAll();
    }

    public BandTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtfirst_0()
    {
      //Arrange, action
      int result = Band.GetAll().Count;

      //Assert
      Assert.AreEqual(0, result);
    }
  }
}
