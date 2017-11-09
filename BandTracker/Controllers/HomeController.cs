using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using BandTracker.Models;

namespace BandTracker.Controllers
{
  public class HomeController : Controller
  {
    [HttpGet("/")]
    public ActionResult Index()
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      List<Venue> venueList = Venue.GetAll();
      List<Band> bandList = Band.GetAll();
      model.Add("venues", venueList);
      model.Add("bands", bandList);
      return View(model);
    }

    [HttpGet("/venues/add")]
    public ActionResult AddVenue()
    {
      return View();
    }

    [HttpPost("/venues/add/venue")]
    public ActionResult AddVenueSuccess()
    {
      string venueName = Request.Form["venue-name"];
      Venue newVenue = new Venue(venueName);
      newVenue.Save();

      List<Venue> model = Venue.GetAll();
      return View(model);
    }

    [HttpPost("/venues/bands/add")]
    public ActionResult AddBandSuccess()
    {
      return View("AddBandSuccess");
    }

    [HttpGet("/venues/{venueId}/bands/new")]
    public ActionResult AddBand(int venueId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Venue selectedVenue = Venue.FindById(venueId);
      model.Add("venue", selectedVenue);
      return View(model);
    }
    [HttpPost("/venues/{venueId}/bands/add")]
    public ActionResult AddBandToVenue(int venueId)
    {
      string bandName = Request.Form["band-name"];
      Band newBand = new Band(bandName, venueId);
      newBand.Save();

      Dictionary<string, object> model = new Dictionary<string, object>();
      Venue selectedVenue = Venue.FindById(venueId);
      List<Band> venuesBands = selectedVenue.GetBands();
      model.Add("venue", selectedVenue);
      model.Add("bands", venuesBands);

      return View("VenueDetail", model);
    }

    [HttpGet("/venues/{id}")]
    public ActionResult VenueDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Venue selectedVenue = Venue.FindById(id);
      List<Band> venuesBands = selectedVenue.GetBands();
      model.Add("venue", selectedVenue);
      model.Add("bands", venuesBands);

      return View(model);
    }

    [HttpGet("/bands/{id}")]
    public ActionResult BandDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Band selectedBand = Band.Find(id);
      List<Venue> bandsVenues = selectedBand.GetVenues();
      model.Add("band", selectedBand);
      model.Add("venues", bandsVenues);

      return View(model);
    }
  }
}
