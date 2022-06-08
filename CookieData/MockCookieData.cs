using CookieMonster.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CookieMonster.CookieData
{
    public class MockCookieData : ICookieData
    {
        private List<Cookie> cookies = new List<Cookie>()
        {
            new Cookie()
            {
                Ref_ID = "CC0001",
                Name = "Kue Coklat",
                Description = "Kue Coklat Buatan Batam"
            },
            new Cookie()
            {
                Ref_ID = "CC0002",
                Name = "Kue Manis",
                Description = "Kue Manis Buatan Sendiri"
            }
        };

        public List<Cookie> GetCookies()
        {
            return cookies;
        }

        public Cookie GetCookie(string refId)
        {
            return cookies.SingleOrDefault(x => x.Ref_ID == refId);
        }

        public Cookie AddCookie(Cookie cookie)
        {
            cookies.Add(cookie);

            return cookie;
        }

        public void DeleteCookie(Cookie cookie)
        {
            cookies.Remove(cookie);
        }

        public Cookie EditCookie(Cookie cookie)
        {
            var existingCookie = GetCookie(cookie.Ref_ID);

            if (existingCookie != null)
            {
                existingCookie.Name = cookie.Name;
                existingCookie.Description = cookie.Description;
            }

            return existingCookie;
        }
    }
}