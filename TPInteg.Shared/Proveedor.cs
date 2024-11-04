namespace TPInteg.Shared;
public class Proveedor : BaseEntity
{
    public string Direccion {  get; set; }
    public Localidad Localidad { get; set; }
}
