using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using BandTracker.Models;

namespace BandTracker.Tests
{
  [TestClass]
  public class VenueTests : IDisposable
  {
    public void Dispose()
    {
      Venue.DeleteAll();
      Band.DeleteAll();
    }

    public VenueTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=band_tracker_test;";
    }

    [TestMethod]
    public void GetAll_DatabaseEmptyAtfirst_0()
    {
      //Arrange, action
      int result = Venue.GetAll().Count;

      //Assert
      Console.WriteLine("####################" +result);
      Assert.AreEqual(0, result);
    }

    // [TestMethod]
    // public void Save_SavesToDatabase_VenueList()
    // {
    //   //Arrange
    //   Venue testVenue = new Venue("Parkway Theater");
    //
    //   //Act
    //   testVenue.Save();
    //   Console.WriteLine("###########" + testVenue);
    //   int result = Venue.GetAll().Count;
    //
    //   //Assert
    //   Assert.AreEqual(1, result);
    // }

    [TestMethod]
    public void Save_SavesToDatabase_VenueList()
    {
      //Arrange
      Venue testVenue = new Venue("Parkway Theater");

      //Act
      testVenue.Save();
      List<Venue> result = Venue.GetAll();
      List<Venue> testList = new List<Venue>{testVenue};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfVenuesAreTheSame_Venue()
    {
      //Arrange, Act
      Venue firstVenue = new Venue("The Rose Garden");
      Venue secondVenue = new Venue("The Rose Garden");

      //Assert
      Assert.AreEqual(firstVenue, secondVenue);
    }

    [TestMethod]
    public void Save_AssignsIdToObject_Id()
    {
      //Arrange
      Venue testVenue = new Venue("The Rose Garden");

      //Act
      testVenue.Save();
      Venue savedVenue = Venue.GetAll()[0];

      int result = savedVenue.GetVenueId();
      int testId = testVenue.GetVenueId();

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Find_FindsVenueInDatabase_Venue()
    {
      //Arrange
      Venue testVenue = new Venue("Carter Museum");
      testVenue.Save();

      //Act
      Venue foundVenue = Venue.Find(testVenue.GetVenueId());

      //Assert
      Assert.AreEqual(testVenue, foundVenue);
    }

    [TestMethod]
    public void AddBand_AddsBandToVenue_BandList()
    {
      //Arrange
      Venue testVenue = new Venue("Rialto Room");
      testVenue.Save();

      Band testBand = new Band("Firehouse Blues");
      testBand.Save();

      //Act
      testVenue.AddBand(testBand);

      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Delete_DeletesVenueAssociationsFromDatabase_VenueList()
    {
      //Task = Venue
      //Category = Band
      //Arrange
      Band testBand = new Band("Metallica");
      testBand.Save();

      string testName = "Rose Theater";
      Venue testVenue = new Venue(testName);
      testVenue.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.Delete();

      List<Venue> resultBandVenues = testBand.GetVenues();
      List<Venue> testBandVenues = new List<Venue> {};

      //Assert
      CollectionAssert.AreEqual(testBandVenues, resultBandVenues);
    }

    [TestMethod]
    public void Test_AddBand_AddsBandToVenue()
    {
      //Category = Venue
      //Task = Band
      //Arrange
      Venue testVenue = new Venue("Sound Garden");
      testVenue.Save();

      Band testBand = new Band("Foo Fighters");
      testBand.Save();

      Band testBand2 = new Band("Smashing Pumpkins");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand);
      testVenue.AddBand(testBand2);

      List<Band> result = testVenue.GetBands();
      List<Band> testList = new List<Band>{testBand, testBand2};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetTasks_ReturnsAllCategoryTasks_TaskList()
    {
      //Category = Venue
      //Task = Band
      //Arrange
      Venue testVenue = new Venue("Blue Theater");
      testVenue.Save();

      Band testBand1 = new Band("Counting Crows");
      testBand1.Save();

      Band testBand2 = new Band("Dave Matthews Band");
      testBand2.Save();

      //Act
      testVenue.AddBand(testBand1);
      List<Band> savedBands = testVenue.GetBands();
      List<Band> testList = new List<Band> {testBand1};

      //Assert
      CollectionAssert.AreEqual(testList, savedBands);
    }
  }
}
