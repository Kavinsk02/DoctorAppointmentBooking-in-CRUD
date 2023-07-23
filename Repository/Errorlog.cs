using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebApplication13.Repository
{
    public static class Errorlog
    {
        public static void LogError(Exception ex)
        {
            string filepath = @"E:\\Logfile\\log.txt";
            using (StreamWriter writer = new StreamWriter(filepath, true))
            {
                writer.WriteLine(DateTime.Now.ToString()+""+ex.ToString());
            }
        }
    }
}