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
        public async Task<IActionResult> Get()
        {
            var userIdClaim = User.FindFirst("UserId");
            string userId = userIdClaim.Value;
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
        [HttpGet]
        [Route("GetCurrentPedido")]
        public async Task<IActionResult> GetCurrentPedido()
        {
            var userIdClaim = User.FindFirst("UserId");
            string userId = userIdClaim.Value;
            var entity = _service.GetCurrentPedido(userId);
            return Ok(entity);
        }
        [HttpGet]
        [Route("GetPedidoId")]
        public async Task<IActionResult> GetPedidoId(long pedidoId)
        {
            var entity = _service.GetPedidoWithId(pedidoId);
            return Ok(entity);
        }
        [HttpPut]
        public async Task<IActionResult> Update(PedidoDto toEdit)
        {
            var entity = _service.Update(toEdit);
            return Ok(entity);
        }
        [HttpPut]
        [Route("UpdateRating")]
        public async Task<IActionResult> UpdateRating(PedidoDto toEdit)
        {
            var entity = _service.UpdateRating(toEdit);
            return Ok(entity);
        }
    }
}
