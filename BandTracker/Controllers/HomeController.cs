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

    [HttpPost("/venues/add/band/success")]
    public ActionResult AddBandSuccess()
    {
      return View("AddBandSuccess");
    }

    [HttpGet("/venues/{venueId}/bands/{bandId}/update")]
    public ActionResult UpdateBand(int venueId, int bandId)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      Venue selectedVenue = Venue.FindById(venueId);
      Band selectedBand = Band.Find(bandId);
      model.Add("venue", selectedVenue);
      model.Add("band", selectedBand);
      return View(model);
    }

    [HttpPost("/venues/{venueId}/bands/{bandId}/update/success")]
    public ActionResult UpdateBandSuccess(int bandId)
    {
      Console.WriteLine(bandId);

      Band selectedBand = Band.Find(bandId);
      selectedBand.Update(Request.Form["band-update"]);
      Console.WriteLine(selectedBand.Name);
      Console.WriteLine(Request.Form["band-update"]);
      return View();
    }

    [HttpPost("/venues/{venueId}/bands/{bandId}/delete/success")]
    public ActionResult DeleteBandSuccess(int bandId)
    {
      Band.Delete(bandId);
      return View();
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

  }
}
