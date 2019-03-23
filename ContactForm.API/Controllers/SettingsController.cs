using System;
using System.Threading.Tasks;
using AutoMapper;
using ContactForm.API.Data;
using ContactForm.API.Dtos;
using ContactForm.API.Helpers.SMS;
using ContactForm.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ContactForm.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IEntityRepository _repo;
        private readonly IMapper _mapper;
        public SettingsController(IEntityRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddTenant(TenantDto tenantDto)
        {
            try
            {
                var tenant = _mapper.Map<Tenant>(tenantDto);
                _repo.Add<Tenant>(tenant);
                return StatusCode(201);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }
    }
}