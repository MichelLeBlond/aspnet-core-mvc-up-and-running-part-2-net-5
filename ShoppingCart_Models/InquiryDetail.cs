using ShoppingCart_Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCart_Models
{
  public  class InquiryDetail
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int InqueryHeaderId { get; set; }
        [ForeignKey("InqueryHeaderId")]
        public InquiryHeader InquiryHeader { get; set; }
        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public InquiryHeader Product { get; set; }
    }
}
