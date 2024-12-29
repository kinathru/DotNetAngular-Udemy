﻿using Microsoft.AspNetCore.Mvc;
using WebAPI.ReadModels;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class FlightController(ILogger<FlightController> logger) : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<FlightRm>> Search()
    {
        var dummyFlightRms = new List<FlightRm>
        {
            new(
                Guid.NewGuid(),
                "Airways Alpha",
                "$450",
                new TimePlaceRm("Los Angeles", new DateTime(2024, 12, 28, 10, 0, 0)),
                new TimePlaceRm("New York", new DateTime(2024, 12, 28, 14, 30, 0)),
                20
            ),
            new(
                Guid.NewGuid(),
                "Bravo Airlines",
                "$320",
                new TimePlaceRm("Houston", new DateTime(2024, 12, 28, 15, 0, 0)),
                new TimePlaceRm("Chicago", new DateTime(2024, 12, 28, 18, 0, 0)),
                15
            ),
            new(
                Guid.NewGuid(),
                "SkyHigh",
                "$280",
                new TimePlaceRm("Seattle", new DateTime(2024, 12, 28, 11, 0, 0)),
                new TimePlaceRm("San Francisco", new DateTime(2024, 12, 28, 13, 45, 0)),
                25
            ),
            new(
                Guid.NewGuid(),
                "Delta Express",
                "$400",
                new TimePlaceRm("Atlanta", new DateTime(2024, 12, 28, 17, 0, 0)),
                new TimePlaceRm("Miami", new DateTime(2024, 12, 28, 19, 30, 0)),
                10
            ),
            new(
                Guid.NewGuid(),
                "Echo Flights",
                "$380",
                new TimePlaceRm("Denver", new DateTime(2024, 12, 28, 16, 30, 0)),
                new TimePlaceRm("Boston", new DateTime(2024, 12, 28, 20, 15, 0)),
                5
            ),
            new(
                Guid.NewGuid(),
                "Foxtrot Wings",
                "$150",
                new TimePlaceRm("Phoenix", new DateTime(2024, 12, 28, 10, 0, 0)),
                new TimePlaceRm("Las Vegas", new DateTime(2024, 12, 28, 12, 30, 0)),
                30
            ),
            new(
                Guid.NewGuid(),
                "Global Air",
                "$310",
                new TimePlaceRm("Dallas", new DateTime(2024, 12, 28, 18, 0, 0)),
                new TimePlaceRm("Orlando", new DateTime(2024, 12, 28, 21, 0, 0)),
                8
            ),
            new(
                Guid.NewGuid(),
                "Horizon",
                "$220",
                new TimePlaceRm("Portland", new DateTime(2024, 12, 28, 13, 0, 0)),
                new TimePlaceRm("Salt Lake City", new DateTime(2024, 12, 28, 15, 45, 0)),
                12
            ),
            new(
                Guid.NewGuid(),
                "Inland Airways",
                "$330",
                new TimePlaceRm("Charlotte", new DateTime(2024, 12, 28, 20, 0, 0)),
                new TimePlaceRm("Washington D.C.", new DateTime(2024, 12, 28, 22, 30, 0)),
                7
            ),
            new(
                Guid.NewGuid(),
                "Jet Stream",
                "$270",
                new TimePlaceRm("Detroit", new DateTime(2024, 12, 28, 21, 15, 0)),
                new TimePlaceRm("Minneapolis", new DateTime(2024, 12, 28, 23, 45, 0)),
                18
            )
        };


        return Ok(dummyFlightRms);
    }
}