using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;
using System;

namespace BandTracker.Controllers
{
  public class VenuesController : Controller
  {
    [HttpGet("/venues")]
    public ActionResult Index()
    {
      List<Venue> allveneus = Venues.GetAllVenues();
      return View(allVenues);
    }
    [HttpGet("/venues/new")]
    public ActionResult CreateForm()
    {
      return View("venuesForm");
    }
    [HttpPost("/venues")]
    public ActionResult Create()
    {
      Venue newVenues = new Venues(Request.Form["venuesName"]);
      newVenues.Save();
      return RedirectToAction("Success", "Home");
    }
    [HttpGet("/venues/delete")]
    public ActionResult DeleteVenues()
    {
      List<Venues> allVenues = Venues.GetAll();
      return View(allVenues);
    }
  }
}
