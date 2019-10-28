using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DbRepositories.Data.Object
{
    public class Log
    {
        public int LogId { get; set; }
        public DateTime Date { get; set; }
        public string Message { get; set; }
    }
}
