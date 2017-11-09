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
        return View();
      }

      [HttpGet("/bands")]
      public ActionResult Bands()
      {
        List<Band> allBands = Band.GetAll();
        return View(allBands);
      }

      [HttpGet("/venues")]
      public ActionResult Venues()
      {
        List<Venue> allVenues = Venue.GetAll();
        return View(allVenues);
      }

      [HttpGet("/bands/new")]
      public ActionResult BandForm()
      {
          return View();
      }

      [HttpPost("/bands/new")]
      public ActionResult BandCreate()
      {
          Band newBand = new Band(Request.Form["band-name"]);
          newBand.Save();
          return View("Success");
      }

      [HttpGet("/venues/new")]
      public ActionResult VenueForm()
      {
          return View();
      }
      [HttpPost("/venues/new")]
      public ActionResult VenueCreate()
      {
          Venue newVenue = new Venue(Request.Form["venue-name"]);
          newVenue.Save();
          return View("Success");
      }


      [HttpGet("/bands/{id}")]
      public ActionResult BandDetail(int id)
      {
          //Category = Venue
          //Task = Band
          Dictionary<string, object> model = new Dictionary<string, object>();
          Band selectedBand = Band.Find(id);
          List<Venue> BandVenues = selectedBand.GetVenues();
          List<Venue> AllVenues = Venue.GetAll();
          List<Band> AllBands = Band.GetAll();
          model.Add("band", selectedBand);
          model.Add("bandVenues", BandVenues);
          model.Add("allVenues", AllVenues);
          model.Add("allBands", AllBands);
          // List<Band> VenueBands = SelectedVenue.GetBands();
          // List<Band> AllBands = Band.GetAll();
          // model.Add("venue", SelectedVenue);
          // model.Add("venueBands", VenueBands);
          // model.Add("allBands", AllBands);
          return View(model);
      }

      [HttpGet("/venues/{id}")]
      public ActionResult VenueDetail(int id)
      {
          //Category = Venue
          //Task = Band
          Dictionary<string, object> model = new Dictionary<string, object>();
          Venue SelectedVenue = Venue.Find(id);
          List<Band> VenueBands = SelectedVenue.GetBands();
          List<Band> AllBands = Band.GetAll();
          List<Venue> AllVenues = Venue.GetAll();
          model.Add("venue", SelectedVenue);
          model.Add("venueBands", VenueBands);
          model.Add("allBands", AllBands);
          model.Add("allVenues", AllVenues);
          return View(model);
      }

      [HttpPost("/venues/{venueId}/bands/new")]
      public ActionResult VenueAddBand(int venueId)
      {
          //Category = Venue
          //Task = Band
          Venue venue = Venue.Find(venueId);
          Band band = Band.Find(Int32.Parse(Request.Form["band-id"]));
          venue.AddBand(band);
          return View("Success");
      }

      [HttpPost("/bands/{bandId}/venues/new")]
      public ActionResult BandAddVenue(int bandId)
      {
          //Category = Venue
          //Task = Band
          Band band = Band.Find(bandId);
          Venue venue = Venue.Find(Int32.Parse(Request.Form["venue-id"]));
          band.AddVenue(venue);
          return View("Success");
      }

      [HttpPost("/bands/{bandId}/delete")]
      public ActionResult BandDelete(int bandId)
      {
        Band band = Band.Find(bandId);
        band.Delete();
        return View("Success");
      }

      [HttpPost("/bands/{bandId}/update")]
      public ActionResult BandUpdate(int bandId)
      {
        Band band = Band.Find(bandId);
        band.UpdateName(Request.Form["band-name-update"]);
        // Band newName = new Band(Request.Form["band-name-update"]);
        // newName.UpdateName();
        return View("Success");
      }

      [HttpPost("/venues/{venueId}/delete")]
      public ActionResult VenueDelete(int venueId)
      {
        Venue venue = Venue.Find(venueId);
        venue.Delete();
        return View("Success");
      }

      [HttpPost("/venues/{venueId}/update")]
      public ActionResult VenueUpdate(int venueId)
      {
        Venue venue = Venue.Find(venueId);
        venue.UpdateName(Request.Form["venue-name-update"]);
        // Band newName = new Band(Request.Form["band-name-update"]);
        // newName.UpdateName();
        return View("Success");
      }
    }
}
