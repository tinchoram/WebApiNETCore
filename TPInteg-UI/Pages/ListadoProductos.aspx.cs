using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
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

        private readonly ProductoService _productoService;

        public ListadoProductos()
        {
            _productoService = new ProductoService();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RegisterAsyncTask(new PageAsyncTask(LoadProductosAsync));
            }
        }

        private void ShowLoading(bool show)
        {
            if (UpdateProgressProductos != null)
                UpdateProgressProductos.Visible = show;
        }

        private void ShowError(bool show, string message = "")
        {
            if (PanelErrorProductos != null)
            {
                PanelErrorProductos.Visible = show;
                if (LabelErrorProductos != null)
                    LabelErrorProductos.Text = message;
            }
            if (GridViewProductos != null)
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
                        if (GridViewProductos != null)
                        {
                            GridViewProductos.DataSource = result.Data;
                            GridViewProductos.DataBind();
                            GridViewProductos.Visible = true;
                        }
                        if (NoDataPanelProductos != null)
                            NoDataPanelProductos.Visible = false;
                    }
                    else
                    {
                        if (GridViewProductos != null)
                            GridViewProductos.Visible = false;
                        if (NoDataPanelProductos != null)
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

        protected void ButtonReintentarProductos_Click(object sender, EventArgs e)
        {
            RegisterAsyncTask(new PageAsyncTask(LoadProductosAsync));
        }
    }
}