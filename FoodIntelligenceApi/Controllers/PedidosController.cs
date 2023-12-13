﻿using FoodIntelligence.Data.DTOs;
using FoodIntelligence.Service.Services.PedidosServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FoodIntelligenceApi.Controllers
{
    [Route("api/Pedidos")]
    [ApiController]
    [Authorize]
    public class PedidosController : ControllerBase
    {
        protected readonly IPedidosService _service;

        public PedidosController(IPedidosService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(string userId)
        {
            var entityList = _service.GetAll(userId);
            return Ok(entityList);
        }
        [HttpPost]
        public async Task<IActionResult> AddItem(DetallesPedidoDto newItem)
        {
            var userIdClaim = User.FindFirst("UserId");
            string userId = userIdClaim.Value;
            var entity = _service.AddItem(newItem, userId);
            return Ok(entity);
        }

    }
}