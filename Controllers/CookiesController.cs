using CookieMonster.Models;
using CookieMonster.CookieData;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace CookieMonster.Controllers
{
    [ApiController]
    public class CookiesController : ControllerBase
    {
        private ICookieData _cookieData;

        public CookiesController(ICookieData cookieData)
        {
            _cookieData = cookieData;
        }

        [HttpGet]
        [Route("api/GetCookies")]
        public IActionResult GetCookie()
        {
            return Ok(_cookieData.GetCookies());
        }

        [HttpGet]
        [Route("api/GetCookie")]
        public IActionResult GetCookie(string refId)
        {
            var cookie = _cookieData.GetCookie(refId);

            if (cookie == null)
            {
                return NotFound($"Cookie with Id {refId} was not found.");
            }

            return Ok(_cookieData.GetCookie(refId));
        }

        [HttpPost]
        [Route("api/AddCookie")]
        public IActionResult AddCookie(Cookie cookie)
        {
            var existingCookie = _cookieData.GetCookie(cookie.Ref_ID);

            if (existingCookie != null)
            {
                return BadRequest($"Cookie with Id {cookie.Ref_ID} already exist.");
            }
            
            var result = _cookieData.AddCookie(cookie);

            return Created($"{HttpContext.Request.Scheme}://{HttpContext.Request.Host}/api/GetCookie?id={cookie.Ref_ID}", result);
        }

        [HttpDelete]
        [Route("api/DeleteCookie")]
        public IActionResult DeleteCookie(string refId)
        {
            var cookie = _cookieData.GetCookie(refId);

            if (cookie == null)
            {
                return NotFound($"Unable to delete, cookie with Id {refId} was not found");
            }

            _cookieData.DeleteCookie(cookie);

            return Ok();
        }

        [HttpPatch]
        [Route("api/EditCookie")]
        public IActionResult EditCookie(Cookie cookie)
        {
            var existingCookie = _cookieData.GetCookie(cookie.Ref_ID);

            if (existingCookie == null)
            {
                return NotFound($"Unable to edit, cookie with Id {cookie.Ref_ID} was not found.");
            }

            _cookieData.EditCookie(cookie);

            return Ok(cookie);
        }
    }
}