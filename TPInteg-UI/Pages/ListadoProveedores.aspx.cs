using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPInteg_UI.DTO;
using TPInteg_UI.Services;
using TPInteg_UI.Utilities;

namespace TPInteg_UI.Pages
{
    public partial class ListadoProveedores : Page
    {
        protected UpdateProgress UpdateProgressProveedores;
        protected UpdatePanel UpdatePanelProveedores;
        protected Panel PanelErrorProveedores;
        protected Panel NoDataPanelProveedores;
        protected GridView GridViewProveedores;
        protected Label LabelErrorProveedores;
        protected Button ButtonReintentarProveedores;
        protected TextBox TextBoxNombre;
        protected TextBox TextBoxDireccion;
        protected TextBox TextBoxEmail;
        protected TextBox TextBoxTelefono;
        protected DropDownList DropDownListLocalidad;
        protected Button ButtonGuardar;

        private readonly ProveedorService _proveedorService;
        private readonly LocalidadService _localidadService;

        public ListadoProveedores()
        {
            _proveedorService = new ProveedorService();
            _localidadService = new LocalidadService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(LoadProveedoresAsync));
                RegisterAsyncTask(new PageAsyncTask(LoadLocalidadesAsync));
            }
            else 
            { 

            }
        }

        private void ShowLoading(bool show)
        {
            if (UpdateProgressProveedores != null)
                UpdateProgressProveedores.Visible = show;
        }

        private void ShowError(bool show, string message = "")
        {
            if (PanelErrorProveedores != null)
            {
                PanelErrorProveedores.Visible = show;
                if (LabelErrorProveedores != null)
                    LabelErrorProveedores.Text = message;
            }
            if (GridViewProveedores != null)
                GridViewProveedores.Visible = !show;
        }

        private async Task LoadProveedoresAsync()
        {
            try
            {
                ShowLoading(true);
                ShowError(false, string.Empty);

                var result = await _proveedorService.GetProveedoresAsync();

                if (result.Success)
                {
                    if (result.Data != null && result.Data.Count > 0)
                    {
                        if (GridViewProveedores != null)
                        {
                            GridViewProveedores.DataSource = result.Data;
                            GridViewProveedores.DataBind();
                            GridViewProveedores.Visible = true;
                        }
                        if (NoDataPanelProveedores != null)
                            NoDataPanelProveedores.Visible = false;
                    }
                    else
                    {
                        if (GridViewProveedores != null)
                            GridViewProveedores.Visible = false;
                        if (NoDataPanelProveedores != null)
                            NoDataPanelProveedores.Visible = true;
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

        protected void GridViewProveedores_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarProveedor")
            {
                int proveedorId = Convert.ToInt32(e.CommandArgument);
                // Redirigir a la página de edición de proveedor, pasando el ID como parámetro
                Response.Redirect($"EditarProveedor.aspx?id={proveedorId}");
            }
            else if (e.CommandName == "EliminarProveedor")
            {
                int proveedorId = Convert.ToInt32(e.CommandArgument);
                // Llamar al método para eliminar el proveedor
                EliminarProveedorAsync(proveedorId);
            }
        }

        private async void EliminarProveedorAsync(int proveedorId)
        {
            try
            {
                ShowLoading(true);
                ShowError(false, string.Empty);

                var result = await _proveedorService.DeleteProveedorAsync(proveedorId);

                if (result.Success)
                {
                    // Recargar la grilla después de eliminar el proveedor
                    await LoadProveedoresAsync();
                }
                else
                {
                    ShowError(true, result.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al eliminar el proveedor. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                ShowLoading(false);
            }
        }

        protected void ButtonReintentarProveedores_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(LoadProveedoresAsync));
        }

        protected async void ButtonGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (Page.IsValid)
                {
                    ShowLoading(true);
                    ShowError(false, string.Empty);

                    string nombre = TextBoxNombre.Text;
                    string apellido = TextBoxApellido.Text;
                    string nombreComercial = TextBoxNombreComercial.Text;
                    string direccion = TextBoxDireccion.Text;
                    string email = TextBoxEmail.Text;
                    string telefono = TextBoxTelefono.Text;
                    int localidadId = Convert.ToInt32(DropDownListLocalidad.SelectedValue);
                    string cuit = ucNumberFormatter.CuitNumber;
                    string websiteUrl = TextBoxSitioWebUrl.Text;
                    bool active = CheckEsActivo.Checked;
                    DateTime fechaNacimiento = DateTimeParsing.DateTimeFromString(TextBoxDate.Text);

                    var result = await _proveedorService.CreateProveedorAsync(new ProveedorDTO
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
                        FechaAlta = DateTime.Now
                    });

                    if (result.Success)
                    {
                        // Limpiar los campos después de guardar
                        TextBoxNombre.Text = string.Empty;
                        TextBoxApellido.Text = string.Empty;
                        TextBoxNombreComercial.Text = string.Empty;
                        TextBoxDireccion.Text = string.Empty;
                        TextBoxEmail.Text = string.Empty;
                        TextBoxTelefono.Text = string.Empty;
                        ucNumberFormatter.CuitNumber = string.Empty;
                        TextBoxSitioWebUrl.Text = string.Empty;
                        DropDownListLocalidad.SelectedIndex = 0;
                        TextBoxDate.Text = string.Empty;
                        // Recargar la grilla después de agregar el proveedor
                        await LoadProveedoresAsync();
                    }
                    else
                    {
                        ShowError(true, result.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al agregar el proveedor. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                ShowLoading(false);
            }
        }
    }
}