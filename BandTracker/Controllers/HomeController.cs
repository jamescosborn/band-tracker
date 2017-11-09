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

    [HttpGet("/bands/add")]
    public ActionResult AddBand()
    {
      return View();
    }

    [HttpPost("/venues/add/success")]
    public ActionResult AddVenueSuccess()
    {
      string venueName = Request.Form["venue-name"];
      Venue newVenue = new Venue(venueName);
      newVenue.Save();

      List<Venue> model = Venue.GetAll();
      return View(model);
    }

    [HttpPost("/bands/add/success")]
    public ActionResult AddBandSuccess()
    {
      string bandName = Request.Form["band-name"];
      Band newBand = new Band(bandName);
      newBand.Save();

      List<Band> model = Band.GetAll();
      return View(model);
    }

    [HttpGet("/venues/{venueId}/update")]
    public ActionResult UpdateVenue(int venueId)
    {
      Venue model = Venue.FindById(venueId);
      return View(model);
    }

    [HttpPost("/venues/{venueId}/update/success")]
    public ActionResult UpdateVenueSuccess(int venueId)
    {
      Venue selectedVenue = Venue.FindById(venueId);
      selectedVenue.Update(Request.Form["venue-update"]);
      return View();
    }

    [HttpPost("/venues/{venueId}/delete/success")]
    public ActionResult DeleteVenueSuccess(int venueId)
    {
      Venue selectedVenue = Venue.FindById(venueId);
      selectedVenue.Delete();
      return View();
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

    //ONE TASK
    [HttpGet("/bands/{id}")]
    public ActionResult BandDetail(int id)
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Band selectedBand = Band.Find(id);
        List<Venue> BandVenues = selectedBand.GetVenues();
        List<Venue> AllVenues = Venue.GetAll();
        model.Add("band", selectedBand);
        model.Add("bandVenues", BandVenues);
        model.Add("allVenues", AllVenues);
        return View( model);
    }

    //ONE CATEGORY
    [HttpGet("/venues/{id}")]
    public ActionResult VenueDetail(int id)
    {
        Dictionary<string, object> model = new Dictionary<string, object>();
        Venue SelectedVenue = Venue.FindById(id);
        List<Band> VenueBands = SelectedVenue.GetBands();
        List<Band> AllBands = Band.GetAll();
        model.Add("venue", SelectedVenue);
        model.Add("venueBands", VenueBands);
        model.Add("allBands", AllBands);
        return View(model);
    }
    //ADD TASK TO CATEGORY
    [HttpPost("venues/{venueId}/bands/new")]
    public ActionResult VenueAddBand(int venueId)
    {
        Venue venue = Venue.FindById(venueId);
        Band band = Band.Find(Int32.Parse(Request.Form["band-id"]));
        venue.AddBand(band);
        return View("Success");
    }
    //ADD CATEGORY TO TASK
    [HttpPost("bands/{bandId}/venues/new")]
    public ActionResult BandAddVenue(int bandId)
    {
        Band band = Band.Find(bandId);
        Venue venue = Venue.FindById(Int32.Parse(Request.Form["venue-id"]));
        band.AddVenue(venue);
        return View("Success");
    }
  }
}

    // [HttpGet("/bands/{id}")]
    // public ActionResult BandDetail(int id)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Band selectedBand = Band.Find(id);
    //   List<Venue> bandsVenues = selectedBand.GetVenues();
    //   model.Add("band", selectedBand);
    //   model.Add("venues", bandsVenues);
    //
    //   return View(model);
    // }

    // [HttpGet("/venues/{venueId}/bands/new")]
    // public ActionResult AddBand(int venueId)
    // {
    //   Dictionary<string, object> model = new Dictionary<string, object>();
    //   Venue selectedVenue = Venue.FindById(venueId);
    //   model.Add("venue", selectedVenue);
    //   return View(model);
    // }
