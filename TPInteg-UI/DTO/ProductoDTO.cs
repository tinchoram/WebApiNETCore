using System;

namespace TPInteg_UI.DTO
{
    public class ProductoDTO
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string descripcion { get; set; }
        public decimal precioUnitario { get; set; }
        public int stock { get; set; }
        public string estado { get; set; }
        public int proveedorId { get; set; }
        public DateTime? fechaAlta { get; set; }
        public DateTime? fechaBaja { get; set; }
    }
}