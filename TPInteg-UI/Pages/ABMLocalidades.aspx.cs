using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPInteg_UI.DTO;
using TPInteg_UI.Services;

namespace TPInteg_UI.Pages
{
    public partial class ABMLocalidades : Page
    {
        private readonly LocalidadService _localidadService;

        // Definimos los controles como protected para que se generen automáticamente
        protected Label TituloPagina;
        protected Panel PanelError;
        protected Panel PanelSuccess;
        protected Label LabelError;
        protected Label LabelSuccess;
        protected TextBox TextBoxNombre;
        protected TextBox TextBoxCodigoPostal;
        protected DropDownList DropDownListEstado;
        protected Button ButtonGuardar;
        protected Button ButtonCancelar;
        protected HiddenField HiddenLocalidadId;
        protected UpdateProgress UpdateProgressLocalidad;
        protected UpdatePanel UpdatePanelLocalidad;

        public ABMLocalidades()
        {
            _localidadService = new LocalidadService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ConfigurarPagina();
            }
        }

        private void ConfigurarPagina()
        {
            try
            {
                string idStr = Request.QueryString["id"];

                if (TituloPagina != null)
                {
                    TituloPagina.Text = string.IsNullOrEmpty(idStr) ? "Nueva Localidad" : "Editar Localidad";
                }

                if (!string.IsNullOrEmpty(idStr) && int.TryParse(idStr, out int id))
                {
                    RegisterAsyncTask(new PageAsyncTask(async () => await LoadLocalidadAsync(id)));
                }
            }
            catch (Exception ex)
            {
                ShowError("Error al configurar la página: " + ex.Message);
            }
        }

        private async Task LoadLocalidadAsync(int id)
        {
            try
            {
                var result = await _localidadService.GetLocalidadByIdAsync(id);
                if (result.Success && result.Data != null)
                {
                    if (HiddenLocalidadId != null) HiddenLocalidadId.Value = result.Data.id.ToString();
                    if (TextBoxNombre != null) TextBoxNombre.Text = result.Data.nombre;
                    if (TextBoxCodigoPostal != null) TextBoxCodigoPostal.Text = result.Data.codigoPostal.ToString();
                    if (DropDownListEstado != null) DropDownListEstado.SelectedValue = result.Data.fechaBaja == null ? "1" : "0";
                }
                else
                {
                    ShowError("No se encontró la localidad solicitada.");
                    DisableForm();
                }
            }
            catch (Exception ex)
            {
                ShowError("Error al cargar la localidad: " + ex.Message);
                DisableForm();
            }
        }

        protected async void ButtonGuardar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            try
            {
                var localidad = new LocalidadDTO
                {
                    nombre = TextBoxNombre?.Text.Trim(),
                    codigoPostal = int.Parse(TextBoxCodigoPostal?.Text ?? "0"),
                    fechaAlta = DateTime.Now
                };

                if (!string.IsNullOrEmpty(HiddenLocalidadId?.Value))
                {
                    localidad.id = int.Parse(HiddenLocalidadId.Value);
                    localidad.fechaBaja = DropDownListEstado?.SelectedValue == "0" ? DateTime.Now : (DateTime?)null;

                    var result = await _localidadService.UpdateLocalidadAsync(localidad.id, localidad);
                    if (result.Success)
                    {
                        ShowSuccess("Localidad actualizada exitosamente.");
                        RedirectToListado();
                    }
                    else
                    {
                        ShowError(result.Message ?? "Error al actualizar la localidad.");
                    }
                }
                else
                {
                    var result = await _localidadService.CreateLocalidadAsync(localidad);
                    if (result.Success)
                    {
                        ShowSuccess("Localidad creada exitosamente.");
                        RedirectToListado();
                    }
                    else
                    {
                        ShowError(result.Message ?? "Error al crear la localidad.");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowError("Error al guardar: " + ex.Message);
            }
        }

        protected void ButtonCancelar_Click(object sender, EventArgs e)
        {
            RedirectToListado();
        }

        private void ShowError(string message)
        {
            if (PanelError != null && LabelError != null)
            {
                PanelError.Visible = true;
                if (PanelSuccess != null) PanelSuccess.Visible = false;
                LabelError.Text = message;
            }
        }

        private void ShowSuccess(string message)
        {
            if (PanelSuccess != null && LabelSuccess != null)
            {
                if (PanelError != null) PanelError.Visible = false;
                PanelSuccess.Visible = true;
                LabelSuccess.Text = message;
            }
        }

        private void DisableForm()
        {
            if (TextBoxNombre != null) TextBoxNombre.Enabled = false;
            if (TextBoxCodigoPostal != null) TextBoxCodigoPostal.Enabled = false;
            if (DropDownListEstado != null) DropDownListEstado.Enabled = false;
            if (ButtonGuardar != null) ButtonGuardar.Visible = false;
        }

        private void RedirectToListado()
        {
            Response.Redirect("~/Pages/ListadoLocalidades.aspx");
        }
    }
}