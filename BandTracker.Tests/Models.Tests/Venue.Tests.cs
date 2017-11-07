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
  }
}
