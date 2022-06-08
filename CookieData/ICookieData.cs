using CookieMonster.Models;
using System.Collections.Generic;
using System;

namespace CookieMonster.CookieData
{
    public interface ICookieData
    {
        List<Cookie> GetCookies();

        Cookie GetCookie(string refId);

        Cookie AddCookie(Cookie cookie);

        void DeleteCookie(Cookie cookie);

        Cookie EditCookie(Cookie cookie);
    }
}