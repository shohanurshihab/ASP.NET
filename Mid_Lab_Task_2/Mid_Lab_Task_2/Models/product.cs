using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mid_Lab_Task_2.Models
{
    public class product
    {
        public string[] GenerateIds()
        {
            string[] ids = new string[10];
            for (int i = 0; i < 10; i++)
            {
                ids[i] = i.ToString();
            }
            return ids;
        }
    }
}