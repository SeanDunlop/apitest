using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apitest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Azure;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Data.Entity;

namespace apitest.Common
{
    public class SessionManager
    {
        public int isValid(ScheduleContext c, String sessionToken)
        {
            var token = c.sessions.Where(x => x.SessionGuid == sessionToken);
            if(token == null)
            {
                return 0;
            }
            else
            {
                if (DateTime.Compare(token.First().Timeout, DateTime.UtcNow) > 0)
                {
                    token.First().Timeout = DateTime.UtcNow.AddMinutes(30);
                    return token.First().UserId;
                }
                else
                {
                    return 0;
                }
            }
        }
    }
}
