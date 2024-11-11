using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPInteg_UI.DTO;
using TPInteg_UI.Services;
using TPInteg_UI.Utilities;

namespace TPInteg_UI.Pages
{
    public partial class EditarProveedor : Page
    {
        protected DropDownList DropDownListLocalidad;
        protected TextBox TextBoxNombre;
        protected TextBox TextBoxEmail;
        protected TextBox TextBoxTelefono;
        protected TextBox TextBoxDireccion;
        protected Button ButtonActualizar;
        protected Button ButtonCancelar;

        private readonly ProveedorService _proveedorService;
        private readonly LocalidadService _localidadService;
        private int _proveedorId;

        public EditarProveedor()
        {
            _proveedorService = new ProveedorService();
            _localidadService = new LocalidadService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["id"], out int proveedorId))
                {
                    _proveedorId = proveedorId;
                    HiddenFieldProveedorId.Value = proveedorId.ToString();
                    RegisterAsyncTask(new PageAsyncTask(LoadLocalidadesAsync));
                    RegisterAsyncTask(new PageAsyncTask(LoadProveedorAsync));
                }
                else
                {
                    // Si no se proporciona un ID válido, redirigir a la página de listado
                    Response.Redirect("ListadoProveedores.aspx");
                }
            }
        }

        private async Task LoadLocalidadesAsync()
        {
            try
            {
                var result = await _localidadService.GetLocalidadesAsync();

                if (result.Success && result.Data != null)
                {
                    DropDownListLocalidad.DataSource = result.Data;
                    DropDownListLocalidad.DataTextField = "Nombre";
                    DropDownListLocalidad.DataValueField = "Id";
                    DropDownListLocalidad.DataBind();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error al cargar las localidades: {ex.Message}");
            }
        }

        private async Task LoadProveedorAsync()
        {
            try
            {
                var result = await _proveedorService.GetProveedorByIdAsync(_proveedorId);

                if (result.Success && result.Data != null)
                {
                    var proveedor = result.Data;
                    TextBoxNombre.Text = proveedor.Nombre;
                    TextBoxApellido.Text = proveedor.Apellido;
                    TextBoxDireccion.Text = proveedor.Direccion;
                    TextBoxNombreComercial.Text = proveedor.NombreComercial;
                    TextBoxEmail.Text = proveedor.Email;
                    TextBoxTelefono.Text = proveedor.Telefono;
                    TextBoxSitioWebUrl.Text = proveedor.SitioWebUrl;
                    TextBoxDate.Text = proveedor.FechaNacimiento.Value.ToShortDateString();
                    ucNumberFormatter.CuitNumber = proveedor.Cuit;
                    TextBoxCantSucursales.Text = proveedor.CantSucursales.ToString();
                    // Seleccionar la localidad actual en el DropDownList
                    DropDownListLocalidad.SelectedValue = proveedor.LocalidadId.ToString();
                }
                else
                {
                    ShowError(true, "No se pudo cargar el proveedor. Por favor, intente nuevamente más tarde.");
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al cargar el proveedor. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ShowError(bool show, string message = "")
        {
            if (PanelErrorEditarProveedor != null)
            {
                PanelErrorEditarProveedor.Visible = show;
                if (LabelErrorEditarProveedor != null)
                    LabelErrorEditarProveedor.Text = message;
            }
        }

        protected async void ButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ShowError(false, string.Empty);

                    string nombre = TextBoxNombre.Text;
                    string apellido = TextBoxApellido.Text;
                    string nombreComercial = TextBoxNombreComercial.Text;
                    string direccion = TextBoxDireccion.Text;
                    string email = TextBoxEmail.Text;
                    string telefono = TextBoxTelefono.Text;
                    int localidadId = Convert.ToInt32(DropDownListLocalidad.SelectedValue);
                    int proveedorId = Convert.ToInt32(HiddenFieldProveedorId.Value);
                    string cuit = ucNumberFormatter.CuitNumber;
                    string websiteUrl = TextBoxSitioWebUrl.Text;
                    bool active = CheckEsActivo.Checked;
                    int cantSucursales = 0;
                    int.TryParse(TextBoxCantSucursales.Text, out cantSucursales);
                    DateTime fechaNacimiento = DateTimeParsing.DateTimeFromString(TextBoxDate.Text);

                    var proveedor = new ProveedorDTO
                    {
                        Nombre = nombre,
                        Apellido = apellido,
                        NombreComercial = nombreComercial,
                        Direccion = direccion,
                        Email = email,
                        Telefono = telefono,
                        LocalidadId = localidadId,
                        Cuit = cuit,
                        SitioWebUrl = websiteUrl,
                        Activo = active,
                        FechaNacimiento = fechaNacimiento,
                        CantSucursales = cantSucursales,
                        FechaAlta = DateTime.Now
                    };

                    var result = await _proveedorService.UpdateProveedorAsync(proveedorId, proveedor);

                    if (result.Success)
                    {
                        // Redirigir a la página de listado después de actualizar
                        Response.Redirect("ListadoProveedores.aspx");
                    }
                    else
                    {
                        ShowError(true, result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al actualizar el proveedor. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de listado al cancelar
            Response.Redirect("ListadoProveedores.aspx");
        }
    }
}
