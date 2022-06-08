using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using CookieMonster.Models;
using System.Data;
using Microsoft.EntityFrameworkCore;

namespace CookieMonster.CookieData
{
    public class ActualCookieData : ICookieData
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

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

        public ActualCookieData(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public List<Cookie> GetCookies()
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = @"
                SELECT [Ref_ID], [Name], [Description]
                FROM T_Catalogue
                ";
                cmd.CommandType = CommandType.Text;
                _context.Database.OpenConnection();
                using (var result = cmd.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        var o = new List<Cookie>();
                        while (result.Read())
                        {
                            o.Add(new Cookie {
                                Ref_ID = (string)result["Ref_ID"],
                                Name = (string)result["Name"],
                                Description = (string)result["Description"]
                            });
                        }

                        return o;
                    }
                }
            }
            return null;
        }

        public Cookie GetCookie(string refId)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = @"
                SELECT [Ref_ID], [Name], [Description]
                FROM T_Catalogue
                WHERE [Ref_ID] = @Ref_ID
                ";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(new SqlParameter("Ref_ID", refId));
                _context.Database.OpenConnection();
                using (var result = cmd.ExecuteReader())
                {
                    if (result.HasRows)
                    {
                        var o = new Cookie();
                        while (result.Read())
                        {
                            o.Ref_ID = (string)result["Ref_ID"];
                            o.Name = (string)result["Name"];
                            o.Description = (string)result["Description"];
                        }

                        return o;
                    }
                }
            }
            return null;
        }

        public Cookie AddCookie(Cookie cookie)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "sp_CreateCookie";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Ref_ID", cookie.Ref_ID));
                cmd.Parameters.Add(new SqlParameter("Name", cookie.Name));
                cmd.Parameters.Add(new SqlParameter("Description", cookie.Description));
                
                var returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnParameter);

                _context.Database.OpenConnection();
                cmd.ExecuteNonQuery();
                var result = (int)cmd.Parameters["@ReturnVal"].Value;

                if (result == 1)
                {
                    return cookie;
                }
            }
            return null;
        }

        public void DeleteCookie(Cookie cookie)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "sp_DeleteCookie";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Ref_ID", cookie.Ref_ID));
                
                var returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnParameter);

                _context.Database.OpenConnection();
                cmd.ExecuteNonQuery();
                var result = (int)cmd.Parameters["@ReturnVal"].Value;
            }
        }

        public Cookie EditCookie(Cookie cookie)
        {
            using (var cmd = _context.Database.GetDbConnection().CreateCommand())
            {
                cmd.CommandText = "sp_EditCookie";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("Ref_ID", cookie.Ref_ID));
                cmd.Parameters.Add(new SqlParameter("Name", cookie.Name));
                cmd.Parameters.Add(new SqlParameter("Description", cookie.Description));
                
                var returnParameter = new SqlParameter("@ReturnVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;
                cmd.Parameters.Add(returnParameter);

                _context.Database.OpenConnection();
                cmd.ExecuteNonQuery();
                var result = (int)cmd.Parameters["@ReturnVal"].Value;

                if (result == 1)
                {
                    return cookie;
                }
            }
            return null;
        }
    }
}