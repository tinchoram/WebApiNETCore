using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPInteg_UI.DTO;
using TPInteg_UI.Services;

namespace TPInteg_UI.Pages
{
    public partial class EditarLocalidad : Page
    {
        protected TextBox TextBoxNombre;
        protected TextBox TextBoxCodigoPostal;
        protected Button ButtonActualizar;
        protected Button ButtonCancelar;

        private readonly LocalidadService _localidadService;
        private int _localidadId;

        public EditarLocalidad()
        {
            _localidadService = new LocalidadService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (int.TryParse(Request.QueryString["id"], out int localidadId))
                {
                    _localidadId = localidadId;
                    HiddenFieldLocalidadId.Value = localidadId.ToString();
                    RegisterAsyncTask(new PageAsyncTask(LoadLocalidadAsync));
                }
                else
                {
                    // Si no se proporciona un ID válido, redirigir a la página de listado
                    Response.Redirect("ListadoLocalidades.aspx");
                }
            }
        }

        private void ShowError(bool show, string message = "")
        {
            if (PanelErrorEditarLocalidad != null)
            {
                PanelErrorEditarLocalidad.Visible = show;
                if (LabelErrorEditarLocalidad != null)
                    LabelErrorEditarLocalidad.Text = message;
            }
        }

        private async Task LoadLocalidadAsync()
        {
            try
            {
                var result = await _localidadService.GetLocalidadByIdAsync(_localidadId);

                if (result.Success && result.Data != null)
                {
                    var localidad = result.Data;
                    TextBoxNombre.Text = localidad.nombre;
                    TextBoxCodigoPostal.Text = localidad.codigoPostal.ToString();
                }
                else
                {
                    ShowError(true, "No se pudo cargar la localidad. Por favor, intente nuevamente más tarde.");
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al cargar la localidad. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        protected async void ButtonActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                ShowError(false, string.Empty);

                string nombre = TextBoxNombre.Text;
                int codigoPostal = Convert.ToInt32(TextBoxCodigoPostal.Text);
                int localidadId = Convert.ToInt32(HiddenFieldLocalidadId.Value);

                var localidad = new LocalidadDTO
                {
                    id = localidadId,
                    nombre = nombre,
                    codigoPostal = codigoPostal
                };

                var result = await _localidadService.UpdateLocalidadAsync(localidadId, localidad);

                if (result.Success)
                {
                    // Redirigir a la página de listado después de actualizar
                    Response.Redirect("ListadoLocalidades.aspx");
                }
                else
                {
                    ShowError(true, result.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al actualizar la localidad. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            // Redirigir a la página de listado al cancelar
            Response.Redirect("ListadoLocalidades.aspx");
        }
    }
}