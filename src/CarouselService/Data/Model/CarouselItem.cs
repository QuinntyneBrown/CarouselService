using System;
using CarouselService.Data.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using static CarouselService.Constants;

namespace CarouselService.Data.Model
{
    [SoftDelete("IsDeleted")]
    public class CarouselItem: ILoggable
    {
        public int Id { get; set; }
        
		[ForeignKey("Tenant")]
        public int? TenantId { get; set; }

        [ForeignKey("Carousel")]
        public int? CarouselId { get; set; }

        [Index("CarouselItemNameIndex", IsUnique = false)]
        [Column(TypeName = "VARCHAR")]     
        [StringLength(MaxStringLength)]		   
		public string Name { get; set; }
        
        public int? OrderIndex { get; set; }

        public string ImageUrl { get; set; }
        
        public DateTime CreatedOn { get; set; }
        
		public DateTime LastModifiedOn { get; set; }
        
		public string CreatedBy { get; set; }
        
		public string LastModifiedBy { get; set; }
        
		public bool IsDeleted { get; set; }

        public virtual Tenant Tenant { get; set; }

        public virtual Carousel Carousel { get; set; }
    }
}
