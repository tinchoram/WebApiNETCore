using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPInteg_UI.DTO;
using TPInteg_UI.Services;

namespace TPInteg_UI.Pages
{
    public partial class ListadoProductos : Page
    {
        protected UpdateProgress UpdateProgressProductos;
        protected UpdatePanel UpdatePanelProductos;
        protected Panel PanelErrorProductos;
        protected Panel NoDataPanelProductos;
        protected GridView GridViewProductos;
        protected Label LabelErrorProductos;
        protected Button ButtonReintentarProductos;
        protected TextBox TextBoxCodigo;
        protected TextBox TextBoxDescripcion;
        protected TextBox TextBoxPrecioUnitario;
        protected TextBox TextBoxStock;
        protected DropDownList DropDownListProveedor;
        protected Button ButtonGuardarProducto;

        private readonly ProductoService _productoService;
        private readonly ProveedorService _proveedorService;

        public ListadoProductos()
        {
            _productoService = new ProductoService();
            _proveedorService = new ProveedorService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(LoadProductosAsync));
                RegisterAsyncTask(new PageAsyncTask(LoadProveedoresAsync));
            }
        }

        private void ShowLoading(bool show)
        {
            UpdateProgressProductos.Visible = show;
        }

        private void ShowError(bool show, string message = "")
        {
            PanelErrorProductos.Visible = show;
            LabelErrorProductos.Text = message;
            GridViewProductos.Visible = !show;
        }

        private async Task LoadProductosAsync()
        {
            try
            {
                ShowLoading(true);
                ShowError(false, string.Empty);

                var result = await _productoService.GetProductosAsync();

                if (result.Success)
                {
                    if (result.Data != null && result.Data.Count > 0)
                    {
                        GridViewProductos.DataSource = result.Data;
                        GridViewProductos.DataBind();
                        GridViewProductos.Visible = true;
                        NoDataPanelProductos.Visible = false;
                    }
                    else
                    {
                        GridViewProductos.Visible = false;
                        NoDataPanelProductos.Visible = true;
                    }
                }
                else
                {
                    ShowError(true, result.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error inesperado. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                ShowLoading(false);
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

        protected async void ButtonGuardarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                ShowLoading(true);
                ShowError(false, string.Empty);

                var producto = new ProductoDTO
                {
                    codigo = TextBoxCodigo.Text,
                    descripcion = TextBoxDescripcion.Text,
                    precioUnitario = Convert.ToDecimal(TextBoxPrecioUnitario.Text),
                    stock = Convert.ToInt32(TextBoxStock.Text),
                    proveedorId = Convert.ToInt32(DropDownListProveedor.SelectedValue),
                    fechaAlta = DateTime.Now
                };

                var result = await _productoService.CreateProductoAsync(producto);

                if (result.Success)
                {
                    // Limpiar los campos después de guardar
                    TextBoxCodigo.Text = string.Empty;
                    TextBoxDescripcion.Text = string.Empty;
                    TextBoxPrecioUnitario.Text = string.Empty;
                    TextBoxStock.Text = string.Empty;
                    DropDownListProveedor.SelectedIndex = 0;

                    // Recargar la lista de productos
                    await LoadProductosAsync();
                }
                else
                {
                    ShowError(true, result.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al agregar el producto. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                ShowLoading(false);
            }
        }

        protected void ButtonReintentarProductos_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(LoadProductosAsync));
        }

        protected void GridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarProducto")
            {
                int productoId = Convert.ToInt32(e.CommandArgument);
                // Redirigir a la página de edición de producto, pasando el ID como parámetro
                Response.Redirect($"EditarProducto.aspx?id={productoId}");
            }
            else if (e.CommandName == "EliminarProducto")
            {
                int productoId = Convert.ToInt32(e.CommandArgument);
                // Llamar al método para eliminar el producto
                EliminarProductoAsync(productoId);
            }
        }

        private async void EliminarProductoAsync(int productoId)
        {
            try
            {
                ShowLoading(true);
                ShowError(false, string.Empty);

                var result = await _productoService.DeleteProductoAsync(productoId);

                if (result.Success)
                {
                    // Recargar la grilla después de eliminar el producto
                    await LoadProductosAsync();
                }
                else
                {
                    ShowError(true, result.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al eliminar el producto. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                ShowLoading(false);
            }
        }

    }
}
