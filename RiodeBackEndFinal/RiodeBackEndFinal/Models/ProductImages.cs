namespace RiodeBackEndFinal.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public bool? IsMain { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
