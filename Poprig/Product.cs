//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Poprig
{
    using System;
    using System.Collections.Generic;
    
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.Sales = new HashSet<Sales>();
        }
    
        public int ID { get; set; }
        public string Title { get; set; }
        public Nullable<int> TypeID { get; set; }
        public string Artikul { get; set; }
        public Nullable<int> ValueMan { get; set; }
        public Nullable<int> NumberCeh { get; set; }
        public Nullable<decimal> MinPriceForAgent { get; set; }
    
        public virtual TypeProduct TypeProduct { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Sales> Sales { get; set; }
    }
}
