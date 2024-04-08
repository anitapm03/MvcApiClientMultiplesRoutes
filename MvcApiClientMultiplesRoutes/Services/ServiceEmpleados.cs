using NugetApiModels.Models;
using System.Net.Http.Headers;

namespace MvcApiClientMultiplesRoutes.Services
{
    public class ServiceEmpleados
    {
        private MediaTypeWithQualityHeaderValue header;

        private string ApiUrlEmpleados;

        public ServiceEmpleados(IConfiguration configuration)
        {
            this.ApiUrlEmpleados =
                configuration.GetValue<string>("ApiUrls:ApiUrlEmpleados");
            this.header =
                new MediaTypeWithQualityHeaderValue("application/json");
        }

        //nos creamos un metodo generico para la peticion
        //a todos los metodos get de la api

        private async Task<T> CallApisAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.ApiUrlEmpleados);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.header);

                HttpResponseMessage response =
                    await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data =
                        await response.Content
                        .ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Empleado>> GetEmpleadosAsync()
        {
            string request = "api/empleados";
            List<Empleado> empleados = await
                this.CallApisAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<List<string>> GetOficiosAsync()
        {
            string request = "api/empleados/getoficios";

            List<string> oficios = await
                this.CallApisAsync<List<string>>(request);

            return oficios;
        }

        public async Task<List<Empleado>>
            GetEmpleadosOficioAsync(string oficio)
        {
            string request = "api/empleados/empleadosoficio/" + oficio;
            List<Empleado> empleados = await
                this.CallApisAsync<List<Empleado>>(request);
            return empleados;
        }

        public async Task<Empleado>
            FindEmpleadoAsync(int idempleado)
        {
            string request = "api/empleados/" + idempleado;
            Empleado empleado = await
                this.CallApisAsync<Empleado>(request);
            return empleado;
        }

        /*public async Task<List<Empleado>>
            GetEmpleadosSalarioAsync(int salario, int dept)
        {

        }*/
    }
}
