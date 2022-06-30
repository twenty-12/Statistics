using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SQLite;
using Xamarin.Forms;

namespace Statistics.Models
{
    public class Report
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique]
        public string Category { get; set; }
        public Operations Operation { get; set; }
        public string Description { get; set; }
        public double Summ { get; set; }
        public DateTime Date { get; set; }

    }

    public enum Operations
    {
        plus, minus
    }
}