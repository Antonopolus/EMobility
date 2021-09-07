using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EMobility.WebApi.Controllers
{
    public record PriceChangeDto(string User, decimal PercentagePriceChange);

    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly EvChargerContext Context;

        //public readonly PriceManager Manager;
        private readonly LogManager LogManager;

        //public PriceController(EvChargerContext context, PriceManager manager, LogManager logManager)
        //{
        //    this.Context = context;
        //    this.Manager = manager;
        //    this.LogManager = logManager;
        //}


        //[HttpPost]
        //[Route("fill")]
        //public async Task<IActionResult> CreateDemoData()
        //{
        //    Context.Prices.Add(new Price { Product = "Apples", ProductPrice = 100 });
        //    await Context.SaveChangesAsync();
        //    return StatusCode((int)HttpStatusCode.Created);
        //}

        //[HttpPost]
        //public async Task<IActionResult> PriceChange(PriceChangeDto priceChange)
        //{
        //    Manager.ChangePrices(priceChange.PercentagePriceChange);
        //    LogManager.LogMessage(priceChange.User, priceChange.PercentagePriceChange);
        //    await Context.SaveChangesAsync();
        //    return Ok();
        //}
    }
}
