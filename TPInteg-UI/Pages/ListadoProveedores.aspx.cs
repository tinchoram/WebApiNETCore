using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using TPInteg_UI.DTO;

namespace TPInteg_UI
{
    public partial class ListadoProveedores : Page
    {
        private static readonly HttpClient httpClient;
        private const string API_BASE_URL = "https://localhost:7176/api/";

        // Control declarations
        protected UpdateProgress UpdateProgress1;
        protected Panel PanelError;
        protected Panel NoDataPanel;
        protected GridView GridViewProveedores;
        protected Label LabelError;
        protected Button ButtonReintentar;
        protected UpdatePanel UpdatePanel1;

        static ListadoProveedores()
        {
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(API_BASE_URL),
                Timeout = TimeSpan.FromSeconds(30)
            };
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(LoadProveedoresAsync));
            }
        }

        private async Task LoadProveedoresAsync()
        {
            try
            {
                ShowLoading(true);
                ShowError(false, string.Empty);
                ShowNoData(false);

                var response = await httpClient.GetAsync("Proveedor");
                var jsonString = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var proveedores = JsonConvert.DeserializeObject<List<ProveedorDTO>>(jsonString);

                        if (proveedores != null && proveedores.Count > 0)
                        {
                            GridViewProveedores.DataSource = proveedores;
                            GridViewProveedores.DataBind();
                            GridViewProveedores.Visible = true;
                            ShowNoData(false);
                        }
                        else
                        {
                            GridViewProveedores.Visible = false;
                            ShowNoData(true);
                        }
                    }
                    catch (JsonSerializationException ex)
                    {
                        ShowError(true, $"Error al procesar los datos: {ex.Message}");
                        LogError("Error de deserialización", ex);
                    }
                }
                else
                {
                    ShowError(true, $"Error del servidor: {response.StatusCode}");
                    LogError("Error de API", null, $"Status: {response.StatusCode}, Content: {jsonString}");
                }
            }
            catch (HttpRequestException ex)
            {
                ShowError(true, "Error de conexión con el servidor. Por favor, verifique su conexión e intente nuevamente.");
                LogError("Error de conexión HTTP", ex);
            }
            catch (Exception ex)
            {
                ShowError(true, "Ha ocurrido un error inesperado. Por favor, intente nuevamente más tarde.");
                LogError("Error general", ex);
            }
            finally
            {
                ShowLoading(false);
            }
        }

        private void ShowLoading(bool show)
        {
            if (UpdateProgress1 != null)
                UpdateProgress1.Visible = show;
        }

        private void ShowError(bool show, string message = "")
        {
            if (PanelError != null)
            {
                PanelError.Visible = show;
                if (LabelError != null)
                    LabelError.Text = message;
            }
            if (GridViewProveedores != null)
                GridViewProveedores.Visible = !show;
        }

        private void ShowNoData(bool show)
        {
            if (NoDataPanel != null)
                NoDataPanel.Visible = show;
        }

        private void LogError(string tipo, Exception ex = null, string detalles = null)
        {
            System.Diagnostics.Debug.WriteLine($"Error: {tipo}");
            if (ex != null)
                System.Diagnostics.Debug.WriteLine($"Exception: {ex.Message}\nStack: {ex.StackTrace}");
            if (detalles != null)
                System.Diagnostics.Debug.WriteLine($"Detalles: {detalles}");
        }

        protected void ButtonReintentar_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(LoadProveedoresAsync));
        }
    }

    
}