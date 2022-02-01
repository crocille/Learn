namespace LearnApp.Database
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Runtime.CompilerServices;

    [Table("Service")]
    public partial class Service:INotifyPropertyChanged
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Service()
        {
            ClientServices = new HashSet<ClientService>();
            ServicePhotoes = new HashSet<ServicePhoto>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        public decimal Cost { get; set; }

        public int DurationInSeconds { get; set; }

        [NotMapped]
        public int DurationImMinute
        {
            get
            {
                return DurationInSeconds / 60;
            }
            set { }
        }
        [StringLength(1073741823)]
        public string Description { get; set; }

        public double? Discount { get; set; }

        public bool IsDiscount => Discount != null;

        [NotMapped]
        public decimal DiscountPrice
        {
            get
            {
                if(Discount!=null)
                    return  Cost - (Cost / 100) * (decimal)Discount;
                return Cost;
            }
            set { }
        }
            
        private byte[] _MainImage;

        public byte[] MainImage
        {
            get { return _MainImage; }
            set { 
                _MainImage = value;
                PropChange();
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ClientService> ClientServices { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ServicePhoto> ServicePhotoes { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void PropChange([CallerMemberName]string PropName = "") {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
    }
}
