using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPInteg_UI.DTO;
using TPInteg_UI.Services;

namespace TPInteg_UI.Pages
{
    public partial class EditarProducto : Page
    {
        protected DropDownList DropDownListProveedor;
        protected DropDownList DropDownListEstado;
        protected TextBox TextBoxCodigo;
        protected TextBox TextBoxDescripcion;
        protected TextBox TextBoxPrecioUnitario;
        protected TextBox TextBoxStock;
        protected Button ButtonActualizar;
        protected Button ButtonCancelar;
        protected Panel PanelErrorEditarProducto;
        protected Label LabelErrorEditarProducto;
        protected HiddenField HiddenFieldProductoId;

        private readonly ProductoService _productoService;
        private readonly ProveedorService _proveedorService;
        private int _productoId;

        public EditarProducto()
        {
            _productoService = new ProductoService();
            _proveedorService = new ProveedorService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["id"], out int productoId))
                {
                    _productoId = productoId;
                    HiddenFieldProductoId.Value = productoId.ToString();
                    RegisterAsyncTask(new PageAsyncTask(LoadProveedoresAsync));
                    RegisterAsyncTask(new PageAsyncTask(LoadProductoAsync));
                }
                else
                {
                    Response.Redirect("ListadoProductos.aspx");
                }
            }
        }

        private async Task LoadProveedoresAsync()
        {
            try
            {
                var result = await _proveedorService.GetProveedoresAsync();

                if (result.Success && result.Data != null)
                {
                    DropDownListProveedor.DataSource = result.Data;
                    DropDownListProveedor.DataTextField = "Nombre";
                    DropDownListProveedor.DataValueField = "Id";
                    DropDownListProveedor.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar los proveedores: {ex.Message}");
            }
        }

        private async Task LoadProductoAsync()
        {
            try
            {
                var result = await _productoService.GetProductoByIdAsync(_productoId);

                if (result.Success && result.Data != null)
                {
                    var producto = result.Data;
                    TextBoxCodigo.Text = producto.codigo;
                    TextBoxDescripcion.Text = producto.descripcion;
                    TextBoxPrecioUnitario.Text = producto.precioUnitario.ToString();
                    TextBoxStock.Text = producto.stock.ToString();
                    DropDownListProveedor.SelectedValue = producto.proveedorId.ToString();

                    // Establecer el estado del producto y mantener las fechas
                    if (producto.fechaBaja.HasValue)
                    {
                        DropDownListEstado.SelectedValue = "inactivo";
                    }
                    else
                    {
                        DropDownListEstado.SelectedValue = "activo";
                    }
                }
                else
                {
                    ShowError(true, "No se pudo cargar el producto. Por favor, intente nuevamente más tarde.");
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al cargar el producto. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ShowError(bool show, string message = "")
        {
            if (PanelErrorEditarProducto != null)
            {
                PanelErrorEditarProducto.Visible = show;
                if (LabelErrorEditarProducto != null)
                    LabelErrorEditarProducto.Text = message;
            }
        }

        protected async void ButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                ShowError(false, string.Empty);

                var producto = new ProductoDTO
                {
                    id = Convert.ToInt32(HiddenFieldProductoId.Value),
                    codigo = TextBoxCodigo.Text,
                    descripcion = TextBoxDescripcion.Text,
                    precioUnitario = Convert.ToDecimal(TextBoxPrecioUnitario.Text),
                    stock = Convert.ToInt32(TextBoxStock.Text),
                    proveedorId = Convert.ToInt32(DropDownListProveedor.SelectedValue),
                    estado = DropDownListEstado.SelectedValue,
                    // Agregamos las fechas según el estado
                    fechaAlta = DateTime.Now.Date // Siempre enviamos la fecha actual
                };

                // Si el estado es inactivo, establecemos la fecha de baja
                if (DropDownListEstado.SelectedValue == "inactivo")
                {
                    producto.fechaBaja = DateTime.Now.Date;
                }
                else
                {
                    producto.fechaBaja = null; // Si es activo, la fecha de baja es null
                }

                var result = await _productoService.UpdateProductoAsync(producto.id, producto);

                if (result.Success)
                {
                    Response.Redirect("ListadoProductos.aspx");
                }
                else
                {
                    ShowError(true, result.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al actualizar el producto. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListadoProductos.aspx");
        }
    }
}