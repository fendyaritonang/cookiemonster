using System;
using System.ComponentModel.DataAnnotations;

namespace CookieMonster.Models
{
    public class Cookie
    {
        public string Ref_ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}