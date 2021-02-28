using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        public Address Address { get; set; }

        [Required(ErrorMessage = "Please Enter Your Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [BindNever]
        public PaymentType PaymentType { get; set; } = PaymentType.Cash;
        [BindNever]
        public decimal OrderTotal { get; set; }
        [BindNever]
        public DateTime OrderPlaced { get; set; }
        [BindNever]
        public string TrackingID { get; set; }
    }
    public enum PaymentType
    {
        Cash,
        Visa
    }

}
    