using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using BandTracker.Models;
using System;

namespace BandTracker.Controllers
{
  public class BandController : Controller
  {
    [HttpGet("/bands")]
    public ActionResult Index()
    {
      List<Band> allBands = Bands.GetAllBands();
      return View(allBands);
    }
    [HttpGet("/bands/new")]
    public ActionResult CreateForm()
    {
      return View("bandsForm");
    }
    [HttpPost("/bands")]
    public ActionResult Create()
    {
      Band newBands = new Bands(Request.Form["bandsName"]);
      newBands.Save();
      return RedirectToAction("Success", "Home");
    }
    [HttpGet("/bands/delete")]
    public ActionResult DeleteBand()
    {
      List<Band> allBands = Band.GetAll();
      return View(allBands);
    }
  }
}
