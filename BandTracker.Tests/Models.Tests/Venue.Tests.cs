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
  }
}
