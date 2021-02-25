﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YallaNakol.Data.Models
{
    public partial class Order
    {
        [BindNever]
        public int OrderId { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        
        [Required(ErrorMessage ="Please Enter Your First Name")]
        [MinLength(5,ErrorMessage ="Min 5 Characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Last Name")]
        [MinLength(5, ErrorMessage = "Min 5 Characters")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter Your Address")]
        [MinLength(5, ErrorMessage = "Min 5 Characters")]
        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }


        [Required(ErrorMessage = "Please Enter Your City")]
        public string City { get; set; }


        [Required(ErrorMessage = "Please Enter Your Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [BindNever]
        public decimal OrderTotal { get; set; }
        [BindNever]
        public DateTime OrderPlaced { get; set; }
    }

}
    