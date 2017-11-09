using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
  [TestClass]
  public class VenueTests : IDisposable
  {
    public VenueTests()
    {
      DBConfiguration.ConnectionString = "server=localhost; user id=root; password=root; port=3306; database=band_tracker_test;";
    }
    public void Dispose()
    {
      Venue.ClearAll();
    }

    [TestMethod]
    public void GetAll_DatabaseIsEmptyAtFirst_0()
    {
      int result = Venue.GetAll().Count;

      Assert.AreEqual(0, result);
    }
    [TestMethod]
    public void HasSamePropertiesAs_BothHaveSameProperties_True()
    {
      Venue venue1 = new Venue("The Echo");
      Venue venue2 = new Venue("The Echo");

      bool result = venue1.HasSamePropertiesAs(venue2);

      Assert.AreEqual(true, result);
    }
    [TestMethod]
    public void HasSamePropertiesAs_BothDontHaveSameProperties_False()
    {
      Venue venue1 = new Venue("The Echo");
      Venue venue2 = new Venue("CBGBs");

      bool result = venue1.HasSamePropertiesAs(venue2);

      Assert.AreEqual(false, result);
    }
    [TestMethod]
    public void Save_SavesVenueToDatabase_DatabaseSaved()
    {
      Venue localVenue = new Venue("The Echo");
      localVenue.Save();
      Venue databaseVenue = Venue.GetAll()[0];

      bool result = localVenue.HasSamePropertiesAs(databaseVenue);

      Assert.AreEqual(true, result);
    }
    [TestMethod]
    public void Save_SavesMultipleVenuesToDatabase_VenuesSaved()
    {
      Venue localVenue1 = new Venue("Sunset Lounge");
      localVenue1.Save();
      Venue localVenue2 = new Venue("The Troubadour");
      localVenue2.Save();
      Venue databaseVenue1 = Venue.GetAll()[0];
      Venue databaseVenue2 = Venue.GetAll()[1];

      bool result =
        localVenue1.HasSamePropertiesAs(databaseVenue1) &&
        localVenue2.HasSamePropertiesAs(databaseVenue2);

      Assert.AreEqual(true, result);
    }
    [TestMethod]
    public void FindById_GetsVenueFromDatabase_VenueFound()
    {
      Venue localVenue = new Venue("The Crystal Ballroom");
      localVenue.Save();
      Venue databaseVenue = Venue.FindById(localVenue.Id);

      bool result = localVenue.HasSamePropertiesAs(databaseVenue);

      Assert.AreEqual(true, result);
    }
  }
}
