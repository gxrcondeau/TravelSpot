﻿using Api.Data;
using Api.Models.DTO.Request;
using Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SpotController : ControllerBase
    {
        private readonly AppDbContext _context;
        public SpotController(AppDbContext context)
        {
            _context = context;
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateSpot(CreateSpotRequestDto request)
        //{

        //}
    }
}