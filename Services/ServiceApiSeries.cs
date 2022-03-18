using MvcApiExamenLuisEnrique.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MvcApiExamenLuisEnrique.Services
{
    public class ServiceApiSeries
    {
        private Uri UriApi;
        private MediaTypeWithQualityHeaderValue Header;

        public ServiceApiSeries(string url)
        {
            this.UriApi = new Uri(url);
            this.Header = new MediaTypeWithQualityHeaderValue("application/json");
        }

        private async Task<T> CallApiAsync<T>(string request)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response = await client.GetAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    T data = await response.Content.ReadAsAsync<T>();
                    return data;
                }
                else
                {
                    return default(T);
                }
            }
        }

        public async Task<List<Serie>> GetSeriesAsync()
        {
            string request = "/api/series";
            List<Serie> series = await this.CallApiAsync<List<Serie>>(request);
            return series;
        }

        public async Task<List<Personaje>> GetPersonajesAsync()
        {
            string request = "/api/personajes";
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Serie> FindSerieAsync(int idserie)
        {
            string request = "/api/series/" + idserie;
            Serie serie = await this.CallApiAsync<Serie>(request);
            return serie;
        }

        public async Task InsertSerieAsync(int idserie, string nombre, string imagen, double puntuacion, int anio)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/series";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie = new Serie();
                serie.IdSerie = 0;
                serie.NombreSerie = nombre;
                serie.Imagen = imagen;
                serie.Puntuacion = puntuacion;
                serie.Anio = anio;
                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response =
                await client.PostAsync(request, content);
            }
        }

        public async Task UpdateSerieAsync(int idserie, string nombre, string imagen, double puntuacion, int anio)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/series";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Serie serie = new Serie
                { 
                    IdSerie = idserie, 
                    NombreSerie = nombre, 
                    Imagen = imagen,
                    Puntuacion = puntuacion,
                    Anio = anio
                };
                string json = JsonConvert.SerializeObject(serie);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }

        public async Task DeleteSerieAsync(int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/series/" + idserie;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                await client.DeleteAsync(request);
            }
        }


        public async Task<List<Personaje>> GetPersonajesSeriesAsync(int idserie)
        {
            string request = "/api/series/personajesserie/" + idserie;
            List<Personaje> personajes = await this.CallApiAsync<List<Personaje>>(request);
            return personajes;
        }

        public async Task<Personaje> FindPersonajeAsync(int id)
        {
            string request = "/api/personajes/" + id;
            Personaje personaje = await this.CallApiAsync<Personaje>(request);
            return personaje;
        }

        public async Task InsertPersonajeAsync(string nombre, string imagen, int idserie)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes";
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = new Personaje();
                personaje.IdPersonaje = 0;
                personaje.NombrePersonaje = nombre;
                personaje.Imagen = imagen;
                personaje.IdSerie = idserie;
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(request, content);
            }
        }

        public async Task DeletePersonajeAsync(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes/" + id;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                HttpResponseMessage response =
                await client.DeleteAsync(request);
            }
        }

        public async Task UpdatePersonajeSerieAsync(int idserie, int idpersonaje)
        {
            using (HttpClient client = new HttpClient())
            {
                string request = "/api/personajes/" + idpersonaje + "/" + idserie;
                client.BaseAddress = this.UriApi;
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(this.Header);
                Personaje personaje = new Personaje();
                string json = JsonConvert.SerializeObject(personaje);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync(request, content);
            }
        }
    }
}
