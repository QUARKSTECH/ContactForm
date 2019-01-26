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
    public class EnquiryController : ControllerBase
    {
        private readonly IEntityRepository _repo;
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;
        public EnquiryController(IEntityRepository repo, IMapper mapper, ISmsService smsService)
        {
            _smsService = smsService;
            _mapper = mapper;
            _repo = repo;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddEnquiry(EnquiryDto enquiryDto)
        {
            try
            {
                var enquiry = _mapper.Map<Enquiry>(enquiryDto);
                //_repo.Add<Enquiry>(enquiry);
                var result = await _smsService.ReadAndModifyXMLFile(enquiryDto);
                return StatusCode(201);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetEnquiries()
        {
            var enquiries = await _repo.GetAllEnquiries();
            return Ok(enquiries);
        }
    }
}