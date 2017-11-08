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

    [TestMethod]
    public void Delete_DeletesBandAssociationsFromDatabase_BandList()
    {
      //Task = Band
      //Category = Venue
      //Arrange
      Venue testVenue = new Venue("Kennedy School");
      testVenue.Save();

      string testName = "ACDC";
      Band testBand = new Band(testName);
      testBand.Save();

      //Act
      testBand.AddVenue(testVenue);
      testBand.Delete();

      List<Band> resultVenueBands = testVenue.GetBands();
      List<Band> testVenueBands = new List<Band> {};

      //Assert
      CollectionAssert.AreEqual(testVenueBands, resultVenueBands);
    }

    [TestMethod]
    public void Test_AddVenue_AddsVenueToBand()
    {
      //Category = Band
      //Task = Venue
      //Arrange
      Band testBand = new Band("Aerosmith");
      testBand.Save();

      Venue testVenue = new Venue("Rose Garden");
      testVenue.Save();

      Venue testVenue2 = new Venue("Crystal Ballroom");
      testVenue2.Save();

      //Act
      testBand.AddVenue(testVenue);
      testBand.AddVenue(testVenue2);

      List<Venue> result = testBand.GetVenues();
      List<Venue> testList = new List<Venue>{testVenue, testVenue2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetVenues_ReturnsAllBandVenues_VenueList()
    {
      //Category = Band
      //Task = Venue
      //Arrange
      Band testBand = new Band("GooGoo Dolls");
      testBand.Save();

      Venue testVenue1 = new Venue("Gorge Whitehouse");
      testVenue1.Save();

      Venue testVenue2 = new Venue("Key Arena");
      testVenue2.Save();

      //Act
      testBand.AddVenue(testVenue1);
      List<Venue> savedVenues = testBand.GetVenues();
      List<Venue> testList = new List<Venue> {testVenue1};

      //Assert
      CollectionAssert.AreEqual(testList, savedVenues);
    }

  }
}
