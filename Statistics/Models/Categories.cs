using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Statistics.Models
{
    public class Categories
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Category { get; set; }

        public Operations Operations { get; set; }  
    }
}
