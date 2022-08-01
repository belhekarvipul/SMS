using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS.WebAPI
{
    public static class Helper
    {
        public static string GetDataFilePhysicalPath(string fileName) { 
            return HttpContext.Current.Server.MapPath("~/Data/" + fileName);
        }
    }
}