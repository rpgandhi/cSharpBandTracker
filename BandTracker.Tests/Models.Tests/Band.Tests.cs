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
      Band.DeleteAll();
      Venue.DeleteAll();
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

    // [TestMethod]
    // public void Save_SavesToDatabase_BandList()
    // {
    //   //Arrange
    //   Band testBand = new Band("Counting Crows");
    //
    //   //Act
    //   testBand.Save();
    //   int result = Band.GetAll().Count;
    //
    //   //Assert
    //   Console.WriteLine("######" + result);
    //   Assert.AreEqual(1, result);
    // }

    [TestMethod]
    public void Save_SavesToDatabase_BandList()
    {
      //Arrange
      Band testBand = new Band("Baby Whales");

      //Act
      testBand.Save();
      List<Band> result = Band.GetAll();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueBandsAreTheSame_Band()
    {
      //Arrange, Act
      Band firstBand = new Band("Monster Smash");
      Band secondBand = new Band("Monster Smash");

      //Assert
      Assert.AreEqual(firstBand, secondBand);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Band testBand = new Band("Monster Smash");

      //Act
      testBand.Save();
      Band savedBand = Band.GetAll()[0];

      int result = savedBand.GetBandId();
      int testId = testBand.GetBandId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsBandInDatabase_Band()
    {
      //Arrange
      Band testBand = new Band("Morning Bird");
      testBand.Save();

      //Act
      Band foundBand = Band.Find(testBand.GetBandId());

      //Assert
      Assert.AreEqual(testBand, foundBand);
    }

    [TestMethod]
    public void AddVenue_AddsVenueToBand_VenueList()
    {
      //Arrange
      Band testBand = new Band("Firehouse Blues");
      testBand.Save();

      Venue testVenue = new Venue("Rialto Room");
      testVenue.Save();

      //Act
      testBand.AddVenue(testVenue);

      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{testVenue};
      Console.WriteLine("################"+ result + testList);

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

  }
}
