//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcApplicationA2.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    public partial class OrderDetail
    {
        public int OrderDetailID { get; set; }

        [Required(ErrorMessage = "Please Enter your First Name!", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z',-.\s]{1,40}$", ErrorMessage = "First Name has Invalid Characters!")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please Enter your Last Name!", AllowEmptyStrings = false)]
        [RegularExpression(@"^[a-zA-Z',-.\s]{1,40}$", ErrorMessage = "Last Name has Invalid Characters!")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please Enter your Address!", AllowEmptyStrings = false)]
        [RegularExpression(@"^[0-9a-zA-Z'/,-.\s]{1,40}$", ErrorMessage = "Address has Invalid Characters!")]

        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter your City!", AllowEmptyStrings = false)]
        public string City { get; set; }

        [Required(ErrorMessage = "Please Enter your State!", AllowEmptyStrings = false)]
        public string State { get; set; }

        [RegularExpression(@"^\d{4}$", ErrorMessage = "Post Code is Invalid!")]
        public int PostCode { get; set; }
        
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVC is Invalid!")]
        public int cvc { get; set; }

        [Required(ErrorMessage = "Please Enter Expiry Month!", AllowEmptyStrings = false)]
        public string month { get; set; }

        [Required(ErrorMessage = "Please Enter Expiry Year!", AllowEmptyStrings = false)]
        public string year { get; set; }
        public string CreditCard { get; set; }
    }
}
