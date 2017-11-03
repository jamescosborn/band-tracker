using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BandTracker.Models;

namespace BandTracker.Models.Tests
{
  [TestClass]
  public class VenueTests : IDisposable
  {
    public void Dispose()
    {
      Venue.ClearAll();
    }
    public VenueTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=3306;database=band_tracker_tests;";
    }
    [TestMethod]
    public void ClearAll_ClearsAllVenuesFromDB_0()
    {
      List<Venue> venueTestList = new List <Venue>();
      Venue venueA = new Venue("Black Sun");
      Venue venueB = new Venue("St. Alphonzo's Pancake Breakfast");
      Venue venueC = new Venue("The 3rd Rail");

      venueTestList.Add(venueA);
      venueTestList.Add(venueB);
      venueTestList.Add(venueC);

      Venue.ClearAll();
      List<Venue> resultList = Venue.GetAll();

      Assert.AreEqual(0,resultList.Count);
    }

    [TestMethod]
    public void Save_SaveVenue_VenueSaved()
    {
      Venue testVenue = new Venue("Black Sun");
      testVenue.Save();

      Assert.AreEqual(true,Venue.GetAll().Count==1);
    }

    [TestMethod]
    public void FindById_GetsSpecificVenueFromDatabase_Venue()
    {
      Venue localVenue = new Venue("Black Sun");
      localVenue.Save();
      Venue databaseVenue = Venue.FindById(localVenue.Id);

      bool result = localVenue.HasSamePropertiesAs(databaseVenue);

      Assert.AreEqual(true, result);
    }
    [TestMethod]
    public void Update_UpdateVenueInDatabase_VenueWithNewInfo()
    {
      Venue initialVenue = new Venue("Black Sun");
      initialVenue.Save();
      Venue newVenue = new Venue("Zero's", initialVenue.Id);
      initialVenue.Update(newVenue);
      Venue updatedVenue = Venue.FindById(initialVenue.Id);

      bool result = updatedVenue.HasSamePropertiesAs(newVenue);

      Assert.AreEqual(true, result);
    }
  }
}
