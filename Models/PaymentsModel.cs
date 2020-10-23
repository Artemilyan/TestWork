using System;

namespace file_uploader.Models
{
    public class PaymentsModel
    {
        // номер платежа
        public int Number { get; set; }
        // дата платежа
        public decimal Summ { get; set; }
        // сумма платежа
        public DateTime Date { get; set; }
    }
}