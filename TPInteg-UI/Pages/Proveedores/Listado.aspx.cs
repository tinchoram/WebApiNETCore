using System;
using System.Threading.Tasks;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Http;
using TPInteg_UI.Services;
using System.Web.Services.Description;
using Newtonsoft.Json;
using System.Collections.Generic;
using TPInteg_UI.DTO;

namespace TPInteg_UI.Pages.Proveedores
{
    public partial class Listado : Page
    {
        List<LocalidadDTO> localidadesList;// = new List<LocalidadDTO>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                localidadesList = new List<LocalidadDTO>();
                BindGridView();
                //RegisterAsyncTask(new PageAsyncTask(CallEndpoint));
            }
        }
        //static readonly HttpClient client = Client.GetClient();
        //private async Task CallEndpoint()
        //{
        //    try
        //    {
        //        var response = await client.GetAsync("https://localhost:7176/api/Localidad/TraerTodos");
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var readResponse = await response.Content.ReadAsStringAsync();
        //            localidadesList = JsonConvert.DeserializeObject<List<LocalidadDTO>>(readResponse);
        //            BindGridView();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private void BindGridView()
        {
            localidadesList = LocalidadService.GetAllLocalidades("https://localhost:7176/api/Localidad/TraerTodos");
            GridView1.DataSource = localidadesList;
            GridView1.DataBind();
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridView1.EditIndex = e.NewEditIndex;
            BindGridView();
        }

        protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["Id"]);
            string nombre = ((TextBox)GridView1.Rows[e.RowIndex].Cells[1].Controls[0]).Text;
                        
            LocalidadDTO loc = LocalidadService.GetLocalidadById("https://localhost:7176/api/Localidad/TraerPorId", id);
            loc.Nombre = nombre;
            var response = LocalidadService.UpdateLocalidad("https://localhost:7176/api/Localidad", loc);

            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridView1.EditIndex = -1;
            BindGridView();
        }

        protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Values["Id"]);
            var response = LocalidadService.DeleteLocalidad("https://localhost:7176/api/Localidad", id);
            BindGridView();
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            var newLocalidad = new LocalidadDTO
            {
                Id = localidadesList.Count + 1,
                Nombre = txtNombre.Text
            };
            localidadesList.Add(newLocalidad);
            BindGridView();
        }
    }
}