using System;

namespace TPInteg_UI.DTO
{
    public class LocalidadDTO
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public int codigoPostal { get; set; }
        public DateTime? fechaAlta { get; set; }
        public DateTime? fechaBaja { get; set; }
    }
}