using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPInteg_UI.Services;

namespace TPInteg_UI.Pages
{
    public partial class ListadoLocalidades : Page
    {
        protected UpdateProgress UpdateProgressLocalidades;
        protected UpdatePanel UpdatePanelLocalidades;
        protected Panel PanelErrorLocalidades;
        protected Panel NoDataPanelLocalidades;
        protected GridView GridViewLocalidades;
        protected Label LabelErrorLocalidades;
        protected Button ButtonReintentarLocalidades;

        private readonly LocalidadService _localidadService;

        public ListadoLocalidades()
        {
            _localidadService = new LocalidadService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(LoadLocalidadesAsync));
            }
        }

        private void ShowLoading(bool show)
        {
            if (UpdateProgressLocalidades != null)
                UpdateProgressLocalidades.Visible = show;
        }

        private void ShowError(bool show, string message = "")
        {
            if (PanelErrorLocalidades != null)
            {
                PanelErrorLocalidades.Visible = show;
                if (LabelErrorLocalidades != null)
                    LabelErrorLocalidades.Text = message;
            }
            if (GridViewLocalidades != null)
                GridViewLocalidades.Visible = !show;
        }

        private async Task LoadLocalidadesAsync()
        {
            try
            {
                ShowLoading(true);
                ShowError(false, string.Empty);

                var result = await _localidadService.GetLocalidadesAsync();

                if (result.Success)
                {
                    if (result.Data != null && result.Data.Count > 0)
                    {
                        if (GridViewLocalidades != null)
                        {
                            GridViewLocalidades.DataSource = result.Data;
                            GridViewLocalidades.DataBind();
                            GridViewLocalidades.Visible = true;
                        }
                        if (NoDataPanelLocalidades != null)
                            NoDataPanelLocalidades.Visible = false;
                    }
                    else
                    {
                        if (GridViewLocalidades != null)
                            GridViewLocalidades.Visible = false;
                        if (NoDataPanelLocalidades != null)
                            NoDataPanelLocalidades.Visible = true;
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

        protected void GridViewLocalidades_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditarLocalidad")
            {
                int localidadId = Convert.ToInt32(e.CommandArgument);
                // Redirigir a la página de edición de localidad, pasando el ID como parámetro
                Response.Redirect($"EditarLocalidad.aspx?id={localidadId}");
            }
            else if (e.CommandName == "EliminarLocalidad")
            {
                int localidadId = Convert.ToInt32(e.CommandArgument);
                // Llamar al método para eliminar la localidad
                EliminarLocalidadAsync(localidadId);
            }
        }

        private async void EliminarLocalidadAsync(int localidadId)
        {
            try
            {
                ShowLoading(true);
                ShowError(false, string.Empty);

                var result = await _localidadService.DeleteLocalidadAsync(localidadId);

                if (result.Success)
                {
                    // Recargar la grilla después de eliminar la localidad
                    await LoadLocalidadesAsync();
                }
                else
                {
                    ShowError(true, result.Message);
                }
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error al eliminar la localidad. Por favor, intente nuevamente más tarde.");
                System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                ShowLoading(false);
            }
        }

        protected void ButtonReintentarLocalidades_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(LoadLocalidadesAsync));
        }
    }
}