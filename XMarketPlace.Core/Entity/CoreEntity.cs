using System;
using System.Collections.Generic;
using System.Text;
using XMarketPlace.Core.Entity.Enums;

namespace XMarketPlace.Core.Entity
{
    // Bu sınıfın içerisinde bütün tablolarımda bulunacak ortak sütunları barındırıyor olacağız
    // CoreEntity sınıfı Model katmanında oluşturacağımız tablo sınıflarımıza miras verme görevini yerine getirecek.
    public class CoreEntity : IEntity<Guid>
    {
        public Guid ID { get; set; }
        public Status Status { get; set; }
        public DateTime? CreatedDate { get; set; } // ? işaretinin görevi bu alanın boş geçilebilir olduğunu belirtmek
        public string CreatedComputerName { get; set; }
        public string CreatedIP { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedComputerName { get; set; }
        public string ModifiedIP { get; set; }
    }
}
