using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using assignment2.Data;
using assignment2.Models;
using assignment2.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace assignment2.Controllers
{
    [Route("api/service")]
    [ApiController]
    [Route("[controller]")]
    public class ApiController : Controller
    {
        //modify so that it returns from this project
        private AppointmentService _service;
        private SignInManager<ApplicationUser> _signInManager;

        public ApiController(IMongoRepository<Appointment> repository, SignInManager<ApplicationUser> signInManager)
        {
            _service = new AppointmentService(repository);
            _signInManager = signInManager;
        }
        [Route("")]
        public string Get()
        {
            return _service.GetAndConvertToJson();
        }
        [Route("auth")]
        [HttpPost]
        public async Task<bool> Authenticate(string email, string password)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, true, true);

            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        [Route("update")]
        [HttpPut]
        public string Update(Appointment appointment)
        {
            if (_service.AppointmentExistsWithDate(appointment) == false)
            {
                _service.Update(appointment);
            }
            return "updated";
        }
    }
}
