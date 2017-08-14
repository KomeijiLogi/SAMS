using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SAMS.Web.Areas.Admin.Models.Product
{
    public class CreateViewModel
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public string EASNumber { get; set; }
        public string K3Number { get; set; }
    }
}