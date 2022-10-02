using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AjmeraAssignment.Models
{
    public partial class Book
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public string AuthorName { get; set; }
    }
}
