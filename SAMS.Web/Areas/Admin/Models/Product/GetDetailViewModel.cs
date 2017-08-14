
using SAMS.Customers.Dtos;
using System;
using System.ComponentModel.DataAnnotations;

namespace SAMS.Web.Areas.Admin.Models.Product
{
    public class GetDetailViewModel
    {
        public int Id { get; set; }
        public  string Name { get; set; }

        public  string Model { get; set; }
        public  string EASNumber { get; set; }
        public  string K3Number { get; set; }

    }
}