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
        return view(allBands);
      }

      [HttpGet("/venues")]
      public ActionResult Venues()
      {
        List<Venue> allVenues = Venue.GetAll();
        return view(allVenues);
      }

      //NEW TASK
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

//NEW CATEGORY
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
    }
}
